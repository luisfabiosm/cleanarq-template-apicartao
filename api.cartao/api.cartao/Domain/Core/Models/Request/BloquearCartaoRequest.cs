using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Request
{
    public struct BloquearCartaoRequest
    {
        public int Motivo { get; }
        public string InformacaoAdicoonal { get; }
        public Cartao DadosCartao { get; }
    }
}
