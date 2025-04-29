// SignalRContext.tsx
import React, { createContext, useContext, useEffect, useRef, useState } from 'react';
import { SignalRService } from './SignalRService';
import { Telemetry, defaultTelemetry, updateTelemetry } from '@/models/Telemetry';

interface SignalRContextType {
  telemetry: Telemetry;
  status: string;
  message: string;
  setRequiredFields: (fields: string[]) => void;
}

const SignalRContext = createContext<SignalRContextType>({
  telemetry: defaultTelemetry,
  status: 'Waiting on server...',
  message: '',
  setRequiredFields: () => {},
});

export const SignalRProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const serviceRef = useRef<SignalRService | null>(null);
  const [telemetry, setTelemetry] = useState<Telemetry>(defaultTelemetry);
  const [status, setStatus] = useState('Waiting on server...');
  const [message, setMessage] = useState('');
  const [requiredFields, setRequiredFields] = useState<string[]>([]);

  useEffect(() => {
    const service = new SignalRService('http://localhost:5000/simdynohub');
    serviceRef.current = service;

    const startConnection = async () => {
      try {
        await service.start();
        setStatus('Connected to server');
        await service.setRequiredFields(Object.keys(defaultTelemetry));
        setRequiredFields(Object.keys(defaultTelemetry));
      } catch (err) {
        console.log('SignalR connection failed: ', err);
        setStatus('Connection failed. Retrying in 5s...');
        setTimeout(startConnection, 5000);
      }
    };

    // Register event handlers
    service.onTelemetry((partialData: Partial<Telemetry>) => {
      try {
        setTelemetry((prev) => ({ ...prev, ...partialData }));
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
      // Re-request fields on reconnect
      if (requiredFields.length > 0) {
        serviceRef.current?.setRequiredFields(requiredFields).catch((err) =>
          console.error('Error re-setting required fields:', err)
        );
      }
    });

    startConnection();

    // Cleanup
    return () => {
      service.stop().catch((err) => console.error('Error stopping SignalR:', err));
    };
  }, []);

  useEffect(() => {
    if (serviceRef.current && requiredFields.length > 0) {
      serviceRef.current.setRequiredFields(requiredFields).catch((err) =>
        console.error('Error setting required fields:', err)
      );
    }
  }, [requiredFields]);

  return (
    <SignalRContext.Provider value={{ telemetry, status, message, setRequiredFields }}>
      {children}
    </SignalRContext.Provider>
  );
};

export const useSignalR = () => useContext(SignalRContext);