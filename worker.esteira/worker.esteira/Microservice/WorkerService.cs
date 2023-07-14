using Domain.Core.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace worker.esteira.Microservice
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IEsteiraAdicionarNovoCartao _esteiraNovoCartao;
        private readonly IEsteiraNovoLimiteCartao _esteiraNovoLimiteCartao;
        public WorkerService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<WorkerService>>();
            _esteiraNovoCartao = serviceProvider.GetRequiredService<IEsteiraAdicionarNovoCartao>();
            _esteiraNovoLimiteCartao = serviceProvider.GetRequiredService<IEsteiraNovoLimiteCartao>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await initService(stoppingToken);
                _logger.LogTrace($"Esteira iniciada com sucesso.");
                Console.WriteLine($"Esteira iniciada com sucesso.");

            }
            catch (Exception ex)
            {
                _logger.LogError($"[ExecuteAsync] ERROR: {ex.Message}");
                Console.WriteLine($"[ExecuteAsync] ERROR: {ex.Message}");
            }
        }

        private async Task initService(CancellationToken stoppingToken)
        {

            _esteiraNovoCartao.AssinarAdicionarCartao(stoppingToken);
            _esteiraNovoLimiteCartao.AtualizarNovoLimiteCartao(stoppingToken);

        }
    }
}