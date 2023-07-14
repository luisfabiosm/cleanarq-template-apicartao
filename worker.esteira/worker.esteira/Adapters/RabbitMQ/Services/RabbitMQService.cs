using Adapters.RabbitMQ.Connection;
using Domain.Core.Contracts.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Adapters.RabbitMQ.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        public event Action<dynamic, ulong> ReceberMensagem;
        public event Action<Exception, ulong> ReceberExcecao;

        private readonly ILogger<IRabbitMQService> _logger;

        private readonly IRabbitMQConnection _rabbitConnection;
        private readonly IConnection _connection;
        internal IModel _channel;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            _rabbitConnection = serviceProvider.GetRequiredService<IRabbitMQConnection>();
            _connection = _rabbitConnection.Connection;
            _channel = _rabbitConnection.CreateModel();
            _logger = serviceProvider.GetRequiredService<ILogger<IRabbitMQService>>();
        }


        public async Task PublicarEvento<T>(T @object, string exchange, string fila)
        {
            try
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(@object));

                _channel.BasicQos(0, 1, true);
                _channel.ExchangeDeclare(exchange, ExchangeType.Direct);

                _channel.QueueDeclare(
                  queue: fila,
                  durable: true,
                  exclusive: false,
                  autoDelete: false);
                _channel.QueueBind(fila, exchange, "directexchange_key");

                IBasicProperties properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                _channel.BasicPublish(exchange: exchange,
                                     routingKey: "directexchange_key",
                                     basicProperties: properties,
                                     body: Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(@object)));
            }
            catch (Exception e)
            {
                throw new Exception($"RABBITMQ{DateTime.Now:ddMMyyyy} ERRO[Publish]: {e.Message}");
            }

        }


        public void AssinarEvento<T>(string fila)
        {
            try
            {
                var _arguments = new Dictionary<string, object>();

               // _channel.QueueBind(fila,exchange,"");

                //Normal Queue
                _channel.QueueDeclare(fila,
                                       durable: true,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: _arguments);

                _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        var data = JsonSerializer.Deserialize<T>(message);
                        ReceberMensagem?.Invoke(data, ea.DeliveryTag);
                    }
                    catch (Exception ex)
                    {
                        ReceberExcecao?.Invoke(ex, ea.DeliveryTag);
                    }
                };

                _channel.BasicConsume(queue: fila, autoAck: true, consumer: consumer);

            }
            catch (Exception e)
            {
                throw new Exception($"RABBITMQ{DateTime.Now:ddMMyyyy} ERRO[Consume]: {e.Message}");
            }
        }


        public void Ack(ulong DeliveryTag)
        {
            if (_channel.IsOpen)
                _channel.BasicAck(DeliveryTag, false);
        }

        public void Nack(ulong DeliveryTag, bool Requeued = false)
        {
            if (_channel.IsOpen)
                _channel.BasicNack(DeliveryTag, false, Requeued);
        }
    }


}
