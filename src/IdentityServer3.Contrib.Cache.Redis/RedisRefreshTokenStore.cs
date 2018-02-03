using IdentityServer3.Contrib.Cache.Redis.CacheClient;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer3.Contrib.Cache.Redis
{
    public class RedisRefreshTokenStore : IRefreshTokenStore
    {
        private readonly ICacheManager cacheClient;

        public RedisRefreshTokenStore(ConnectionMultiplexer connection)
            : this(new RedisCacheManager(connection)) { }

        public RedisRefreshTokenStore(ICacheManager cacheClient)
            => this.cacheClient = cacheClient;

        public Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
            => throw new NotImplementedException();

        public Task<RefreshToken> GetAsync(string key)
            => cacheClient.GetAsync<RefreshToken>(key);

        public Task RemoveAsync(string key)
            => cacheClient.RemoveAsync(key);

        public Task RevokeAsync(string subject, string client)
            => throw new NotImplementedException();

        public Task StoreAsync(string key, RefreshToken value)
            => cacheClient.AddAsync(key, value);
    }

    #region need fix with prefix
    //public Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
    //{
    //    var keys = cacheClient.SearchKeys($"*:{subject}:*");
    //    var list = cacheClient.GetAll<RefreshToken>(keys);

    //    return Task.FromResult(list.Cast<ITokenMetadata>());
    //}

    //public Task<RefreshToken> GetAsync(string key)
    //{
    //    var keys = cacheClient.SearchKeys($"*:{key}");
    //    if (!keys.Any()) return null;

    //    return Task.FromResult(cacheClient.Get<RefreshToken>(keys.First()));
    //}

    //public Task RemoveAsync(string key)
    //{
    //    var keys = cacheClient.SearchKeys($"*:{key}");

    //    if (keys.Any())
    //        return cacheClient.RemoveAsync(keys.First());

    //    return Task.FromResult(true);
    //}

    //public Task RevokeAsync(string subject, string client)
    //{
    //    var keys = cacheClient.SearchKeys($"{client}:{subject}:*");
    //    cacheClient.RemoveAll(keys);
    //    return Task.FromResult(true);
    //}

    //public Task StoreAsync(string key, RefreshToken value)
    //    => cacheClient.AddAsync(GetRefreshTokenKey(key, value), value);

    //private string GetRefreshTokenKey(string key, RefreshToken value)
    //    => $"{value.ClientId}:{value.SubjectId}:{key}";
    #endregion

}
