using Domain.Core.Base;

namespace Domain.Application.UseCases.SolicitarCartao
{
    public interface IUseCaseSolicitarCartao
    {

        Task<BaseReturn> Executar(TransacaoSolicitarCartao transacao);

    }
}
