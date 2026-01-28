using Microsoft.Extensions.Caching.Memory;
using UStack.Identity.Application.Interfaces;

namespace UStack.Identity.Infrastructure.Implementations;

public class InMemoryCacheStorage : IInMemoryCacheStorage
{
    private readonly IMemoryCache _memoryCache;

    public InMemoryCacheStorage(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetString(string key)
    {
        return _memoryCache.TryGetValue(key, out string value) ? value : null;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void SetString(string key, string value, TimeSpan? expiration = null)
    {
        if (expiration.HasValue)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            _memoryCache.Set(key, value, options);
            return;
        
        }
        _memoryCache.Set(key, value);
    }
}
