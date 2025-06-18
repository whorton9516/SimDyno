
export type Telemetry = {
    isRaceOn: number;
    timeStampMS: number;
    engineMaxRpm: number;
    engineIdleRpm: number;
    currentEngineRpm: number;
    accelerationX: number;
    accelerationY: number;
    accelerationZ: number;
    velocityX: number;
    velocityY: number;
    velocityZ: number;
    angularVelocityX: number;
    angularVelocityY: number;
    angularVelocityZ: number;
    yaw: number;
    pitch: number;
    roll: number;
    normalizedSuspensionTravelFL: number;
    normalizedSuspensionTravelFR: number;
    normalizedSuspensionTravelRL: number;
    normalizedSuspensionTravelRR: number;
    tireSlipRatioFL: number;
    tireSlipRatioFR: number;
    tireSlipRatioRL: number;
    tireSlipRatioRR: number;
    wheelRotationSpeedFL: number;
    wheelRotationSpeedFR: number;
    wheelRotationSpeedRL: number;
    wheelRotationSpeedRR: number;
    wheelOnRumbleStripFL: number;
    wheelOnRumbleStripFR: number;
    wheelOnRumbleStripRL: number;
    wheelOnRumbleStripRR: number;
    wheelInPuddleDepthFL: number;
    wheelInPuddleDepthFR: number;
    wheelInPuddleDepthRL: number;
    wheelInPuddleDepthRR: number;
    surfaceRumbleFL: number;
    surfaceRumbleFR: number;
    surfaceRumbleRL: number;
    surfaceRumbleRR: number;
    tireSlipAngleFL: number;
    tireSlipAngleFR: number;
    tireSlipAngleRL: number;
    tireSlipAngleRR: number;
    tireCombinedSlipFL: number;
    tireCombinedSlipFR: number;
    tireCombinedSlipRL: number;
    tireCombinedSlipRR: number;
    suspensionTravelMetersFL: number;
    suspensionTravelMetersFR: number;
    suspensionTravelMetersRL: number;
    suspensionTravelMetersRR: number;
    carOrdinal: number;
    carClass: number;
    carPerformanceIndex: number;
    drivetrainType: number;
    numCylinders: number;
    positionX: number;
    positionY: number;
    positionZ: number;
    speed: number;
    speedMPH: number;
    speedKPH: number;
    power: number;
    torque: number;
    tireTempFL: number;
    tireTempFR: number;
    tireTempRL: number;
    tireTempRR: number;
    boost: number;
    fuel: number;
    distance: number;
    bestLapTime: number;
    lastLapTime: number;
    currentLapTime: number;
    currentRaceTime: number;
    lapNumber: number;
    racePosition: number;
    accelerator: number;
    brake: number;
    clutch: number;
    handbrake: number;
    gear: number;
    steer: number;
    normalizedDrivingLine: number;
    normalizedAIBrakeDifference: number;
    tireWearFL: number;
    tireWearFR: number;
    tireWearRL: number;
    tireWearRR: number;
    trackOrdinal: number;
}

export const defaultTelemetry = Object.freeze({
  isRaceOn: 0,
  timeStampMS: 0,
  engineMaxRpm: 0,
  engineIdleRpm: 0,
  currentEngineRpm: 0,
  accelerationX: 0,
  accelerationY: 0,
  accelerationZ: 0,
  velocityX: 0,
  velocityY: 0,
  velocityZ: 0,
  angularVelocityX: 0,
  angularVelocityY: 0,
  angularVelocityZ: 0,
  yaw: 0,
  pitch: 0,
  roll: 0,
  normalizedSuspensionTravelFL: 0,
  normalizedSuspensionTravelFR: 0,
  normalizedSuspensionTravelRL: 0,
  normalizedSuspensionTravelRR: 0,
  tireSlipRatioFL: 0,
  tireSlipRatioFR: 0,
  tireSlipRatioRL: 0,
  tireSlipRatioRR: 0,
  wheelRotationSpeedFL: 0,
  wheelRotationSpeedFR: 0,
  wheelRotationSpeedRL: 0,
  wheelRotationSpeedRR: 0,
  wheelOnRumbleStripFL: 0,
  wheelOnRumbleStripFR: 0,
  wheelOnRumbleStripRL: 0,
  wheelOnRumbleStripRR: 0,
  wheelInPuddleDepthFL: 0,
  wheelInPuddleDepthFR: 0,
  wheelInPuddleDepthRL: 0,
  wheelInPuddleDepthRR: 0,
  surfaceRumbleFL: 0,
  surfaceRumbleFR: 0,
  surfaceRumbleRL: 0,
  surfaceRumbleRR: 0,
  tireSlipAngleFL: 0,
  tireSlipAngleFR: 0,
  tireSlipAngleRL: 0,
  tireSlipAngleRR: 0,
  tireCombinedSlipFL: 0,
  tireCombinedSlipFR: 0,
  tireCombinedSlipRL: 0,
  tireCombinedSlipRR: 0,
  suspensionTravelMetersFL: 0,
  suspensionTravelMetersFR: 0,
  suspensionTravelMetersRL: 0,
  suspensionTravelMetersRR: 0,
  carOrdinal: 0,
  carClass: 0,
  carPerformanceIndex: 0,
  drivetrainType: 0,
  numCylinders: 0,
  positionX: 0,
  positionY: 0,
  positionZ: 0,
  speed: 0,
  speedMPH: 0,
  speedKPH: 0,
  power: 0,
  torque: 0,
  tireTempFL: 0,
  tireTempFR: 0,
  tireTempRL: 0,
  tireTempRR: 0,
  boost: 0,
  fuel: 0,
  distance: 0,
  bestLapTime: 0,
  lastLapTime: 0,
  currentLapTime: 0,
  currentRaceTime: 0,
  lapNumber: 0,
  racePosition: 0,
  accelerator: 0,
  brake: 0,
  clutch: 0,
  handbrake: 0,
  gear: 0,
  steer: 0,
  normalizedDrivingLine: 0,
  normalizedAIBrakeDifference: 0,
  tireWearFL: 0,
  tireWearFR: 0,
  tireWearRL: 0,
  tireWearRR: 0,
  trackOrdinal: 0,
} satisfies Telemetry);

/**
 * Validates and processes raw SignalR data into a telemetry update.
 * Returns a Partial<Telemetry> with valid fields, or a full Telemetry if complete is true.
 * @param data The raw data received from SignalR.
 * @param complete If true, returns a full Telemetry object with defaults for missing fields.
 * @returns A Partial<Telemetry> or full Telemetry object.
 */
export function updateTelemetry(data: unknown, complete: boolean = false): Partial<Telemetry> | Telemetry {
  // If data is not an object, return the default telemetry or null
  if (!data || typeof data !== 'object') {
    return complete ? { ...defaultTelemetry } : {};
  }

  const partial: Partial<Telemetry> = {};
  // Loop through the keys in the given dictionary
  for (const key in data) {
    // Validate that the prop exists in defaultTelemetry
    if (Object.prototype.hasOwnProperty.call(data, key) && key in defaultTelemetry) {
      // Record the value of the key
      const value = (data as Record<string, unknown>)[key];
      // Confirm that the value is a number
      if (typeof value === 'number') {
        // Append the new data to the partial object
        partial[key as keyof Telemetry] = value;
      }
      // TODO: handle when a value is not number.
    }
  }

  // Spread defaultTelemetry and override with validated props
  if (complete) {
    return {
      ...defaultTelemetry,
      ...partial,
    };
  }

  return partial;
}