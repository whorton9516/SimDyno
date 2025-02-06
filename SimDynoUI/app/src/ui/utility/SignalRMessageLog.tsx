import React from 'react';
import { useSignalR } from '../../hooks/useSignalR';
import { useState, useEffect } from 'react';
import '../../../styles/ui/utility/_signalr-message-log.scss';


export function SignalRMessageLog() {
    const { message } = useSignalR();
    const [serverMessage, setServerMessage] = useState('--');

    useEffect(() => {
        setServerMessage(message);
    }, [message]); // Dependency on message to avoid infinite loop

    return (
        <div className="signalr-message-log">
            <div>Server Message: {serverMessage}</div>
        </div>
    );
}