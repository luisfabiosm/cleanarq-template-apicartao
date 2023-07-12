using Domain.Core.Contracts.Repositories;

namespace Domain.Core.Base
{
    public abstract class BaseUseCase
    {
        protected readonly BaseReturn _result;
        protected IDBCartaoRepository _repo;
        //protected IEsteiraService _esteira;

        public BaseUseCase(IServiceProvider serviceProvider)
        {
            _repo = serviceProvider.GetRequiredService<IDBCartaoRepository>();


        }
    }
}
