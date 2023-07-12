using Domain.Core.Enums;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Models.Response
{
    public struct NovoLimiteResponse
    {
        public string Protocolo { get; set; }

        public NovoLimiteResponse(string protocolo)
        {
            Protocolo = protocolo;

        }
    }
}
