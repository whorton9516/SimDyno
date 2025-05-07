using Microsoft.AspNetCore.Mvc;
using SimDynoServer.Services;

namespace SimDynoServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimDynoController : ControllerBase
{
    private readonly ReceiverService _receiverService;

    public SimDynoController(ReceiverService receiverService)
    {
        _receiverService = receiverService;
    }

    [HttpPost("listener/start")]
    public async Task<IActionResult> StartListener()
    {
        try
        {
            Task.Run(() => _receiverService.ListenAsync());

            return Ok("Listener started.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an issue starting the listener: {ex.Message}");
            return BadRequest("Failed to start the listener.");
        }
    }

    [HttpPost("listener/stop")]
    public async Task<IActionResult> StopListener()
    {
        try
        {
            _receiverService.StopListening();
            return Ok("Listener stopped.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an issue stopping the listener: {ex.Message}");
            return BadRequest("Failed to stop the listener.");
        }
    }
}