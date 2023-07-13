using Domain.Core.Base;

namespace Domain.Application.UseCases.BloquearCartao
{
    public class UseCaseBloquearCartao : BaseUseCase, IUseCaseBloquearCartao
    {

        public UseCaseBloquearCartao(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public async Task<BaseReturn> Executar(TransacaoBloquearCartao transacao)
        {
            try
            {

                var _cartao = await _repo.ConsultarCartao(transacao.NumeroCartao);

                if (_cartao is null)
                    return new BaseReturn().BussinesException("Cartão inexistente.");

                return await _repo.BloquearCartao(transacao);

            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }
        }
    }
}
