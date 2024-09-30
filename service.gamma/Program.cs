using Azure;
using Azure.Messaging.ServiceBus;
using service.data;
using service.servicebus;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IServiceBusClient>(sp =>
    new service.servicebus.ServiceBusClient(builder.Configuration["ServiceBus:ConnectionString"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/message", async (IServiceBusClient serviceBusClient, HttpContext httpContext) =>
{
    try
    {
        var message = await serviceBusClient.ReceiveMessageAsync();
        if (message == null)
        {
            return Results.NoContent();
        }
        return Results.Ok(message);
    }
    catch (TimeoutException)
    {
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            statusCode: StatusCodes.Status500InternalServerError,
            title: "An error occurred while processing your request"
        );
    }
});

app.Run();