
using Domain.Application.UseCases.AdicionarNovoCartao;
using Domain.Core.Base;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Contracts.Repositories
{
    public interface IDBCartaoRepository
    {

        ValueTask<BaseReturn> AdicionarCartaoSolicitado(TransacaoAdicionarNovoCartao transacao);

        ValueTask<BaseReturn> AtualizarLimiteCartao(TransacaoNovoLimiteCartao transacao);

        ValueTask<Cartao> ConsultarCartao(string NumeroCartao);

    }
}
