using MassTransit;
using TestSaga.Commands;
using TestSaga.Events;

namespace TestSaga.OrderService.Consumers;

public class CancelOrderConsumer(
	IPublishEndpoint publishEndpoint,
	ILogger<CancelOrderConsumer> logger
	) : IConsumer<CancelOrder>
{
	public async Task Consume(ConsumeContext<CancelOrder> context)
	{
		var cancelOrder = context.Message;
		// Log or handle order cancellation logic if needed

		await Task.Delay(2000); // Simulate order cancellation time

        // Publish OrderCancelled event
        await publishEndpoint.Publish<OrderCancelled>(new
        {
            OrderId = cancelOrder.OrderId
        });

        // Log for visibility
        logger.LogInformation($"Published OrderCancelled event for OrderId: {cancelOrder.OrderId}");
    }
}