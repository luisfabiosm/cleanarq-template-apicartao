using Domain.Core.Base;

namespace Domain.Application.UseCases.NovoLimiteCartao
{
    public interface IUseNovoLimiteCartao
    {
        Task<BaseReturn> Executar(TransacaoNovoLimiteCartao transacao);
    }
}
