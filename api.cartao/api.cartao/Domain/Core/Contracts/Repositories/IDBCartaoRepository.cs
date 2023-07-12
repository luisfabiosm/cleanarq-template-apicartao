using Domain.Application.UseCases.BloquearCartao;
using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Application.UseCases.ConsultarCartao;
using Domain.Application.UseCases.ConsultarProtocolo;
using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Models.Entidades;

namespace Domain.Core.Contracts.Repositories
{
    public interface IDBCartaoRepository
    {

        ValueTask<bool> AdicionarCartaoSolicitado(TransacaoSolicitarCartao transacao);

        ValueTask<bool> AtualizarLimiteCartao(TransacaoNovoLimiteCartao transacao);

        ValueTask<bool> BloquearCartao(TransacaoBloquearCartao transacao);

        ValueTask<Cartao> ConsultarCartao(TransacaoConsultarCartao transacao);

        ValueTask<LogBloqueioCartao> ConsultarBloqueioCartao(TransacaoConsultarBloqueioCartao transacao);

    
        ValueTask<bool> GravarLogCartaoSolicitado(TransacaoSolicitarCartao transacao);

        ValueTask<bool> GravarLogNovoLimiteCartao(TransacaoNovoLimiteCartao transacao);

        ValueTask<dynamic> ConsultarProtocolo(TransacaoConsultarProtocolo transacao);
    }
}
