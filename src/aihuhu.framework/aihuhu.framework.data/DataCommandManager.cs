using aihuhu.framework.data.Configuration.Exports;
using aihuhu.framework.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data
{
    public static class DataCommandManager
    {
        public const string PARAMETER_PREFIX = "@";

        static DataCommandManager()
        {
            OnRefresh = new Refresh((sender, e) => { });

            CommandConfigurationManager.OnRefresh += (sender, e) =>
            {
                OnRefresh.Invoke(sender, e);
            };
        }

        public static IDbCommand CreateCommand(string commandName)
        {
            Command commandConfig = CommandConfigurationManager.GetCommand(commandName);
            return Create(commandConfig);
        }

        public static IDbCommand CreateCustomCommand(string database)
        {
            Database db = CommandConfigurationManager.GetDatabase(database);
            if (db == null)
            {
                return null;
            }
            return Create(db);
        }

        public static IDbDataAdapter CreateAdapter(string commandName)
        {
            Command commandConfig = CommandConfigurationManager.GetCommand(commandName);
            return CreateAdapter(commandConfig);
        }

        public static string FormatParameterName(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }
            return parameterName.StartsWith(PARAMETER_PREFIX) ? parameterName : string.Format("{0}{1}", PARAMETER_PREFIX, parameterName);
        }

        internal static IDbDataAdapter CreateAdapter(Command commandConfig)
        {
            if (commandConfig == null)
            {
                return null;
            }
            Database db = commandConfig.Database;
            if (db.Provider != Configuration.DatabaseProviderEnum.sql)
            {
                throw new NotImplementedException();
            }
            IDbCommand command = Create(commandConfig);
            return new SqlDataAdapter((SqlCommand)command);
        }

        internal static IDbCommand Create(Command commandConfig)
        {
            if (commandConfig == null)
            {
                return null;
            }
            Database databaseConfig = commandConfig.Database;
            IDbCommand command = Create(databaseConfig);
            command.CommandText = commandConfig.CommandText;
            command.CommandType = commandConfig.CommandType;
            if (commandConfig.CommandTimeout > 0)
            {
                command.CommandTimeout = commandConfig.CommandTimeout;
            }

            if (commandConfig.Parameters != null
                && commandConfig.Parameters.Count > 0)
            {
                foreach (string key in commandConfig.Parameters.Keys)
                {
                    Parameter p = commandConfig.Parameters[key];
                    IDbDataParameter param = command.CreateParameter();
                    if (p.DbType.HasValue)
                    {
                        param.DbType = p.DbType.Value;
                    }
                    param.ParameterName = FormatParameterName(p.Name);
                    if (p.Size > 0)
                    {
                        param.Size = p.Size;
                    }
                    param.Direction = p.Direction;
                    param.Value = p.Value;
                    command.Parameters.Add(param);
                }
            }
            return command;
        }

        private static IDbCommand Create(Database database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }
            IDbCommand cmd = null;
            if (database.Provider == Configuration.DatabaseProviderEnum.sql)
            {
                cmd = CreateSqlCommand(database);
            }
            else
            {
                throw new NotImplementedException();
            }
            return cmd;
        }

        private static IDbCommand CreateSqlCommand(Database database)
        {
            IDbCommand cmd = new SqlCommand();
            string conn = GetConnectionString(database);
            cmd.Connection = new SqlConnection(conn);
            cmd.Connection.Open();
            return cmd;
        }

        private static string GetConnectionString(Database database)
        {
            string conn = database.Connection;
            if (database.Encrypt)
            {
                conn = SecurityHelper.AESDecrypt(conn);
            }
            return conn;
        }

        /// <summary>
        /// 当data配置文件发生变化时触发的事件
        /// </summary>
        public static event Refresh OnRefresh;
    }

    public delegate void Refresh(object sender, EventArgs e);
}
