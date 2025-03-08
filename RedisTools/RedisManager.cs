using StackExchange.Redis;

namespace RedisTools
{
    public class RedisManager
    {
        private ConnectionMultiplexer? _redis;
        private IDatabase? _db;

        public bool Connect(string host, int port, string? password = null, int db = 0)
        {
            try
            {
                var config = new ConfigurationOptions
                {
                    EndPoints = { $"{host}:{port}" },
                    Password = password,
                    DefaultDatabase = db,
                    ConnectTimeout = 5000,
                    SyncTimeout = 5000
                };

                _redis = ConnectionMultiplexer.Connect(config);
                _db = _redis.GetDatabase();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> GetAllKeys()
        {
            var keys = new List<string>();
            if (_redis == null) return keys;

            var endpoints = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoints[0]);
            
            try
            {
                var pattern = "*";
                foreach (var key in server.Keys(_db.Database, pattern))
                {
                    keys.Add(key.ToString());
                }
            }
            catch
            {
                // 如果获取键失败，返回空列表
            }
            
            return keys;
        }

        public bool Set(string key, string value, TimeSpan? expiry = null)
        {
            try
            {
                return _db?.StringSet(key, value, expiry) ?? false;
            }
            catch
            {
                return false;
            }
        }

        public string? Get(string key)
        {
            try
            {
                return _db?.StringGet(key);
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(string key)
        {
            try
            {
                return _db?.KeyDelete(key) ?? false;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            _redis?.Close();
            _redis?.Dispose();
            _redis = null;
            _db = null;
        }
    }
}