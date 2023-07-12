using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Response
{
    public struct SolicitarCartaoResponse
    {
        public string Protocolo { get; set; }

        public SolicitarCartaoResponse(string protocolo)
        {
            Protocolo = protocolo;

        }
    }
}
