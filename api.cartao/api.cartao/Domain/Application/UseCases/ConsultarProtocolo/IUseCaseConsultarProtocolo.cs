using Domain.Core.Base;

namespace Domain.Application.UseCases.ConsultarProtocolo
{
    public interface IUseCaseConsultarProtocolo
    {
        Task<BaseReturn> Executar(TransacaoConsultarProtocolo transacao);
    }
}
