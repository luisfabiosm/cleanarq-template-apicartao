using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Core.Base;
using Domain.Core.Models.Response;
using Endpoints.Mapping;

namespace Endpoints.Routes.ConsultarBloqueioCartao
{
    public static class ConsultarBloqueioCartaoRoute
    {
        public static void AddConsultarBloqueioCartaoEndpoint(this WebApplication app)
        {
            app.MapGet("cartao/bloqueio/{numeroCartao}", ProcRequest)
             .WithTags("Consultar Bloqueios Cartão")
             .Produces<ConsultarCartaoResponse>(StatusCodes.Status200OK)
             .Produces<BaseError>(StatusCodes.Status400BadRequest)
             .Produces<BaseError>(StatusCodes.Status500InternalServerError);

        }


        private static async Task<IResult> ProcRequest(IUseCaseConsultarBloqueioCartao useCase, HttpContext context, string numeroCartao)
        {
            var response = await useCase.Executar(MapToTransacao.ToTransacaoTransacaoConsultarBloqueioCartao(numeroCartao));
            return response.GetResponse();
        }
    }
}
