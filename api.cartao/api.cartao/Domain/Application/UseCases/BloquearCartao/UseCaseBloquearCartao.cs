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
                return new BaseReturn().Sucesso(await _repo.BloquearCartao(transacao));

            }
            catch (Exception ex)
            {
                return new BaseReturn().SystemException(ex);
            }
        }
    }
}
