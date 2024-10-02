using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class OrderShippedConsumer(
    IHubContext<OrderHub> hubContext,
    ILogger<OrderShippedConsumer> logger) : IConsumer<OrderShipped>
{
	public async Task Consume(ConsumeContext<OrderShipped> context)
	{
		var message = context.Message;
		var customMessage = $"Order Shipped for OrderId: {message.OrderId} - ShippedDate: {message.ShippedDate}";
        await hubContext.Clients.All.SendAsync("OrderShipped", customMessage);
        logger.LogInformation(customMessage);
	}
}