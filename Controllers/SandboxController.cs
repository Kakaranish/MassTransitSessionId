using MassTransit;
using MassTransitSessionId.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitSessionId.Controllers;

[ApiController]
[Route("sandbox")]
public class SandboxController : ControllerBase
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public SandboxController(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
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
        await sendEndpoint.Send(cmd, cancellationToken);
        
        return Ok();
    }

    public record SomethingToSay(int MessageId, string ContentToSay);
}