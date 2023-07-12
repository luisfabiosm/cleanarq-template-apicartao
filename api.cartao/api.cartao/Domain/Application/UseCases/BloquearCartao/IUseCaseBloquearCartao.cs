using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Core.Base;

namespace Domain.Application.UseCases.BloquearCartao
{
    public interface IUseCaseBloquearCartao
    {
        Task<BaseReturn> Executar(TransacaoBloquearCartao transacao);
    }
}
