// Portions of this file include code originally written by Austin Baccus (MIT License)
using Microsoft.AspNetCore.SignalR;
using SimDynoServer.Hubs;
using SimDynoServer.Models;
using System.Net;
using System.Net.Sockets;
using SimDynoServer.Utils;
using SimDynoServer.Configs;

namespace SimDynoServer.Services;

public class ReceiverService
{
    public Socket? Listener { get; set; }
    readonly IHubContext<SimDynoHub> _hubContext;
    readonly LoggingUtil _loggingUtil;

    const string IpAddress = "127.0.0.1";
    const int Port = 5555;

    bool _gameConnected = false;
    bool _isListening = false;
    readonly object _lock = new();

    readonly ParserConfig _parserConfig;

    // Fields for tracking average parse time and outliers
    double _parseTimeSum = 0;
    double _roundTripSum = 0;
    int _serverLoadSum = 0;
    int _parseCount = 0;
    int _outlierCount = 0;
    const int LogInterval = 6; // Log every 6 packets (10fps at 60Hz)
    const double MaxBudgetMs = 16.67;
    const int ConfirmationTimeoutMs = 16;

    public ReceiverService(IHubContext<SimDynoHub> hubContext, LoggingUtil loggingUtil)
    {
        _hubContext = hubContext;
        _parserConfig = SimDynoHub.GetParserConfig();
        _loggingUtil = loggingUtil;
        _loggingUtil.SetServerStatus(ServerStatus.Starting);
    }

    public async Task ListenAsync()
    {
        lock (_lock)
        {
            // Ignore duplicate requests if already listening and socket is open
            if (_isListening && Listener != null && Listener.Connected == false)
                return;
            if (_isListening && Listener != null && Listener.IsBound)
                return;

            _isListening = true;
        }

        try
        {
            Listener?.Dispose(); // Clean up any previous socket
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var endPoint = new IPEndPoint(IPAddress.Parse(IpAddress), Port);
            Listener.Bind(endPoint);
            _loggingUtil.SetServerStatus(ServerStatus.Running);

            await BroadcastMessageToClients("Waiting for connection from the game...");

            byte[] buffer = new byte[331];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            while (_isListening)
            {
                var bytesReceived = Listener.ReceiveFrom(buffer, ref remote);

                if (bytesReceived > 0)
                {
                    if (!_gameConnected)
                    {
                        SetGameConnected(true);
                        await BroadcastMessageToClients("Game connected. Starting telemetry broadcast...");
                    }

                    // Timing the parsing for performance metrics
                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                    var parsedData = ParseForza(buffer);
                    long packetId = parsedData.TimeStampMS;
                    var parseTimeMs = stopwatch.Elapsed.TotalMilliseconds;

                    // Send data to client with packetId (timestampMS)
                    Task.Run(() => _hubContext.Clients.All.SendAsync("ReceiveData", parsedData));

                    // Track average parse time and log every LogInterval packets
                    _parseTimeSum += parseTimeMs;
                    _parseCount++;

                    // Wait for confirmation or timeout (16ms)
                    bool confirmed = await SimDynoHub.WaitForPacketConfirmationAsync(packetId, ConfirmationTimeoutMs);

                    stopwatch.Stop();
                    var elapsedTimeMs = stopwatch.Elapsed.TotalMilliseconds;
                    _roundTripSum += elapsedTimeMs;
                    int serverLoad = (int)Math.Round(elapsedTimeMs / MaxBudgetMs * 100);
                    _serverLoadSum += serverLoad;

                    // Outlier tracking
                    if (!confirmed || elapsedTimeMs > MaxBudgetMs)
                        _outlierCount++;

                    if (_parseCount == LogInterval)
                    {
                        var avgParseTime = _parseTimeSum / LogInterval;
                        var avgServerLoad = _serverLoadSum / LogInterval;
                        var avgRoundTripTime = _roundTripSum / LogInterval;
                        Task.Run(() =>
                        {
                            _loggingUtil.SetAvgParseTime(avgParseTime, false);
                            _loggingUtil.SetOutlierCount(_outlierCount, false);
                            _loggingUtil.SetServerLoad(avgServerLoad, false);
                            _loggingUtil.SetAvgRoundTripTime(avgRoundTripTime);
                        });
                        _parseTimeSum = 0;
                        _serverLoadSum = 0;
                        _parseCount = 0;
                        _outlierCount = 0;
                        _roundTripSum = 0;
                    }
                }
            }
        }
        catch (ObjectDisposedException)
        {
            // Listener was closed, allow restart
        }
        catch (Exception ex)
        {
            ex.LogException("There was an issue in ListenAsync");
        }
        finally
        {
            lock (_lock)
            {
                _isListening = false;
            }
            _loggingUtil.SetServerStatus(ServerStatus.Closing);
            StopListening();
            Listener = null;
        }
    }

    public ForzaData ParseForza(byte[] packet)
    {
        var data = new ForzaData();
        try
        {
            // Use only enabled parsers
            foreach (var parser in _parserConfig.EnabledParsers)
            {
                parser(packet, data);
            }
        }
        catch (Exception ex)
        {
            ex.LogException("There was an issue parsing the data");
        }
        return data;
    }

    async Task BroadcastMessageToClients(string message)
    {
        try
        {
            await _hubContext.Clients.All.SendAsync("BroadcastMessage", message);
        }
        catch (Exception ex)
        {
            ex.LogException("Error broadcasting message");
        }
    }

    public bool StopListening()
    {
        lock (_lock)
        {
            if (!_isListening)
                return false;
            _isListening = false;
        }

        try
        {
            Listener?.Dispose();
            Listener = null;
            SetGameConnected(false);
            Console.WriteLine("Receiver service stopped.");
            return true;
        }
        catch (Exception ex)
        {
            ex.LogException("Failed to stop the Listener");
            return false;
        }
        finally
        {
            _loggingUtil.SetServerStatus(ServerStatus.Inactive);
        }
    }

    public void SetGameConnected(bool connected)
    {
        lock (_lock)
        {
            _gameConnected = connected;
            _loggingUtil.SetGameStatus(connected ? GameStatus.Connected : GameStatus.Disconnected);
        }
    }
}