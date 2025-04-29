using DataReceiver;

namespace SimDynoServer.Services;

public class ReceiverHostedService : IHostedService
{
    private readonly Receiver _receiver;
    
    public ReceiverHostedService(Receiver receiver)
    {
        _receiver = receiver;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Run(() => _receiver.ListenAsync(), cancellationToken);
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _receiver.Listener?.Close();
        return Task.CompletedTask;
    }
}
