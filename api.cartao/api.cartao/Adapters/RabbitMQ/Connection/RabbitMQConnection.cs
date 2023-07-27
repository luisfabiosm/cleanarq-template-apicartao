using DnsClient.Internal;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace Adapters.RabbitMQ.Connection
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly RabbitMQSettings _settings;
        private ILogger<RabbitMQConnection> _logger;
        public IConnection _connection;

        public RabbitMQConnection(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<RabbitMQConnection>>();
            _settings = serviceProvider.GetRequiredService<IOptions<RabbitMQSettings>>().Value;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.Username,
                Password = _settings.Password,
                // DispatchConsumersAsync = true,
                RequestedConnectionTimeout = TimeSpan.FromMilliseconds(Convert.ToDouble(_settings.ConnectionTimeout))
            };

            if (!IsConnected) TryConnect();
        }


        public IConnection Connection => this._connection;


        public void TryConnect()
        {
            _logger.LogInformation("[EventBus] RabbitMQ Client tentando conectar...");

            var policy = RetryPolicy.Handle<SocketException>()
                                    .Or<BrokerUnreachableException>()
                                    .Or<Exception>()
                                    .WaitAndRetryForever(retryAttempt => TimeSpan.FromSeconds(_settings.Delay), (ex, time) =>
                                    {
                                        _logger.LogWarning($"[EventBus] RabbitMQ Client nao conectou depois de Timeout {time.TotalSeconds:n1}s ERRO: {ex.Message}");
                                    });

            policy.Execute(() =>
            {
                this._connection = _connectionFactory.CreateConnection();
            });

            if (IsConnected)
            {
                this.Connection.ConnectionShutdown += OnConnectionShutdown;
                this.Connection.CallbackException += OnCallbackException;
                this.Connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("------------------------------------------------------");
                _logger.LogInformation($"[EventBus] RabbitMQ Client conectado ao Servidor: {_connection.Endpoint.HostName}");
                _logger.LogInformation("------------------------------------------------------");
            }
            else
            {
                _logger.LogWarning("------------------------------------------------------");
                _logger.LogWarning($"[EventBus] Conexao RabbitMQ nao pode ser estabelecida");
                _logger.LogWarning("------------------------------------------------------");
            }
        }

        public bool IsConnected
        {
            get
            {
                return this.Connection != null && this.Connection.IsOpen;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("[EventBus] Conexao RabbitMQ indisponivel. Servico nao habilitado para transacionamento.");
            }
            return this.Connection.CreateModel();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            _logger.LogWarning("[EventBus] Conexao RabbitMQ finalizada. Tentando reconectar...");

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            _logger.LogWarning("[EventBus] Conexao RabbitMQ error. Tentando reconectar....");

            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            _logger.LogWarning("[EventBus] Conexao RabbitMQ finalizada. Tentando reconectar...");

            TryConnect();
        }


        public void Dispose()
        {


            try
            {
                this.Connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
    }
}
