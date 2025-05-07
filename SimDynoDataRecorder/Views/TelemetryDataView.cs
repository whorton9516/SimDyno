using SimDynoServer.Models;

namespace SimDynoDataRecorder.Views;
public partial class TelemetryDataView : Form
{
    public TelemetryDataView()
    {
        InitializeComponent();
    }

    public void UpdateData(ForzaData data)
    {
        try
        {
            labelDrivetrainType.Text = data.DrivetrainType.ToString() ?? "--";
            labelNumCylinders.Text = data.NumCylinders.ToString() ?? "--";
            labelClassAndIndex.Text = data.CarClass.ToString() ?? "--";
            labelCarOrdinal.Text = data.CarOrdinal.ToString() ?? "--";
            labelSuspensionTravelMetersRR.Text = data.SuspensionTravelMetersRR.ToString() ?? "--";
            labelSuspensionTravelMetersRL.Text = data.SuspensionTravelMetersRL.ToString() ?? "--";
            labelSuspensionTravelMetersFR.Text = data.SuspensionTravelMetersFR.ToString() ?? "--";
            labelSuspensionTravelMetersFL.Text = data.SuspensionTravelMetersFL.ToString() ?? "--";
            labelTireCombinedSlipRR.Text = data.TireCombinedSlipRR.ToString() ?? "--";
            labelTireCombinedSlipRL.Text = data.TireCombinedSlipRL.ToString() ?? "--";
            labelTireCombinedSlipFR.Text = data.TireCombinedSlipFR.ToString() ?? "--";
            labelTireCombinedSlipFL.Text = data.TireCombinedSlipFL.ToString() ?? "--";
            labelTireSlipAngleRR.Text = data.TireSlipAngleRR.ToString() ?? "--";
            labelTireSlipAngleRL.Text = data.TireSlipAngleRL.ToString() ?? "--";
            labelTireSlipAngleFR.Text = data.TireSlipAngleFR.ToString() ?? "--";
            labelTireSlipAngleFL.Text = data.TireSlipAngleFL.ToString() ?? "--";
            labelSurfaceRumbleRR.Text = data.SurfaceRumbleRR.ToString() ?? "--";
            labelSurfaceRumbleRL.Text = data.SurfaceRumbleRL.ToString() ?? "--";
            labelSurfaceRumbleFR.Text = data.SurfaceRumbleFR.ToString() ?? "--";
            labelSurfaceRumbleFL.Text = data.SurfaceRumbleFL.ToString() ?? "--";
            labelWheelInPuddleDepthRR.Text = data.WheelInPuddleDepthRR.ToString() ?? "--";
            labelWheelInPuddleDepthRL.Text = data.WheelInPuddleDepthRL.ToString() ?? "--";
            labelWheelInPuddleDepthFR.Text = data.WheelInPuddleDepthFR.ToString() ?? "--";
            labelWheelInPuddleDepthFL.Text = data.WheelInPuddleDepthFL.ToString() ?? "--";
            labelWheelOnRumbleStripRR.Text = data.WheelOnRumbleStripRR.ToString() ?? "--";
            labelWheelOnRumbleStripRL.Text = data.WheelOnRumbleStripRL.ToString() ?? "--";
            labelWheelOnRumbleStripFR.Text = data.WheelOnRumbleStripFR.ToString() ?? "--";
            labelWheelOnRumbleStripFL.Text = data.WheelOnRumbleStripFL.ToString() ?? "--";
            labelWheelRotationSpeedRR.Text = data.WheelRotationSpeedRR.ToString() ?? "--";
            labelWheelRotationSpeedRL.Text = data.WheelRotationSpeedRL.ToString() ?? "--";
            labelWheelRotationSpeedFR.Text = data.WheelRotationSpeedFR.ToString() ?? "--";
            labelWheelRotationSpeedFL.Text = data.WheelRotationSpeedFL.ToString() ?? "--";
            labelTireSlipRatioRR.Text = data.TireSlipRatioRR.ToString() ?? "--";
            labelTireSlipRatioRL.Text = data.TireSlipRatioRL.ToString() ?? "--";
            labelTireSlipRatioFR.Text = data.TireSlipRatioFR.ToString() ?? "--";
            labelTireSlipRatioFL.Text = data.TireSlipRatioFL.ToString() ?? "--";
            labelRoll.Text = data.Roll.ToString() ?? "--";
            labelPitch.Text = data.Pitch.ToString() ?? "--";
            labelYaw.Text = data.Yaw.ToString() ?? "--";
            labelAngularVelocityZ.Text = data.AngularVelocityZ.ToString() ?? "--";
            labelAngularVelocityY.Text = data.AngularVelocityY.ToString() ?? "--";
            labelAngularVelocityX.Text = data.AngularVelocityX.ToString() ?? "--";
            labelVelocityZ.Text = data.VelocityZ.ToString() ?? "--";
            labelVelocityY.Text = data.VelocityY.ToString() ?? "--";
            labelVelocityX.Text = data.VelocityX.ToString() ?? "--";
            labelNormalizedSuspensionTravelRR.Text = data.NormalizedSuspensionTravelRR.ToString() ?? "--";
            labelNormalizedSuspensionTravelRL.Text = data.NormalizedSuspensionTravelRL.ToString() ?? "--";
            labelNormalizedSuspensionTravelFR.Text = data.NormalizedSuspensionTravelFR.ToString() ?? "--";
            labelNormalizedSuspensionTravelFL.Text = data.NormalizedSuspensionTravelFL.ToString() ?? "--";
            labelAccelerationZ.Text = data.AccelerationZ.ToString() ?? "--";
            labelAccelerationY.Text = data.AccelerationY.ToString() ?? "--";
            labelAccelerationX.Text = data.AccelerationX.ToString() ?? "--";
            labelCurrentEngineRpm.Text = data.CurrentEngineRpm.ToString() ?? "--";
            labelEngineIdleRpm.Text = data.EngineIdleRpm.ToString() ?? "--";
            labelEngineMaxRpm.Text = data.EngineMaxRpm.ToString() ?? "--";
            labelIsRaceOn.Text = data.IsRaceOn.ToString() ?? "--";
            labelTimeStampMS.Text = data.TimestampMS.ToString() ?? "--";
            labelSteer.Text = data.Steer.ToString() ?? "--";
            labelGear.Text = data.Gear.ToString() ?? "--";
            labelHandBrake.Text = data.Handbrake.ToString() ?? "--";
            labelClutch.Text = data.Clutch.ToString() ?? "--";
            labelBrake.Text = data.Brake.ToString() ?? "--";
            labelAccel.Text = data.Accelerator.ToString() ?? "--";
            labelRacePosition.Text = data.RacePosition.ToString() ?? "--";
            labelLastLap.Text = data.LastLapTime.ToString() ?? "--";
            labelLapNumber.Text = data.LapNumber.ToString() ?? "--";
            labelCurrentRaceTime.Text = data.CurrentRaceTime.ToString() ?? "--";
            labelCurrentLap.Text = data.CurrentLapTime.ToString() ?? "--";
            labelBestLap.Text = data.BestLapTime.ToString() ?? "--";
            labelTireTempRR.Text = data.TireTempRR.ToString() ?? "--";
            labelTireTempRL.Text = data.TireTempRL.ToString() ?? "--";
            labelTireTempFR.Text = data.TireTempFR.ToString() ?? "--";
            labelTireTempFL.Text = data.TireTempFL.ToString() ?? "--";
            labelFuel.Text = data.Fuel.ToString() ?? "--";
            labelDistanceTraveled.Text = data.Distance.ToString() ?? "--";
            labelBoost.Text = data.Boost.ToString() ?? "--";
            labelSpeedKPH.Text = data.SpeedKPH.ToString() ?? "--";
            labelSpeedMPH.Text = data.SpeedMPH.ToString() ?? "--";
            labelTorque.Text = data.Torque.ToString() ?? "--";
            labelPower.Text = data.Power.ToString() ?? "--";
            labelPositionZ.Text = data.PositionZ.ToString() ?? "--";
            labelPositionY.Text = data.PositionY.ToString() ?? "--";
            labelPositionX.Text = data.PositionX.ToString() ?? "--";
            labelNormalizedAIBrakeDifference.Text = data.NormalizedAiBrakeDifference.ToString() ?? "--";
            labelNormalizedDrivingLine.Text = data.NormalizedDrivingLine.ToString() ?? "--";
            labelTireWearRR.Text = data.TireWearRR.ToString() ?? "--";
            labelTireWearRL.Text = data.TireWearRL.ToString() ?? "--";
            labelTireWearFR.Text = data.TireWearFR.ToString() ?? "--";
            labelTireWearFL.Text = data.TireWearFL.ToString() ?? "--";
            labelTrackOrdinal.Text = data.TrackOrdinal.ToString() ?? "--";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"UpdateData: {ex.Message}");
        }
    }

    public void UpdateGameConnected(bool isConnected)
    {
        labelGameConnected.Text = isConnected.ToString();
    }
}
