﻿using SimDynoDevSuite.Views;
using SimDynoServer.Models;
using SimDynoServer.Utils;
using System.Net;
using System.Net.Sockets;

namespace SimDynoDevSuite.Services;
public class BroadcastService
{
    private UdpClient? _udpClient;
    private IPEndPoint? _endPoint;
    private string _ipAddress;
    private string _port;
    private byte[]? _latestPacket;
    private CancellationTokenSource? _cts;
    private bool _gameConnected = false;

    // Event to notify when broadcasting stops
    public event Action? BroadcastStopped;

    public BroadcastService(string ipAddress, string port)
    {
        _ipAddress = ipAddress;
        _port = port;
    }

    public bool GameConnected
    {
        get => _gameConnected;
        set => _gameConnected = value;
    }

    public async Task StartBroadcasting(List<byte[]> packets)
    {
        _endPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), int.Parse(_port));
        _udpClient = new UdpClient();
        _cts = new CancellationTokenSource();
        _gameConnected = true;

        try
        {
            int i = 0;
            uint? prevTimestamp = null;

            while (!_cts.Token.IsCancellationRequested)
            {
                var packet = packets[i];
                uint currTimestamp = packet.TimestampMs();

                // Calculate delay based on timestamp difference
                int delayMs = 0;
                if (prevTimestamp.HasValue)
                {
                    // Handle possible wrap-around (uint overflow)
                    delayMs = (int)((currTimestamp >= prevTimestamp)
                        ? (currTimestamp - prevTimestamp)
                        : (uint.MaxValue - prevTimestamp.Value + currTimestamp + 1));
                }

                await _udpClient.SendAsync(packet, packet.Length, _endPoint);
                _latestPacket = packet;

                prevTimestamp = currTimestamp;

                if (delayMs > 0 && delayMs < 10000)
                {
                    await Task.Delay(delayMs, _cts.Token);
                }

                i++;
                if (i >= packets.Count)
                    i = 0; // Loop back to the beginning
            }
        }
        catch (ObjectDisposedException)
        {
            // Ignore, occurs if stopped during send
        }
        catch (Exception ex)
        {
            ex.LogException("Broadcast Error");
        }
        finally
        {
            _udpClient?.Dispose();
            _udpClient = null;
            _cts?.Dispose();
            _cts = null;
            _gameConnected = false;
            BroadcastStopped?.Invoke();
        }
    }

    public void StopBroadcasting()
    {
        _cts?.Cancel();
        _udpClient?.Dispose();
        _udpClient = null;
        _gameConnected = false;
        BroadcastStopped?.Invoke();
    }

    public ForzaData ParseForza(byte[] packet)
    {
        var data = new ForzaData();

        try
        {
            // Sled
            data.IsRaceOn = packet.IsRaceOn();
            data.TimeStampMS = packet.TimestampMs();
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
        }
        catch (Exception ex)
        {
            ex.LogException("Error parsing Forza packet");
        }

        return data;
    }

    // Add a method to get the latest packet
    public byte[]? GetLatestPacket() => _latestPacket;
}
