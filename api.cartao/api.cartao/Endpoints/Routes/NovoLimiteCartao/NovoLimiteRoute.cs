using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Core.Base;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Endpoints.Mapping;

namespace Endpoints.Routes.NovoLimiteCartao
{
    public static class NovoLimiteRoute
    {
        public static void AddNovoLimiteRouteEndpoint(this WebApplication app)
        {
            app.MapPost("limite/proposta", ProcRequest)
             .WithTags("Proposta Novo Limite Cartão")
             .Accepts<NovoLimiteRequest>("application/json")
             .Produces<NovoLimiteResponse>(StatusCodes.Status200OK)
             .Produces<BaseError>(StatusCodes.Status400BadRequest)
             .Produces<BaseError>(StatusCodes.Status500InternalServerError);

        }


        private static async Task<IResult> ProcRequest(IUseNovoLimiteCartao useCase, HttpContext context, NovoLimiteRequest request)
        {
            var response = await useCase.Executar(MapToTransacao.ToTransacaoTransacaoPropostaNovoLimiteCartao(context, request));
            return response.GetResponse();
        }
    }
}
