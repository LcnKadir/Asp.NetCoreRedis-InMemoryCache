using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {
        private readonly string _redishost;

        private readonly string _redisport;

        private ConnectionMultiplexer _redis; //connection for redis server.//

        public IDatabase db { get; set; }

        public RedisService(IConfiguration configuration)
        {
            _redishost = configuration["Redis:Host"];
            _redisport = configuration["Redis:Port"];
        }




        //The communication of the redis server, which we specified in appseting, took place with the connetcion method.//
        public void Connet()
        {
            var configserver = $"{_redishost}:{_redisport}";

            _redis = ConnectionMultiplexer.Connect(configserver);

        }


        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }

    }
}
