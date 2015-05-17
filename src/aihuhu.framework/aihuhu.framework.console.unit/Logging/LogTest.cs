using aihuhu.framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.Logging
{
    [FrameworkTest]
    public class LogTest
    {
        [MethodTest]
        public void ConsoleLogProviderTest()
        {
            ILog logger = LogManager.GetLogProvider(this.GetType());
            logger.WriteLog("hello word. info");

            logger.WriteLog("hello word. info", LogType.Info);

            logger.WriteLog("hello word. Warning", LogType.Warning);

            logger.WriteLog("hello word. Debug", LogType.Debug);

            logger.WriteLog("hello word. Exception", LogType.Exception);

            logger.WriteLog("hello word. Error", LogType.Error);
        }
    }
}
