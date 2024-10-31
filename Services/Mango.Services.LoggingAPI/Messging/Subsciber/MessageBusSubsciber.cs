using System.Text;
using System.Text.Json;
using Mango.Services.LoggingAPI.Models.Dto;
using Mango.Services.LoggingAPI.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mango.Services.LoggingAPI.Messging.Subsciber
{
    public class MessageBusSubsciber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory; 
        private  IConnection _connection;
        private  IChannel _channel;
        private string _queueName;

        public MessageBusSubsciber(IConfiguration configuration,IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"], 
                Port = int.Parse(_configuration["RabbitMQPort"]
                )};

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout).GetAwaiter().GetResult();
            _queueName = _channel.QueueDeclareAsync().GetAwaiter().GetResult().QueueName;
            _channel.QueueBindAsync(queue:_queueName , exchange:"trigger", routingKey:"").GetAwaiter().GetResult();
            
            Console.WriteLine("--> Listenting on the Message Bus...");
            
            _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShitdown;
        }

        
        
        private Task RabbitMQ_ConnectionShitdown(object sender, ShutdownEventArgs @event)
        {
            Console.WriteLine("--> Connection Shutdown");
            return Task.CompletedTask;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
          stoppingToken.ThrowIfCancellationRequested();
          
          var consumer = new AsyncEventingBasicConsumer(_channel);

          consumer.ReceivedAsync += async (sender, ea) =>
          {
              Console.WriteLine("--> Event Received!");

              var body = ea.Body.ToArray();
              var notificationMessage = Encoding.UTF8.GetString(body);

              using (var scope = _serviceScopeFactory.CreateScope())
              {
                  var loginDto = JsonSerializer.Deserialize<LoginFromAuthDto>(notificationMessage);
                  var logService = scope.ServiceProvider.GetRequiredService<ILogService>();
                  await logService.addToLogsFromPublisher(loginDto);
              }
          };
          
          _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
          
          
          
          return Task.CompletedTask;
        }
    }
}