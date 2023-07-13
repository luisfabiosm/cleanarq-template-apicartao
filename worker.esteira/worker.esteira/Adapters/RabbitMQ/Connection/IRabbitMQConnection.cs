using RabbitMQ.Client;

namespace Adapters.RabbitMQ.Connection
{
    public interface IRabbitMQConnection
    {
        bool IsConnected { get; }
        IConnection Connection { get; }
        IModel CreateModel();
        void Dispose();

    }
}
