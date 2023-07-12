using Domain.Application.UseCases.BloquearCartao;
using Domain.Core.Base;
using Domain.Core.Models.Request;
using Domain.Core.Models.Response;
using Endpoints.Mapping;

namespace Endpoints.Routes.BloquearCartao
{
    public static class BloquearCartaotaoRoute
    {
        public static void AddBloquearCartaoEndpoint(this WebApplication app)
        {
            app.MapPost("cartao/bloqueio", ProcRequest)
             .WithTags("Bloqueio de Cartão")
             .Accepts<BloquearCartaoRequest>("application/json")
             .Produces<BloquearCartaoResponse>(StatusCodes.Status200OK)
             .Produces<BaseError>(StatusCodes.Status400BadRequest)
             .Produces<BaseError>(StatusCodes.Status500InternalServerError);

        }


        private static async Task<IResult> ProcRequest(IUseCaseBloquearCartao useCase, HttpContext context, BloquearCartaoRequest request)
        {
            var response = await useCase.Executar(MapToTransacao.ToTransacaoBloquearCartao(context, request));
            return response.GetResponse();
        }
    }
}
