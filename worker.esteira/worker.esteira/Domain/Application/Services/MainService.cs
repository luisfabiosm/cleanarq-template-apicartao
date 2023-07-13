using Domain.Core.Contracts.Services;


namespace Domain.Application.Services
{
    internal class MainService : IMainService
    {
        private readonly IEsteiraAdicionarNovoCartao _esteiraNovoCartao;
        private readonly IEsteiraNovoLimiteCartao _esteiraNovoLimiteCartao;
        private readonly ILogger<MainService> _logger;
        public MainService(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<MainService>>();
            _esteiraNovoCartao = serviceProvider.GetRequiredService<IEsteiraAdicionarNovoCartao>();
            _esteiraNovoLimiteCartao = serviceProvider.GetRequiredService<IEsteiraNovoLimiteCartao>();
        }
        public async Task IniciarService(CancellationToken stoppingToken)
        {

            //Assinar fila novo cartao
            await _esteiraNovoCartao.AssinarAdicionarCartao();

            //Assinar fila novo limite
            await _esteiraNovoLimiteCartao.AtualizarNovoLimiteCartao();

        }


    }
}
