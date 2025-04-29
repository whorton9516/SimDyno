import React, { useEffect } from "react";
import { useSignalR } from "../../../services/SignalRContext";

const speedometerStyle: React.CSSProperties = {
  fontFamily: '"Michroma", serif',
  fontWeight: 400,
  fontStyle: "normal",
  fontSize: "48px",
  flex: 1,
  width: "100%",
  whiteSpace: "nowrap",
};

export function Speedometer() {
  const { telemetry, setRequiredFields } = useSignalR();

  useEffect(() => {
    setRequiredFields(['Speed', 'SpeedMPH', 'SpeedKPH']);
  }, [setRequiredFields]);
    
  return (
    <div style={speedometerStyle}>{telemetry.speedMPH} MPH</div>
  );
}