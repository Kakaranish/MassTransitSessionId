using MassTransit;

namespace MassTransitSessionId.Messaging.Consumers;

public class SaySomethingIntegrationCommandConsumer : IConsumer<SaySomethingIntegrationCommand>
{
    private readonly ILogger<SaySomethingIntegrationCommandConsumer> _logger;

    public SaySomethingIntegrationCommandConsumer(ILogger<SaySomethingIntegrationCommandConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<SaySomethingIntegrationCommand> context)
    {
        _logger.LogInformation("Message {MessageId} | Saying: {Content}", 
            context.Message.MessageId, context.Message.Content);
        
        return Task.CompletedTask;
    }
}