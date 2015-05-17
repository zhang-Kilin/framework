using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Logging
{
    public class LogManager
    {
        public static ILog GetLogProvider(Type type)
        {
            return LogFactory.CreateInstance(type);
        }
    }
}
