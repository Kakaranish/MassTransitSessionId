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
        _logger.LogInformation("Message {MessageId} | SessionId {SessionId} | Trying process...", 
            context.MessageId, context.SessionId());
        
        PlayRoulette();
        
        _logger.LogInformation("Message {MessageId} | SessionId {SessionId} | Saying: {Content}", 
            context.MessageId, context.SessionId(), context.Message.Content);
        
        return Task.CompletedTask;
    }
    
    private static void PlayRoulette()
    {
        if (new Random().NextInt64(1, 5) == 2)
            throw new InvalidOperationException("Lost in roulette");
    }
}