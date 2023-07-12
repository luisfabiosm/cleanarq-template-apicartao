using Domain.Core.Base;

namespace Domain.Application.UseCases.ConsultarCartao
{
    public interface IUseCaseConsultarCartao
    {
        Task<BaseReturn> Executar(TransacaoConsultarCartao transacao);
    }
}
