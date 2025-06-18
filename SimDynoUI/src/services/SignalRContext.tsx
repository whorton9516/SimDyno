import React, { createContext, useContext, useEffect, useRef, useState } from 'react';
import { SignalRService } from './SignalRService';
import { Telemetry, defaultTelemetry, updateTelemetry } from '../type/Telemetry';

interface SignalRContextType {
  telemetry: Telemetry;
  status: string;
  message: string;
}

const SignalRContext = createContext<SignalRContextType>({
  telemetry: defaultTelemetry,
  status: 'Waiting on server...',
  message: ''
});

export const SignalRProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [telemetry, setTelemetry] = useState<Telemetry>(defaultTelemetry);
  const [status, setStatus] = useState('Waiting on server...');
  const [message, setMessage] = useState('');
  const initialTimeStampMS = useRef<number | null>(null);

  useEffect(() => {
    const service = new SignalRService('http://localhost:5000/simdynohub');

    const startConnection = async () => {
      try {
        await service.start(); // Rely on SignalRService to handle state
        setStatus('Connected to server');
      } catch (err) {
        console.log('SignalR connection failed: ', err);
        setStatus('Connection failed. Retrying in 5s...');
        setTimeout(startConnection, 5000);
      }
    };

    // Register event handlers
    service.onTelemetry((partialData: Partial<Telemetry>) => {
      try {
        if (typeof partialData.timeStampMS === 'number' &&
            initialTimeStampMS.current === null) {
          initialTimeStampMS.current = partialData.timeStampMS;
        }

        if (typeof partialData.timeStampMS === 'number' &&
            initialTimeStampMS.current !== null) {
          partialData.timeStampMS = partialData.timeStampMS - initialTimeStampMS.current;
        }
        setTelemetry((prev) => ({ ...prev, ...updateTelemetry(partialData) }));
      } catch (error) {
        console.error('Error processing telemetry:', error);
      }
    });

    service.onMessage((msg: string) => {
      try {
        setMessage(msg);
      } catch (error) {
        console.error('Error processing message:', error);
      }
    });

    service.onClose((error) => {
      setStatus('Connection closed.');
      if (error) {
        console.error('Connection closed due to error:', error);
      }
    });

    service.onReconnecting((error) => {
      setStatus('Reconnecting...');
      if (error) {
        console.error('Reconnecting due to error:', error);
      }
    });

    service.onReconnected((connectionId) => {
      setStatus('Reconnected.');
      console.log('Reconnected with connectionId:', connectionId);
    });

    startConnection();

    // Cleanup
    return () => {
      service.stop().catch((err) => console.error('Error stopping SignalR:', err));
    };
  }, []);

  return (
    <SignalRContext.Provider value={{ telemetry, status, message }}>
      {children}
    </SignalRContext.Provider>
  );
};

export const useSignalR = () => useContext(SignalRContext);