using MassTransit;
using TestSaga.Events;

namespace TestSaga.ShippingService.Consumers;

public class PaymentSucceededConsumer(
	IPublishEndpoint publishEndpoint,
	ILogger<PaymentSucceededConsumer> logger
	) : IConsumer<PaymentSucceeded>
{
	public async Task Consume(ConsumeContext<PaymentSucceeded> context)
	{
		var paymentProcessed = context.Message;

		// Simulate shipping process (in a real scenario, you would call a shipping API)
		var shipped = await ShipOrder(paymentProcessed.OrderId);

		if (shipped)
		{
			// Publish Shipped event
			await publishEndpoint.Publish(new OrderShipped
			{
				OrderId = paymentProcessed.OrderId,
				ShippedDate = DateTime.UtcNow
			});

			logger.LogInformation($"Published OrderShipped event for order: {paymentProcessed.OrderId}");
		}
	}

	// Dummy function for shipping order
	private async Task<bool> ShipOrder(Guid orderId)
	{
		// Simulate the shipping logic
		logger.LogInformation($"Shipping order {orderId}...");

		await Task.Delay(2000); // Simulate shipping time

		return true; // Assume the order is always shipped successfully for demo purposes
	}
}