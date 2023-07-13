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
            try
            {
                var ret = await _repo.ConsultarCartao(transacao.NumeroCartao);

                if (ret is null)
                    return new BaseReturn().BussinesException("Cartão inexistente.");


                return await _repo.AtualizarLimiteCartao(transacao);
            }
            catch (Exception ex)
            {
                return new BaseReturn().ErroSistema(ex);
            }
        }
    }
}
