using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class InventoryUnavailableConsumer(
    IHubContext<OrderHub> hubContext,
    ILogger<InventoryUnavailableConsumer> logger
    ) : IConsumer<InventoryUnavailable>
{
	public async Task Consume(ConsumeContext<InventoryUnavailable> context)
	{
		var inventoryUnavailable = context.Message;
		var customMessage = $"Inventory Unavailable for OrderId: {inventoryUnavailable.OrderId} - Reason: {inventoryUnavailable.Reason}";
        await hubContext.Clients.All.SendAsync("InventoryUnavailable", customMessage);
        logger.LogInformation(customMessage);
	}
}