using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.Application.UseCases.ConsultarBloqueioCartao
{
    public class UseCaseConsultarBloqueioCartao : BaseUseCase, IUseCaseConsultarBloqueioCartao
    {

        public UseCaseConsultarBloqueioCartao(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public async Task<BaseReturn> Executar(TransacaoConsultarBloqueioCartao transacao)
        {
            try
            {

                var ret = await _repo.ConsultarBloqueioCartao(transacao);

                if (ret is null)
                    return new BaseReturn().BussinesException("Bloqueio inexistente."); ;

                return new BaseReturn().Sucesso(new ConsultarBloqueioCartaoResponse(ret));

            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }
        }
    }
}
