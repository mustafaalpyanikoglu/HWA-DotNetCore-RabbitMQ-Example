// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to the ticketing service");

var factory = new ConnectionFactory()
{
  HostName = "localhost",
  UserName = "guest",
  Password = "guest",
};

var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare
(
    queue: "books",
    durable: true,
    exclusive: false
);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, eventArgs) =>
{
  var body = eventArgs.Body.ToArray();
  var message = Encoding.UTF8.GetString(body);
  Console.WriteLine($"New ticket processing is initiated for - {message}");
};

channel.BasicConsume
(
    queue: "books",
    autoAck: true,
    consumer: consumer
);

Console.ReadKey();