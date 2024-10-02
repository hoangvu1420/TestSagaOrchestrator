using MassTransit;
using TestSaga.Events;
using Microsoft.Extensions.Logging; // Add this using directive

namespace TestSaga.SagaOrchestrator.Consumers;

public class InventoryUnavailableConsumer(ILogger<InventoryUnavailableConsumer> logger) : IConsumer<InventoryUnavailable>
{
	public async Task Consume(ConsumeContext<InventoryUnavailable> context)
	{
		var inventoryUnavailable = context.Message;
		// Log or handle inventory unavailable logic if needed

		await Task.CompletedTask;

		// Log for visibility
		logger.LogInformation($"Inventory Unavailable for OrderId: {inventoryUnavailable.OrderId} - Reason: {inventoryUnavailable.Reason}");
	}
}