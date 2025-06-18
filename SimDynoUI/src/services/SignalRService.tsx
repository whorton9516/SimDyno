import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Telemetry, updateTelemetry } from '../type/Telemetry';

export class SignalRService {
  private connection: HubConnection;

  constructor(url: string) {
    this.connection = new HubConnectionBuilder()
      .withUrl(url)
      .withAutomaticReconnect()
      .build();
  }

  async start(): Promise<void> {
    if (this.connection.state === 'Disconnected') {
      try {
        await this.connection.start();
        console.log('SignalR Connected');
      } catch (err) {
        console.error('SignalR Connection Error:', err);
        throw err; // Let the caller handle retries
      }
    } else {
      console.log(`Cannot start connection. Current state: ${this.connection.state}`);
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

  async stop(): Promise<void> {
    await this.connection.stop();
  }
}