using SimDynoServer.Utils;

namespace SimDynoServer.Services;

public class ReceiverHostedService : IHostedService
{
    private readonly ReceiverService _receiverService;

    public ReceiverHostedService(ReceiverService receiverService)
    {
        _receiverService = receiverService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _ = Task.Run(() => _receiverService.ListenAsync(), cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _receiverService.Listener?.Close();
        return Task.CompletedTask;
    }
}
