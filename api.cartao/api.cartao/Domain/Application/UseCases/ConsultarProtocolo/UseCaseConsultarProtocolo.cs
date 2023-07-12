using Domain.Core.Base;
using Domain.Core.Models.Response;

namespace Domain.Application.UseCases.ConsultarProtocolo
{
    public class UseCaseConsultarProtocolo : BaseUseCase, IUseCaseConsultarProtocolo
    {

        public UseCaseConsultarProtocolo(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public async Task<BaseReturn> Executar(TransacaoConsultarProtocolo transacao)
        {
            try
            {
                var ret = await _repo.ConsultarProtocolo(transacao);

                if (ret is null)
                    return new BaseReturn().BussinesException("Protocolo inexistente."); ;

                return new BaseReturn().Sucesso(new ConsultarProtocoloResponse(ret));

            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }
        }
    }
}
