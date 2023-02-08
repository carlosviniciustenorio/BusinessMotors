using Microsoft.Extensions.Caching.Distributed;

namespace CManager.Integration.Cache
{
    public sealed class CacheService : ICacheService
    {
        private readonly DistributedCacheEntryOptions _options;
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache) 
            => (_options, _distributedCache) = (new DistributedCacheEntryOptions {
                                                    SlidingExpiration = TimeSpan.FromSeconds(1000),
                                                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3000)
                                                }, distributedCache);

        public async Task Add(string key, string value) => await _distributedCache.SetStringAsync(key, value, _options);
        public async Task<string?> Get(string key) => await _distributedCache.GetStringAsync(key);
        public async Task Remove(string key) => await _distributedCache.RemoveAsync(key);
    }
}
