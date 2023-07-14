using Adapters.RabbitMQ.Connection;
using Domain.Application.UseCases.AdicionarNovoCartao;
using Domain.Core.Contracts.Services;
using Domain.Core.Models.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Domain.Application.Services
{
    public class EsteiraNovoLimiteCartao : IEsteiraNovoLimiteCartao
    {

        private readonly IUseCaseNovoLimiteCartao _useCase;
        private readonly ILogger<IEsteiraNovoLimiteCartao> _logger;
        private readonly IRabbitMQConnection _rabbitConnection;
        private readonly IConnection _connection;
        internal IModel _channel;


        public EsteiraNovoLimiteCartao(IServiceProvider serviceProvider)
        {
            _useCase = serviceProvider.GetRequiredService<IUseCaseNovoLimiteCartao>();
            _rabbitConnection = serviceProvider.GetRequiredService<IRabbitMQConnection>();
            _connection = _rabbitConnection.Connection;
            _channel = _rabbitConnection.CreateModel();
        }


        public void AtualizarNovoLimiteCartao(CancellationToken stoppingToken)
        {
            new Task(() =>
            {
                try
                {
                    AssinarEvento("NOVO.LIMITE");
                }

                catch
                {
                    throw;
                }
            }).Start();

        }

        public virtual void AssinarEvento(string fila)
        {
            try
            {
                var _arguments = new Dictionary<string, object>();

                _channel.QueueDeclare(fila,
                                       durable: true,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body.ToArray());
                        var data = JsonSerializer.Deserialize<EventoAtualizarNovoLimite>(message);
                        Console.WriteLine($"PROCESSANDO MENSAGEM: {message}");
                        _useCase.Executar(new TransacaoNovoLimiteCartao(data));
                        Ack(ea.DeliveryTag);
                    }
                    catch(Exception ex)
                    {
                        Nack(ea.DeliveryTag, true);
                        Console.WriteLine($"ERRO {ex.Message} MENSAGEM  {Encoding.UTF8.GetString(ea.Body.ToArray())} retorna para fila.");
                        throw ex;
                    }
                };

                _channel.BasicConsume(queue: fila, autoAck: false, consumer: consumer);

            }
            catch (Exception e)
            {

                throw new Exception($"RABBITMQ{DateTime.Now:ddMMyyyy} ERRO[Consume]: {e.Message}");
            }
        }

        private void Ack(ulong DeliveryTag)
        {
            if (_channel.IsOpen)
                _channel.BasicAck(DeliveryTag, false);
        }

        private void Nack(ulong DeliveryTag, bool Requeued = false)
        {
            if (_channel.IsOpen)
                _channel.BasicNack(DeliveryTag, false, Requeued);
        }
    }
}
