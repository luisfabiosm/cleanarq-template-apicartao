using Domain.Application.UseCases.ConsultarCartao;
using Domain.Application.UseCases.ConsultarProtocolo;
using Domain.Core.Base;
using Domain.Core.Models.Response;
using Endpoints.Mapping;
using Microsoft.AspNetCore.Builder;

namespace Endpoints.Routes.ConsultarProtocolo
{
    public static class ConsultarProtocoloRoute
    {
        public static void AddConsultarProtocoloEndpoint(this WebApplication app)
        {
            app.MapGet("protocolo/{protocol}", ProcRequest)
             .WithTags("Consultar informação Protocolo")
             .Produces<ConsultarProtocoloResponse>(StatusCodes.Status200OK)
             .Produces<BaseError>(StatusCodes.Status400BadRequest)
             .Produces<BaseError>(StatusCodes.Status500InternalServerError);

        }

 

        private static async Task<IResult> ProcRequest(IUseCaseConsultarProtocolo useCase,  HttpContext context, string protocol)
        {
            var response = await useCase.Executar(MapToTransacao.ToTransacaoTransacaoConsultarProtocolo(protocol));
            return response.GetResponse();
        }


    }
}
