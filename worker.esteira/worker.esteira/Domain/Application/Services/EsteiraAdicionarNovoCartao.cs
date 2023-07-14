using Domain.Application.UseCases.AdicionarNovoCartao;
using Domain.Core.Contracts.Services;
using Domain.Core.Models.Events;


namespace Domain.Application.Services
{
    public class EsteiraAdicionarNovoCartao : IEsteiraAdicionarNovoCartao
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IUseCaseAdicionarNovoCartao _useCase;

        public EsteiraAdicionarNovoCartao(IServiceProvider serviceProvider)
        {
            _rabbitMQService = serviceProvider.GetRequiredService<IRabbitMQService>();
            _useCase = serviceProvider.GetRequiredService<IUseCaseAdicionarNovoCartao>();
        }
        public  void AssinarAdicionarCartao(CancellationToken stoppingToken)
        {
            try
            {

                _rabbitMQService.AssinarEvento<EventoCriarNovoCartao>("SOLICITACAO.CARTAO", "AMBIENTE.CEE");
                _rabbitMQService.ReceberExcecao += async (exception, deliveryTag) =>
                {
                    Console.WriteLine(exception.Message);
                    throw exception;
                };

                _rabbitMQService.ReceberMensagem += async (message, deliveryTag) =>
                {
                    try
                    {
                        Console.WriteLine($"Mensagem {message}");

                        var ret = await _useCase.Executar(new TransacaoAdicionarNovoCartao(message));


                        if (ret.status != Core.Enums.EnumReturnStatus.SUCESSO)
                            _rabbitMQService.Nack(deliveryTag, true);

                        _rabbitMQService.Ack(deliveryTag);

                        Console.WriteLine($"Mensagem Processada: {message}");

                    }
                    catch (Exception ex)
                    {
                        Console.Write($"ERRO: {ex.Message}");
                        _rabbitMQService.Nack(deliveryTag, true);
                        throw;
                    }
                };
            }

            catch
            {
                throw;
            }
        }
    }
}
