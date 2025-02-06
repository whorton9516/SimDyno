using Microsoft.AspNetCore.Mvc;
using SimDynoServer.Models;

namespace SimDynoServer.Controllers;

public class SimDynoController : ControllerBase
{

    [HttpPost]
    [Route("data")]
    public ForzaData Data_Get()
    {
        var data = new ForzaData();

        // Call Get method here

        return new ForzaData();
    }
}
