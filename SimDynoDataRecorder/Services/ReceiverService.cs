// Portions of this file include code originally written by Austin Baccus (MIT License)

using SimDynoServer.Models;
using System.Net;
using System.Net.Sockets;
using SimDynoServer.Utils;
using SimDynoDevSuite.Models;
using SimDynoServer.Configs;

namespace SimDynoDevSuite.Services;

public class ReceiverService
{
    public Socket? Listener { get; set; }
    AppState _state { get; set; } = AppState.Idle;
    readonly RecordingService _recordingService;

    private string _ipAddress = "127.0.0.1";
    private int _port = 5555;

    bool _raceStarted = false;
    public bool Listen = false;

    MainForm _parent;
    bool _recording = false;
    bool _firstPacket = true;

    // Add parser config for dynamic field selection
    private readonly ParserConfig _parserConfig = new();

    public ReceiverService(RecordingService recordingService)
    {
        _recordingService = recordingService;
    }

    // Allow setting requested fields for parsing
    public void SetRequestedFields(IEnumerable<string> fields)
    {
        if (fields == null || !fields.Any())
            _parserConfig.EnableAllParsers();
        else
            _parserConfig.EnableParsers(fields);
    }

    public async Task ListenAsync(MainForm parentForm)
    {
        try
        {
            _parent = parentForm;
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var endPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);
            Listener.Bind(endPoint);

            byte[] buffer = new byte[331];
            EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine($"Started Listening at Endpoint: {endPoint.Address}:{endPoint.Port}");

            while (Listen)
            {
                var bytesReceived = Listener.ReceiveFrom(buffer, ref remote);

                if (bytesReceived > 0)
                {
                    var parsedData = ParseForza(buffer);

                    if (!_raceStarted && parsedData.IsRaceOn)
                        _raceStarted = true;

                    if (_state == AppState.Recording)
                    {
                        if (_recordingService != null && parsedData.LapNumber == 1)
                        {
                            if (_recordingService.Filename.Length == 0)
                                _recordingService.Filename = 
                                    $"{parsedData.TrackOrdinal}_{parsedData.CarOrdinal}_{DateTime.Now.ToString("MMddyyyy")}.txt";

                            if (!_recording)
                            {
                                Console.WriteLine("Reached lap range. Now recording.");
                                _recordingService.StartRecording();
                                _recording = true;
                            }

                            _recordingService.Record(buffer, _firstPacket);
                            if (_firstPacket)
                                _firstPacket = false;
                        }

                        if (parsedData.LapNumber > 1 && _recording)
                        {
                            _recording = false;
                            Console.WriteLine("Out of lap range. Stopping recording.");
                            _recordingService?.StopRecording();
                            Listen = false;
                        }
                    }

                    BroadcastData(parsedData);
                }
            }

            //_recordingService?.StopRecording();
            Console.WriteLine("Listening stopped.");
        }
        catch (Exception ex)
        {
            ex.LogException("There was an issue in ListenAsync");
        }
        finally
        {
            Console.WriteLine("Closing the Listener");
            Listener?.Close();
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
            ex.LogException("There was an issue parsing the Forza data packet.");
        }
        return data;
    }

    private void BroadcastData(ForzaData data)
    {
        _parent.UpdateTelemetryDataView(data);
    }

    public void UpdateState(AppState state)
    {
        _state = state;

        switch (_state)
        {
            case AppState.Idle:
            case AppState.Broadcast:
                Listen = false;
                break;
            case AppState.Listen:
                Listen = true;
                break;
            case AppState.Recording:
                Listen = true;
                break;
        }
    }

    public void StartListening(MainForm parent, string ipAddress, string port)
    {
        _ipAddress = ipAddress;
        _port = int.Parse(port);
        Task.Run(() => ListenAsync(parent));
    }
}