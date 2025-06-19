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
    const service = new SignalRService('https://localhost:5001/simdynohub');

    const startConnection = async () => {
      try {
        await service.start()
        setStatus('Connected to server');
      } catch (err) {
        console.log('SignalR connection failed: ', err);
        setStatus('Connection failed. Retrying in 5s...');
        setTimeout(startConnection, 5000);
      }
    };

    // Register event handlers
    service.onTelemetry((partialData: Partial<Telemetry>) => {
      let packetID: number | undefined = undefined;
      try {
        if (typeof partialData.timeStampMS === 'number' &&
            initialTimeStampMS.current === null) {
          initialTimeStampMS.current = partialData.timeStampMS;
        }
        console.log('Received telemetry:', partialData);
        if (typeof partialData.timeStampMS === 'number' &&
            initialTimeStampMS.current !== null) {
          packetID = partialData.timeStampMS;
          partialData.timeStampMS = partialData.timeStampMS - initialTimeStampMS.current;
        }
        setTelemetry((prev) => ({ ...prev, ...updateTelemetry(partialData) }));
        // Confirm receipt to server if packetID is available
        if (typeof packetID !== 'undefined') {
          service.confirmReceipt(packetID).catch((err) => console.error('Error confirming receipt:', err));
        }
        else {
          console.warn('Received telemetry without packetID, not confirming receipt.');
        }
      } catch (error) {
        console.error('Error processing telemetry:', error);
        // On error, respond with packetID -1
        service.confirmReceipt(-1).catch((err) => console.error('Error confirming receipt with -1:', err));
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

  const contextValue = React.useMemo(
    () => ({ telemetry, status, message }),
    [telemetry, status, message]
  );

  return (
    <SignalRContext.Provider value={contextValue}>
      {children}
    </SignalRContext.Provider>
  );
};

export const useSignalR = () => useContext(SignalRContext);