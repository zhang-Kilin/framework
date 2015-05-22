using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    internal static class PropertyCacheManager
    {
        static PropertyCacheManager()
        {
            DataCommandManager.OnRefresh += (sender, e) =>
            {
                m_PropertyCache.Clear();
            };
        }

        private static readonly Dictionary<Type, OrmMapping> m_PropertyCache = new Dictionary<Type, OrmMapping>(100);

        internal static KeyValuePair<PropertyInfo, ColumnNameAttribute>[] GetPropertyMapping(Type modelType)
        {
            OrmMapping ormMapping = GetOrmMapping(modelType);

            return ormMapping.PropertyMapping;
        }

        internal static OrmMapping GetOrmMapping(Type modelType)
        {
            if (!m_PropertyCache.ContainsKey(modelType))
            {
                OrmMapping ormMapping = GetOrmMappingWithoutCache(modelType);

                if (!m_PropertyCache.ContainsKey(modelType))
                {
                    m_PropertyCache[modelType] = ormMapping;
                }
            }

            return m_PropertyCache[modelType];
        }

        private static OrmMapping GetOrmMappingWithoutCache(Type modelType)
        {
            KeyValuePair<PropertyInfo, ColumnNameAttribute>[] propertyMapping = null;

            List<KeyValuePair<PropertyInfo, ColumnNameAttribute>> arr = new List<KeyValuePair<PropertyInfo, ColumnNameAttribute>>();
            bool primaryKeyExists = false;
            PropertyInfo[] properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo property = null;
            ColumnNameAttribute attr = null;
            for (int i = 0; i < properties.Length; i++)
            {
                property = properties[i];
                //属性不可写，continue
                if (!property.CanWrite)
                {
                    continue;
                }

                attr = (ColumnNameAttribute)Attribute.GetCustomAttribute(property, typeof(ColumnNameAttribute));
                //未找到映射字段，continue
                if (attr == null)
                {
                    continue;
                }
                //主键处理 
                if (attr.IsPrimaryKey)
                {
                    //主键不能重复出现
                    if (primaryKeyExists)
                    {
                        throw new InvalidOperationException(string.Format("primary key arise once more in {0},pls check it.", modelType.FullName));
                    }
                    primaryKeyExists = true;
                }

                arr.Add(new KeyValuePair<PropertyInfo, ColumnNameAttribute>(property, attr));
            }

            propertyMapping = arr.ToArray();
            TableAttribute tableAttr = GetTableAttribute(modelType);

            OrmMapping ormMapping = new OrmMapping(tableAttr, propertyMapping);

            return ormMapping;
        }

        private static TableAttribute GetTableAttribute(Type modelType)
        {
            TableAttribute attr = (TableAttribute)Attribute.GetCustomAttribute(modelType, typeof(TableAttribute));
            return attr;
        }

    }
}
