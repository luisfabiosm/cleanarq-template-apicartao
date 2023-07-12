using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Contracts.Services;

namespace Domain.Application.Services
{
    public class EsteiraService : IEsteiraService
    {
        private readonly ILogger<IEsteiraService> _logger;
        private readonly IRabbitMQService _rabbitMQService;
        

        public EsteiraService(IServiceProvider serviceProvider)
        {
            _rabbitMQService = serviceProvider.GetService<IRabbitMQService>();
            _logger = serviceProvider.GetRequiredService<ILogger<IEsteiraService>>();
        }

        public async ValueTask PublicarPropostaNovoLimite(TransacaoNovoLimiteCartao transacao)
        {
            try
            {
                await _rabbitMQService.PublicarEvento(transacao, "AMBIENTE.CEE", "NOVO.LIMITE");
            }
            catch
            {
                throw;
            }
        }

        public async ValueTask PublicarPropostaSolicitacao(TransacaoSolicitarCartao transacao)
        {
            try
            {
                await _rabbitMQService.PublicarEvento(transacao, "AMBIENTE.CEE", "SOLICITACAO.CARTAO");
            }
            catch
            {
                throw;
            }
        }
    }
}
