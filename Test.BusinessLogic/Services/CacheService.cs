using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Test.BusinessLogic.Services.Interfaces;
using Test.BusinessLogic.Settings;

namespace Test.BusinessLogic.Services
{
    public class CacheService<TKey, TValue> : ICacheService<TKey, TValue>
    {
        private readonly IMemoryCache _cache;

        private readonly CacheSettings _settings;

        public CacheService(
            IMemoryCache cache,
            IOptions<CacheSettings> options)
        {
            _cache = cache;
            _settings = options.Value;
        }

        public async Task<TValue?> Get(TKey key, Func<Task<TValue>> action)
        {
            TValue? value = default;
            if (!_cache.TryGetValue(key, out value))
            {
                value = await action();
                _cache.Set(key, value, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_settings.ExpirationTime));
            }

            return value;
        }

        public async Task<IList<TValue>> Get(TKey key, Func<Task<IList<TValue>>> action)
        {
            IList<TValue> value = default;
            if (!_cache.TryGetValue(key, out value))
            {
                value = await action();
                _cache.Set(key, value, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_settings.ExpirationTime));
            }

            return value;
        }
    }
}
