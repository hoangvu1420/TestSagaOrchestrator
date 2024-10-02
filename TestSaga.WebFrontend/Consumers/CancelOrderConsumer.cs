using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Commands;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class CancelOrderConsumer(
	IHubContext<OrderHub> hubContext,
    ILogger<CancelOrderConsumer> logger
	) : IConsumer<CancelOrder>
{
	public async Task Consume(ConsumeContext<CancelOrder> context)
	{
		var cancelOrder = context.Message;
		var customMessage = $"Order Cancelled: {cancelOrder.OrderId}";
        await hubContext.Clients.All.SendAsync("OrderCancelled", customMessage);
        logger.LogInformation(customMessage);
    }
}