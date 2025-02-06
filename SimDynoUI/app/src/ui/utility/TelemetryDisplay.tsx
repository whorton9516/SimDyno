import React from 'react';
import { useSignalR } from '../../hooks/useSignalR';
import '../../../styles/ui/utility/_telemetry-display.scss';

export function TelemetryDisplay() {
    const { telemetry } = useSignalR();

    if (!telemetry) {
        return <div className="telemetry-display">No telemetry data available yet.</div>;
    }

    return (
        <div className="telemetry-display">
            <div>isRaceOn: {telemetry.isRaceOn.toString()}</div>
            <div>timeStampMS: {telemetry.timeStampMS}</div>
            <div>engineMaxRpm: {telemetry.engineMaxRpm}</div>
            <div>engineIdleRpm: {telemetry.engineIdleRpm}</div>
            <div>currentEngineRpm: {telemetry.currentEngineRpm}</div>
            <div>accelerationX: {telemetry.accelerationX}</div>
            <div>accelerationY: {telemetry.accelerationY}</div>
            <div>accelerationZ: {telemetry.accelerationZ}</div>
            <div>velocityX: {telemetry.velocityX}</div>
            <div>velocityY: {telemetry.velocityY}</div>
            <div>velocityZ: {telemetry.velocityZ}</div>
            <div>angularVelocityX: {telemetry.angularVelocityX}</div>
            <div>angularVelocityY: {telemetry.angularVelocityY}</div>
            <div>angularVelocityZ: {telemetry.angularVelocityZ}</div>
            <div>yaw: {telemetry.yaw}</div>
            <div>pitch: {telemetry.pitch}</div>
            <div>roll: {telemetry.roll}</div>
            <div>normalizedSuspensionTravelFL: {telemetry.normalizedSuspensionTravelFL}</div>
            <div>normalizedSuspensionTravelFR: {telemetry.normalizedSuspensionTravelFR}</div>
            <div>normalizedSuspensionTravelRL: {telemetry.normalizedSuspensionTravelRL}</div>
            <div>normalizedSuspensionTravelRR: {telemetry.normalizedSuspensionTravelRR}</div>
            <div>tireSlipRatioFL: {telemetry.tireSlipRatioFL}</div>
            <div>tireSlipRatioFR: {telemetry.tireSlipRatioFR}</div>
            <div>tireSlipRatioRL: {telemetry.tireSlipRatioRL}</div>
            <div>tireSlipRatioRR: {telemetry.tireSlipRatioRR}</div>
            <div>wheelRotationSpeedFL: {telemetry.wheelRotationSpeedFL}</div>
            <div>wheelRotationSpeedFR: {telemetry.wheelRotationSpeedFR}</div>
            <div>wheelRotationSpeedRL: {telemetry.wheelRotationSpeedRL}</div>
            <div>wheelRotationSpeedRR: {telemetry.wheelRotationSpeedRR}</div>
            <div>wheelOnRumbleStripFL: {telemetry.wheelOnRumbleStripFL}</div>
            <div>wheelOnRumbleStripFR: {telemetry.wheelOnRumbleStripFR}</div>
            <div>wheelOnRumbleStripRL: {telemetry.wheelOnRumbleStripRL}</div>
            <div>wheelOnRumbleStripRR: {telemetry.wheelOnRumbleStripRR}</div>
            <div>wheelInPuddleDepthFL: {telemetry.wheelInPuddleDepthFL}</div>
            <div>wheelInPuddleDepthFR: {telemetry.wheelInPuddleDepthFR}</div>
            <div>wheelInPuddleDepthRL: {telemetry.wheelInPuddleDepthRL}</div>
            <div>wheelInPuddleDepthRR: {telemetry.wheelInPuddleDepthRR}</div>
            <div>surfaceRumbleFL: {telemetry.surfaceRumbleFL}</div>
            <div>surfaceRumbleFR: {telemetry.surfaceRumbleFR}</div>
            <div>surfaceRumbleRL: {telemetry.surfaceRumbleRL}</div>
            <div>surfaceRumbleRR: {telemetry.surfaceRumbleRR}</div>
            <div>tireSlipAngleFL: {telemetry.tireSlipAngleFL}</div>
            <div>tireSlipAngleFR: {telemetry.tireSlipAngleFR}</div>
            <div>tireSlipAngleRL: {telemetry.tireSlipAngleRL}</div>
            <div>tireSlipAngleRR: {telemetry.tireSlipAngleRR}</div>
            <div>tireCombinedSlipFL: {telemetry.tireCombinedSlipFL}</div>
            <div>tireCombinedSlipFR: {telemetry.tireCombinedSlipFR}</div>
            <div>tireCombinedSlipRL: {telemetry.tireCombinedSlipRL}</div>
            <div>tireCombinedSlipRR: {telemetry.tireCombinedSlipRR}</div>
            <div>suspensionTravelMetersFL: {telemetry.suspensionTravelMetersFL}</div>
            <div>suspensionTravelMetersFR: {telemetry.suspensionTravelMetersFR}</div>
            <div>suspensionTravelMetersRL: {telemetry.suspensionTravelMetersRL}</div>
            <div>suspensionTravelMetersRR: {telemetry.suspensionTravelMetersRR}</div>
            <div>carOrdinal: {telemetry.carOrdinal}</div>
            <div>carClass: {telemetry.carClass}</div>
            <div>carPerformanceIndex: {telemetry.carPerformanceIndex}</div>
            <div>drivetrainType: {telemetry.drivetrainType}</div>
            <div>numCylinders: {telemetry.numCylinders}</div>
            <div>positionX: {telemetry.positionX}</div>
            <div>positionY: {telemetry.positionY}</div>
            <div>positionZ: {telemetry.positionZ}</div>
            <div>speed: {telemetry.speed}</div>
            <div>speedMPH: {telemetry.speedMPH}</div>
            <div>speedKPH: {telemetry.speedKPH}</div>
            <div>power: {telemetry.power}</div>
            <div>torque: {telemetry.torque}</div>
            <div>tireTempFL: {telemetry.tireTempFL}</div>
            <div>tireTempFR: {telemetry.tireTempFR}</div>
            <div>tireTempRL: {telemetry.tireTempRL}</div>
            <div>tireTempRR: {telemetry.tireTempRR}</div>
            <div>boost: {telemetry.boost}</div>
            <div>fuel: {telemetry.fuel}</div>
            <div>distance: {telemetry.distance}</div>
            <div>bestLapTime: {telemetry.bestLapTime}</div>
            <div>lastLapTime: {telemetry.lastLapTime}</div>
            <div>currentLapTime: {telemetry.currentLapTime}</div>
            <div>currentRaceTime: {telemetry.currentRaceTime}</div>
            <div>lapNumber: {telemetry.lapNumber}</div>
            <div>racePosition: {telemetry.racePosition}</div>
            <div>accelerator: {telemetry.accelerator}</div>
            <div>brake: {telemetry.brake}</div>
            <div>clutch: {telemetry.clutch}</div>
            <div>handbrake: {telemetry.handbrake}</div>
            <div>gear: {telemetry.gear}</div>
            <div>steer: {telemetry.steer}</div>
            <div>normalizedDrivingLine: {telemetry.normalizedDrivingLine}</div>
            <div>normalizedAIBrakeDifference: {telemetry.normalizedAIBrakeDifference}</div>
            <div>tireWearFL: {telemetry.tireWearFL}</div>
            <div>tireWearFR: {telemetry.tireWearFR}</div>
            <div>tireWearRL: {telemetry.tireWearRL}</div>
            <div>tireWearRR: {telemetry.tireWearRR}</div>
            <div>trackOrdinal: {telemetry.trackOrdinal}</div>
        </div>
    );
}