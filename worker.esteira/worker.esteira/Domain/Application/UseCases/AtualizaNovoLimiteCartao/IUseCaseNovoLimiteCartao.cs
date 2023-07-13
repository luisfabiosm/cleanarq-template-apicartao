using Domain.Core.Base;


namespace Domain.Application.UseCases.AdicionarNovoCartao
{
    public interface IUseCaseNovoLimiteCartao
    {
        Task<BaseReturn> Executar(TransacaoNovoLimiteCartao transacao);
    }
}
