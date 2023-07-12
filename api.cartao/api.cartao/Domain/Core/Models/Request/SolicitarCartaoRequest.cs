using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Request
{
    public struct SolicitarCartaoRequest
    {
        public Conta DadosConta { get; set; }
        public int DiaVencimento { get; set; }
        public int Bandeira { get; set; }
        public int TipoCartao { get; set; }

    }
}
