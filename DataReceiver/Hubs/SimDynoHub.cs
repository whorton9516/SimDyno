using Microsoft.AspNetCore.SignalR;
using SimDynoServer.Helpers;
using SimDynoServer.Configs;
using SimDynoServer.Utils;
using SimDynoServer.Models;

namespace SimDynoServer.Hubs;

public class SimDynoHub : Hub
{
    static ParserConfig _parserConfig = new ParserConfig();
    static readonly Dictionary<long, TaskCompletionSource<bool>> _packetConfirmations = new();
    static readonly object _packetConfirmationsLock = new();
    static int _connectedClients = 0;

    readonly LoggingUtil _loggingUtil;

    public SimDynoHub(LoggingUtil loggingUtil)
    {
        _loggingUtil = loggingUtil;
    }

    public override async Task OnConnectedAsync()
    {
        var connectionID = Context.ConnectionId;

        await Clients.All.SendAsync("ClientConnected", connectionID);
        _loggingUtil.SetSignalRStatus(SignalRStatus.Connected);
        _connectedClients++;
        await base.OnConnectedAsync();
    }

    public async Task BroadcastMessage(string message)
    {
        await Clients.All.SendAsync("BroadcastMessage", message);
    }

    public async Task SetRequestedFields(string[] fields)
    {
        try
        {
            SignalRHelper.SetRequestedFields(fields);
            _parserConfig.EnableParsers(fields);
            var accepted = _parserConfig.LastAcceptedFields;
            var rejected = _parserConfig.LastRejectedFields;
            await Clients.Caller.SendAsync("RequestedFieldsUpdated", new {
                acceptedFields = accepted,
                rejectedFields = rejected
            });
        }
        catch (Exception ex)
        {
            ex.LogException("Error in SetRequestedFields");
            await Clients.Caller.SendAsync("RequestedFieldsUpdated", new {
                acceptedFields = new List<string>(),
                rejectedFields = fields.ToList(),
                error = ex.Message
            });
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;

        await Clients.All.SendAsync("ClientDisconnected", connectionId);
        _loggingUtil.SetSignalRStatus(SignalRStatus.WaitingOnClients);
        _connectedClients--;
        await base.OnDisconnectedAsync(exception);
    }

    // Called by ReceiverService to register a confirmation waiter
    public static Task<bool> WaitForPacketConfirmationAsync(long packetId, int timeoutMs)
    {
        if (_connectedClients == 0) return Task.FromResult(true); // No clients connected, no need to wait
        var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        lock (_packetConfirmationsLock)
        {
            _packetConfirmations[packetId] = tcs;
        }
        // Set up a timeout to avoid leaking
        _ = Task.Delay(timeoutMs).ContinueWith(_ =>
        {
            lock (_packetConfirmationsLock)
            {
                if (_packetConfirmations.TryGetValue(packetId, out var t))
                {
                    t.TrySetResult(false); // Timed out
                    _packetConfirmations.Remove(packetId);
                }
            }
        });
        return tcs.Task;
    }

    // Called by client to acknowledge receipt and allow round-trip timing
    public async Task ConfirmReceipt(long packetId)
    {
        lock (_packetConfirmationsLock)
        {
            if (_packetConfirmations.TryGetValue(packetId, out var tcs))
            {
                tcs.TrySetResult(true);
                _packetConfirmations.Remove(packetId);
            }
        }
    }

    // TODO: Add a method to start the ForzaListener
    // TODO: Add a method to stop the ForzaListener

    public static ParserConfig GetParserConfig() => _parserConfig;
}
