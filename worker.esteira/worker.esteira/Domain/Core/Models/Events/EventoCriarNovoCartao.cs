using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Events
{
    public record EventoCriarNovoCartao
    {
        public string Protocolo { get; set; }
        public string NumeroCartao { get; }
        public Conta DadosConta { get; set; }
        public int DiaVencimento { get; set; }
        public EnumBandeiraCartao Bandeira { get; set; }
        public EnumTipoCartao TipoCartao { get; set; }
      
    }
}
