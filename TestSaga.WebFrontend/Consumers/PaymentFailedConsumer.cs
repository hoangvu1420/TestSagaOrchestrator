using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class PaymentFailedConsumer(
    IHubContext<OrderHub> hubContext,
    ILogger<PaymentFailedConsumer> logger
    ) : IConsumer<PaymentFailed>
{
	public async Task Consume(ConsumeContext<PaymentFailed> context)
	{
		var message = context.Message;
		var customMessage = $"Payment Failed for OrderId: {message.OrderId} - Reason: {message.FailureReason}";
        await hubContext.Clients.All.SendAsync("PaymentFailed", customMessage);
        logger.LogInformation(customMessage);
    }
}
