using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using ModelLayer.DTO;
using BusinessLogicLayer.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var emailService = new SmtpEmailService(configuration);

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "fundoo.email.queue",
    durable: true,
    exclusive: false,
    autoDelete: false
);

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, e) =>
{
    var json = Encoding.UTF8.GetString(e.Body.ToArray());
    var email = JsonSerializer.Deserialize<EmailMessageDTO>(json);

    Console.WriteLine($" Sending email to {email.To}");

    emailService.Send(
        email.To,
        email.Subject,
        email.Body
    );

    channel.BasicAck(e.DeliveryTag, false);

    Console.WriteLine(" Email sent & message removed from queue");
};

channel.BasicConsume(
    queue: "fundoo.email.queue",
    autoAck: false,
    consumer: consumer
);

Console.WriteLine(" Email Worker is running...");
Console.ReadLine();
