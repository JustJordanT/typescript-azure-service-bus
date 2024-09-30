using Azure.Messaging.ServiceBus;
using Azure.Identity;
using System.Text.Json;
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/message", async (IServiceBusClient serviceBusClient, Events.MessageEventRequest request) =>
{
    await serviceBusClient.SendMessageAsync(request, "service.alpha", "1.0.0", "development", "localhost");
    return Results.Ok();
});

app.Run();