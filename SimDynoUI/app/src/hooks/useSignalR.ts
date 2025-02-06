import { useEffect, useState } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export function useSignalR() {
    const [connection, setConnection] = useState<HubConnection | null>(null);
    const [telemetry, setTelemetry] = useState<any>(null);
    const [status, setStatus] = useState('Waiting on server...');
    const [message, setMessage] = useState('');
    const [hasConnected, setHasConnected] = useState(false);

    // Initialize connection only once
    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5000/simdynohub')
            .withAutomaticReconnect()
            .build();

        setConnection(newConnection);
    }, []);

    // Start connection and set up event handlers
    useEffect(() => {
        if (connection) {
            // Function to start the connection with retry logic
            const startConnection = async () => {
                try {
                    await connection.start();
                    console.log('Connected to server');
                    setStatus('Connected to server');
                    setHasConnected(true);
                } catch (err) {
                    console.error('Connection failed: ', err);
                    if (hasConnected){
                        setStatus('Connection failed');
                    }
                    // Retry after 5 seconds
                    setTimeout(startConnection, 5000);
                }
            };

            // Set up event handlers
            connection.on('ReceiveData', (data) => {
                console.log('Received telemetry data:', data);
                setTelemetry(data);
            });

            connection.on('BroadcastMessage', (message) => {
                console.log(`BroadcastMessage received: ${message}`);
                setMessage(message); // Update status based on broadcast message
            });

            // Start the connection
            startConnection();
        }
    }, [connection]);

    return { telemetry, status, message };
}
