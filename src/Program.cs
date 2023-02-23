using MassTransit;
using MassTransitSessionId.Messaging.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<SaySomethingIntegrationCommandConsumer>();
    
    configurator.UsingAzureServiceBus((azContext, azBusConfigurator) =>
    {
        azBusConfigurator.Host(builder.Configuration.GetValue<string>("ServiceBus:ConnectionString"));
        azBusConfigurator.ReceiveEndpoint("main-app", endpointConfigurator =>
        {
            endpointConfigurator.ConfigureConsumer<SaySomethingIntegrationCommandConsumer>(azContext);
        });
    });
});


var app = builder.Build();
app.MapControllers();
app.Run();