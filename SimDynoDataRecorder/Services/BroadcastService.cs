using System.Net;
using System.Net.Sockets;

namespace SimDynoDataRecorder.Services;
public class BroadcastService
{
    readonly UdpClient _udpClient;
    readonly IPEndPoint _endPoint;

    volatile bool _broadcasting = false;

    public BroadcastService(string ipAddress, string port)
    {
        _udpClient = new UdpClient();
        _endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), int.Parse(port));
    }

    public async Task StartBroadcasting(List<byte[]> packets)
    {
        _broadcasting = true;
        const int interval = 1000 / 60; // 60 Hz

        try
        {
            while (_broadcasting)
            {
                Console.WriteLine($"Starting Broadcast.");

                foreach (var packet in packets)
                {
                    try
                    {
                        await _udpClient.SendAsync(packet, packet.Length, _endPoint);
                        Console.WriteLine($"Sent Packet.");
                        await Task.Delay(interval);
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine($"Issue sending packet: {packet}\n{ex.Message}");
                    }
                }

                Console.WriteLine("Recording Broadcast finished.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Broadcasting error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Broadcasting Stopped.");
            _udpClient.Dispose();
        }
    }

    public void StopBroadcasting()
    {
        _broadcasting = false;
    }
}
