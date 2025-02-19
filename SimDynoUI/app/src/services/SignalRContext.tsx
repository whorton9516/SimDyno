import React, { createContext, useContext, useEffect, useRef, useState } from "react";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

interface SignalRContextType {
    telemetry: any;
    status: string;
    message: string;
}

const SignalRContext = createContext<SignalRContextType>({
    telemetry: null,
    status: "Waiting on server...",
    message: "",
});

export const SignalRProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const connectionRef = useRef<HubConnection | null>(null);
    const [telemetry, setTelemetry] = useState<any>(null);
    const [status, setStatus] = useState("Waiting on server...");
    const [message, setMessage] = useState("");
    const [hasConnected, setHasConnected] = useState(false);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl("http://localhost:5000/simdynohub")
            .withAutomaticReconnect()
            .build();

        connectionRef.current = newConnection;

        const startConnection = async () => {
            try {
                await newConnection.start();
                console.log("Connected to server");
                setStatus("Connected to server");
                setHasConnected(true);
            } catch (err) {
                if (hasConnected){
                    setStatus("Connection failed. Retrying in 5s...");
                    setTimeout(startConnection, 5000); // Retry logic
                }
            }
        };

        // Set up event listeners
        newConnection.on("ReceiveData", (data) => {
            setTelemetry(data);
        });

        newConnection.on("BroadcastMessage", (msg) => {
            setMessage(msg);
        });

        startConnection();

        return () => {
            newConnection.stop();
        };
    }, []);

    return (
        <SignalRContext.Provider value={{ telemetry, status, message }}>
            {children}
        </SignalRContext.Provider>
    );
};

export const useSignalR = () => useContext(SignalRContext);
