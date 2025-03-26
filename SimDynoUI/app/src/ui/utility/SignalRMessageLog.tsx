import { useSignalR } from "../../services/SignalRContext";

const SignalRMessageLogStyle: React.CSSProperties = {
    textAlign: 'center',
    marginTop: '10px',
    padding: '10px',
    border: '1px solid #bbb',
    flexShrink: 0,
};

export function SignalRMessageLog() {
    const { message } = useSignalR();

    return (
        <div style={SignalRMessageLogStyle}>
            <div>Server Message: {message || "--"}</div>
        </div>
    );
}