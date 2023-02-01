using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace RabbitMQ.Watermark.Services
{
    public class RabbitMQClientService:IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string exchangeName = "ImageDirectExcange";
        public static string routingName = "watermark-route-image";
        public static string queueName = "watermark-image-queue";

        private readonly ILogger<RabbitMQClientService> _logger;
        //Bu connection factory oluşturma işini DI Container yapacak yani singleton olacak bu servis     
        public RabbitMQClientService(ILogger<RabbitMQClientService> logger, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public IModel Connect()
        {
            _connection= _connectionFactory.CreateConnection();

            if (_channel != null)
            {
                return _channel;
            }
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchangeName, type: "direct",true,false,null);

            _channel.QueueDeclare(queueName, true, false,false, null);

            _channel.QueueBind(queueName,exchangeName,routingName,null);

            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu.");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _channel = default;
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ ile olan bağlantı koparıldı.");
        }
    }
}
