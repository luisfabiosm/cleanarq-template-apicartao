using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Response
{
    public struct ConsultarCartaoResponse
    {
        public string NumeroCartao { get; internal set; }
        public string CVV { get; internal set; }
        public int DiaVencimento { get; internal set; }
        public EnumBandeiraCartao Bandeira { get; internal set; }
        public EnumTipoCartao TipoCartao { get; internal set; }
        public EnumStatusCartao StatusCartao { get; internal set; }

        public decimal Limite { get; internal set; }
        public Conta CartaoConta { get; internal set; }

        public ConsultarCartaoResponse(Cartao cartao)
        {               

            NumeroCartao = cartao.NumeroCartao;
            CVV = cartao.CVV;
            DiaVencimento = cartao.DiaVencimento;
            Bandeira = cartao.Bandeira;
            TipoCartao = cartao.TipoCartao;
            StatusCartao = cartao.StatusCartao;
            Limite = cartao.Limite;
            CartaoConta = cartao.DadosConta;
        }
    }
}
