using Domain.Core.Base;

namespace Domain.Application.UseCases.ConsultarBloqueioCartao
{
    public interface IUseCaseConsultarBloqueioCartao
    {

        Task<BaseReturn> Executar(TransacaoConsultarBloqueioCartao transacao);
    }
}
