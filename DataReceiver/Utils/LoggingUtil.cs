using SimDynoServer.Models;
using SimDynoServer.Services;

namespace SimDynoServer.Utils;

public class LoggingUtil
{
    public event Action? OnRedrawRequested;

    ServerStatus _serverStatus;
    SignalRStatus _signalRStatus;
    GameStatus _gameStatus;
    Game _game;

    double _avgParseTime = 0;
    double _avgRoundTripTime = 0;
    int _outlierCount = 0;
    int _serverLoad = 0;

    string _signalRUrl = "";

    readonly object _lock = new();
    // Index -> (key, value)
    readonly Dictionary<int, (string key, string value)> _metrics = new();
    List<string> _previousLines = new();

    public LoggingUtil()
    {
        InitStatuses();
        InitDefaults();
        OnRedrawRequested += RedrawConsole;
        RedrawConsole();
    }

    private void InitDefaults()
    {
        SetGame(Game.ForzaMotorsport, false);
        SetBlank(2, false);
        SetServerStatus(ServerStatus.Inactive, false);
        SetSignalRStatus(SignalRStatus.Inactive, false);
        SetSignalRUrl(_signalRUrl, false);
        SetGameStatus(GameStatus.Waiting, false);
        SetBlank(7, false);
        SetAvgParseTime(_avgParseTime, false);
        SetOutlierCount(_outlierCount, false);
        SetServerLoad(_outlierCount, false);
        SetAvgRoundTripTime(_outlierCount, false);
    }

    private void InitStatuses()
    {
        _game = Game.ForzaMotorsport;
        _serverStatus = ServerStatus.Inactive;
        _signalRStatus = SignalRStatus.Inactive;
        _gameStatus = GameStatus.Waiting;
    }

    public void RedrawConsole()
    {
        List<string> lines = new();
        lines.Add("====================== Server Status Dashboard ======================");
        lock (_lock)
        {
            if (_metrics.Count > 0)
            {
                int min = int.MaxValue, max = int.MinValue;
                foreach (var idx in _metrics.Keys)
                {
                    if (idx < min) min = idx;
                    if (idx > max) max = idx;
                }
                for (int i = min; i <= max; i++)
                {
                    if (_metrics.TryGetValue(i, out var kvp))
                        lines.Add($"{kvp.key}{(kvp.key == string.Empty ? "" : ":")} {kvp.value}");
                    else
                        lines.Add("");
                }
            }
        }
        lines.Add("======================================================================");

        int maxLines = Math.Max(lines.Count, _previousLines.Count);
        for (int i = 0; i < maxLines; i++)
        {
            string newLine = i < lines.Count ? lines[i] : string.Empty;
            string oldLine = i < _previousLines.Count ? _previousLines[i] : string.Empty;
            if (newLine != oldLine)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth - 1));
                Console.SetCursorPosition(0, i);
                Console.Write(newLine);
            }
        }
        for (int i = lines.Count; i < _previousLines.Count; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth - 1));
        }
        _previousLines = lines;
    }

    public void SetBlank(int index, bool redraw = true)
    {
        SetMetric(index, "", "", redraw);
    }

    public void SetMetric(int index, string key, string value, bool redraw = true)
    {
        lock (_lock)
        {
            _metrics[index] = (key, value);
        }
        if (redraw)
            OnRedrawRequested?.Invoke();
    }

    /// <summary>
    /// Set the Game metric.
    /// </summary>
    public void SetGame(Game game, bool redraw = true)
    {
        _game = game;
        SetMetric(1, "Game", _game.ToString(), redraw);
    }

    /// <summary>
    /// Set the SignalR Status metric.
    /// </summary>
    public void SetSignalRStatus(SignalRStatus status, bool redraw = true)
    {
        _signalRStatus = status;
        var statusString = _signalRStatus.ToString();
        if (status == SignalRStatus.WaitingOnClients)
            statusString = "Waiting on Clients";
        SetMetric(4, "SignalR Status", statusString, redraw);
    }

    /// <summary>
    /// Set the SignalR URL metric.
    /// </summary>
    public void SetSignalRUrl(string url, bool redraw = true)
    {
        _signalRUrl = url;
        SetMetric(5, "SignalR URLs", _signalRUrl, redraw);
    }

    /// <summary>
    /// Set the Server Status metric.
    /// </summary>
    public void SetServerStatus(ServerStatus status, bool redraw = true)
    {
        _serverStatus = status;
        SetMetric(3, "Server Status", _serverStatus.ToString(), redraw);
    }

    /// <summary>
    /// Set the Game Status metric.
    /// </summary>
    public void SetGameStatus(GameStatus status, bool redraw = true)
    {
        _gameStatus = status;
        SetMetric(6, "Game Status", _gameStatus.ToString(), redraw);
    }

    /// <summary>
    /// Set the Avg Parse Time metric.
    /// </summary>
    public void SetAvgParseTime(double time, bool redraw = true)
    {
        _avgParseTime = time;
        SetMetric(8, "Avg. Parsing Time", $"{_avgParseTime:F3} ms", redraw);
    }

    /// <summary>
    /// Set the Outlier Count metric.
    /// </summary>
    public void SetOutlierCount(int count, bool redraw = true)
    {
        _outlierCount = count;
        SetMetric(9, "Outliers", _outlierCount.ToString(), redraw);
    }

    /// <summary>
    /// Set the Server Load metric.
    /// </summary>
    public void SetServerLoad(int load, bool redraw = true)
    {
        _serverLoad = load;
        SetMetric(10, "Server Load", $"{_serverLoad}%", redraw);
    }

    /// <summary>
    /// Set the Avg Round Trip Time metric.
    /// </summary>
    public void SetAvgRoundTripTime(double time, bool redraw = true)
    {
        _avgRoundTripTime = time;
        SetMetric(11, "Avg. Round Trip Time", $"{_avgRoundTripTime:F3} ms", redraw);
    }
}
