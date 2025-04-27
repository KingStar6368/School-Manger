using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Manager.Core.Services.Interfaces
{
    public interface ICachService
    {
        Task<T> GetOrSetAsync<T>(
        object key,
        Func<Task<T>> acquire,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? slidingExpireTime = null);
        Task<T> GetOrSetByKeyAsync<T>(
        string key,
        Func<Task<T>> acquire,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? slidingExpireTime = null);
        void Remove(object key);
        Task RemoveAsync(object value);
    }
}
