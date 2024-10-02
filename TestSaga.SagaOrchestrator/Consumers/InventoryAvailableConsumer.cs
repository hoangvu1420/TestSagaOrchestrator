using MassTransit;
using TestSaga.Events;

namespace TestSaga.SagaOrchestrator.Consumers;

public class InventoryAvailableConsumer(ILogger<InventoryAvailableConsumer> logger) : IConsumer<InventoryAvailable>
{
	public async Task Consume(ConsumeContext<InventoryAvailable> context)
	{
		var message = context.Message;
		// Log or handle inventory checked logic if needed
		await Task.CompletedTask;

		// Log for visibility
		logger.LogInformation($"Inventory Available for OrderId: {message.OrderId}");
	}
}