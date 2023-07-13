using Adapters.MongoDB.Connection;
using Domain.Application.UseCases.AdicionarNovoCartao;
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
        private FilterDefinition<Cartao> _filterCartao;
        private UpdateOptions _updateOptions;
        


        public DBCartaoRepository(IServiceProvider serviceProvider)
        {
            _mongo = serviceProvider.GetService<IDBConnection>();
            _logger = serviceProvider.GetRequiredService<ILogger<IDBCartaoRepository>>();
            _colCartao = _mongo.Connection("CARTAO").GetCollection<Cartao>("Cartao");

        }


   
        public async ValueTask<BaseReturn> AdicionarCartaoSolicitado(TransacaoAdicionarNovoCartao transacao)
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

            return new BaseReturn().Sucesso(_cartao);
        }

            
        public async ValueTask<BaseReturn> AtualizarLimiteCartao(TransacaoNovoLimiteCartao transacao)
        {
            try
            {
                _filterCartao = Builders<Cartao>.Filter.Where(x => x.NumeroCartao == transacao.NumeroCartao);
                var _updateCartao = Builders<Cartao>.Update.Set("Limite", transacao.Limite);

                await _colCartao.UpdateOneAsync(_filterCartao, _updateCartao, _updateOptions);

                return new BaseReturn().Sucesso(transacao);
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
            }

            // Free unmanaged resources
        }


    }
}
