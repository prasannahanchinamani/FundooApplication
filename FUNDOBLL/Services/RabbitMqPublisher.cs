using ModelLayer.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BusinessLogicLayer.Services
{
    public class RabbitMqPublisher
    {
        public void PublishEmail(EmailMessageDTO message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "fundoo.email.queue",
                durable:true,
                exclusive:false,
                autoDelete:false
                );

            var json = JsonSerializer.Serialize(message);
           var body=Encoding.UTF8.GetBytes(json);

       var prop=channel.CreateBasicProperties();
            prop.Persistent=true;

            channel.BasicPublish(
                exchange: "",
                routingKey: "fundoo.email.queue",
                basicProperties: prop,
                body: body
            );
        }
    }
}
