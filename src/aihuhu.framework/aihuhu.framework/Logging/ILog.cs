using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Logging
{
    public interface ILog
    {
        /// <summary>
        /// 书写日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="logType">日志类型</param>
        void WriteLog(string message, LogType logType);

        /// <summary>
        /// 以默认类型书写日志，通常为Info
        /// </summary>
        /// <param name="message">日志内容</param>
        void WriteLog(string message);
    }
}
