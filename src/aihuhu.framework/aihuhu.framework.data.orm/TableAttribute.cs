using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName)
        {
            this.TableName = tableName;
            this.Schema = "dbo";
        }
        /// <summary>
        /// 映射的表明
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// schema，默认为dbo
        /// </summary>
        public string Schema { get; set; }

    }
}
