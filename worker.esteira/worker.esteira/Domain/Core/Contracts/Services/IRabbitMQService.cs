using RabbitMQ.Client.Events;

namespace Domain.Core.Contracts.Services
{
    public interface IRabbitMQService
    {
        Task PublicarEvento<T>(T @object, string exchange, string fila);

        void AssinarEvento<T>(string fila);

       // void AssinarEvento(EventHandler<BasicDeliverEventArgs> func, string Queue = null);


        event Action<dynamic, ulong> ReceberMensagem;


        event Action<Exception, ulong> ReceberExcecao;


        void Ack(ulong DeliveryTag);

        void Nack(ulong DeliveryTag, bool Requeued = true);
    }
}
