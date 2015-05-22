using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace aihuhu.framework.web.Stores
{
    public class RuntimeCacheStore : ICache
    {
        private Cache m_Cache = HttpRuntime.Cache;

        public void Add(string key, object value)
        {
            m_Cache[key] = value;
        }

        public void Add(string key, object value, DateTime expires)
        {
            m_Cache.Add(key, value, null, expires, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public object Get(string key)
        {
            return m_Cache[key];
        }

        public object Remove(string key)
        {
            return m_Cache.Remove(key);
        }

        public static bool Support()
        {
            return HttpRuntime.Cache != null;
        }
    }
}
