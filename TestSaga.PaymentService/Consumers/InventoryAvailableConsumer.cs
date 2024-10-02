using MassTransit;
using TestSaga.Events;

namespace TestSaga.PaymentService.Consumers;

public class InventoryCheckedConsumer(
	IPublishEndpoint publishEndpoint,
	ILogger<InventoryCheckedConsumer> logger
	) : IConsumer<InventoryAvailable>
{
	public async Task Consume(ConsumeContext<InventoryAvailable> context)
	{
		var inventoryChecked = context.Message;

		// Simulate payment process (in a real scenario, you would use a payment gateway)
		var paymentEvent = await ProcessPayment(inventoryChecked.OrderId);

		// Publish PaymentSucceeded event
		await publishEndpoint.Publish(paymentEvent);
		logger.LogInformation($"Published {paymentEvent.GetType().Name} event for order: {inventoryChecked.OrderId}");
	}

	// Dummy function for payment process
	private async Task<object> ProcessPayment(Guid orderId)
	{
		// Simulate the payment logic
		logger.LogInformation($"Processing payment for order {orderId}...");

		await Task.Delay(2000); // Simulate payment processing time

		// 70% chance of payment success
		var isSuccessful = new Random().Next(0, 10) < 7;

		logger.LogInformation($"Payment for order {orderId} is {(isSuccessful ? "successful" : "failed")}");

		if (isSuccessful)
		{
			return new PaymentSucceeded()
			{
				OrderId = orderId
			};
		}

		return new PaymentFailed
		{
			OrderId = orderId,
			FailureReason = "Insufficient balance"
		};
	}
}
