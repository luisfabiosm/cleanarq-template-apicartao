using Adapters.MongoDB.Connection;
using Adapters.MongoDB.Repositories;
using Adapters.RabbitMQ.Connection;
using Adapters.RabbitMQ.Services;
using Domain.Core.Contracts.Repositories;
using Domain.Core.Contracts.Services;


namespace Infra.Register
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongoAdapter(this IServiceCollection service, IConfiguration configuration)
        {

            #region NoSQL Session Management

            service.AddScoped<IDBCartaoRepository, DBCartaoRepository>();
            service.AddSingleton<IDBConnection, DBConnection>(services => new DBConnection(services, "mongodb://localhost:27017", "CARTAO"));

            #endregion

            #region RabbitMQ

            service.Configure<RabbitMQSettings>(settings =>
            {
                configuration.GetSection("RabbitMQ").Bind(settings);

                settings.Host = "";
                settings.Port = 5672;
                settings.Username = "guest";
                settings.Password = "guest";
                settings.Retry = 3;
                settings.Delay = 6;
                settings.ConnectionTimeout = 5000;
            });

            service.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
            service.AddSingleton<IRabbitMQService, RabbitMQService>();
            #endregion

            return service;
        }
    }
}
