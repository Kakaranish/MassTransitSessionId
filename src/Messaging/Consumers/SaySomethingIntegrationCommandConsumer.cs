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
        
        _logger.LogInformation("Message {MessageId} | SessionId {SessionId}", 
            context.MessageId, context.SessionId());
        
        return Task.CompletedTask;
    }
    
    private static void PlayRoulette()
    {
        throw new InvalidOperationException();
        
        // if (new Random().NextInt64(1, 210037) == 2)
        //     throw new InvalidOperationException("Lost in roulette");
    }
}