using Domain.Core.Base;


namespace Domain.Application.UseCases.AdicionarNovoCartao
{
    public class UseCaseNovoLimiteCartao : BaseUseCase, IUseCaseNovoLimiteCartao
    {

        public UseCaseNovoLimiteCartao(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        public async Task<BaseReturn> Executar(TransacaoNovoLimiteCartao transacao)
        {
            throw new NotImplementedException();
        }
    }
}
