using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    internal class Parameter : ICloneable
    {
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            internal set
            {
                this.m_Name = DataCommandManager.FormatParameterName(value);
            }
        }

        public DbType? DbType
        {
            get;
            internal set;
        }

        public int Size
        {
            get;
            internal set;
        }

        public ParameterDirection Direction
        {
            get;
            internal set;
        }

        public object Value
        {
            get;
            internal set;
        }

        public object Clone()
        {
            return new Parameter
            {
                DbType = this.DbType,
                Direction = this.Direction,
                Name = this.Name,
                Size = this.Size,
                Value = this.Value
            };
        }
    }
}
