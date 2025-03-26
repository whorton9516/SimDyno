// Portions of this file include code originally written by Austin Baccus (MIT License)

using DataReceiver.Helpers;
using Microsoft.AspNetCore.SignalR;
using SimDynoServer.Hubs;
using SimDynoServer.Models;
using System.Net;
using System.Net.Sockets;

namespace DataReceiver;

public class Receiver
{
    public Socket? Listener { get; set; }
    readonly IHubContext<SimDynoHub> _hubContext;

    const string IpAddress = "127.0.0.1";
    const int Port = 5555;

    bool _gameConnected = false;

    public Receiver(IHubContext<SimDynoHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task ListenAsync()
    {
        try
        {
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var endPoint = new IPEndPoint(IPAddress.Parse(IpAddress), Port);
            Listener.Bind(endPoint);

            // Notify clients that the server is waiting for a connection from the game
            await BroadcastMessageToClients("Waiting for connection from the game...");

            byte[] buffer = new byte[1024];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                var bytesReceived = Listener.ReceiveFrom(buffer, ref remote);

                if (bytesReceived > 0)
                {
                    // If this is the first packet received, notify clients that the game is connected
                    if (!_gameConnected)
                    {
                        _gameConnected = true;
                        await BroadcastMessageToClients("Game connected. Starting telemetry broadcast...");
                    }

                    var parsedData = ParseForza(buffer);

                    await _hubContext.Clients.All.SendAsync("ReceiveData", parsedData);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Listener?.Close();
        }
    }
    
    public ForzaData ParseForza(byte[] packet)
    {
        var data = new ForzaData();

        // Sled
        data.IsRaceOn = packet.IsRaceOn();
        data.TimestampMS = packet.TimestampMs();
        data.EngineMaxRpm = packet.EngineMaxRpm();
        data.EngineIdleRpm = packet.EngineIdleRpm();
        data.CurrentEngineRpm = packet.CurrentEngineRpm();
        data.AccelerationX = packet.AccelerationX();
        data.AccelerationY = packet.AccelerationY();
        data.AccelerationZ = packet.AccelerationZ();
        data.VelocityX = packet.VelocityX();
        data.VelocityY = packet.VelocityY();
        data.VelocityZ = packet.VelocityZ();
        data.AngularVelocityX = packet.AngularVelocityX();
        data.AngularVelocityY = packet.AngularVelocityY();
        data.AngularVelocityZ = packet.AngularVelocityZ();
        data.Yaw = packet.Yaw();
        data.Pitch = packet.Pitch();
        data.Roll = packet.Roll();
        data.NormalizedSuspensionTravelFL = packet.NormalizedSuspensionTravelFL();
        data.NormalizedSuspensionTravelFR = packet.NormalizedSuspensionTravelFR();
        data.NormalizedSuspensionTravelRL = packet.NormalizedSuspensionTravelRL();
        data.NormalizedSuspensionTravelRR = packet.NormalizedSuspensionTravelRR();
        data.TireSlipRatioFL = packet.TireSlipRatioFL();
        data.TireSlipRatioFR = packet.TireSlipRatioFR();
        data.TireSlipRatioRL = packet.TireSlipRatioRL();
        data.TireSlipRatioRR = packet.TireSlipRatioRR();
        data.WheelRotationSpeedFL = packet.WheelRotationSpeedFL();
        data.WheelRotationSpeedFR = packet.WheelRotationSpeedFR();
        data.WheelRotationSpeedRL = packet.WheelRotationSpeedRL();
        data.WheelRotationSpeedRR = packet.WheelRotationSpeedRR();
        data.WheelOnRumbleStripFL = packet.WheelOnRumbleStripFL();
        data.WheelOnRumbleStripFR = packet.WheelOnRumbleStripFR();
        data.WheelOnRumbleStripRL = packet.WheelOnRumbleStripRL();
        data.WheelOnRumbleStripRR = packet.WheelOnRumbleStripRR();
        data.WheelInPuddleDepthFL = packet.WheelInPuddleFL();
        data.WheelInPuddleDepthFR = packet.WheelInPuddleFR();
        data.WheelInPuddleDepthRL = packet.WheelInPuddleRL();
        data.WheelInPuddleDepthRR = packet.WheelInPuddleRR();
        data.SurfaceRumbleFL = packet.SurfaceRumbleFL();
        data.SurfaceRumbleFR = packet.SurfaceRumbleFR();
        data.SurfaceRumbleRL = packet.SurfaceRumbleRL();
        data.SurfaceRumbleRR = packet.SurfaceRumbleRR();
        data.TireSlipAngleFL = packet.TireSlipAngleFL();
        data.TireSlipAngleFR = packet.TireSlipAngleFR();
        data.TireSlipAngleRL = packet.TireSlipAngleRL();
        data.TireSlipAngleRR = packet.TireSlipAngleRR();
        data.TireCombinedSlipFL = packet.TireCombinedSlipFL();
        data.TireCombinedSlipFR = packet.TireCombinedSlipFR();
        data.TireCombinedSlipRL = packet.TireCombinedSlipRL();
        data.TireCombinedSlipRR = packet.TireCombinedSlipRR();
        data.SuspensionTravelMetersFL = packet.SuspensionTravelMetersFL();
        data.SuspensionTravelMetersFR = packet.SuspensionTravelMetersFR();
        data.SuspensionTravelMetersRL = packet.SuspensionTravelMetersRL();
        data.SuspensionTravelMetersRR = packet.SuspensionTravelMetersRR();
        data.CarOrdinal = packet.CarOrdinal();
        data.CarClass = packet.CarClass();
        data.CarPerformanceIndex = packet.CarPerformanceIndex();
        data.DrivetrainType = packet.DriveTrain();
        data.NumCylinders = packet.NumCylinders();

        // Dash
        data.PositionX = packet.PositionX();
        data.PositionY = packet.PositionY();
        data.PositionZ = packet.PositionZ();
        data.Speed = packet.Speed();
        data.Power = packet.Power();
        data.Torque = packet.Torque();
        data.TireTempFL = packet.TireTempFL();
        data.TireTempFR = packet.TireTempFR();
        data.TireTempRL = packet.TireTempRL();
        data.TireTempRR = packet.TireTempRR();
        data.Boost = packet.Boost();
        data.Fuel = packet.Fuel();
        data.Distance = packet.Distance();
        data.BestLapTime = packet.BestLapTime();
        data.LastLapTime = packet.LastLapTime();
        data.CurrentLapTime = packet.CurrentLapTime();
        data.CurrentRaceTime = packet.CurrentRaceTime();
        data.LapNumber = packet.LapNumber();
        data.RacePosition = packet.RacePosition();
        data.Accelerator = packet.Accelerator();
        data.Brake = packet.Brake();
        data.Clutch = packet.Clutch();
        data.Handbrake = packet.Handbrake();
        data.Gear = packet.Gear();
        data.Steer = packet.Steer();
        data.NormalizedDrivingLine = packet.NormalizedDrivingLine();
        data.NormalizedAiBrakeDifference = packet.NormalizedAiBrakeDifference();
        data.TireWearFL = packet.TireWearFL();
        data.TireWearFR = packet.TireWearFR();
        data.TireWearRL = packet.TireWearRL();
        data.TireWearRR = packet.TireWearRR();
        data.TrackOrdinal = packet.TrackOrdinal();

        // QOL
        data.SpeedMPH = MathF.Round(packet.Speed() * 2.23694f);
        data.SpeedKPH = MathF.Round(packet.Speed() * 3.6f);

        return data;
    }

    async Task BroadcastMessageToClients(string message)
    {
        try
        {
            Console.WriteLine(message); // Log the message to the server console
            await _hubContext.Clients.All.SendAsync("BroadcastMessage", message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error broadcasting message: {ex.Message}");
        }
    }   
}
