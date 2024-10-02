using MassTransit;
using TestSaga.InventoryService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
	// Register the consumer
	x.AddConsumer<OrderSubmittedConsumer>();

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

		cfg.ReceiveEndpoint("inventory-service", e =>
		{
			// Configure consumer to handle OrderSubmitted events
			e.ConfigureConsumer<OrderSubmittedConsumer>(context);
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

app.MapGet("/", () => "Inventory Service is running...")
	.WithName("GetRoot")
	.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
