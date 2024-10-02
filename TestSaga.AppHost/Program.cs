using System.Security.Cryptography;

var builder = DistributedApplication.CreateBuilder(args);

// Add infrastructures to the builder
var cache = builder.AddRedis("cache");

var messaging = builder.AddRabbitMQ("rabbitmq");

// Add projects to the builder
builder.AddProject<Projects.TestSaga_SagaOrchestrator>("testsaga-sagaorchestrator")
	.WithReference(messaging);

var orderService = builder.AddProject<Projects.TestSaga_OrderService>("testsaga-orderservice")
	.WithReference(messaging);

builder.AddProject<Projects.TestSaga_InventoryService>("testsaga-inventoryservice")
	.WithReference(messaging);

builder.AddProject<Projects.TestSaga_PaymentService>("testsaga-paymentservice")
	.WithReference(messaging);

builder.AddProject<Projects.TestSaga_ShippingService>("testsaga-shippingservice")
	.WithReference(messaging);

builder.AddProject<Projects.TestSaga_WebFrontend>("testsaga-webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(orderService)
    .WithReference(cache)
    .WithReference(messaging);

builder.Build().Run();
