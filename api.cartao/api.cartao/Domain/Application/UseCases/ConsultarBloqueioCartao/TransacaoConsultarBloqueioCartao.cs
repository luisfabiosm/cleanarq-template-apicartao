using Domain.Core.Base;

namespace Domain.Application.UseCases.ConsultarBloqueioCartao
{
    public class TransacaoConsultarBloqueioCartao : BaseTransacao
    {
        public string NumeroCartao { get; }

        public TransacaoConsultarBloqueioCartao(string numero) : base(numero)
        {

        }
    }
}
