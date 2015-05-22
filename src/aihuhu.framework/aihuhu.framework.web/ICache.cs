using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        void Add(string key, object value);

        void Add(string key, object value, DateTime expires);

        object Get(string key);

        object Remove(string key);
    }
}
