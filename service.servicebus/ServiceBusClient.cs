using System.Text.Json;
using Azure.Messaging.ServiceBus;
using service.data;

namespace service.servicebus;

public interface IServiceBusClient
{
    Task SendMessageAsync(service.data.Events.MessageEventRequest message, string service, string version, string environment, string host);
    Task<service.data.Events.MessageEventResponse> ReceiveMessageAsync();
}

public class ServiceBusClient : IServiceBusClient, IAsyncDisposable
{
    private readonly Azure.Messaging.ServiceBus.ServiceBusClient _client;
    private readonly string _queueName = "master";

    public ServiceBusClient(string connectionString)
    {
        _client = new Azure.Messaging.ServiceBus.ServiceBusClient(connectionString);
    }

    public async Task SendMessageAsync(service.data.Events.MessageEventRequest message, string service, string version, string environment, string host)
    {
        ServiceBusSender sender = _client.CreateSender(_queueName);

        var request = new Events.MessageEvent(
            Message: message.Message,
            Timestamp: DateTime.UtcNow,
            Service: service,
            Version: version,
            Environment: environment,
            Host: host
        );

        await sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(request)));
    }

    public async Task<Events.MessageEventResponse> ReceiveMessageAsync()
    {
        var timeout = TimeSpan.FromSeconds(5);
        ServiceBusReceiver receiver = _client.CreateReceiver(_queueName);

        try
        {
            ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync(timeout);

            if (receivedMessage == null)
            {
                throw new TimeoutException($"Timeout waiting for message, no message received in time span: {timeout}");
            }

            var response = JsonSerializer.Deserialize<Events.MessageEventResponse>(receivedMessage.Body.ToString());

            await receiver.CompleteMessageAsync(receivedMessage);

            return response ?? throw new InvalidOperationException("Error deserializing message");
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Error receiving message", ex);
        }
        finally
        {
            await receiver.DisposeAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_client != null)
        {
            await _client.DisposeAsync();
        }
    }
}

