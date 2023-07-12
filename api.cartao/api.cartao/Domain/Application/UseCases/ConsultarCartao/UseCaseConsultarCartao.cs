using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.Application.UseCases.ConsultarCartao
{
    public class UseCaseConsultarCartao : BaseUseCase, IUseCaseConsultarCartao
    {

        public UseCaseConsultarCartao(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public async Task<BaseReturn> Executar(TransacaoConsultarCartao transacao)
        {
            try
            {
                var ret = await _repo.ConsultarCartao(transacao);

                if (ret is null)
                    return new BaseReturn().BussinesException("Cartão inexistente."); ;

                return new BaseReturn().Sucesso(new ConsultarCartaoResponse(ret));

            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }
        }
    }
}
