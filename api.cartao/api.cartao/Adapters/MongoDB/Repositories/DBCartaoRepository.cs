using Adapters.MongoDB.Connection;
using Domain.Application.UseCases.BloquearCartao;
using Domain.Application.UseCases.ConsultarBloqueioCartao;
using Domain.Application.UseCases.ConsultarCartao;
using Domain.Application.UseCases.ConsultarProtocolo;
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
        private IMongoCollection<LogBloqueioCartao> _colBloqueioCartao;
        private IMongoCollection<Cartao>? _colCartao;
        private IMongoCollection<LogPropostaLimiteCartao> _colHistoricoLimiteCartao;
        private IMongoCollection<LogPropostaSolicitaCartao> _colHistoricoSolicitaCartao;
        private FilterDefinition<Cartao> _filterCartao;
        private FilterDefinition<LogBloqueioCartao> _filterBloqueioCartao;
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
            _colBloqueioCartao = _mongo.Connection("CARTAO").GetCollection<LogBloqueioCartao>("LogBloqueioCartao");

        }


        public async ValueTask<dynamic> ConsultarProtocolo(TransacaoConsultarProtocolo transacao)
        {

            _filterBloqueioCartao = Builders<LogBloqueioCartao>.Filter.Where(x => x.Protocolo == transacao.Protocolo);
            var logBloq = await _colBloqueioCartao.Find(_filterBloqueioCartao).FirstOrDefaultAsync();

            if (logBloq is not null)
                return (LogBloqueioCartao)logBloq;


            _filterLogNovoLimiteCartao = Builders<LogPropostaLimiteCartao>.Filter.Where(x => x.Protocolo == transacao.Protocolo);
            var logLimite = await _colHistoricoLimiteCartao.Find(_filterLogNovoLimiteCartao).FirstOrDefaultAsync();

            if (logLimite is not null)
                return (LogPropostaLimiteCartao)logLimite;


            _filterLogSolicitaCartao = Builders<LogPropostaSolicitaCartao>.Filter.Where(x => x.Protocolo == transacao.Protocolo);
            var logSolicitacao = await _colHistoricoSolicitaCartao.Find(_filterLogSolicitaCartao).FirstOrDefaultAsync();

            if (logSolicitacao is not null)
                return (LogPropostaSolicitaCartao)logSolicitacao;

            return null;
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

        public async ValueTask<bool> GravarLogCartaoSolicitado(TransacaoSolicitarCartao transacao)
        {
            try
            {
                var _log = new LogPropostaSolicitaCartao
                {
                    Protocolo = transacao.Protocolo,
                    Conta = transacao.DadosConta,
                    DiaVencimento = transacao.DiaVencimento,
                    Bandeira = transacao.Bandeira,

                };

                _colHistoricoSolicitaCartao.InsertOne(_log);

                return true;
            }
            catch { throw; }
        }


        public async ValueTask<bool> GravarLogNovoLimiteCartao(TransacaoNovoLimiteCartao transacao)
        {
            try
            {
                var _log = new LogPropostaLimiteCartao
                {
                    Protocolo = transacao.Protocolo,
                    Cartao = transacao.DadosCartao,
                    DataOcorrencia = DateTime.Now,
                    Limite = transacao.Limite,
                    FaixaCalculo = transacao.FaixaCalculo,
                    Multiplicador = transacao.Multiplicador,

                };

                _colHistoricoLimiteCartao.InsertOne(_log);
                return true;
            }
            catch { throw; }
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

        public async ValueTask<BaseReturn> BloquearCartao(TransacaoBloquearCartao transacao)
        {
            try
            {
                _filterCartao = Builders<Cartao>.Filter.Where(x => x.NumeroCartao == transacao.NumeroCartao);
                var _updateCartao = Builders<Cartao>.Update.Set("StatusCartao", transacao.Motivo);

                var _updated = await _colCartao.UpdateOneAsync(_filterCartao, _updateCartao, _updateOptions);

                var _bloqueio = new LogBloqueioCartao
                {
                    Protocolo = transacao.Protocolo,
                    Cartao = transacao.DadosCartao,
                    Motivo = transacao.Motivo,
                    InformacaoAdicoonal = transacao.InformacaoAdicoonal,
                    DataOcorrencia = DateTime.Now,
                };

                _colBloqueioCartao.InsertOne(_bloqueio);

                return new BaseReturn().Sucesso(true);
            }
            catch { throw; }

        }

        public async ValueTask<LogBloqueioCartao> ConsultarBloqueioCartao(TransacaoConsultarBloqueioCartao transacao)
        {
            _filterBloqueioCartao = Builders<LogBloqueioCartao>.Filter.Where(x => x.Cartao.NumeroCartao == transacao.NumeroCartao);
            return await _colBloqueioCartao.Find(_filterBloqueioCartao).FirstOrDefaultAsync();
        }

        public async ValueTask<Cartao> ConsultarCartao(TransacaoConsultarCartao transacao)
        {
            //_filterCartao = Builders<Cartao>.Filter.Where(x => x.NumeroCartao == transacao.NumeroCartao);
            //return await _colCartao.Find(_filterCartao).FirstOrDefaultAsync<Cartao>();

            return await ConsultarCartao(transacao.NumeroCartao);
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
