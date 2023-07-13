using Domain.Application.UseCases.BloquearCartao;
using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Application.UseCases.ConsultarCartao;
using Domain.Application.UseCases.ConsultarProtocolo;
using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Base;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Contracts.Repositories
{
    public interface IDBCartaoRepository
    {

        ValueTask<bool> AdicionarCartaoSolicitado(TransacaoAdicionarNovoCartao transacao);

        ValueTask<bool> AtualizarLimiteCartao(TransacaoNovoLimiteCartao transacao);

        ValueTask<Cartao> ConsultarCartao(string NumeroCartao);

    }
}
