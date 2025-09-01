using Microsoft.Extensions.Caching.Memory;
using School_Manager.Core.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Implemetations
{
    public class CachService : ICachService
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();
        public CachService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrSetAsync<T>(string keyData, Func<Task<T>> acquire, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
        {
            var key = GenerateKey(keyData);

            if (_memoryCache.TryGetValue(key, out T cacheEntry))
            {
                return cacheEntry;
            }

            var myLock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

            await myLock.WaitAsync();
            try
            {
                if (_memoryCache.TryGetValue(key, out cacheEntry))
                {
                    return cacheEntry;
                }

                var data = await acquire();

                var cacheOptions = new MemoryCacheEntryOptions();
                if (absoluteExpireTime.HasValue)
                    cacheOptions.AbsoluteExpirationRelativeToNow = absoluteExpireTime;
                if (slidingExpireTime.HasValue)
                    cacheOptions.SlidingExpiration = slidingExpireTime;

                _memoryCache.Set(key, data, cacheOptions);

                return data;
            }
            finally
            {
                myLock.Release();
                _locks.TryRemove(key, out _);
            }
        }

        public void Remove(string keyData)
        {
            var key = GenerateKey(keyData);
            _memoryCache.Remove(key);
        }
        private static string GenerateKey(object keyData)
        {
            var json = JsonSerializer.Serialize(keyData);
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(json);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        //public async Task<T> GetOrSetByKeyAsync<T>(object key, Func<Task<T>> acquire, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
        //{
        //    return await GetOrSetAsync
        //        (
        //            new { CacheKey = key }, // اینجا خودش keyData میسازه
        //            acquire,
        //            absoluteExpireTime,
        //            slidingExpireTime
        //        );
        //}

        public Task RemoveAsync(string keyData)
        {
            Remove(keyData);
            return Task.CompletedTask;
        }
    }
}
