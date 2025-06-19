namespace SimDynoServer.Models;

public enum Game
{
    Unknown = -1,
    ForzaMotorsport,
}

public enum SignalRStatus
{
    Unknown = -1,
    Inactive,
    WaitingOnClients,
    Connected,
    Disconnected
}

public enum ServerStatus
{
    Unknown = -1,
    Inactive,
    Starting,
    Running,
    Closing,
}

public enum GameStatus
{
    Unknown = -1,
    Waiting,
    Connected,
    Disconnected
}

public enum RecordingServiceStatus
{
    Unknown = -1,
    Inactive,
    Starting,
    Recording,
    Stopping,
}

public enum BroadcastServiceStatus
{
    Unknown = -1,
    Inactive,
    Starting,
    Broadcasting,
    Stopping,
}