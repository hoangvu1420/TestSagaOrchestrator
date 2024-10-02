using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class InventoryAvailableConsumer(
	IHubContext<OrderHub> hubContext,
	ILogger<InventoryAvailableConsumer> logger) : IConsumer<InventoryAvailable>
{
	public async Task Consume(ConsumeContext<InventoryAvailable> context)
	{
		var message = context.Message;
		var customMessage = $"Inventory Available for OrderId: {message.OrderId}";
        await hubContext.Clients.All.SendAsync("InventoryAvailable", customMessage);
        logger.LogInformation(customMessage);
    }
}