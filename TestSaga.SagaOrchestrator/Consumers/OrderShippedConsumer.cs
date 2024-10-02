using MassTransit;
using TestSaga.Events;

namespace TestSaga.SagaOrchestrator.Consumers;

public class OrderShippedConsumer(ILogger<OrderShippedConsumer> logger) : IConsumer<OrderShipped>
{
	public async Task Consume(ConsumeContext<OrderShipped> context)
	{
		var message = context.Message;
		// Log or handle shipped order logic if needed
		await Task.CompletedTask;

		// Log for visibility
		logger.LogInformation($"Order OrderShipped: {message.OrderId}, OrderShipped Date: {message.ShippedDate}");
	}
}