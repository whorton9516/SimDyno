import { useSignalR } from "../../services/SignalRContext";
import '../../../styles/ui/utility/_signalr-status-bar.scss';

export function SignalRStatusBar() {
    const { status } = useSignalR();

    return (
        <div className="signalr-status-bar">
            <div>Connection Status: {status}</div>
        </div>
    );
}