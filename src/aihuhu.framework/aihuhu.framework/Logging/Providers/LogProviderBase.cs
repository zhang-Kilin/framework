using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Logging.Providers
{
    /// <summary>
    /// LogProvider抽象基类
    /// </summary>
    public abstract class LogProviderBase : ILog
    {
        private Type m_TargetType;
        /// <summary>
        /// 当前Log发生的类
        /// </summary>
        protected Type TargetType
        {
            get { return this.m_TargetType; }
        }
        /// <summary>
        /// Instance LogProvider
        /// </summary>
        /// <param name="type"></param>
        public LogProviderBase(Type type)
        {
            this.m_TargetType = type;
        }

        public abstract void WriteLog(string message, LogType logType);

        public abstract void WriteLog(string message);
    }
}
