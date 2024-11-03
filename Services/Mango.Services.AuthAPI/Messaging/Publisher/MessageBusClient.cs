using System.Text;
using System.Text.Json;
using Mango.Services.AuthAPI.Models.Dto;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mango.Services.AuthAPI.Messaging.Publisher
{
    public class MessageBusClient : IMessageBusClient
    {

        private readonly IConfiguration _configuration;

        private readonly IConnection _connection;
        private readonly IChannel _channel;
        
        public  MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"], 
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {

                _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
                _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult(); 
                _channel.ExchangeDeclareAsync(exchange: "mangologs" , type: ExchangeType.Direct).GetAwaiter().GetResult();

                _connection.ConnectionShutdownAsync +=  RabbitMQ_ConnectionShutDown; 

            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could Not Connect to RabbitMQ Exception --> {e.Message}");
               
            }

        }

        public void publishNewLoginAttempt(LoginRequestPublishDto loginRequestPublishDto)
        {
            var message = JsonSerializer.Serialize(loginRequestPublishDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine("---> RabbitMQ Connection Open, Sending Message");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("---> RabbitMQ Connection Closed , Not Sending Message");
            }
            
            
        }


        private void SendMessage(string message , string routingKey = "auth.logs")
        {
            var body = Encoding.UTF8.GetBytes(message);
            
            _channel.BasicPublishAsync(exchange: "mangologs" , routingKey: routingKey ,  body: body ).GetAwaiter().GetResult();
            
            Console.WriteLine($" ---> We Have Sent {message}");
            
        }

        public void Dispose()
        {
            Console.WriteLine(" --> Messgae Bus Disposed ");

            if (_channel.IsOpen)
            {
                _channel.CloseAsync().GetAwaiter().GetResult();
                _connection.CloseAsync().GetAwaiter().GetResult();
            }
            
        }

        private Task RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs eventArgs)
        {
            Console.WriteLine("---> RabbitMQ Connection ShutDown ");
            return Task.CompletedTask; // Return a completed Task to satisfy async signature
        }

    }
}