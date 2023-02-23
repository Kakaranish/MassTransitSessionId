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
    public async Task<IActionResult> SaySomething([FromBody] SomethingToSay somethingToSay, 
        CancellationToken cancellationToken)
    {
        var cmd = new SaySomethingIntegrationCommand
        {
            MessageId = somethingToSay.MessageId,
            Content = somethingToSay.ContentToSay
        };

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:main-app"));
        await sendEndpoint.Send(cmd, context => context.SetSessionId("XD"), cancellationToken);
        
        _logger.LogInformation("Message sent");
        
        return Ok();
    }

    public record SomethingToSay(int MessageId, string ContentToSay);
}