using SimDynoDataRecorder.Services;
using SimDynoServer.Models;

namespace SimDynoDataRecorder.Views;
public partial class TelemetryDataView : Form
{
    private ForzaData? _latestData;
    private readonly System.Windows.Forms.Timer _uiTimer;
    private readonly Dictionary<string, Label> _labelMap;

    private uint? _startTimestampMS = null;
    private BroadcastService? _broadcastService;

    public TelemetryDataView()
    {
        InitializeComponent();
        DoubleBuffered = true;

        _labelMap = new Dictionary<string, Label>
        {
            { nameof(ForzaData.DrivetrainType), labelDrivetrainType },
            { nameof(ForzaData.NumCylinders), labelNumCylinders },
            { nameof(ForzaData.CarClass), labelClassAndIndex },
            { nameof(ForzaData.CarOrdinal), labelCarOrdinal },
            { nameof(ForzaData.SuspensionTravelMetersRR), labelSuspensionTravelMetersRR },
            { nameof(ForzaData.SuspensionTravelMetersRL), labelSuspensionTravelMetersRL },
            { nameof(ForzaData.SuspensionTravelMetersFR), labelSuspensionTravelMetersFR },
            { nameof(ForzaData.SuspensionTravelMetersFL), labelSuspensionTravelMetersFL },
            { nameof(ForzaData.TireCombinedSlipRR), labelTireCombinedSlipRR },
            { nameof(ForzaData.TireCombinedSlipRL), labelTireCombinedSlipRL },
            { nameof(ForzaData.TireCombinedSlipFR), labelTireCombinedSlipFR },
            { nameof(ForzaData.TireCombinedSlipFL), labelTireCombinedSlipFL },
            { nameof(ForzaData.TireSlipAngleRR), labelTireSlipAngleRR },
            { nameof(ForzaData.TireSlipAngleRL), labelTireSlipAngleRL },
            { nameof(ForzaData.TireSlipAngleFR), labelTireSlipAngleFR },
            { nameof(ForzaData.TireSlipAngleFL), labelTireSlipAngleFL },
            { nameof(ForzaData.SurfaceRumbleRR), labelSurfaceRumbleRR },
            { nameof(ForzaData.SurfaceRumbleRL), labelSurfaceRumbleRL },
            { nameof(ForzaData.SurfaceRumbleFR), labelSurfaceRumbleFR },
            { nameof(ForzaData.SurfaceRumbleFL), labelSurfaceRumbleFL },
            { nameof(ForzaData.WheelInPuddleDepthRR), labelWheelInPuddleDepthRR },
            { nameof(ForzaData.WheelInPuddleDepthRL), labelWheelInPuddleDepthRL },
            { nameof(ForzaData.WheelInPuddleDepthFR), labelWheelInPuddleDepthFR },
            { nameof(ForzaData.WheelInPuddleDepthFL), labelWheelInPuddleDepthFL },
            { nameof(ForzaData.WheelOnRumbleStripRR), labelWheelOnRumbleStripRR },
            { nameof(ForzaData.WheelOnRumbleStripRL), labelWheelOnRumbleStripRL },
            { nameof(ForzaData.WheelOnRumbleStripFR), labelWheelOnRumbleStripFR },
            { nameof(ForzaData.WheelOnRumbleStripFL), labelWheelOnRumbleStripFL },
            { nameof(ForzaData.WheelRotationSpeedRR), labelWheelRotationSpeedRR },
            { nameof(ForzaData.WheelRotationSpeedRL), labelWheelRotationSpeedRL },
            { nameof(ForzaData.WheelRotationSpeedFR), labelWheelRotationSpeedFR },
            { nameof(ForzaData.WheelRotationSpeedFL), labelWheelRotationSpeedFL },
            { nameof(ForzaData.TireSlipRatioRR), labelTireSlipRatioRR },
            { nameof(ForzaData.TireSlipRatioRL), labelTireSlipRatioRL },
            { nameof(ForzaData.TireSlipRatioFR), labelTireSlipRatioFR },
            { nameof(ForzaData.TireSlipRatioFL), labelTireSlipRatioFL },
            { nameof(ForzaData.Roll), labelRoll },
            { nameof(ForzaData.Pitch), labelPitch },
            { nameof(ForzaData.Yaw), labelYaw },
            { nameof(ForzaData.AngularVelocityZ), labelAngularVelocityZ },
            { nameof(ForzaData.AngularVelocityY), labelAngularVelocityY },
            { nameof(ForzaData.AngularVelocityX), labelAngularVelocityX },
            { nameof(ForzaData.VelocityZ), labelVelocityZ },
            { nameof(ForzaData.VelocityY), labelVelocityY },
            { nameof(ForzaData.VelocityX), labelVelocityX },
            { nameof(ForzaData.NormalizedSuspensionTravelRR), labelNormalizedSuspensionTravelRR },
            { nameof(ForzaData.NormalizedSuspensionTravelRL), labelNormalizedSuspensionTravelRL },
            { nameof(ForzaData.NormalizedSuspensionTravelFR), labelNormalizedSuspensionTravelFR },
            { nameof(ForzaData.NormalizedSuspensionTravelFL), labelNormalizedSuspensionTravelFL },
            { nameof(ForzaData.AccelerationZ), labelAccelerationZ },
            { nameof(ForzaData.AccelerationY), labelAccelerationY },
            { nameof(ForzaData.AccelerationX), labelAccelerationX },
            { nameof(ForzaData.CurrentEngineRpm), labelCurrentEngineRpm },
            { nameof(ForzaData.EngineIdleRpm), labelEngineIdleRpm },
            { nameof(ForzaData.EngineMaxRpm), labelEngineMaxRpm },
            { nameof(ForzaData.IsRaceOn), labelIsRaceOn },
            { nameof(ForzaData.TimeStampMS), labelTimeStampMS },
            { nameof(ForzaData.Steer), labelSteer },
            { nameof(ForzaData.Gear), labelGear },
            { nameof(ForzaData.Handbrake), labelHandBrake },
            { nameof(ForzaData.Clutch), labelClutch },
            { nameof(ForzaData.Brake), labelBrake },
            { nameof(ForzaData.Accelerator), labelAccel },
            { nameof(ForzaData.RacePosition), labelRacePosition },
            { nameof(ForzaData.LastLapTime), labelLastLap },
            { nameof(ForzaData.LapNumber), labelLapNumber },
            { nameof(ForzaData.CurrentRaceTime), labelCurrentRaceTime },
            { nameof(ForzaData.CurrentLapTime), labelCurrentLap },
            { nameof(ForzaData.BestLapTime), labelBestLap },
            { nameof(ForzaData.TireTempRR), labelTireTempRR },
            { nameof(ForzaData.TireTempRL), labelTireTempRL },
            { nameof(ForzaData.TireTempFR), labelTireTempFR },
            { nameof(ForzaData.TireTempFL), labelTireTempFL },
            { nameof(ForzaData.Fuel), labelFuel },
            { nameof(ForzaData.Distance), labelDistanceTraveled },
            { nameof(ForzaData.Boost), labelBoost },
            { nameof(ForzaData.SpeedKPH), labelSpeedKPH },
            { nameof(ForzaData.SpeedMPH), labelSpeedMPH },
            { nameof(ForzaData.Torque), labelTorque },
            { nameof(ForzaData.Power), labelPower },
            { nameof(ForzaData.PositionZ), labelPositionZ },
            { nameof(ForzaData.PositionY), labelPositionY },
            { nameof(ForzaData.PositionX), labelPositionX },
            { nameof(ForzaData.NormalizedAiBrakeDifference), labelNormalizedAIBrakeDifference },
            { nameof(ForzaData.NormalizedDrivingLine), labelNormalizedDrivingLine },
            { nameof(ForzaData.TireWearRR), labelTireWearRR },
            { nameof(ForzaData.TireWearRL), labelTireWearRL },
            { nameof(ForzaData.TireWearFR), labelTireWearFR },
            { nameof(ForzaData.TireWearFL), labelTireWearFL },
            { nameof(ForzaData.TrackOrdinal), labelTrackOrdinal },
        };

        _uiTimer = new System.Windows.Forms.Timer();
        _uiTimer.Interval = 16; // ~60Hz
        _uiTimer.Tick += (s, e) => UpdateUI();
        _uiTimer.Start();
    }

    public void QueueData(ForzaData data)
    {
        _latestData = data;
    }

    public void SetBroadcastService(BroadcastService broadcastService)
    {
        _broadcastService = broadcastService;
    }

    private void UpdateUI()
    {
        if (_broadcastService != null)
        {
            var packet = _broadcastService.GetLatestPacket();
            if (packet != null)
            {
                var parsedData = _broadcastService.ParseForza(packet); // Renamed 'data' to 'parsedData'
                QueueData(parsedData);
            }
        }

        if (_latestData == null) return;

        var data = _latestData.Value;
        _latestData = null; // Mark as consumed

        if (_startTimestampMS == null && data.TimeStampMS > 0)
            _startTimestampMS = data.TimeStampMS;

        groupBoxSledData.SuspendLayout();
        groupBoxDashData.SuspendLayout();

        try
        {
            foreach (var kvp in _labelMap)
            {
                var prop = typeof(ForzaData).GetProperty(kvp.Key);
                if (prop != null)
                {
                    var value = prop.GetValue(data);
                    string text = value is float f ? MathF.Round(f, 2).ToString() : value?.ToString() ?? "";

                    if (kvp.Key == nameof(ForzaData.TimeStampMS) && _startTimestampMS != null)
                    {
                        var elapsedMs = data.TimeStampMS - _startTimestampMS.Value;
                        var elapsed = TimeSpan.FromMilliseconds(elapsedMs);
                        text = $"{(int)elapsed.Minutes:D2}:{elapsed.Seconds:D2}:{elapsed.Milliseconds:D2}";
                    }

                    if (kvp.Value.Text != text)
                        kvp.Value.Text = text;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UpdateUI: {ex.Message}");
        }
        finally
        {
            groupBoxSledData.ResumeLayout();
            groupBoxDashData.ResumeLayout();
        }
    }
}
