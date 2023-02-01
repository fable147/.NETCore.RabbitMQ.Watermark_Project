//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Drawing;
//using System.Text;
//using System.Text.Json;

//namespace RabbitMQ.Watermark.Services
//{
//    public class ImageWatermarkProcessBackgroundService : BackgroundService
//    {
//        private readonly RabbitMQClientService _rabbitMQClientService;
//        private readonly ILogger<ImageWatermarkProcessBackgroundService> _logger;
//        private IModel _channel;

//        public ImageWatermarkProcessBackgroundService(RabbitMQClientService rabbitMQClientService, ILogger<ImageWatermarkProcessBackgroundService> logger)
//        {
//            _rabbitMQClientService = rabbitMQClientService;
//            _logger = logger;
//        }

//        public override Task StartAsync(CancellationToken cancellationToken)
//        {
//            _channel = _rabbitMQClientService.Connect();
//            _channel.BasicQos(0, 1, false);

//            return base.StartAsync(cancellationToken);
//        }
//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            Thread.Sleep(20000);
//            var consumer = new AsyncEventingBasicConsumer(_channel);

//            _channel.BasicConsume(RabbitMQClientService.queueName, false, consumer);
            

//            consumer.Received += Consumer_Received;

//            return Task.CompletedTask;






//            throw new NotImplementedException();
//        }

//        private Task Consumer_Received(object? sender, BasicDeliverEventArgs e)
//        {
//                _logger.LogInformation("Consume İşlemi Başladı");
//            try
//            {
//                var my_str = "www.mysite.com";
//                var productImageCreatedEvent = JsonSerializer.Deserialize<productImageCreatedEvent>(Encoding.UTF8.GetString(e.Body.Span.ToArray()));

//                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", productImageCreatedEvent.imageName);

//                using var img = Image.FromFile(path);

//                using var graphic = Graphics.FromImage(img);

//                var font = new Font(FontFamily.GenericSansSerif, 32, FontStyle.Bold, GraphicsUnit.Pixel);

//                var textSize = graphic.MeasureString(my_str, font);

//                var color = Color.FromArgb(128, 255, 255, 255);
//                var brush = new SolidBrush(color);

//                var position = new Point(img.Width - ((int)textSize.Width + 30), img.Height - ((int)textSize.Height + 30));

//                graphic.DrawString(my_str, font, brush, position);
//                var watermark_path = "wwwroot\\images\\watermarks\\" + productImageCreatedEvent.imageName;

//                img.Save(watermark_path);

//                img.Dispose();
//                graphic.Dispose();
//                _channel.BasicAck(e.DeliveryTag, false);
                
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError("Watermark ekleme aşamasında bir hata meydan geldi" + ex);
//            }

//            return Task.CompletedTask;






//        }

//        public override Task StopAsync(CancellationToken cancellationToken)
//        {
//            return base.StopAsync(cancellationToken);
//        }
//    }
//}
