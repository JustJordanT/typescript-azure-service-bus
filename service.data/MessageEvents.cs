using System.Text.Json.Serialization;

namespace service.data;

public class Events
{
    public record MessageEvent(
        Guid Id,
    string Message,
    DateTime Timestamp,
        string Service,
        string Version,
        string Environment,
        string Host)
    {
        [JsonConstructor]
        public MessageEvent(
            string Message,
            DateTime Timestamp,
            string Service,
            string Version,
            string Environment,
            string Host = "localhost") : this(Guid.NewGuid(), Message, Timestamp, Service, Version, Environment, Host)
        { }
    }

    public record MessageEventRequest(
        string Message
    );

    public record MessageEventResponse(
        Guid Id,
        string Message,
        DateTime Timestamp,
        string Service,
        string Version,
        string Environment,
        string Host
    );
}
