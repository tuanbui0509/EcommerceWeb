using System;

namespace EcommerceSolution.Utilities.Cache
{
    public interface IMemoryCacheHelper
    {
        public object GetValue(string key);
        public object Add(string key, object value, DateTimeOffset absExpiration);
        public void Delete(string key);

    }
}