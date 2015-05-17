using aihuhu.framework.data.Configuration.CommandConfiguration;
using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.data.configuration
{
    [FrameworkTest]
    public class CommandConfigurationTest
    {
        [MethodTest]
        public void CommandFileListTest()
        {
            string path = FileHelper.RootPath(ConfigurationManager.AppSettings["commandFilePath"]);

            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                CommandFileListConfiguration config = SerializeHelper.DeserializeXml<CommandFileListConfiguration>(reader.ReadToEnd());
                if (config == null)
                {
                    throw new UnitTestException("出错：CommandFileListConfiguration");
                }
                if (config.CommandFileList == null)
                {
                    throw new UnitTestException("出错：CommandFileListConfiguration");
                }
                Console.WriteLine(config.CommandFileList[0].Path);
            }

        }

        [MethodTest]
        public void CommandTest()
        {
            CommandConfiguration config = new CommandConfiguration
            {
                CommandText = "Select top 1 1 from users",
                CommandType = System.Data.CommandType.Text,
                Database = "myframework",
                Name = "QueryUser",
                Parameters = new ParameterConfigurationCollection
                {
                    ParameterList = new List<ParameterConfiguration>() { 
                        new ParameterConfiguration{
                            DbType = System.Data.DbType.String,
                            Name = "UserName",
                            DefaultValue = "Kilin",
                            Direction = ParameterDirection.Input,
                            Size = 50
                        }
                    }
                }
            };
            string txt = SerializeHelper.SerializeXml(config);
            Console.WriteLine(txt);
        }

        [MethodTest]
        public void CommandDeserializeTest()
        {
            string path = FileHelper.RootPath(ConfigurationManager.AppSettings["commandFilePath"]);
            using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
            {
                CommandFileListConfiguration config = SerializeHelper.DeserializeXml<CommandFileListConfiguration>(reader.ReadToEnd());
                string relativeFilePath = config.CommandFileList[0].Path;
                path = FileHelper.ResolvePath(path, relativeFilePath);
                using (StreamReader commandReader = new StreamReader(path,Encoding.UTF8))
                {
                    CommandConfigurationCollection command = SerializeHelper.DeserializeXml<CommandConfigurationCollection>(commandReader.ReadToEnd());
                    if (command == null || command.CommandList == null)
                    {
                        throw new UnitTestException("出错：CommandConfiguration");
                    }
                    Console.WriteLine(command.CommandList[0].CommandText);
                }
            }
        }
    }
}
