﻿using SimDynoDataRecorder.Views;
using SimDynoServer.Models;
using SimDynoServer.Utils;
using System.Net;
using System.Net.Sockets;

namespace SimDynoDataRecorder.Services;
public class BroadcastService
{
    readonly UdpClient _udpClient;
    readonly IPEndPoint _endPoint;
    CancellationTokenSource? _cts;
    volatile bool _broadcasting = false;

    public BroadcastService(string ipAddress, string port)
    {
        _udpClient = new UdpClient();
        _endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), int.Parse(port));
    }

    public async Task StartBroadcasting(List<byte[]> packets, TelemetryDataView? telemetryDataView)
    {
        if (_broadcasting) return;

        _broadcasting = true;
        _cts = new CancellationTokenSource();
        var token = _cts.Token;
        const int interval = 1000 / 60; // 60Hz

        try
        {
            Console.WriteLine($"Starting Broadcast.");

            foreach (var packet in packets)
            {
                if (token.IsCancellationRequested)
                    break;

                try
                {
                    await _udpClient.SendAsync(packet, packet.Length, _endPoint);
                    var parsedData = ParseForza(packet);
                    telemetryDataView?.Invoke(() => telemetryDataView?.UpdateData(parsedData));
                    await Task.Delay(interval);
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Broadcasting operation canceled.");
                    break;
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Issue sending packet: {packet}\n{ex.Message}");
                }
            }

            Console.WriteLine("Broadcast finished.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Broadcasting error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Broadcasting Stopped.");
            _udpClient.Dispose();
        }
    }

    public void StopBroadcasting()
    {
        if (!_broadcasting) return;
        Console.WriteLine("Stopping Broadcast.");
        _cts?.Cancel();
        _broadcasting = false;
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
}
