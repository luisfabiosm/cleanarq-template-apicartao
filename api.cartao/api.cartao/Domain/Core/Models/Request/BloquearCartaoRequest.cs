using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Request
{
    public struct BloquearCartaoRequest
    {
        public int Motivo { get; set; }
        public string informacaoAdicional { get; set; }
        public Cartao DadosCartao { get; set; }
    }
}
