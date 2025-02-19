import { useSignalR } from "../../services/SignalRContext";
import "../../../styles/ui/utility/_signalr-message-log.scss";

export function SignalRMessageLog() {
    const { message } = useSignalR();

    return (
        <div className="signalr-message-log">
            <div>Server Message: {message || "--"}</div>
        </div>
    );
}