using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Response
{
    public struct ConsultarBloqueioCartaoResponse
    {
        public string Protocolo { get; set; }
        public Cartao Cartao { get; set; }
        public EnumMotivoBloqueio Motivo { get; set; }

        public ConsultarBloqueioCartaoResponse(LogBloqueioCartao log)
        {
            Cartao = log.Cartao;
            Protocolo = log.Protocolo;
            Motivo = log.Motivo;

        }
    }
}
