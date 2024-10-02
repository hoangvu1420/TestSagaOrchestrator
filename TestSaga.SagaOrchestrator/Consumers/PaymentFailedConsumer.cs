using MassTransit;
using TestSaga.Events;

namespace TestSaga.SagaOrchestrator.Consumers;

public class PaymentFailedConsumer(ILogger<PaymentFailedConsumer> logger) : IConsumer<PaymentFailed>
{
	public async Task Consume(ConsumeContext<PaymentFailed> context)
	{
		var message = context.Message;
		// Log or handle payment failure logic if needed

		await Task.CompletedTask;

		// Log for visibility
		logger.LogInformation($"Payment Failed for OrderId: {message.OrderId} - Reason: {message.FailureReason}");
	}
}
