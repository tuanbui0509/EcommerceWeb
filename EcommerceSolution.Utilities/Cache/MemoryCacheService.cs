using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace EcommerceSolution.Utilities.Cache
{
    public class MemoryCacheHelper : IMemoryCacheHelper
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public object GetValue(string key)
        {
            return _memoryCache.Get(key);
        }

        public object Add(string key, object value, DateTimeOffset absExpiration)
        {
            return _memoryCache.Set(key, value, absExpiration);
        }

        public void Delete(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
