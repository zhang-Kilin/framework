using aihuhu.framework.data.Configuration.CommandConfiguration;
using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    internal class CommandConfigurationManager
    {
        private const string DATABASE_FILE_PATH_CONFIGNAME = @"databaseFilePath";
        private const string COMMAND_FILE_PATH_CONFIGNAME = @"commandFilePath";
        
        /// <summary>
        /// dabase配置文件路径
        /// </summary>
        private static string m_DatabaseConfigFilePath = null;

        /// <summary>
        /// commandFileList配置路径
        /// </summary>
        private static string m_CommandConfigFilePath = null;

        private static readonly IDictionary<string, Command> m_CommandsCache = new Dictionary<string, Command>(500);
        private static readonly IDictionary<string, Database> m_DatabaseCache = new Dictionary<string, Database>(20);

        static CommandConfigurationManager()
        {
            OnRefresh = new Refresh((sender, e) => { });
            Refresh();
        }

        private static void Init()
        {
            string databaseFilePath = null;
            string commandFilePath = null;

            FrameworkDataConfigurationSection section = (FrameworkDataConfigurationSection)ConfigurationManager.GetSection("aihuhu.framework.data");
            if (section == null)
            {
                databaseFilePath = ConfigurationManager.AppSettings[DATABASE_FILE_PATH_CONFIGNAME];
                commandFilePath = ConfigurationManager.AppSettings[COMMAND_FILE_PATH_CONFIGNAME];
            }
            else
            {
                databaseFilePath = section.DatabaseFilePath;
                commandFilePath = section.CommandFilePath;
            }
            if (string.IsNullOrWhiteSpace(databaseFilePath))
            {
                throw new ConfigurationException("can not found databaseFilePath ,pls check your config file and find the element of 'aihuhu.framework.data'");
            }
            if (string.IsNullOrWhiteSpace(commandFilePath))
            {
                throw new ConfigurationException("can not found commandFilePath ,pls check your config file and find the element of 'aihuhu.framework.data'");
            }

            databaseFilePath = FileHelper.RootPath(databaseFilePath);
            if (!File.Exists(databaseFilePath))
            {
                throw new FileNotFoundException(string.Format("the file path of '{0}' is not exists.pls check.", databaseFilePath));
            }
            m_DatabaseConfigFilePath = databaseFilePath;

            commandFilePath = FileHelper.RootPath(commandFilePath);
            if (!File.Exists(databaseFilePath))
            {
                throw new FileNotFoundException(string.Format("the file path of '{0}' is not exists.pls check.", commandFilePath));
            }
            m_CommandConfigFilePath = commandFilePath;

            //add watch
            DatabaseFileWatcher.Watch(m_DatabaseConfigFilePath);
            DatabaseFileWatcher.Watch(m_CommandConfigFilePath);

            IDictionary<string, Database> databaseMap = InitDatabase();
            IDictionary<string, Command> commands = InitCommands(databaseMap);
            try
            {
                Monitor.Enter(m_CommandsCache);
                Monitor.Enter(m_DatabaseCache);
                m_DatabaseCache.Clear();
                m_CommandsCache.Clear();
                foreach (string key in databaseMap.Keys)
                {
                    m_DatabaseCache.Add(key, databaseMap[key]);
                }
                foreach (string key in commands.Keys)
                {
                    m_CommandsCache.Add(key, commands[key]);
                }
            }
            finally
            {
                Monitor.Exit(m_CommandsCache);
                Monitor.Exit(m_DatabaseCache);
            }
        }

        private static IDictionary<string, Database> InitDatabase()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = m_DatabaseConfigFilePath;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            DatabaseConfigurationSection section = (DatabaseConfigurationSection)config.GetSection("database");
            IDictionary<string, Database> dic = new Dictionary<string, Database>(20);
            foreach (DatabaseConfigurationElement element in section.DatabaseCollection)
            {
                dic[element.Name] = new Database
                {
                    Connection = element.Connection,
                    Encrypt = element.Encrypt,
                    Name = element.Name,
                    Provider = element.Provider
                };
            }
            return dic;
        }

        private static IDictionary<string, Command> InitCommands(IDictionary<string, Database> databaseMap)
        {
            IDictionary<string, Command> commands = new Dictionary<string, Command>(500);
            using (StreamReader reader = new StreamReader(m_CommandConfigFilePath))
            {
                CommandFileListConfiguration config = SerializeHelper.DeserializeXml<CommandFileListConfiguration>(reader.ReadToEnd());
                if (config.CommandFileList != null)
                {
                    foreach (CommandFileConfiguration file in config.CommandFileList)
                    {
                        string path = file.Path;
                        path = FileHelper.ResolvePath(m_CommandConfigFilePath, path);

                        //add watch
                        DatabaseFileWatcher.Watch(path);

                        IDictionary<string, Command> dic = InitCommandsByConfigFile(path, databaseMap);
                        foreach (string key in dic.Keys)
                        {
                            commands.Add(key, dic[key]);
                        }
                    }
                }
            }

            return commands;
        }

        private static IDictionary<string, Command> InitCommandsByConfigFile(string filePath, IDictionary<string, Database> databaseMap)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("the command file path of '{0}' is not exists.pls check.", filePath));
            }

            IDictionary<string, Command> dic = new Dictionary<string, Command>(50);
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                CommandConfigurationCollection collection = SerializeHelper.DeserializeXml<CommandConfigurationCollection>(reader.ReadToEnd());
                if (collection.CommandList != null)
                {
                    foreach (CommandConfiguration.CommandConfiguration item in collection.CommandList)
                    {
                        Command cmd = new Command
                        {
                            CommandText = item.CommandText,
                            CommandType = item.CommandType,
                            Name = item.Name,
                            Database = databaseMap[item.Database],
                            Parameters = new ParameterCollection()
                        };
                        foreach (ParameterConfiguration p in item.Parameters.ParameterList)
                        {
                            Parameter parameter = new Parameter
                            {
                                DbType = p.DbType,
                                Value = p.DefaultValue,
                                Direction = p.Direction,
                                Name = p.Name,
                                Size = p.Size
                            };
                            cmd.Parameters.Add(parameter.Name, parameter);
                        }
                        dic.Add(item.Name, cmd);
                    }
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取指定command
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        public static Command GetCommand(string commandName)
        {
            Command command = null;

            if (m_CommandsCache.ContainsKey(commandName))
            {
                command = (Command)m_CommandsCache[commandName].Clone();
            }

            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        public static Database GetDatabase(string database)
        {
            if (m_DatabaseCache.ContainsKey(database))
            {
                return m_DatabaseCache[database];
            }
            return null;
        }

        public static void Refresh()
        {
            Init();
            if (OnRefresh != null)
            {
                OnRefresh.Invoke(null, null);
            }
        }

        /// <summary>
        /// 配置文件发生变化时触发的事件
        /// </summary>
        public static event Refresh OnRefresh;
    }
}
