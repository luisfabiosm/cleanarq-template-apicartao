
using Domain.Application.Services;
using Domain.Application.UseCases.AdicionarNovoCartao;
using Domain.Core.Contracts.Services;
using System.Collections.Concurrent;

namespace Gateway.Infra.Registers
{
    public static class DomainExtensions
    {

        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMainService, MainService>();
            services.AddSingleton<IUseCaseAdicionarNovoCartao, UseCaseAdicionarNovoCartao>();
            services.AddSingleton<IUseCaseNovoLimiteCartao, UseCaseNovoLimiteCartao>();
            services.AddSingleton<IEsteiraAdicionarNovoCartao, EsteiraAdicionarNovoCartao>();
            services.AddSingleton<IEsteiraNovoLimiteCartao, EsteiraNovoLimiteCartao>();

            return services;
        }




    }
}
