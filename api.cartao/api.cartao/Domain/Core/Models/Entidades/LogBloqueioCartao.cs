using Domain.Core.Base;
using Domain.Core.Enums;

namespace Domain.Core.Models.Entidades
{
    public record LogBloqueioCartao : BaseLog
    {
        public Cartao Cartao { get; set; }

        public EnumMotivoBloqueio Motivo { get; set; }

        public string InformacaoAdicoonal { get; set; }

    }
}
