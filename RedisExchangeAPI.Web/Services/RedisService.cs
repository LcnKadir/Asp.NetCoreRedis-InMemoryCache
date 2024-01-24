using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {

        private readonly ConnectionMultiplexer _redis; //connection for redis server.//

        public RedisService(string url)
        {
            _redis = ConnectionMultiplexer.Connect(url);  //The communication of the redis server, which we specified in appseting, took place with the connetcion method.//

        }

        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }

    }
}
