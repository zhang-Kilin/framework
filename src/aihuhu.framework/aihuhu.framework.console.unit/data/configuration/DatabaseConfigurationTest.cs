using aihuhu.framework.data.Configuration;
using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.data.configuration
{
    [FrameworkTest]
    public class DatabaseConfigurationTest
    {
        [MethodTest]
        public void ConfigurationTest()
        {
            string filePath = ConfigurationManager.AppSettings["databaseFilePath"];
            filePath = FileHelper.RootPath(filePath);
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = filePath;
           System.Configuration.Configuration config =  ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
           DatabaseConfigurationSection section = (DatabaseConfigurationSection) config.GetSection("database");
           DatabaseConfigurationElement element = section.DatabaseCollection["myframework"];
           if (element.Provider != DatabaseProviderEnum.sql)
           {
               throw new UnitTestException("出错：DatabaseConfigurationSection");
           }
           element = section.DatabaseCollection["default"];
           if (!element.Encrypt)
           {
               throw new UnitTestException("出错：DatabaseConfigurationSection");
           }
        }
    }
}
