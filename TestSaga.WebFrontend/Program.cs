using MassTransit;
using TestSaga.WebFrontend.Components;
using TestSaga.WebFrontend.Consumers;
using TestSaga.WebFrontend.Services;
using TestSaga.WebFrontend.Hubs; // Add this line to include the Hubs namespace

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add SignalR services
builder.Services.AddSignalR();

// Add the OrderServiceClient
builder.Services.AddScoped<OrderServiceClient>();

builder.Services.AddHttpClient<OrderServiceClient>(client =>
{
    client.BaseAddress = new Uri("https+http://testsaga-orderservice");
});

builder.Services.AddMassTransit(x =>
{
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

        cfg.ReceiveEndpoint("web-frontend", e =>
        {
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

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map the SignalR hub
app.MapHub<OrderHub>("/orderHub");

app.MapDefaultEndpoints();

app.Run();