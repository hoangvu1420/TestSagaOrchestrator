using MassTransit;
using TestSaga.ShippingService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
	// Add Consumer for PaymentSucceeded
	x.AddConsumer<PaymentSucceededConsumer>();

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

		cfg.ReceiveEndpoint("shipping-service", e =>
		{
			// Configure consumer to handle PaymentSucceeded events
			e.ConfigureConsumer<PaymentSucceededConsumer>(context);
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

app.MapGet("/", () => "Payment Service is running...")
	.WithName("GetRoot")
	.WithOpenApi();

app.Run();
