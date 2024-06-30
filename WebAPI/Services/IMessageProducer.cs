using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace WebAPI.Services;

public interface IMessageProducer
{
    public void SendingMessage<T>(T message);
}

public class MessageProducer : IMessageProducer
{

    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/",
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare
        (
            queue: "books",
            durable: true,
            exclusive: false
        );

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish
        (
            exchange: "",
            routingKey: "books",
            body: body
        );
    }
}