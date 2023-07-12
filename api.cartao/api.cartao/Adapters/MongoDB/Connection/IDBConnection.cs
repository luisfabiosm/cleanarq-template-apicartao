using MongoDB.Driver;

namespace Adapters.MongoDB.Connection
{
    public interface IDBConnection
    {
        public IMongoDatabase Connection(string DB);
        IClientSessionHandle SessionManager();
    }
}
