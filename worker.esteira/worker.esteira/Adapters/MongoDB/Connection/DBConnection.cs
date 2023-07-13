using MongoDB.Driver;

namespace Adapters.MongoDB.Connection
{
    public class DBConnection : IDBConnection
    {
        private IClientSessionHandle _client { get; set; }
        private MongoClient _dbSession { get; set; }
        private readonly string CONNECTION_STRING = "mongodb://localhost:27017";
        private readonly string DATABASE = "CARTAO";

        public ILogger<DBConnection> _logger;

        public DBConnection(IServiceProvider serviceProvider, string ConnectionString = null, string Database = null)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<DBConnection>>();
            _dbSession = new MongoClient(ConnectionString ?? CONNECTION_STRING);
            TryConnect(Database ?? DATABASE);
        }

        private void TryConnect(string Database)
        {
            _client = Connection(Database).Client.StartSession();
        }

        public IMongoDatabase Connection(string DB)
        {
            return _dbSession.GetDatabase(DB);
        }

        public IClientSessionHandle SessionManager()
        {
            return _client;
        }
    }
}
