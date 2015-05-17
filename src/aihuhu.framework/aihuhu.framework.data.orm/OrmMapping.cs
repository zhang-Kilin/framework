using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    internal class OrmMapping
    {
        private TableAttribute m_TableAttr;
        private KeyValuePair<PropertyInfo, ColumnNameAttribute>[] m_PropertyMapping;

        internal OrmMapping(TableAttribute tableAttr, KeyValuePair<PropertyInfo, ColumnNameAttribute>[] propertyMapping)
        {
            this.m_TableAttr = tableAttr;
            this.m_PropertyMapping = propertyMapping;
        }

        public TableAttribute TableAttribute
        {
            get
            {
                return m_TableAttr;
            }
        }

        public KeyValuePair<PropertyInfo, ColumnNameAttribute>[] PropertyMapping
        {
            get
            {
                return m_PropertyMapping;
            }
        }
    }
}
