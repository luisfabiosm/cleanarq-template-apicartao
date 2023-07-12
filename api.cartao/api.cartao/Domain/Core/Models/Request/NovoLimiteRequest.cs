using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Request
{
    public struct NovoLimiteRequest
    {
        public decimal Limite { get; }
        public decimal Renda { get; }
        public double FaixaCalculo { get; }
        public int Multiplicador { get; }
        public Cartao DadosCartao { get; }

    }
}
