using MassTransit;
using MassTransitSessionId.Messaging.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<SaySomethingIntegrationCommandConsumer>();
    
    configurator.AddDelayedMessageScheduler();
    
    configurator.UsingAzureServiceBus((azContext, azBusConfigurator) =>
    {
        azBusConfigurator.UseServiceBusMessageScheduler();
        
        azBusConfigurator.UseDelayedRedelivery(retryConfigurator =>
        {
            retryConfigurator.Handle<InvalidOperationException>();
            retryConfigurator.Interval(10, TimeSpan.FromSeconds(10));
        });

        azBusConfigurator.Host(builder.Configuration.GetValue<string>("ServiceBus:ConnectionString"));
        
        azBusConfigurator.ReceiveEndpoint("lock-test-queue", endpointConfigurator =>
        {
            endpointConfigurator.ConfigureConsumer<SaySomethingIntegrationCommandConsumer>(azContext);
            
            endpointConfigurator.MaxAutoRenewDuration = TimeSpan.FromMinutes(30);
            endpointConfigurator.RequiresSession = true;
            
            endpointConfigurator.UseMessageRetry(r =>
            {
                r.Interval(10, TimeSpan.FromMinutes(1));
            });
        });
    });
});

var app = builder.Build();
app.MapControllers();
app.Run();