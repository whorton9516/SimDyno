import { useSignalR } from "../../../services/SignalRContext";

export function Speedometer() {
    const { telemetry } = useSignalR();
    if (!telemetry) {
        return <div> 0 MPH</div>
    }
    
    return (
        <div>
            <p>{telemetry.speedMPH} MPH</p>
        </div>
    );
}