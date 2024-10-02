# TestSaga Distributed System

This project is a demo of a Saga Orchestrator in a distributed system. The system demonstrates the orchestration of various services to handle order processing, including compensating transactions when a failure occurs. The technology stack includes .NET Aspire for distributed services orchestrating, RabbitMQ for the message bus, MassTransit for message handling, and SignalR for real-time web updates.

## System Overview

The system consists of the following services:

- **OrderService**: Handles order submissions.
- **InventoryService**: Checks and reserves inventory for orders.
- **PaymentService**: Processes payments for orders.
- **ShippingService**: Manages the shipping of orders.
- **SagaOrchestrator**: Monitors the flow of the order process and issues compensating transactions (CancelOrder) when a failure occurs.
- **WebFrontend**: Observes the flow and updates the UI in real-time.

## System Flow

1. **Order Submission**: The process starts with the `OrderService` publishing an [`OrderSubmitted`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A8%2C%22character%22%3A14%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") event.
2. **Inventory Check**: The [`InventoryService`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A3%2C%22character%22%3A19%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") consumes the [`OrderSubmitted`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A8%2C%22character%22%3A14%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") event, checks inventory, and publishes either an [`InventoryAvailable`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A63%2C%22character%22%3A23%7D%7D%2C%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.PaymentService%2FConsumers%2FInventoryAvailableConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A8%2C%22character%22%3A15%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") or [`InventoryUnavailable`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A69%2C%22character%22%3A19%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") event.
3. **Payment Processing**: If inventory is available, the [`PaymentService`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.PaymentService%2FConsumers%2FInventoryAvailableConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A3%2C%22character%22%3A19%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") processes the payment and publishes either a [`PaymentSucceeded`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.PaymentService%2FConsumers%2FInventoryAvailableConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A37%2C%22character%22%3A14%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") or [`PaymentFailed`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.PaymentService%2FConsumers%2FInventoryAvailableConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A43%2C%22character%22%3A13%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition") event.
4. **Shipping**: If the payment is successful, the `ShippingService` ships the order and publishes an `OrderShipped` event.
5. **Compensating Transactions**: If any step fails, the `SagaOrchestrator` issues a `CancelOrder` command to roll back the transaction.

## Technology Stack

- **.NET Aspire**: Used for orchestrating distributed services.
- **RabbitMQ**: Acts as the message bus for inter-service communication.
- **MassTransit**: Facilitates message handling and consumer configuration.
- **SignalR**: Provides real-time updates to the web frontend.

## Project Structure

```
TestSaga.sln
TestSaga.AppHost/
TestSaga.InventoryService/
TestSaga.OrderService/
TestSaga.PaymentService/
TestSaga.SagaOrchestrator/
TestSaga.ServiceDefaults/
TestSaga.ShippingService/
TestSaga.WebFrontend/
```

## Getting Started

### Prerequisites

- .NET SDK
- RabbitMQ
- Redis

### Building the Project

To build the project, run the following command in the root directory:

```sh
dotnet build
```

### Running the Project

To run the project, execute the following command in the root directory:

```sh
dotnet run --project TestSaga.AppHost
```
You can navigate to the URL of the Aspire Dashboard (https://localhost:17008/login?t=a91a4c7460b7ebee832b1f1e3c544ae7) to see the flow of the order process. The frontend web application is accessible from the Aspire Dashboard.

### Configuration

Configuration settings for the services can be found in the `appsettings.json` and `appsettings.Development.json` files located in the [`TestSaga.AppHost`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.AppHost%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "e:\source\repos\YummyZoom\test\TestSaga\TestSaga.AppHost") directory.

### Monitoring the Flow

You can observe the flow of the order process using the Aspire Dashboard. The dashboard provides a visual representation of the services and the loggings of the events.

The `WebFrontend` service provides a UI to observe the flow of the order process in real-time. It uses SignalR to update the UI based on the events published by the services.

## Demo Guide

To see the demo in action, follow these steps:

1. **Navigate to the Web Frontend**: Open your web browser and navigate to the frontend web application.
2. **Submit an Order**: Use the UI to submit an order. The inventory data is fixed in the OrderSubmittedConsumer.cs file. The available products and their quantities are:
   - [`product1`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A12%2C%22character%22%3A11%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition"): 10 units
   - [`product2`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A13%2C%22character%22%3A11%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition"): 5 units
   - [`product3`](command:_github.copilot.openSymbolFromReferences?%5B%22%22%2C%5B%7B%22uri%22%3A%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fe%3A%2Fsource%2Frepos%2FYummyZoom%2Ftest%2FTestSaga%2FTestSaga.InventoryService%2FConsumers%2FOrderSubmittedConsumer.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%2C%22pos%22%3A%7B%22line%22%3A14%2C%22character%22%3A11%7D%7D%5D%2C%22bd3fffde-ef8d-46fd-97d1-03bc86bb937b%22%5D "Go to definition"): 3 units
3. **Inventory Check**: If the quantity is out of range or the product is not found, the saga will roll back.
4. **Payment Processing**: In the InventoryAvailableConsumer.cs file, there is a 30% chance of payment failure, which will cause the saga to roll back.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.

## Acknowledgements

Special thanks to the developers and contributors of .NET Aspire, RabbitMQ, MassTransit, and SignalR for their excellent tools and libraries.