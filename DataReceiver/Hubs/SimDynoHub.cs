using SimDynoServer.Models;
using Microsoft.AspNetCore.SignalR;
using SimDynoServer.Helpers;
using System.Threading.Tasks;

namespace SimDynoServer.Hubs;

public class SimDynoHub : Hub
{
    public SimDynoHub()
    {
        // Console.WriteLine("SimDynoHub instance created.");
    }

    public override async Task OnConnectedAsync()
    {
        var connectionID = Context.ConnectionId;

        // Log the connection on the server
        Console.WriteLine($"New client connected: {connectionID}");

        // Notify all connected clients about the new connection
        await Clients.All.SendAsync("ClientConnected", connectionID);

        await base.OnConnectedAsync();
    }

    public async Task BroadcastMessage(string message)
    {
        Console.WriteLine($"Broadcasting message: {message}");
        await Clients.All.SendAsync("BroadcastMessage", message);
    }

    // Called when a client disconnects from the hub
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;

        // Log the disconnection on the server
        Console.WriteLine($"Client disconnected: {connectionId}");

        // Notify all connected clients about the disconnection
        await Clients.All.SendAsync("ClientDisconnected", connectionId);

        await base.OnDisconnectedAsync(exception);
    }

    // TODO: Add a method to start the ForzaListener
    // TODO: Add a method to stop the ForzaListener
}
