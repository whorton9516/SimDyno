const signalR = require("@microsoft/signalr");

// Create the SignalR connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/simdynohub")
    .withAutomaticReconnect()
    .build();

// DOM elements
const statusElement = document.getElementById("status");
const statusBarElement = document.getElementById("statusBar");

function updateTelemetryUI(telemetry) {
    if (!telemetry) return;

    document.getElementById("isRaceOn").textContent = `isRaceOn: ${telemetry.isRaceOn}`;
    document.getElementById("timeStampMS").textContent = `timeStampMS: ${telemetry.timeStampMS}`;
    document.getElementById("engineMaxRpm").textContent = `engineMaxRpm: ${telemetry.engineMaxRpm}`;
    document.getElementById("engineIdleRpm").textContent = `engineIdleRpm: ${telemetry.engineIdleRpm}`;
    document.getElementById("currentEngineRpm").textContent = `currentEngineRpm: ${telemetry.currentEngineRpm}`;
    document.getElementById("accelerationX").textContent = `accelerationX: ${telemetry.accelerationX}`;
    document.getElementById("accelerationY").textContent = `accelerationY: ${telemetry.accelerationY}`;
    document.getElementById("accelerationZ").textContent = `accelerationZ: ${telemetry.accelerationZ}`;
    document.getElementById("velocityX").textContent = `velocityX: ${telemetry.velocityX}`;
    document.getElementById("velocityY").textContent = `velocityY: ${telemetry.velocityY}`;
    document.getElementById("velocityZ").textContent = `velocityZ: ${telemetry.velocityZ}`;
    document.getElementById("angularVelocityX").textContent = `angularVelocityX: ${telemetry.angularVelocityX}`;
    document.getElementById("angularVelocityY").textContent = `angularVelocityY: ${telemetry.angularVelocityY}`;
    document.getElementById("angularVelocityZ").textContent = `angularVelocityZ: ${telemetry.angularVelocityZ}`;
    document.getElementById("yaw").textContent = `yaw: ${telemetry.yaw}`;
    document.getElementById("pitch").textContent = `pitch: ${telemetry.pitch}`;
    document.getElementById("roll").textContent = `roll: ${telemetry.roll}`;
    document.getElementById("normalizedSuspensionTravelFL").textContent = `normalizedSuspensionTravelFL: ${telemetry.normalizedSuspensionTravelFL}`;
    document.getElementById("normalizedSuspensionTravelFR").textContent = `normalizedSuspensionTravelFR: ${telemetry.normalizedSuspensionTravelFR}`;
    document.getElementById("normalizedSuspensionTravelRL").textContent = `normalizedSuspensionTravelRL: ${telemetry.normalizedSuspensionTravelRL}`;
    document.getElementById("normalizedSuspensionTravelRR").textContent = `normalizedSuspensionTravelRR: ${telemetry.normalizedSuspensionTravelRR}`;
    document.getElementById("tireSlipRatioFL").textContent = `tireSlipRatioFL: ${telemetry.tireSlipRatioFL}`;
    document.getElementById("tireSlipRatioFR").textContent = `tireSlipRatioFR: ${telemetry.tireSlipRatioFR}`;
    document.getElementById("tireSlipRatioRL").textContent = `tireSlipRatioRL: ${telemetry.tireSlipRatioRL}`;
    document.getElementById("tireSlipRatioRR").textContent = `tireSlipRatioRR: ${telemetry.tireSlipRatioRR}`;
    document.getElementById("wheelRotationSpeedFL").textContent = `wheelRotationSpeedFL: ${telemetry.wheelRotationSpeedFL}`;
    document.getElementById("wheelRotationSpeedFR").textContent = `wheelRotationSpeedFR: ${telemetry.wheelRotationSpeedFR}`;
    document.getElementById("wheelRotationSpeedRL").textContent = `wheelRotationSpeedRL: ${telemetry.wheelRotationSpeedRL}`;
    document.getElementById("wheelRotationSpeedRR").textContent = `wheelRotationSpeedRR: ${telemetry.wheelRotationSpeedRR}`;
    document.getElementById("wheelOnRumbleStripFL").textContent = `wheelOnRumbleStripFL: ${telemetry.wheelOnRumbleStripFL}`;
    document.getElementById("wheelOnRumbleStripFR").textContent = `wheelOnRumbleStripFR: ${telemetry.wheelOnRumbleStripFR}`;
    document.getElementById("wheelOnRumbleStripRL").textContent = `wheelOnRumbleStripRL: ${telemetry.wheelOnRumbleStripRL}`;
    document.getElementById("wheelOnRumbleStripRR").textContent = `wheelOnRumbleStripRR: ${telemetry.wheelOnRumbleStripRR}`;
    document.getElementById("wheelInPuddleDepthFL").textContent = `wheelInPuddleDepthFL: ${telemetry.wheelInPuddleDepthFL}`;
    document.getElementById("wheelInPuddleDepthFR").textContent = `wheelInPuddleDepthFR: ${telemetry.wheelInPuddleDepthFR}`;
    document.getElementById("wheelInPuddleDepthRL").textContent = `wheelInPuddleDepthRL: ${telemetry.wheelInPuddleDepthRL}`;
    document.getElementById("wheelInPuddleDepthRR").textContent = `wheelInPuddleDepthRR: ${telemetry.wheelInPuddleDepthRR}`;
    document.getElementById("surfaceRumbleFL").textContent = `surfaceRumbleFL: ${telemetry.surfaceRumbleFL}`;
    document.getElementById("surfaceRumbleFR").textContent = `surfaceRumbleFR: ${telemetry.surfaceRumbleFR}`;
    document.getElementById("surfaceRumbleRL").textContent = `surfaceRumbleRL: ${telemetry.surfaceRumbleRL}`;
    document.getElementById("surfaceRumbleRR").textContent = `surfaceRumbleRR: ${telemetry.surfaceRumbleRR}`;
    document.getElementById("tireSlipAngleFL").textContent = `tireSlipAngleFL: ${telemetry.tireSlipAngleFL}`;
    document.getElementById("tireSlipAngleFR").textContent = `tireSlipAngleFR: ${telemetry.tireSlipAngleFR}`;
    document.getElementById("tireSlipAngleRR").textContent = `tireSlipAngleRR: ${telemetry.tireSlipAngleRR}`;
    document.getElementById("tireSlipAngleRL").textContent = `tireSlipAngleRL: ${telemetry.tireSlipAngleRL}`;
    document.getElementById("tireCombinedSlipFL").textContent = `tireCombinedSlipFL: ${telemetry.tireCombinedSlipFL}`;
    document.getElementById("tireCombinedSlipFR").textContent = `tireCombinedSlipFR: ${telemetry.tireCombinedSlipFR}`;
    document.getElementById("tireCombinedSlipRL").textContent = `tireCombinedSlipRL: ${telemetry.tireCombinedSlipRL}`;
    document.getElementById("tireCombinedSlipRR").textContent = `tireCombinedSlipRR: ${telemetry.tireCombinedSlipRR}`;
    document.getElementById("suspensionTravelMetersFL").textContent = `suspensionTravelMetersFL: ${telemetry.suspensionTravelMetersFL}`;
    document.getElementById("suspensionTravelMetersFR").textContent = `suspensionTravelMetersFR: ${telemetry.suspensionTravelMetersFR}`;
    document.getElementById("suspensionTravelMetersRL").textContent = `suspensionTravelMetersRL: ${telemetry.suspensionTravelMetersRL}`;
    document.getElementById("suspensionTravelMetersRR").textContent = `suspensionTravelMetersRR: ${telemetry.suspensionTravelMetersRR}`;
    document.getElementById("carOrdinal").textContent = `carOrdinal: ${telemetry.carOrdinal}`;
    document.getElementById("carClass").textContent = `carClass: ${telemetry.carClass}`;
    document.getElementById("carPerformanceIndex").textContent = `carPerformanceIndex: ${telemetry.carPerformanceIndex}`;
    document.getElementById("drivetrainType").textContent = `drivetrainType: ${telemetry.drivetrainType}`;
    document.getElementById("numCylinders").textContent = `numCylinders: ${telemetry.numCylinders}`;
    document.getElementById("positionX").textContent = `positionX: ${telemetry.positionX}`;
    document.getElementById("positionY").textContent = `positionY: ${telemetry.positionY}`;
    document.getElementById("positionZ").textContent = `positionZ: ${telemetry.positionZ}`;
    document.getElementById("speed").textContent = `speed: ${telemetry.speed}`;
    document.getElementById("speedMPH").textContent = `speedMPH: ${telemetry.speedMPH}`;
    document.getElementById("speedKPH").textContent = `speedKPH: ${telemetry.speedKPH}`;
    document.getElementById("power").textContent = `power: ${telemetry.power}`;
    document.getElementById("torque").textContent = `torque: ${telemetry.torque}`;
    document.getElementById("tireTempFL").textContent = `tireTempFL: ${telemetry.tireTempFL}`;
    document.getElementById("tireTempFR").textContent = `tireTempFR: ${telemetry.tireTempFR}`;
    document.getElementById("tireTempRL").textContent = `tireTempRL: ${telemetry.tireTempRL}`;
    document.getElementById("tireTempRR").textContent = `tireTempRR: ${telemetry.tireTempRR}`;
    document.getElementById("boost").textContent = `boost: ${telemetry.boost}`;
    document.getElementById("fuel").textContent = `fuel: ${telemetry.fuel}`;
    document.getElementById("distance").textContent = `distance: ${telemetry.distance}`;
    document.getElementById("bestLapTime").textContent = `bestLapTime: ${telemetry.bestLapTime}`;
    document.getElementById("lastLapTime").textContent = `lastLapTime: ${telemetry.lastLapTime}`;
    document.getElementById("currentLapTime").textContent = `currentLapTime: ${telemetry.currentLapTime}`;
    document.getElementById("currentRaceTime").textContent = `currentRaceTime: ${telemetry.currentRaceTime}`;
    document.getElementById("lapNumber").textContent = `lapNumber: ${telemetry.lapNumber}`;
    document.getElementById("racePosition").textContent = `racePosition: ${telemetry.racePosition}`;
    document.getElementById("accelerator").textContent = `accelerator: ${telemetry.accelerator}`;
    document.getElementById("brake").textContent = `brake: ${telemetry.brake}`;
    document.getElementById("clutch").textContent = `clutch: ${telemetry.clutch}`;
    document.getElementById("handbrake").textContent = `handbrake: ${telemetry.handbrake}`;
    document.getElementById("gear").textContent = `gear: ${telemetry.gear}`;
    document.getElementById("steer").textContent = `steer: ${telemetry.steer}`;
    document.getElementById("normalizedDrivingLine").textContent = `normalizedDrivingLine: ${telemetry.normalizedDrivingLine}`;
    document.getElementById("normalizedAIBrakeDifference").textContent = `normalizedAIBrakeDifference: ${telemetry.normalizedAIBrakeDifference}`;
    document.getElementById("tireWearFL").textContent = `tireWearFL: ${telemetry.tireWearFL}`;
    document.getElementById("tireWearFR").textContent = `tireWearFR: ${telemetry.tireWearFR}`;
    document.getElementById("tireWearRL").textContent = `tireWearRL: ${telemetry.tireWearRL}`;
    document.getElementById("tireWearRR").textContent = `tireWearRR: ${telemetry.tireWearRR}`;
    document.getElementById("trackOrdinal").textContent = `trackOrdinal: ${telemetry.trackOrdinal}`;
}

// Start the connection
async function startConnection() {
    try {
        await connection.start();
        console.log("Connected to server");
        statusElement.textContent = "Connected to server";
        statusBarElement.textContent = "Connected to server.";
    } catch (err) {
        console.error("Connection failed: ", err);
        statusElement.textContent = "Connection failed. Retrying...";
        statusBarElement.textContent = "Connection failed. Retrying...";
        setTimeout(startConnection, 5000); // Retry connection every 5 seconds
    }
}

// Handle `ReceiveData` telemetry updates
connection.on("ReceiveData", (data) => {
    console.log("Received telemetry data:", data);
    updateTelemetryUI(data);
});

// Handle `BroadcastMessage` for status updates
connection.on("BroadcastMessage", (message) => {
    console.log(`BroadcastMessage received: ${message}`);
    statusBarElement.textContent = message; // Update status bar directly
});

// Start SignalR connection
startConnection();