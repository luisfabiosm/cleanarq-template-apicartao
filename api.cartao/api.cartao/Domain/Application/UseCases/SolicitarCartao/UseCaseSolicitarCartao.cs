using Domain.Core.Base;
using Domain.Core.Contracts.Services;
using Domain.Core.Models.Response;

namespace Domain.Application.UseCases.SolicitarCartao
{
    public class UseCaseSolicitarCartao : BaseUseCase, IUseCaseSolicitarCartao
    {
        private readonly IEsteiraService _esteira;

        public UseCaseSolicitarCartao(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _esteira = serviceProvider.GetRequiredService<IEsteiraService>();
        }



        public async Task<BaseReturn> Executar(TransacaoSolicitarCartao transacao)
        {

            try
            {
                await _esteira.PublicarPropostaSolicitacao(transacao);   
                
                return new BaseReturn().Sucesso(new SolicitarCartaoResponse(transacao.Protocolo));
            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }

        }
    }
}
