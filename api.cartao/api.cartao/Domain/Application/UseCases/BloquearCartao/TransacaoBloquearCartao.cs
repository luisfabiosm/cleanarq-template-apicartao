using Domain.Core.Base;
using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Application.UseCases.BloquearCartao
{
    public class TransacaoBloquearCartao : BaseTransacao
    {
        public EnumMotivoBloqueio Motivo { get; }
        public string InformacaoAdicoonal { get; }
        public Cartao DadosCartao { get; }

        public TransacaoBloquearCartao(Cartao cartao, EnumMotivoBloqueio motivo, string info = "") : base(cartao.NumeroCartao)
        {

            this.DadosCartao = cartao;
            this.Motivo = motivo;
            this.InformacaoAdicoonal = info;
        }

    }
}
