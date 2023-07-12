using Domain.Core.Base;
using Domain.Core.Enums;

namespace Domain.Core.Models.Entidades
{
    public record LogPropostaSolicitaCartao : BaseLog
    {
        public Conta Conta { get; set; }
        public int DiaVencimento { get; set; }
        public EnumBandeiraCartao Bandeira { get; set; }

    }
}
