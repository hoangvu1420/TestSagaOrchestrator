using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class OrderSubmittedConsumer(
	IHubContext<OrderHub> hubContext,
	ILogger<OrderSubmittedConsumer> logger
) : IConsumer<OrderSubmitted>
{
	public async Task Consume(ConsumeContext<OrderSubmitted> context)
	{
		var message = context.Message;
		var customMessage = $"Order Submitted for OrderId: {message.OrderId}";
		await hubContext.Clients.All.SendAsync("OrderSubmitted", customMessage);
		logger.LogInformation(customMessage);
	}
}