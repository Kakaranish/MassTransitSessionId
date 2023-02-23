namespace MassTransitSessionId.Messaging;

public class SaySomethingIntegrationCommand
{
    public int MessageId { get; set; }
    public string Content { get; set; } = null!;
}