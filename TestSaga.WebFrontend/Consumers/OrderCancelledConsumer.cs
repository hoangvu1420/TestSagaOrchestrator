using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class OrderCancelledConsumer(
    IHubContext<OrderHub> hubContext,
    ILogger<OrderCancelledConsumer> logger
    ) : IConsumer<OrderCancelled>
{
    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var message = context.Message;
        var customMessage = $"Order Cancelled for OrderId: {message.OrderId}";
        await hubContext.Clients.All.SendAsync("OrderCancelled", customMessage);
        logger.LogInformation(customMessage);
    }
}