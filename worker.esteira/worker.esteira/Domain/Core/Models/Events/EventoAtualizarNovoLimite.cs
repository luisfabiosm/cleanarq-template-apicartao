using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Request
{
    public struct EventoAtualizarNovoLimite
    {
        public string Protocolo { get; set; }
        public string NumeroCartao { get; set; }
        public decimal Limite { get;  set; }
        public decimal Renda { get;  set; }
        public double FaixaCalculo { get;  set; }
        public int Multiplicador { get;  set; }
        public Cartao DadosCartao { get;  set; }

    }
}
