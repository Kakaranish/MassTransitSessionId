using Microsoft.AspNetCore.Mvc;

namespace MassTransitSessionId.Controllers;

[ApiController]
[Route("sandbox")]
public class SandboxController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;

        return Ok();
    }
}