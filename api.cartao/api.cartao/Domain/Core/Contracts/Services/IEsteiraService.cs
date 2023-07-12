using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;

namespace Domain.Core.Contracts.Services
{
    public interface IEsteiraService
    {

        ValueTask PublicarPropostaSolicitacao(TransacaoSolicitarCartao transacao);

        ValueTask PublicarPropostaNovoLimite(TransacaoNovoLimiteCartao transacao);

    }
}
