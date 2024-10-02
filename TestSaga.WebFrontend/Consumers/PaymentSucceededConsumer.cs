using MassTransit;
using Microsoft.AspNetCore.SignalR;
using TestSaga.Events;
using TestSaga.WebFrontend.Hubs;

namespace TestSaga.WebFrontend.Consumers;

public class PaymentSucceededConsumer(
    IHubContext<OrderHub> hubContext,
    ILogger<PaymentSucceededConsumer> logger
    ) : IConsumer<PaymentSucceeded>
{
	public async Task Consume(ConsumeContext<PaymentSucceeded> context)
	{
		var message = context.Message;
		var customMessage = $"Payment Succeeded for OrderId: {message.OrderId}";
        await hubContext.Clients.All.SendAsync("PaymentSucceeded", customMessage);
        logger.LogInformation(customMessage);
    }
}