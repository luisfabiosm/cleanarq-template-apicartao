
using Domain.Application.UseCases.AdicionarNovoCartao;

namespace Gateway.Infra.Registers
{
    public static class DomainExtensions
    {

        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUseCaseAdicionarNovoCartao, UseCaseAdicionarNovoCartao>();
            services.AddSingleton<IUseCaseNovoLimiteCartao, UseCaseNovoLimiteCartao>();

            return services;
        }




    }
}
