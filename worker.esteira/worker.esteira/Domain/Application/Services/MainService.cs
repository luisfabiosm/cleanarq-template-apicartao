using Domain.Application.UseCases.AdicionarNovoCartao;
using Domain.Core.Contracts.Services;
using Domain.Core.Models.Request;
using System.Diagnostics;
using System.Text.Json;
using worker.esteira.Microservice;

namespace Domain.Application.Services
{
    internal class MainService : IMainService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IUseCaseAdicionarNovoCartao _useCaseNovoCartao;
        private readonly IUseCaseNovoLimiteCartao _useCaseNovoLimiteCartao;
        private readonly ILogger<MainService> _logger;
        public MainService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<MainService>>();
            _rabbitMQService = serviceProvider.GetRequiredService<IRabbitMQService>();
            _useCaseNovoCartao = serviceProvider.GetRequiredService<IUseCaseAdicionarNovoCartao>();
            _useCaseNovoLimiteCartao = serviceProvider.GetRequiredService<IUseCaseNovoLimiteCartao>();
        }
        public async Task IniciarService(CancellationToken stoppingToken)
        {

            //Assinar fila novo cartao
            await EsteriaAdicionarCartao();

            //Assinar fila novo limite
            await EsteriaNovoLimiteCartao();

        }




        private async Task EsteriaAdicionarCartao()
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
                        
                        var ret = await _useCaseNovoCartao.Executar(new TransacaoAdicionarNovoCartao(message));


                        if (ret.status != Core.Enums.EnumReturnStatus.SUCESSO)
                            _rabbitMQService.Nack(deliveryTag,true);

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


        private async Task EsteriaNovoLimiteCartao()
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

                        var ret = await _useCaseNovoLimiteCartao.Executar(new TransacaoNovoLimiteCartao(message));


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
