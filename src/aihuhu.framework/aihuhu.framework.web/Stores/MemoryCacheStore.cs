using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web.Stores
{
    public class MemoryCacheStore : ICache
    {
        private Hashtable m_Cache = new Hashtable(500);

        public void Add(string key, object value)
        {
            m_Cache[key] = new MemoryCacheAdapter
            {
                Key = key,
                Value = value
            };
        }

        public void Add(string key, object value, DateTime expires)
        {
            m_Cache[key] = new MemoryCacheAdapter
            {
                Key = key,
                Value = value,
            };
        }

        public object Get(string key)
        {
            MemoryCacheAdapter value = m_Cache[key] as MemoryCacheAdapter;
            if (object.Equals(value, null))
            {
                return null;
            }
            if (value.Expires.HasValue
                && value.Expires.Value <= DateTime.Now)
            {
                m_Cache.Remove(key);
                return null;
            }
            return value.Value;
        }

        public object Remove(string key)
        {
            object value = Get(key);
            m_Cache.Remove(key);
            return value;
        }

        private class MemoryCacheAdapter
        {
            public string Key { get; set; }
            public Object Value { get; set; }
            public DateTime? Expires { get; set; }
        }
    }


}
