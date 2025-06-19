using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimDynoServer.Models;

public class ForzaData
{

    private bool  _isRaceOn;
    private uint  _timeStampMS;
    private float _engineMaxRpm;
    private float _engineIdleRpm;
    private float _currentEngineRpm;
    private float _accelerationX;
    private float _accelerationY;
    private float _accelerationZ;
    private float _velocityX;
    private float _velocityY;
    private float _velocityZ;
    private float _angularVelocityX;
    private float _angularVelocityY;
    private float _angularVelocityZ;
    private float _yaw;
    private float _pitch;
    private float _roll;
    private float _normalizedSuspensionTravelFL;
    private float _normalizedSuspensionTravelFR;
    private float _normalizedSuspensionTravelRL;
    private float _normalizedSuspensionTravelRR;
    private float _tireSlipRatioFL;
    private float _tireSlipRatioFR;
    private float _tireSlipRatioRL;
    private float _tireSlipRatioRR;
    private float _wheelRotationSpeedFL;
    private float _wheelRotationSpeedFR;
    private float _wheelRotationSpeedRL;
    private float _wheelRotationSpeedRR;
    private float _wheelOnRumbleStripFL;
    private float _wheelOnRumbleStripFR;
    private float _wheelOnRumbleStripRL;
    private float _wheelOnRumbleStripRR;
    private float _wheelInPuddleDepthFL;
    private float _wheelInPuddleDepthFR;
    private float _wheelInPuddleDepthRL;
    private float _wheelInPuddleDepthRR;
    private float _surfaceRumbleFL;
    private float _surfaceRumbleFR;
    private float _surfaceRumbleRL;
    private float _surfaceRumbleRR;
    private float _tireSlipAngleFL;
    private float _tireSlipAngleFR;
    private float _tireSlipAngleRL;
    private float _tireSlipAngleRR;
    private float _tireCombinedSlipFL;
    private float _tireCombinedSlipFR;
    private float _tireCombinedSlipRL;
    private float _tireCombinedSlipRR;
    private float _suspensionTravelMetersFL;
    private float _suspensionTravelMetersFR;
    private float _suspensionTravelMetersRL;
    private float _suspensionTravelMetersRR;
    private uint  _carOrdinal;
    private uint  _carClass;
    private uint  _carPerformanceIndex;
    private uint  _drivetrainType;
    private uint  _numCylinders;
    private float _positionX;
    private float _positionY;
    private float _positionZ;
    private float _speed;
    private float _speedMPH;
    private float _speedKPH;
    private float _power;
    private float _torque;
    private float _tireTempFL;
    private float _tireTempFR;
    private float _tireTempRL;
    private float _tireTempRR;
    private float _boost;
    private float _fuel;
    private float _distance;
    private float _bestLapTime;
    private float _lastLapTime;
    private float _currentLapTime;
    private float _currentRaceTime;
    private uint  _lapNumber;
    private uint  _racePosition;
    private uint  _accelerator;
    private uint  _brake;
    private uint  _clutch;
    private uint  _handbrake;
    private uint  _gear;
    private int   _steer;
    private uint  _normalizedDrivingLine;
    private uint  _normalizedAIBrakeDifference;
    private float _tireWearFL;
    private float _tireWearFR;
    private float _tireWearRL;
    private float _tireWearRR;
    private int   _trackOrdinal;

    /// <summary>
    /// 1 when race is on, 0 when in menus or the race is stopped
    /// </summary>
    public bool IsRaceOn
    {
        get => _isRaceOn;
        set
        {
            if (_isRaceOn != value)
            {
                _isRaceOn = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Current session timer. Will eventually overflow to 0.
    /// </summary>
    public uint TimeStampMS
    {
        get => _timeStampMS;
        set
        {
            if (_timeStampMS != value)
            {
                _timeStampMS = value;
                OnPropertyChanged();
            }
        }
    }

    public float EngineMaxRpm
    {
        get => _engineMaxRpm;
        set
        {
            if (_engineMaxRpm != value)
            {
                _engineMaxRpm = value;
                OnPropertyChanged();
            }
        }
    }

    public float EngineIdleRpm
    {
        get => _engineIdleRpm;
        set
        {
            if (_engineIdleRpm != value)
            {
                _engineIdleRpm = value;
                OnPropertyChanged();
            }
        }
    }

    public float CurrentEngineRpm
    {
        get => _currentEngineRpm;
        set
        {
            if (_currentEngineRpm != value)
            {
                _currentEngineRpm = value;
                OnPropertyChanged();
            }
        }
    }

    // In the car's local space; X = right, Y = up, Z = forward
    public float AccelerationX
    {
        get => _accelerationX;
        set
        {
            if (_accelerationX != value)
            {
                _accelerationX = value;
                OnPropertyChanged();
            }
        }
    }

    public float AccelerationY
    {
        get => _accelerationY;
        set
        {
            if (_accelerationY != value)
            {
                _accelerationY = value;
                OnPropertyChanged();
            }
        }
    }

    public float AccelerationZ
    {
        get => _accelerationZ;
        set
        {
            if (_accelerationZ != value)
            {
                _accelerationZ = value;
                OnPropertyChanged();
            }
        }
    }

    // In the car's local space; X = right, Y = up, Z = forward
    public float VelocityX
    {
        get => _velocityX;
        set
        {
            if (_velocityX != value)
            {
                _velocityX = value;
                OnPropertyChanged();
            }
        }
    }

    public float VelocityY
    {
        get => _velocityY;
        set
        {
            if (_velocityY != value)
            {
                _velocityY = value;
                OnPropertyChanged();
            }
        }
    }

    public float VelocityZ
    {
        get => _velocityZ;
        set
        {
            if (_velocityZ != value)
            {
                _velocityZ = value;
                OnPropertyChanged();
            }
        }
    }

    // In the car's local space; X = pitch, Y = yaw, Z = roll
    public float AngularVelocityX
    {
        get => _angularVelocityX;
        set
        {
            if (_angularVelocityX != value)
            {
                _angularVelocityX = value;
                OnPropertyChanged();
            }
        }
    }

    public float AngularVelocityY
    {
        get => _angularVelocityY;
        set
        {
            if (_angularVelocityY != value)
            {
                _angularVelocityY = value;
                OnPropertyChanged();
            }
        }
    }

    public float AngularVelocityZ
    {
        get => _angularVelocityZ;
        set
        {
            if (_angularVelocityZ != value)
            {
                _angularVelocityZ = value;
                OnPropertyChanged();
            }
        }
    }

    public float Yaw
    {
        get => _yaw;
        set
        {
            if (_yaw != value)
            {
                _yaw = value;
                OnPropertyChanged();
            }
        }
    }

    public float Pitch
    {
        get => _pitch;
        set
        {
            if (_pitch != value)
            {
                _pitch = value;
                OnPropertyChanged();
            }
        }
    }

    public float Roll
    {
        get => _roll;
        set
        {
            if (_roll != value)
            {
                _roll = value;
                OnPropertyChanged();
            }
        }
    }

    // Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
    public float NormalizedSuspensionTravelFL
    {
        get => _normalizedSuspensionTravelFL;
        set
        {
            if (_normalizedSuspensionTravelFL != value)
            {
                _normalizedSuspensionTravelFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float NormalizedSuspensionTravelFR
    {
        get => _normalizedSuspensionTravelFR;
        set
        {
            if (_normalizedSuspensionTravelFR != value)
            {
                _normalizedSuspensionTravelFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float NormalizedSuspensionTravelRL
    {
        get => _normalizedSuspensionTravelRL;
        set
        {
            if (_normalizedSuspensionTravelRL != value)
            {
                _normalizedSuspensionTravelRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float NormalizedSuspensionTravelRR
    {
        get => _normalizedSuspensionTravelRR;
        set
        {
            if (_normalizedSuspensionTravelRR != value)
            {
                _normalizedSuspensionTravelRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip.
    public float TireSlipRatioFL
    {
        get => _tireSlipRatioFL;
        set
        {
            if (_tireSlipRatioFL != value)
            {
                _tireSlipRatioFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireSlipRatioFR
    {
        get => _tireSlipRatioFR;
        set
        {
            if (_tireSlipRatioFR != value)
            {
                _tireSlipRatioFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireSlipRatioRL
    {
        get => _tireSlipRatioRL;
        set
        {
            if (_tireSlipRatioRL != value)
            {
                _tireSlipRatioRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireSlipRatioRR
    {
        get => _tireSlipRatioRR;
        set
        {
            if (_tireSlipRatioRR != value)
            {
                _tireSlipRatioRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Wheels rotation speed radians/sec.
    public float WheelRotationSpeedFL
    {
        get => _wheelRotationSpeedFL;
        set
        {
            if (_wheelRotationSpeedFL != value)
            {
                _wheelRotationSpeedFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelRotationSpeedFR
    {
        get => _wheelRotationSpeedFR;
        set
        {
            if (_wheelRotationSpeedFR != value)
            {
                _wheelRotationSpeedFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelRotationSpeedRL
    {
        get => _wheelRotationSpeedRL;
        set
        {
            if (_wheelRotationSpeedRL != value)
            {
                _wheelRotationSpeedRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelRotationSpeedRR
    {
        get => _wheelRotationSpeedRR;
        set
        {
            if (_wheelRotationSpeedRR != value)
            {
                _wheelRotationSpeedRR = value;
                OnPropertyChanged();
            }
        }
    }

    // = 1 when wheel is on rumble strip, = 0 when off.
    public float WheelOnRumbleStripFL
    {
        get => _wheelOnRumbleStripFL;
        set
        {
            if (_wheelOnRumbleStripFL != value)
            {
                _wheelOnRumbleStripFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelOnRumbleStripFR
    {
        get => _wheelOnRumbleStripFR;
        set
        {
            if (_wheelOnRumbleStripFR != value)
            {
                _wheelOnRumbleStripFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelOnRumbleStripRL
    {
        get => _wheelOnRumbleStripRL;
        set
        {
            if (_wheelOnRumbleStripRL != value)
            {
                _wheelOnRumbleStripRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelOnRumbleStripRR
    {
        get => _wheelOnRumbleStripRR;
        set
        {
            if (_wheelOnRumbleStripRR != value)
            {
                _wheelOnRumbleStripRR = value;
                OnPropertyChanged();
            }
        }
    }

    // = from 0 to 1, where 1 is the deepest puddle
    public float WheelInPuddleDepthFL
    {
        get => _wheelInPuddleDepthFL;
        set
        {
            if (_wheelInPuddleDepthFL != value)
            {
                _wheelInPuddleDepthFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelInPuddleDepthFR
    {
        get => _wheelInPuddleDepthFR;
        set
        {
            if (_wheelInPuddleDepthFR != value)
            {
                _wheelInPuddleDepthFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelInPuddleDepthRL
    {
        get => _wheelInPuddleDepthRL;
        set
        {
            if (_wheelInPuddleDepthRL != value)
            {
                _wheelInPuddleDepthRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float WheelInPuddleDepthRR
    {
        get => _wheelInPuddleDepthRR;
        set
        {
            if (_wheelInPuddleDepthRR != value)
            {
                _wheelInPuddleDepthRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Non-dimensional surface rumble values passed to controller force feedback
    public float SurfaceRumbleFL
    {
        get => _surfaceRumbleFL;
        set
        {
            if (_surfaceRumbleFL != value)
            {
                _surfaceRumbleFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float SurfaceRumbleFR
    {
        get => _surfaceRumbleFR;
        set
        {
            if (_surfaceRumbleFR != value)
            {
                _surfaceRumbleFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float SurfaceRumbleRL
    {
        get => _surfaceRumbleRL;
        set
        {
            if (_surfaceRumbleRL != value)
            {
                _surfaceRumbleRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float SurfaceRumbleRR
    {
        get => _surfaceRumbleRR;
        set
        {
            if (_surfaceRumbleRR != value)
            {
                _surfaceRumbleRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip.
    public float TireSlipAngleFL
    {
        get => _tireSlipAngleFL;
        set
        {
            if (_tireSlipAngleFL != value)
            {
                _tireSlipAngleFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireSlipAngleFR
    {
        get => _tireSlipAngleFR;
        set
        {
            if (_tireSlipAngleFR != value)
            {
                _tireSlipAngleFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireSlipAngleRL
    {
        get => _tireSlipAngleRL;
        set
        {
            if (_tireSlipAngleRL != value)
            {
                _tireSlipAngleRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireSlipAngleRR
    {
        get => _tireSlipAngleRR;
        set
        {
            if (_tireSlipAngleRR != value)
            {
                _tireSlipAngleRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip.
    public float TireCombinedSlipFL
    {
        get => _tireCombinedSlipFL;
        set
        {
            if (_tireCombinedSlipFL != value)
            {
                _tireCombinedSlipFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireCombinedSlipFR
    {
        get => _tireCombinedSlipFR;
        set
        {
            if (_tireCombinedSlipFR != value)
            {
                _tireCombinedSlipFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireCombinedSlipRL
    {
        get => _tireCombinedSlipRL;
        set
        {
            if (_tireCombinedSlipRL != value)
            {
                _tireCombinedSlipRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireCombinedSlipRR
    {
        get => _tireCombinedSlipRR;
        set
        {
            if (_tireCombinedSlipRR != value)
            {
                _tireCombinedSlipRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Actual suspension travel in meters
    public float SuspensionTravelMetersFL
    {
        get => _suspensionTravelMetersFL;
        set
        {
            if (_suspensionTravelMetersFL != value)
            {
                _suspensionTravelMetersFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float SuspensionTravelMetersFR
    {
        get => _suspensionTravelMetersFR;
        set
        {
            if (_suspensionTravelMetersFR != value)
            {
                _suspensionTravelMetersFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float SuspensionTravelMetersRL
    {
        get => _suspensionTravelMetersRL;
        set
        {
            if (_suspensionTravelMetersRL != value)
            {
                _suspensionTravelMetersRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float SuspensionTravelMetersRR
    {
        get => _suspensionTravelMetersRR;
        set
        {
            if (_suspensionTravelMetersRR != value)
            {
                _suspensionTravelMetersRR = value;
                OnPropertyChanged();
            }
        }
    }

    // Unique ID of the car make/model
    public uint CarOrdinal
    {
        get => _carOrdinal;
        set
        {
            if (_carOrdinal != value)
            {
                _carOrdinal = value;
                OnPropertyChanged();
            }
        }
    }

    // Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive
    public uint CarClass
    {
        get => _carClass;
        set
        {
            if (_carClass != value)
            {
                _carClass = value;
                OnPropertyChanged();
            }
        }
    }

    // Between 100 (worst car) and 999 (best car) inclusive
    public uint CarPerformanceIndex
    {
        get => _carPerformanceIndex;
        set
        {
            if (_carPerformanceIndex != value)
            {
                _carPerformanceIndex = value;
                OnPropertyChanged();
            }
        }
    }

    // 0 = FWD, 1 = RWD, 2 = AWD
    public uint DrivetrainType
    {
        get => _drivetrainType;
        set
        {
            if (_drivetrainType != value)
            {
                _drivetrainType = value;
                OnPropertyChanged();
            }
        }
    }

    // Number of cylinders in the engine
    public uint NumCylinders
    {
        get => _numCylinders;
        set
        {
            if (_numCylinders != value)
            {
                _numCylinders = value;
                OnPropertyChanged();
            }
        }
    }

    public float PositionX
    {
        get => _positionX;
        set
        {
            if (_positionX != value)
            {
                _positionX = value;
                OnPropertyChanged();
            }
        }
    }

    public float PositionY
    {
        get => _positionY;
        set
        {
            if (_positionY != value)
            {
                _positionY = value;
                OnPropertyChanged();
            }
        }
    }

    public float PositionZ
    {
        get => _positionZ;
        set
        {
            if (_positionZ != value)
            {
                _positionZ = value;
                OnPropertyChanged();
            }
        }
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if (_speed != value)
            {
                _speed = value;
                OnPropertyChanged();
            }
        }
    }

    public float SpeedMPH
    {
        get => _speedMPH;
        set
        {
            if (_speedMPH != value)
            {
                _speedMPH = value;
                OnPropertyChanged();
            }
        }
    }

    public float SpeedKPH
    {
        get => _speedKPH;
        set
        {
            if (_speedKPH != value)
            {
                _speedKPH = value;
                OnPropertyChanged();
            }
        }
    }

    public float Power
    {
        get => _power;
        set
        {
            if (_power != value)
            {
                _power = value;
                OnPropertyChanged();
            }
        }
    }

    public float Torque
    {
        get => _torque;
        set
        {
            if (_torque != value)
            {
                _torque = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireTempFL
    {
        get => _tireTempFL;
        set
        {
            if (_tireTempFL != value)
            {
                _tireTempFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireTempFR
    {
        get => _tireTempFR;
        set
        {
            if (_tireTempFR != value)
            {
                _tireTempFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireTempRL
    {
        get => _tireTempRL;
        set
        {
            if (_tireTempRL != value)
            {
                _tireTempRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireTempRR
    {
        get => _tireTempRR;
        set
        {
            if (_tireTempRR != value)
            {
                _tireTempRR = value;
                OnPropertyChanged();
            }
        }
    }

    public float Boost
    {
        get => _boost;
        set
        {
            if (_boost != value)
            {
                _boost = value;
                OnPropertyChanged();
            }
        }
    }

    public float Fuel
    {
        get => _fuel;
        set
        {
            if (_fuel != value)
            {
                _fuel = value;
                OnPropertyChanged();
            }
        }
    }

    public float Distance
    {
        get => _distance;
        set
        {
            if (_distance != value)
            {
                _distance = value;
                OnPropertyChanged();
            }
        }
    }

    public float BestLapTime
    {
        get => _bestLapTime;
        set
        {
            if (_bestLapTime != value)
            {
                _bestLapTime = value;
                OnPropertyChanged();
            }
        }
    }

    public float LastLapTime
    {
        get => _lastLapTime;
        set
        {
            if (_lastLapTime != value)
            {
                _lastLapTime = value;
                OnPropertyChanged();
            }
        }
    }

    public float CurrentLapTime
    {
        get => _currentLapTime;
        set
        {
            if (_currentLapTime != value)
            {
                _currentLapTime = value;
                OnPropertyChanged();
            }
        }
    }

    public float CurrentRaceTime
    {
        get => _currentRaceTime;
        set
        {
            if (_currentRaceTime != value)
            {
                _currentRaceTime = value;
                OnPropertyChanged();
            }
        }
    }

    public uint LapNumber
    {
        get => _lapNumber;
        set
        {
            if (_lapNumber != value)
            {
                _lapNumber = value;
                OnPropertyChanged();
            }
        }
    }

    public uint RacePosition
    {
        get => _racePosition;
        set
        {
            if (_racePosition != value)
            {
                _racePosition = value;
                OnPropertyChanged();
            }
        }
    }

    public uint Accelerator
    {
        get => _accelerator;
        set
        {
            if (_accelerator != value)
            {
                _accelerator = value;
                OnPropertyChanged();
            }
        }
    }

    public uint Brake
    {
        get => _brake;
        set
        {
            if (_brake != value)
            {
                _brake = value;
                OnPropertyChanged();
            }
        }
    }

    public uint Clutch
    {
        get => _clutch;
        set
        {
            if (_clutch != value)
            {
                _clutch = value;
                OnPropertyChanged();
            }
        }
    }

    public uint Handbrake
    {
        get => _handbrake;
        set
        {
            if (_handbrake != value)
            {
                _handbrake = value;
                OnPropertyChanged();
            }
        }
    }

    public uint Gear
    {
        get => _gear;
        set
        {
            if (_gear != value)
            {
                _gear = value;
                OnPropertyChanged();
            }
        }
    }

    public int Steer
    {
        get => _steer;
        set
        {
            if (_steer != value)
            {
                _steer = value;
                OnPropertyChanged();
            }
        }
    }

    public uint NormalizedDrivingLine
    {
        get => _normalizedDrivingLine;
        set
        {
            if (_normalizedDrivingLine != value)
            {
                _normalizedDrivingLine = value;
                OnPropertyChanged();
            }
        }
    }

    public uint NormalizedAiBrakeDifference
    {
        get => _normalizedAIBrakeDifference;
        set
        {
            if (_normalizedAIBrakeDifference != value)
            {
                _normalizedAIBrakeDifference = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireWearFL
    {
        get => _tireWearFL;
        set
        {
            if (_tireWearFL != value)
            {
                _tireWearFL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireWearFR
    {
        get => _tireWearFR;
        set
        {
            if (_tireWearFR != value)
            {
                _tireWearFR = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireWearRL
    {
        get => _tireWearRL;
        set
        {
            if (_tireWearRL != value)
            {
                _tireWearRL = value;
                OnPropertyChanged();
            }
        }
    }

    public float TireWearRR
    {
        get => _tireWearRR;
        set
        {
            if (_tireWearRR != value)
            {
                _tireWearRR = value;
                OnPropertyChanged();
            }
        }
    }

    // ID for track
    public int TrackOrdinal
    {
        get => _trackOrdinal;
        set
        {
            if (_trackOrdinal != value)
            {
                _trackOrdinal = value;
                OnPropertyChanged();
            }
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

