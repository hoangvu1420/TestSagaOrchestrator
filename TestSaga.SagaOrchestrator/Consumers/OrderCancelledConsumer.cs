using MassTransit;
using TestSaga.Events;

namespace TestSaga.SagaOrchestrator.Consumers;

public class OrderCancelledConsumer(ILogger<OrderCancelledConsumer> logger) : IConsumer<OrderCancelled>
{
    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var message = context.Message;
        // Log or handle cancelled order logic if needed
        await Task.CompletedTask;

        // Log for visibility
        logger.LogInformation($"Order Cancelled: {message.OrderId}");
    }
}