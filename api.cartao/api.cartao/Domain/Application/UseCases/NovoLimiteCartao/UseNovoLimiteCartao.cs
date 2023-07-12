using Domain.Core.Base;
using Domain.Core.Contracts.Services;

namespace Domain.Application.UseCases.NovoLimiteCartao
{
    public class UseNovoLimiteCartao : BaseUseCase, IUseNovoLimiteCartao
    {
        private readonly IEsteiraService _esteira;

        public UseNovoLimiteCartao(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _esteira = serviceProvider.GetRequiredService<IEsteiraService>();
        }


        public async Task<BaseReturn> Executar(TransacaoNovoLimiteCartao transacao)
        {

            try
            {
                await _esteira.PublicarPropostaNovoLimite(transacao);
                return new BaseReturn().Sucesso(transacao.Protocolo);
            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }

        }
    }
}
