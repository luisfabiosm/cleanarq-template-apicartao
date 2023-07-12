namespace Domain.Core.Contracts.Services
{
    public interface IRabbitMQService
    {
        Task PublicarEvento<T>(T @object, string exchange, string fila);
        void AssinarEvento<T>(string fila, string exchange = null);

        event Action<dynamic, ulong> ReceberMenssagem;

        event Action<Exception, ulong> ReceberExcecao;

        void Ack(ulong DeliveryTag);
        void Nack(ulong DeliveryTag, bool Requeued = true);
    }
}
