using Domain.Core.Base;

namespace Domain.Core.Models.Entidades
{
    public record LogPropostaLimiteCartao : BaseLog
    {
        public Cartao Cartao { get; set; }
        public decimal Limite { get; set; }
        public decimal Renda { get; set; }
        public double FaixaCalculo { get; set; }
        public int Multiplicador { get; set; }

    }
}
