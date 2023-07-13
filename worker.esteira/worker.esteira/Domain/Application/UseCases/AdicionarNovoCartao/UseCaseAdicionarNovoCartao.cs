using Domain.Core.Base;


namespace Domain.Application.UseCases.AdicionarNovoCartao
{

    public class UseCaseAdicionarNovoCartao : BaseUseCase, IUseCaseAdicionarNovoCartao
    {

        public UseCaseAdicionarNovoCartao(IServiceProvider serviceProvider): base(serviceProvider)
        {
            
        }
        public async Task<BaseReturn> Executar(TransacaoAdicionarNovoCartao transacao)
        {
            try
            {
                return await _repo.AdicionarCartaoSolicitado(transacao);

            }
            catch (Exception ex)
            {
                return new BaseReturn().ErroSistema(ex);
            }
        }
    }
}
