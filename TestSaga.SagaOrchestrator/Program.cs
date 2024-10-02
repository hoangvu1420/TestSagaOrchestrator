using MassTransit;
using TestSaga.SagaOrchestrator;
using TestSaga.SagaOrchestrator.Consumers;
using TestSaga.SagaOrchestrator.SagaStates;
using TestSaga.SagaOrchestrator.StateMachines;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
	// Add Saga State Machine and In-Memory State Repository
	x.AddSagaStateMachine<OrderStateMachine, OrderSagaState>()
		.InMemoryRepository(); // Can be replaced with a persistent repository (e.g., EntityFramework)

	// Add Consumers for each event
	x.AddConsumer<OrderSubmittedConsumer>();
	x.AddConsumer<InventoryAvailableConsumer>();
	x.AddConsumer<InventoryUnavailableConsumer>();
	x.AddConsumer<PaymentSucceededConsumer>();
	x.AddConsumer<PaymentFailedConsumer>();
	x.AddConsumer<OrderShippedConsumer>();
    x.AddConsumer<OrderCancelledConsumer>();

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

		cfg.ReceiveEndpoint("saga-orchestrator", e =>
		{
			// Configure endpoints to handle saga events
			e.ConfigureSaga<OrderSagaState>(context);

			// Configure consumers to handle events
			e.ConfigureConsumer<OrderSubmittedConsumer>(context);
			e.ConfigureConsumer<InventoryAvailableConsumer>(context);
			e.ConfigureConsumer<InventoryUnavailableConsumer>(context);
			e.ConfigureConsumer<PaymentSucceededConsumer>(context);
			e.ConfigureConsumer<PaymentFailedConsumer>(context);
			e.ConfigureConsumer<OrderShippedConsumer>(context);
            e.ConfigureConsumer<OrderCancelledConsumer>(context);
        });
	});
});

var host = builder.Build();
host.Run();
