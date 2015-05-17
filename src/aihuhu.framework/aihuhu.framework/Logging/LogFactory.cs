using aihuhu.framework.Logging.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Logging
{
    /// <summary>
    /// LogProvider制造工厂
    /// </summary>
    internal class LogFactory
    {
        public static ILog CreateInstance(Type type)
        {
            return new ConsoleLogProvider(type);
        }
    }
}
