// SignalRService.tsx
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Telemetry, updateTelemetry } from '@/models/Telemetry';

export class SignalRService {
  private connection: HubConnection;

  constructor(url: string) {
    this.connection = new HubConnectionBuilder()
      .withUrl(url)
      .withAutomaticReconnect()
      .build();
  }

  async start(): Promise<void> {
    try {
      await this.connection.start();
      console.log('SignalR Connected');
    } catch (err) {
      console.error('SignalR Connection Error:', err);
      throw err; // Let the caller handle retries
    }
  }

  onTelemetry(callback: (telemetry: Partial<Telemetry>) => void): void {
    this.connection.on('ReceiveData', (data: unknown) => {
      const telemetry = updateTelemetry(data);
      callback(telemetry);
    });
  }

  onMessage(callback: (message: string) => void): void {
    this.connection.on('BroadcastMessage', (msg: string) => {
      callback(msg);
    });
  }

  onClose(callback: (error?: Error) => void): void {
    this.connection.onclose((error) => callback(error));
  }

  onReconnecting(callback: (error?: Error) => void): void {
    this.connection.onreconnecting((error) => callback(error));
  }

  onReconnected(callback: (connectionId?: string) => void): void {
    this.connection.onreconnected((connectionId) => callback(connectionId));
  }

  async setRequiredFields(fields: string[]): Promise<void> {
    try {
      await this.connection.invoke(`SetRequestedFields`, fields);
      console.log(`Requested fields set: `, fields);
    } catch (err) {
      console.error(`Error setting requested fields:`, err);
      throw err;
    }

  }

  async stop(): Promise<void> {
    await this.connection.stop();
  }
}