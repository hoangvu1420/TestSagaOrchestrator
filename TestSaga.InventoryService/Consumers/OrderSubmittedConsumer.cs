using MassTransit;
using TestSaga.Events;

namespace TestSaga.InventoryService.Consumers;

public class OrderSubmittedConsumer(
    IPublishEndpoint publishEndpoint,
    ILogger<OrderSubmittedConsumer> logger
) : IConsumer<OrderSubmitted>
{
    private readonly Dictionary<string, int> _inventory = new()
    {
        { "product1", 10 },
        { "product2", 5 },
        { "product3", 3 }
    };

    public async Task Consume(ConsumeContext<OrderSubmitted> context)
    {
        var order = context.Message;

        try
        {
            // Simulate inventory check (in a real scenario, you would query your inventory database)
            var inventoryEvent = await CheckInventory(order.ProductId, order.Quantity, order.OrderId);

            // Publish the appropriate inventory event
            await publishEndpoint.Publish(inventoryEvent);
            logger.LogInformation($"Published {inventoryEvent.GetType().Name} event for order: {order.OrderId}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error processing order: {order.OrderId}");
            throw;
        }
    }

    // Dummy function for inventory check
    private async Task<object> CheckInventory(string productId, int quantity, Guid orderId)
    {
        // Simulate the product availability logic
        logger.LogInformation($"Checking inventory for product {productId}...");

        await Task.Delay(2000); // Simulate inventory check time

        bool isAvailable;
        string reason;

        if (_inventory.TryGetValue(productId, out var availableQuantity))
        {
            isAvailable = availableQuantity >= quantity;
            reason = "Insufficient inventory";
        }
        else
        {
            isAvailable = false;
            reason = "Product not found";
        }

        logger.LogInformation($"Product {productId} is {(isAvailable ? "available" : "not available")}");

        if (isAvailable)
        {
            return new InventoryAvailable
            {
                OrderId = orderId
            };
        }

        return new InventoryUnavailable
        {
            OrderId = orderId,
            Reason = reason
        };
    }
}
