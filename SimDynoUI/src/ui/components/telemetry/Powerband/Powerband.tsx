import React, { useState, useMemo, useRef, useEffect } from 'react';
import { Box, Typography } from '@mui/material';
import { useSignalR } from '../../../../services/SignalRContext';

// Simple point type for chart
interface PowerPoint {
  rpm: number;
  power: number;
}

const BIN_SIZE = 100;
const SMOOTH_WINDOW = 5; // Number of samples to average per bin
const THROTTLE_THRESHOLD = 0.9;

const Powerband: React.FC = () => {
  const { telemetry } = useSignalR();

  // For each RPM bin, keep a rolling window of last N power values
  const binSamplesRef = useRef<Map<number, number[]>>(new Map());
  const [maxPowerMap, setMaxPowerMap] = useState<Map<number, number>>(new Map());
  const [livePowerMap, setLivePowerMap] = useState<Map<number, number>>(new Map());

  useEffect(() => {
    if (!telemetry) return;
    // Use accelerator (normalized throttle input) for filtering
    if (typeof telemetry.accelerator === 'number' && telemetry.accelerator < THROTTLE_THRESHOLD) return; // Only record at high throttle

    const rpm = Math.round(telemetry.currentEngineRpm / BIN_SIZE) * BIN_SIZE;
    const power = telemetry.power;
    if (rpm <= 0 || power <= 0) return;

    // Rolling window for smoothing
    const prevSamples = binSamplesRef.current.get(rpm) || [];
    const newSamples = [...prevSamples.slice(-SMOOTH_WINDOW + 1), power];
    binSamplesRef.current.set(rpm, newSamples);
    const avgPower = newSamples.reduce((a, b) => a + b, 0) / newSamples.length;

    // Update live power map (smoothed)
    setLivePowerMap((prev) => {
      const newMap = new Map(prev);
      newMap.set(rpm, avgPower);
      return newMap;
    });

    // Update max power map (smoothed)
    setMaxPowerMap((prev) => {
      const prevMax = prev.get(rpm) ?? 0;
      if (avgPower > prevMax) {
        const newMap = new Map(prev);
        newMap.set(rpm, avgPower);
        return newMap;
      }
      return prev;
    });
  }, [telemetry]);

  // Prepare sorted points for the chart
  const sortedRpm = useMemo(() => {
    return Array.from(new Set([
      ...Array.from(maxPowerMap.keys()),
      ...Array.from(livePowerMap.keys()),
    ])).sort((a, b) => a - b);
  }, [maxPowerMap, livePowerMap]);

  const maxPowerData = sortedRpm.map((rpm) => maxPowerMap.get(rpm) ?? null);
  const livePowerData = sortedRpm.map((rpm) => livePowerMap.get(rpm) ?? null);

  const currentRpm = telemetry?.currentEngineRpm;

  return (
    <Box sx={{ width: '100%', height: 400 }}>
      <Typography variant="subtitle1" sx={{ marginBottom: 0.5 }}>
        Current RPM: {Math.round(currentRpm) ?? '-'}
      </Typography>
      <Typography variant="h6" sx={{ marginBottom: 1 }}>
        Gear: {telemetry ? telemetry.gear : '-'}
      </Typography>
    </Box>
  );
};

export { Powerband };
