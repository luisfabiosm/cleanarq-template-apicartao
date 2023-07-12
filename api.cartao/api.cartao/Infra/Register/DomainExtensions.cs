using Domain.Application.Services;
using Domain.Application.UseCases.BloquearCartao;
using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Application.UseCases.ConsultarCartao;
using Domain.Application.UseCases.ConsultarProtocolo;
using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Base;
using Domain.Core.Contracts.Services;
using Endpoints.Validators;

namespace Infra.Register
{
    public static class DomainExtensions
    {

        public static IServiceCollection AddDomainAdapter(this IServiceCollection service)
        {
            //Return
            service.AddScoped<BaseReturn>();

            //Services
            service.AddScoped<IEsteiraService, EsteiraService>();

            //Use Case
            service.AddScoped<IUseCaseBloquearCartao, UseCaseBloquearCartao>();
            service.AddScoped<IUseCaseConsultarBloqueioCartao, UseCaseConsultarBloqueioCartao>();
            service.AddScoped<IUseCaseSolicitarCartao, UseCaseSolicitarCartao>();
            service.AddScoped<IUseNovoLimiteCartao, UseNovoLimiteCartao>();
            service.AddScoped<IUseCaseConsultarCartao, UseCaseConsultarCartao>();
            service.AddScoped<IUseCaseConsultarProtocolo, UseCaseConsultarProtocolo>();

            //Validator
            service.AddScoped<TransacaoBloquearCartaoValidator>();
            service.AddScoped<TransacaoNovoLimiteCartaoValidator>();
            service.AddScoped<TransacaoSolicitarCartaoValidator>();

            return service;
        }
    }
}
