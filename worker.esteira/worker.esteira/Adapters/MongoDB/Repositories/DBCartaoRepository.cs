using Adapters.MongoDB.Connection;
using Domain.Application.UseCases.NovoLimiteCartao;
using Domain.Application.UseCases.SolicitarCartao;
using Domain.Core.Base;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Models.Entidades;
using MongoDB.Driver;

namespace Adapters.MongoDB.Repositories
{
    public class DBCartaoRepository : IDBCartaoRepository, IDisposable
    {
        private readonly ILogger<IDBCartaoRepository> _logger;
        private readonly IDBConnection _mongo;
        private IMongoCollection<Cartao>? _colCartao;
        private IMongoCollection<LogPropostaLimiteCartao> _colHistoricoLimiteCartao;
        private IMongoCollection<LogPropostaSolicitaCartao> _colHistoricoSolicitaCartao;
        private FilterDefinition<Cartao> _filterCartao;
        private FilterDefinition<LogPropostaLimiteCartao> _filterLogNovoLimiteCartao;
        private FilterDefinition<LogPropostaSolicitaCartao> _filterLogSolicitaCartao;
        private UpdateOptions _updateOptions;
        //private FilterDefinition<LogBloqueioCartao> _filterBloqueioCartao;


        public DBCartaoRepository(IServiceProvider serviceProvider)
        {
            _mongo = serviceProvider.GetService<IDBConnection>();
            _logger = serviceProvider.GetRequiredService<ILogger<IDBCartaoRepository>>();

            _colCartao = _mongo.Connection("CARTAO").GetCollection<Cartao>("Cartao");
            _colHistoricoLimiteCartao = _mongo.Connection("CARTAO").GetCollection<LogPropostaLimiteCartao>("LogPropostaLimiteCartao");
            _colHistoricoSolicitaCartao = _mongo.Connection("CARTAO").GetCollection<LogPropostaSolicitaCartao>("LogPropostaSolicitaCartao");

        }


   
        public async ValueTask<bool> AdicionarCartaoSolicitado(TransacaoSolicitarCartao transacao)
        {
            var _cartao = new Cartao
            {
                DadosConta = transacao.DadosConta,
                NumeroCartao = transacao.NumeroCartao,
                CVV = transacao.CVV,
                Limite = transacao.Limite,
                DiaVencimento = transacao.DiaVencimento,
                Bandeira = transacao.Bandeira,
                TipoCartao = transacao.TipoCartao,
                StatusCartao = Domain.Core.Enums.EnumStatusCartao.SOLICITADO

            };

            _colCartao.InsertOne(_cartao);

            return true;
        }

            
        public async ValueTask<bool> AtualizarLimiteCartao(TransacaoNovoLimiteCartao transacao)
        {
            try
            {
                _filterCartao = Builders<Cartao>.Filter.Where(x => x.NumeroCartao == transacao.NumeroCartao);
                var _updateCartao = Builders<Cartao>.Update.Set("Limite", transacao.Limite);

                var _novolimite = new LogPropostaLimiteCartao
                {
                    Protocolo = transacao.Protocolo,
                    Cartao = transacao.DadosCartao,
                    DataOcorrencia = DateTime.Now,
                    Limite = transacao.Limite,
                    FaixaCalculo = transacao.FaixaCalculo,
                    Multiplicador = transacao.Multiplicador,

                };

                _colHistoricoLimiteCartao.InsertOne(_novolimite);

                return true;
            }
            catch { throw; }
        }

      
        public async ValueTask<Cartao> ConsultarCartao(string numeroCartao)
        {
            _filterCartao = Builders<Cartao>.Filter.Where(x => x.NumeroCartao == numeroCartao);
            return await _colCartao.Find(_filterCartao).FirstOrDefaultAsync<Cartao>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                _updateOptions = null;
                _filterCartao = null;
                if (_colCartao != null)
                    _colCartao = null;

                _filterBloqueioCartao = null;
                if (_colBloqueioCartao != null)
                    _colBloqueioCartao = null;

                if (_colHistoricoLimiteCartao != null)
                    _colHistoricoLimiteCartao = null;

                if (_colHistoricoSolicitaCartao != null)
                    _colHistoricoSolicitaCartao = null;
            }

            // Free unmanaged resources
        }


    }
}
