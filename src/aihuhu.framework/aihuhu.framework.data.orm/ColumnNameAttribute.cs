using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public ColumnNameAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 是否主键
        /// 一个类型中，主键只能出现一次
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否自增长列
        /// </summary>
        public bool IsIdentity { get; set; }
    }
}
