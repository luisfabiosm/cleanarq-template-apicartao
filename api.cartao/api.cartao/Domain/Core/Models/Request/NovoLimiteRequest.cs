using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Request
{
    public struct NovoLimiteRequest
    {
        public decimal Limite { get; set; }
        public decimal Renda { get; set; }
        public double FaixaCalculo { get; set; }
        public int Multiplicador { get; set; }
        public Cartao DadosCartao { get; set; }

    }
}
