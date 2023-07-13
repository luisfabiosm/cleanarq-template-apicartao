
using Infra.Register;


namespace Gateway.Infra.Registers
{
    public static class WorkerNativeInjection
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAdapters(configuration);
            service.AddDomainServices(configuration);

            return service;
        }
    }
}