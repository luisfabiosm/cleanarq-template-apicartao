using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Response
{
    public struct BloquearCartaoResponse
    {
        public string Protocolo { get; set; }

        public BloquearCartaoResponse(string protocolo)
        {
            Protocolo = protocolo;

        }
    }
}
