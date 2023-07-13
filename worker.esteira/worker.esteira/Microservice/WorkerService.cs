using Domain.Core.Contracts.Services;

namespace worker.esteira.Microservice
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IMainService _mainService;

        public WorkerService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<WorkerService>>();
            _mainService = serviceProvider.GetRequiredService<IMainService>();

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _mainService.IniciarService(stoppingToken);
                _logger.LogTrace($"Esteira iniciada com sucesso.");
                Console.WriteLine($"Esteira iniciada com sucesso.");

            }
            catch (Exception ex)
            {
                _logger.LogError($"[ExecuteAsync] ERROR: {ex.Message}");
                Console.WriteLine($"[ExecuteAsync] ERROR: {ex.Message}");
            }
        }
    }
}