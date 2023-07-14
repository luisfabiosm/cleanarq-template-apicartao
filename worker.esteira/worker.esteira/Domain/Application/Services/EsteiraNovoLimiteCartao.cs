using Domain.Application.UseCases.AdicionarNovoCartao;
using Domain.Core.Contracts.Services;
using Domain.Core.Models.Events;

namespace Domain.Application.Services
{
    public class EsteiraNovoLimiteCartao : IEsteiraNovoLimiteCartao
    {

        private readonly IRabbitMQService _rabbitMQService;
        private readonly IUseCaseNovoLimiteCartao _useCase;

        public EsteiraNovoLimiteCartao(IServiceProvider serviceProvider)
        {
            _rabbitMQService = serviceProvider.GetRequiredService<IRabbitMQService>();
            _useCase = serviceProvider.GetRequiredService<IUseCaseNovoLimiteCartao>();

        }


        public void AtualizarNovoLimiteCartao(CancellationToken stoppingToken)
        {

            try
            {

                _rabbitMQService.AssinarEvento<EventoAtualizarNovoLimite>("NOVO.LIMITE", "AMBIENTE.CEE");
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

                        var ret = await _useCase.Executar(new TransacaoNovoLimiteCartao(message));


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
