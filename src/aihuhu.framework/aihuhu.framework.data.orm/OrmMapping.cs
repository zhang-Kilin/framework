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
        private KeyValuePair<PropertyInfo, ColumnNameAttribute> m_PrimaryKey = new KeyValuePair<PropertyInfo, ColumnNameAttribute>(null, null);

        internal OrmMapping(TableAttribute tableAttr, KeyValuePair<PropertyInfo, ColumnNameAttribute>[] propertyMapping)
        {
            this.m_TableAttr = tableAttr;
            this.m_PropertyMapping = propertyMapping;
            if (propertyMapping != null)
            {
                for (int i = 0; i < propertyMapping.Length; i++)
                {
                    if (propertyMapping[i].Value.IsPrimaryKey)
                    {
                        m_PrimaryKey = propertyMapping[i];
                        break;
                    }
                }
            }
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

        public KeyValuePair<PropertyInfo, ColumnNameAttribute> PrimaryKey
        {
            get
            {
                return m_PrimaryKey;
            }
        }
    }
}
