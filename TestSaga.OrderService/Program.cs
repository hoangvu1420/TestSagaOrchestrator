using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TestSaga.Events;
using TestSaga.OrderService.Consumers;
using TestSaga.OrderService.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<CancelOrderConsumer>();

	// Configure RabbitMQ
	x.UsingRabbitMq((context, cfg) =>
	{
		//cfg.Host("rabbitmq", h =>
		//{
		//	h.Username("guest");
		//	h.Password("guest");
		//});

		var configuration = context.GetRequiredService<IConfiguration>();
		var host = configuration.GetConnectionString("rabbitmq");
		cfg.Host(host);

		cfg.ReceiveEndpoint("order-service", e =>
		{
			e.ConfigureConsumer<CancelOrderConsumer>(context);
		});
	});
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Order Service is running...")
.WithName("GetRoot")
.WithOpenApi();

// Define the Minimal API route for submitting an order
app.MapPost("/api/orders", async ([FromBody] OrderRequest request, IPublishEndpoint publishEndpoint, ILogger<Program> logger) =>
{
	var orderId = Guid.NewGuid();

	await Task.Delay(1000); // Simulate processing time

	// Publish the OrderSubmitted event to RabbitMQ
	await publishEndpoint.Publish(new OrderSubmitted
	{
		OrderId = orderId,
		ProductId = request.ProductId,
		Quantity = request.Quantity,
		OrderDate = DateTime.UtcNow
	});

	logger.LogInformation("Published OrderSubmitted event for order: {OrderId}", orderId);

	return Results.Ok(new { OrderId = orderId });
}).WithName("SubmitOrder")
.WithOpenApi();

app.Run();
