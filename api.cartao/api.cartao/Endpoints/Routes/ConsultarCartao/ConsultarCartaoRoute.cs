using Domain.Application.UseCases.ConsultarCartao;
using Domain.Core.Base;
using Domain.Core.Models.Response;
using Endpoints.Mapping;

namespace Endpoints.Routes.ConsultarCartao
{
    public static class ConsultarCartaoRoute
    {
        public static void AddConsultarCartaoEndpoint(this WebApplication app)
        {
            app.MapGet("cartao/{numeroCartao}", ProcRequest)
             .WithTags("Consultar Cartão")
             .Produces<ConsultarCartaoResponse>(StatusCodes.Status200OK)
             .Produces<BaseError>(StatusCodes.Status400BadRequest)
             .Produces<BaseError>(StatusCodes.Status500InternalServerError);

        }


        private static async Task<IResult> ProcRequest(IUseCaseConsultarCartao useCase, HttpContext context, string numeroCartao)
        {
            var response = await useCase.Executar(MapToTransacao.ToTransacaoTransacaoConsultarCartao(numeroCartao));
            return response.GetResponse();
        }
    }
}
