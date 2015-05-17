using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Logging
{
    /// <summary>
    /// 日志类型
    /// </summary>
    [Serializable]
    public enum LogType
    {
        Info,
        Warning,
        Debug,
        Exception,
        Error
    }
}
