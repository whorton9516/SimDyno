import React, { useState, useEffect } from 'react';
import { LineChart, ChartsReferenceLine } from '@mui/x-charts';
import { Box, Typography } from '@mui/material';
import { useSignalR } from "../../../../services/SignalRContext";

type PowerMapState = {
  map: Map<number, number>;
  gear: number | null;
};

const Powerband: React.FC = () => {
  const { telemetry } = useSignalR();
  // Only store power values in a Map keyed by RPM
  const [powerMapState, setPowerMapState] = useState<PowerMapState>({
    map: new Map(),
    gear: null,
  });

  useEffect(() => {
    if (!telemetry) return;

    setPowerMapState((prevState) => {
      // If gear changed, reset map
      if (prevState.gear !== telemetry.gear) {
        return {
          map: new Map(),
          gear: telemetry.gear,
        };
      }
      // Round RPM to nearest 100 for discretization
      const rpmKey = Math.round(telemetry.currentEngineRpm / 100) * 100;
      const currentPower = telemetry.power;
      const existing = prevState.map.get(rpmKey);
      if (existing === undefined || currentPower > existing) {
        const newMap = new Map(prevState.map);
        newMap.set(rpmKey, currentPower);
        return {
          map: newMap,
          gear: telemetry.gear,
        };
      }
      return prevState;
    });
  }, [telemetry]);

  // Default X-axis bounds when no data is available
  const defaultMinRPM = 750;
  const defaultMaxRPM = 7000;

  // Use the actual powerMap bounds if available, otherwise default values
  const minRPM = powerMapState.map.size > 0 ? Math.min(...Array.from(powerMapState.map.keys())) : defaultMinRPM;
  const maxRPM = powerMapState.map.size > 0 ? Math.max(...Array.from(powerMapState.map.keys())) : defaultMaxRPM;

  const rpmKeys = Array.from(powerMapState.map.keys()).sort((a, b) => a - b);
  const powerValues = rpmKeys.map((rpm) => powerMapState.map.get(rpm)!);

  // If no data, fallback to a flat line from minRPM to maxRPM
  const xAxisData = rpmKeys.length > 0 ? rpmKeys : [minRPM, maxRPM];
  const defaultSeriesData = [0, 0];
  const seriesPowerData = rpmKeys.length > 0 ? powerValues : defaultSeriesData;

  // Y-axis for power: start at 0; default max is 1000 unless telemetry.power > 1000
  const computedPowerMax = powerValues.length > 0 ? Math.max(...powerValues) : 0;
  const yAxisPowerMax =
    computedPowerMax > 1000
      ? computedPowerMax
      : telemetry && telemetry.power > 1000
        ? telemetry.power + 100
        : 1000;
  const yAxisPowerMin = 0;

  // Use telemetry.currentEngineRpm if available and not below minRPM,
  // otherwise, force it to the left edge (minRPM) so the dot starts at the bottom left.
  const referenceX = telemetry && telemetry.currentEngineRpm >= minRPM
    ? telemetry.currentEngineRpm
    : minRPM;

  return (
    <Box sx={{ width: '100%', height: 400 }}>
      <Typography variant="h6" sx={{ marginBottom: 1 }}>
        Gear: {telemetry ? telemetry.gear : '-'}
      </Typography>

      <LineChart
        xAxis={[{
          data: xAxisData,
          label: 'RPM',
          min: minRPM,
          max: maxRPM,
          scaleType: 'linear'
        }]}
        yAxis={[{
          id: "0",
          label: 'Power (hp)',
          min: yAxisPowerMin,
          max: yAxisPowerMax,
          scaleType: 'linear'
        }]}
        series={[
          {
            data: seriesPowerData,
            label: 'Power',
            yAxisId: "0",
            color: '#8884d8',
          },
        ]}
        width={600}
        height={350}
        sx={{
          '& .MuiLineElement-root': {
            strokeWidth: 2,
          },
          '& .MuiChartsAxis-label': {
            fill: '#ffffff',
          },
          '& .MuiChartsAxis-tick': {
            stroke: '#cccccc',
          },
          '& .MuiChartsAxis-line': {
            stroke: '#cccccc',
          },
          backgroundColor: '#1a1a1a',
          padding: 2,
          borderRadius: 1,
          boxShadow: '0 4px 6px rgba(0,0,0,0.3)',
        }}
      >
        <ChartsReferenceLine 
          x={referenceX} 
          lineStyle={{ stroke: 'red', strokeWidth: 1 }} 
        />
      </LineChart>
    </Box>
  );
};

export default Powerband;
