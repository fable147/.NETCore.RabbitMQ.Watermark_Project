using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace RabbitMQ.Watermark.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }
        public void Publish(productImageCreatedEvent productImageCreatedEvent) 
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent= true;

            channel.BasicPublish(RabbitMQClientService.exchangeName, RabbitMQClientService.routingName, basicProperties: properties, body: bodyByte);
        }
    }
}
