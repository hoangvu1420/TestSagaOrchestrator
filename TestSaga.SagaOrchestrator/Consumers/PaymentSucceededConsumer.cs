using MassTransit;
using TestSaga.Events;

namespace TestSaga.SagaOrchestrator.Consumers;

public class PaymentSucceededConsumer(ILogger<PaymentSucceededConsumer> logger) : IConsumer<PaymentSucceeded>
{
	public async Task Consume(ConsumeContext<PaymentSucceeded> context)
	{
		var message = context.Message;
		// Log or handle payment processing logic if needed
		await Task.CompletedTask;

		// Log for visibility
		logger.LogInformation($"Payment Succeeded for OrderId: {message.OrderId}");
	}
}