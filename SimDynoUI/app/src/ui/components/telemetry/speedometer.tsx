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
    const { telemetry } = useSignalR();
    if (!telemetry) {
        return <div style={speedometerStyle}>0 MPH</div>
    }
    
    return (
        <div className="speedometer">{telemetry.speedMPH} MPH</div>
    );
}