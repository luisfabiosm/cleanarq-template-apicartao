using Domain.Core.Base;


namespace Domain.Application.UseCases.AdicionarNovoCartao
{
    public interface IUseCaseAdicionarNovoCartao
    {
        Task<BaseReturn> Executar(TransacaoAdicionarNovoCartao transacao);
    }
}
