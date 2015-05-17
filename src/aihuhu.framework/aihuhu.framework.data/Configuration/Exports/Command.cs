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

        public string CommandText
        {
            get;
            internal set;
        }

        public ParameterCollection Parameters
        {
            get;
            internal set;
        }

        public object Clone()
        {
            Command cmd = new Command
            {
                CommandText = this.CommandText,
                CommandType = this.CommandType,
                //database负责数据库连接，无需深度copy
                Database = this.Database,
                Name = this.Name,
                Parameters = (ParameterCollection)this.Parameters.Clone()
            };
            return cmd;
        }
    }
}
