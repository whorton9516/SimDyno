using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;  

namespace Common.Models;

[MessagePackObject]
public struct ForzaData
{

    private bool _isRaceOn;
    private uint _timeStampMS;
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
    private uint _carOrdinal;
    private uint _carClass;
    private uint _carPerformanceIndex;
    private uint _drivetrainType;
    private uint _numCylinders;
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
    private uint _lapNumber;
    private uint _racePosition;
    private uint _accelerator;
    private uint _brake;
    private uint _clutch;
    private uint _handbrake;
    private uint _gear;
    private int _steer;
    private uint _normalizedDrivingLine;
    private uint _normalizedAIBrakeDifference;
    private float _tireWearFL;
    private float _tireWearFR;
    private float _tireWearRL;
    private float _tireWearRR;
    private int _trackOrdinal;

    /// <summary>
    /// 1 when race is on, 0 when in menus or the race is stopped
    /// </summary>
    [Key(0)]
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
    [Key(1)]
    public uint TimestampMS
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

    [Key(2)]
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

    [Key(3)]
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

    [Key(4)]
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
    [Key(5)]
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

    [Key(6)]
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

    [Key(7)]
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
    [Key(8)]
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

    [Key(9)]
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

    [Key(10)]
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
    [Key(11)]
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

    [Key(12)]
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

    [Key(13)]
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

    [Key(14)]
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

    [Key(15)]
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

    [Key(16)]
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
    [Key(17)]
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

    [Key(18)]
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

    [Key(19)]
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

    [Key(20)]
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
    [Key(21)]
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

    [Key(22)]
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

    [Key(23)]
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

    [Key(24)]
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
    [Key(25)]
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

    [Key(26)]
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

    [Key(27)]
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

    [Key(28)]
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
    [Key(29)]
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

    [Key(30)]
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

    [Key(31)]
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

    [Key(32)]
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
    [Key(33)]
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

    [Key(34)]
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

    [Key(35)]
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

    [Key(36)]
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
    [Key(37)]
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

    [Key(38)]
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

    [Key(39)]
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

    [Key(40)]
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
    [Key(41)]
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

    [Key(42)]
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

    [Key(43)]
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

    [Key(44)]
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
    [Key(45)]
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

    [Key(46)]
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

    [Key(47)]
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

    [Key(48)]
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
    [Key(49)]
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

    [Key(50)]
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

    [Key(51)]
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

    [Key(52)]
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
    [Key(53)]
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
    [Key(54)]
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
    [Key(55)]
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
    [Key(56)]
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
    [Key(57)]
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

    [Key(58)]
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

    [Key(59)]
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

    [Key(60)]
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

    [Key(61)]
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

    [Key(62)]
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

    [Key(63)]
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

    [Key(64)]
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

    [Key(65)]
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

    [Key(66)]
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

    [Key(67)]
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

    [Key(68)]
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

    [Key(69)]
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

    [Key(70)]
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

    [Key(71)]
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

    [Key(72)]
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

    [Key(73)]
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

    [Key(74)]
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

    [Key(75)]
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

    [Key(76)]
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

    [Key(77)]
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

    [Key(78)]
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

    [Key(79)]
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

    [Key(80)]
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

    [Key(81)]
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

    [Key(82)]
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

    [Key(83)]
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

    [Key(84)]
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

    [Key(85)]
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

    [Key(86)]
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

    [Key(87)]
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

    [Key(88)]
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

    [Key(89)]
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

    [Key(90)]
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
    [Key(91)]
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
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

