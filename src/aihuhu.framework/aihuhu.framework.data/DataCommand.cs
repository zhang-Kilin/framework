using aihuhu.framework.data.Configuration.Exports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data
{
    public sealed class DataCommand : IDataCommand
    {
        private Command m_CommandConfiguration = null;
        private Database m_DatabaseConfiguration = null;

        private IDbCommand m_CurrentCommand = null;

        private DataCommand(Command command)
            : this(command.Database)
        {
            this.m_CommandConfiguration = command;
            this.m_DatabaseConfiguration = command.Database;
            this.CommandText = command.CommandText;
            this.CommandType = command.CommandType;
        }

        private DataCommand(Database database)
        {
            this.m_DatabaseConfiguration = database;
            this.m_CommandConfiguration = new Command
            {
                CommandType = System.Data.CommandType.Text,
                Database = database,
                Parameters = new ParameterCollection()
            };
        }

        public int CommandTimeout
        {
            get
            {
                return this.m_CommandConfiguration.CommandTimeout;
            }
            set
            {
                this.m_CommandConfiguration.CommandTimeout = value;
            }
        }

        public string CommandText
        {
            get
            {
                return this.m_CommandConfiguration.CommandText;
            }
            set
            {
                this.m_CommandConfiguration.CommandText = value;
            }
        }

        public CommandType CommandType
        {
            get
            {
                return this.m_CommandConfiguration.CommandType;
            }
            set
            {
                this.m_CommandConfiguration.CommandType = value;
            }
        }

        public int ExecuteNonQuery()
        {
            using (IDbCommand cmd = DataCommandManager.Create(this.m_CommandConfiguration))
            {
                this.m_CurrentCommand = cmd;
                return cmd.ExecuteNonQuery();
            }
        }

        public IDataReader ExecuteReader()
        {
            IDbCommand cmd = DataCommandManager.Create(this.m_CommandConfiguration);
            this.m_CurrentCommand = cmd;
            IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public object ExecuteScalar()
        {
            using (IDbCommand cmd = DataCommandManager.Create(this.m_CommandConfiguration))
            {
                this.m_CurrentCommand = cmd;
                return cmd.ExecuteScalar();
            }
        }

        public DataSet ExecuteDataset()
        {
            IDataAdapter adapter = DataCommandManager.CreateAdapter(this.m_CommandConfiguration);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }


        public void AddInParameter(string name, object value)
        {
            Parameter parameter = new Parameter
            {
                Name = name,
                Direction = ParameterDirection.Input,
                Value = value
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInParameter(string name, DbType dbType)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.Input
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInParameter(string name, DbType dbType, int size)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.Input,
                Size = size
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInParameter(string name, DbType dbType, object value)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.Input,
                Value = value
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInParameter(string name, DbType dbType, int size, object value)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.Input,
                Size = size
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddOutParameter(string name, DbType dbType)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.Output
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddOutParameter(string name, DbType dbType, int size)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.Output,
                Size = size
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInOutParameter(string name, DbType dbType)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.InputOutput
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInOutParameter(string name, DbType dbType, int size)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.InputOutput,
                Size = size
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInOutParameter(string name, DbType dbType, int size, object value)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.InputOutput,
                Size = size,
                Value = value
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddInOutParameter(string name, DbType dbType, object value)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.InputOutput
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddReturnParameter(string name, DbType dbType)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.ReturnValue
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void AddReturnParameter(string name, DbType dbType, int size)
        {
            Parameter parameter = new Parameter
            {
                DbType = dbType,
                Name = name,
                Direction = ParameterDirection.ReturnValue,
                Size = size
            };
            this.m_CommandConfiguration.Parameters.Add(parameter.Name, parameter);
        }

        public void SetParameterValue(string name, object value)
        {
            name = DataCommandManager.FormatParameterName(name);
            this.m_CommandConfiguration.Parameters[name].Value = value;
        }

        public object GetParameterValue(string name)
        {
            name = DataCommandManager.FormatParameterName(name);
            RefreshParameters();
            return this.m_CommandConfiguration.Parameters[name].Value;
        }

        public void RemoveParameter(string name)
        {
            name = DataCommandManager.FormatParameterName(name);
            this.m_CommandConfiguration.Parameters.Remove(name);
        }

        public void ClearParameter()
        {
            this.m_CommandConfiguration.Parameters.Clear();
        }

        public void Dispose()
        {
            if (this.m_CurrentCommand != null)
            {
                this.m_CurrentCommand.Dispose();
            }
        }

        public static IDataCommand Create(string commandName)
        {
            Command command = CommandConfigurationManager.GetCommand(commandName);
            return new DataCommand(command);
        }

        public static IDataCommand CreateCustomCommand(string database)
        {
            Database db = CommandConfigurationManager.GetDatabase(database);
            return new DataCommand(db);
        }

        public IDataCommand Clone()
        {
            Command cmd = (Command)this.m_CommandConfiguration.Clone();
            return new DataCommand(cmd);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }


        private void RefreshParameters()
        {
            if (this.m_CurrentCommand != null)
            {
                foreach (IDbDataParameter param in this.m_CurrentCommand.Parameters)
                {
                    if (param.Direction != ParameterDirection.Input)
                    {
                        this.m_CommandConfiguration.Parameters[param.ParameterName].Value = param.Value;
                    }
                }
            }
        }


    }
}
