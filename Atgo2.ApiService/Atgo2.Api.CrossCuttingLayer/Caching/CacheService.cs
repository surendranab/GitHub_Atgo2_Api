using System;
using System.Threading.Tasks;
using Atgo2.Api.CrossCuttingLayer.Caching.Interfaces;
using Atgo2.Api.Entity;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Atgo2.Api.CrossCuttingLayer.Caching
{
    public class CacheService : ICacheService
    {
        private readonly AppSettings _appsettings;
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        #region Perfixes

        public const string TokenPrefix = "token_";
        public const string LookupPrefix = "lookup_";
        public const string ConfigPrefix = "config_";
        public const string EtagPrefix = "etag_";
        public const string TimeZonePrefix = "timezone_";
        #endregion

        public CacheService(AppSettings appsettings)
        {
            _appsettings = appsettings;
            _lazyConnection = appsettings.settings.IsCacheEnabled
                ? new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_appsettings.connectionInfo.RedisDatabase))
                : null;
        }

        public ConnectionMultiplexer Connection => _appsettings.settings.IsCacheEnabled ? _lazyConnection.Value : null;

        public IDatabase Cache => _appsettings.settings.IsCacheEnabled ? Connection.GetDatabase() : null;

        public async Task SetAsync(string key, object value)
        {
            if(!_appsettings.settings.IsCacheEnabled) return;

            await Cache.StringSetAsync(key.ToLower(), JsonConvert.SerializeObject(value));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (!_appsettings.settings.IsCacheEnabled) return default(T);

            var response = await Cache.StringGetAsync(key.ToLower());
            if (string.IsNullOrEmpty(response))
                return default(T);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task DeleteAsync(string key)
        {
            if (!_appsettings.settings.IsCacheEnabled) return;

            await Cache.KeyDeleteAsync(key.ToLower());
        }

        public void Set(string key, object value)
        {
            if (!_appsettings.settings.IsCacheEnabled) return;

             Cache.StringSet(key.ToLower(), JsonConvert.SerializeObject(value));
        }

        public T Get<T>(string key)
        {
            if (!_appsettings.settings.IsCacheEnabled) return default(T);

            var response =  Cache.StringGet(key.ToLower());
            if (string.IsNullOrEmpty(response))
                return default(T);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public void Delete(string key)
        {
            if (!_appsettings.settings.IsCacheEnabled) return;

             Cache.KeyDelete(key.ToLower());
        }

        public void Clear()
        {
            var endpoints = Connection.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = Connection.GetServer(endpoint);
                server.FlushDatabase(Cache.Database);
            }
        }
    }
}
