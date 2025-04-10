import React from "react";
import { useSignalR } from "../../services/SignalRContext";

const SignalRStatusBarStyle: React.CSSProperties = {
  fontWeight: 'bold',
  marginBottom: '10px',
};

export function SignalRStatusBar() {
  const { status } = useSignalR();

  return (
    <div style={SignalRStatusBarStyle}>
      <div>Connection Status: {status}</div>
    </div>
  );
}