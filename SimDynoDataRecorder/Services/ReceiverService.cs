// Portions of this file include code originally written by Austin Baccus (MIT License)

using SimDynoServer.Models;
using System.Net;
using System.Net.Sockets;
using SimDynoServer.Utils;
using SimDynoDataRecorder.Models;

namespace SimDynoDataRecorder.Services;

public class ReceiverService
{
    public Socket? Listener { get; set; }
    AppState _state { get; set; } = AppState.Idle;
    readonly RecordingService _recordingService;

    private string _ipAddress = "127.0.0.1";
    private int _port = 5555;

    bool _raceStarted = false;
    bool _gameConnected = false;
    public bool Listen = false;

    MainForm _parent;
    bool _recording = false;
    bool _firstPacket = true;

    public ReceiverService(RecordingService recordingService)
    {
        _recordingService = recordingService;
    }

    public async Task ListenAsync(MainForm parentForm)
    {
        try
        {
            _parent = parentForm;
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var endPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);
            Listener.Bind(endPoint);

            byte[] buffer = new byte[331];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine($"Started Listening at Endpoint: {endPoint.Address}:{endPoint.Port}");

            while (Listen)
            {
                var bytesReceived = Listener.ReceiveFrom(buffer, ref remote);

                if (bytesReceived > 0)
                {
                    var parsedData = ParseForza(buffer);

                    if (!_raceStarted && parsedData.IsRaceOn)
                        _raceStarted = true;


                    if (_state == AppState.Recording)
                    {
                        if (parsedData.LapNumber == 1)
                        {
                            if (_recordingService.Filename.Length == 0)
                                _recordingService.Filename = 
                                    $"{parsedData.TrackOrdinal}_{parsedData.CarOrdinal}_{DateTime.Now.ToString("MMddyyyy")}.txt";

                            if (!_recording)
                            {
                                Console.WriteLine("Reached lap range. Now recording.");
                                _recordingService.StartRecording();
                                _recording = true;
                            }

                            _recordingService.Record(buffer, _firstPacket);
                            if (_firstPacket)
                                _firstPacket = false;
                        }

                        if (parsedData.LapNumber > 1 && _recording)
                        {
                            _recording = false;
                            Console.WriteLine("Out of lap range. Stopping recording.");
                            _recordingService?.StopRecording();
                            Listen = false;
                        }
                    }

                    BroadcastData(parsedData);
                }
            }

            //_recordingService?.StopRecording();
            Console.WriteLine("Listening stopped.");
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

        return data;
    }

    private void BroadcastData(ForzaData data)
    {
        _parent.UpdateTelemetryDataView(data);
    }

    public void UpdateState(AppState state)
    {
        _state = state;

        switch (_state)
        {
            case AppState.Idle:
            case AppState.Broadcast:
                Listen = false;
                break;
            case AppState.Listen:
                Listen = true;
                break;
            case AppState.Recording:
                Listen = true;
                break;
        }
    }

    public void StartListening(MainForm parent, string ipAddress, string port)
    {
        _ipAddress = ipAddress;
        _port = int.Parse(port);
        Task.Run(() => ListenAsync(parent));
    }
}