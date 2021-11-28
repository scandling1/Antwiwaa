using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Antwiwaa.ArchBit.Infrastructure.Services
{
    public static class CacheService
    {
        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string cacheId)
        {
            var jsonData = await cache.GetStringAsync(cacheId);

            return jsonData is null ? default : JsonSerializer.Deserialize<T>(jsonData);
        }

        public static async Task SetRecordAsync<T>(this IDistributedCache cache, T data, string cacheId,
            TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(300),
                SlidingExpiration = slidingExpiration
            };

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(cacheId, jsonData, options);
        }
    }
}