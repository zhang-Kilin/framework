using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    internal class Command : ICloneable
    {
        private ParameterCollection m_Parameters = new ParameterCollection();

        public string Name
        {
            get;
            internal set;
        }

        public Database Database
        {
            get;
            internal set;
        }

        public CommandType CommandType
        {
            get;
            internal set;
        }

        public int CommandTimeout
        {
            get;
            internal set;
        }

        public string CommandText
        {
            get;
            internal set;
        }

        public ParameterCollection Parameters
        {
            get
            {
                return m_Parameters;
            }
            internal set
            {
                this.m_Parameters = value ?? new ParameterCollection();
            }
        }

        public object Clone()
        {
            Command cmd = new Command
            {
                CommandText = this.CommandText,
                CommandType = this.CommandType,
                CommandTimeout = this.CommandTimeout,
                //database负责数据库连接，无需深度copy
                Database = this.Database,
                Name = this.Name,
                Parameters = (ParameterCollection)this.Parameters.Clone()
            };
            return cmd;
        }
    }
}
