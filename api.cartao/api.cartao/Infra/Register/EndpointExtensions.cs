using Endpoints.Routes.BloquearCartao;
using Endpoints.Routes.ConsultarBloqueioCartao;
using Endpoints.Routes.ConsultarCartao;
using Endpoints.Routes.ConsultarProtocolo;
using Endpoints.Routes.NovoLimiteCartao;
using Endpoints.Routes.SolicitarCartao;
using Infra.Middlewares;

namespace Infra.Register
{
    public static class EndpointExtensions
    {
        public static void AddHttpEndpointAdapter(this WebApplication app)
        {
            app.AddBloquearCartaoEndpoint();
            app.AddSolicitarCartaoEndpoint();
            app.AddNovoLimiteRouteEndpoint();
            app.AddConsultarBloqueioCartaoEndpoint();
            app.AddConsultarCartaoEndpoint();
            app.AddConsultarProtocoloEndpoint();
            

            app.UseMiddleware<HttpTraceMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }


    }
}
