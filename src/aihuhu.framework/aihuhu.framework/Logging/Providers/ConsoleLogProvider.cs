using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Logging.Providers
{
    /// <summary>
    /// 控制台输出日志
    /// </summary>
    internal sealed class ConsoleLogProvider : LogProviderBase
    {
        internal ConsoleLogProvider(Type type)
            : base(type)
        { 
        }

        public override void WriteLog(string message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case LogType.Exception:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case LogType.Info:
                default:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("{0} 于 {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), TargetType.FullName);
            Console.WriteLine(message);
        }

        public override void WriteLog(string message)
        {
            WriteLog(message, LogType.Info);
        }
    }
}
