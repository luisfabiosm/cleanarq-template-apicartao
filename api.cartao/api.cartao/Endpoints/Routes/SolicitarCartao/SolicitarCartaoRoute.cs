using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Base;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Endpoints.Mapping;
using Endpoints.Routes.BloquearCartao;

namespace Endpoints.Routes.SolicitarCartao
{
    public static class SolicitarCartaotaoRoute
    {
        public static void AddSolicitarCartaoEndpoint(this WebApplication app)
        {
            app.MapPost("cartao/proposta", ProcRequest)
             .WithTags("Solicitar Cartão")
             .Accepts<SolicitarCartaoRequest>("application/json")
             .Produces<SolicitarCartaoResponse>(StatusCodes.Status200OK)
             .Produces<BaseError>(StatusCodes.Status400BadRequest)
             .Produces<BaseError>(StatusCodes.Status500InternalServerError);

        }


        private static async Task<IResult> ProcRequest(IUseCaseSolicitarCartao useCase, HttpContext context, SolicitarCartaoRequest request)
        {
            var response = await useCase.Executar(MapToTransacao.ToTransacaoTransacaoSolicitarCartao(context, request));
            return response.GetResponse();
        }
    }
}
