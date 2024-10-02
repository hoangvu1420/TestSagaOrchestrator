using MassTransit;
using TestSaga.Events;

namespace TestSaga.SagaOrchestrator.Consumers;

public class OrderSubmittedConsumer(ILogger<OrderSubmittedConsumer> logger) : IConsumer<OrderSubmitted>
{
	public async Task Consume(ConsumeContext<OrderSubmitted> context)
	{
		var message = context.Message;
		// Log or handle order submission logic if needed
		await Task.CompletedTask;

		// Log for visibility
		logger.LogInformation($"Order OrderSubmitted: {message.OrderId}");
	}
}