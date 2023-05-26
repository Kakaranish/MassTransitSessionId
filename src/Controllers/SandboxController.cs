using MassTransit;
using MassTransitSessionId.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitSessionId.Controllers;

[ApiController]
[Route("sandbox")]
public class SandboxController : ControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly ILogger<SandboxController> _logger;

    public SandboxController(ISendEndpointProvider sendEndpointProvider, ILogger<SandboxController> logger)
    {
        _sendEndpointProvider = sendEndpointProvider;
        _logger = logger;
    }

    [HttpPost("say-something")]
    public async Task<IActionResult> SaySomething()
    {
        var cmd = new SaySomethingIntegrationCommand
        {
            MessageId = Guid.NewGuid()
        };

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:lock-test-queue"));
        await sendEndpoint.Send(cmd, context => context.SetSessionId("XD"));
        
        _logger.LogInformation("Message sent with id - {MessageId}", cmd.MessageId);
        
        return Ok();
    }

    public record SomethingToSay(int MessageId, string ContentToSay);
}