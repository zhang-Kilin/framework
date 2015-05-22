using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    public static class ExtendCommand
    {
        public static IList<T> ExecuteList<T>(this IDataCommand command)
        {
            IList<T> results = new List<T>();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    T entity = Bind<T>(reader);
                    results.Add(entity);
                }
            }
            return results;
        }

        public static T ExecuteEntity<T>(this IDataCommand command)
        {
            T result = default(T);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = Bind<T>(reader);
                }
            }
            return result;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static T Bind<T>(IDataReader reader)
        {
            T instance = Activator.CreateInstance<T>();
            Type type = typeof(T);

            KeyValuePair<PropertyInfo, ColumnNameAttribute>[] maps = PropertyCacheManager.GetPropertyMapping(type);
            KeyValuePair<PropertyInfo, ColumnNameAttribute> map;
            for (int i = 0; i < maps.Length; i++)
            {
                map = maps[i];
                object value = null;
                try
                {
                    value = reader[map.Value.ColumnName];
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
                //数据兼容处理
                if (value == DBNull.Value)
                {
                    value = null;
                }

                Bind(instance, map.Key, value);
            }

            return instance;
        }

        private static void Bind(object target, PropertyInfo property, object value)
        {
            Type type = property.PropertyType;

            if (type.IsEnum)
            {
                if (!object.Equals(value, null))
                {
                    object enumValue = Enum.Parse(type, value.ToString());
                    property.SetValue(target, enumValue, null);
                }
            }
            else if (type == typeof(DateTime))
            {
                DateTime time = default(DateTime);
                if (!object.Equals(value, null))
                {
                    time = Convert.ToDateTime(value);
                }
                property.SetValue(target, time, null);
            }
            else if (type == typeof(DateTime?))
            {
                DateTime? time = default(DateTime?);
                if (!object.Equals(value, null))
                {
                    DateTime tm;
                    if (DateTime.TryParse(value.ToString(), out tm))
                    {
                        time = tm;
                    }
                }
                property.SetValue(target, time, null);
            }
            else if (type == typeof(string))
            {
                property.SetValue(target, value ?? default(string), null);
            }
            else if (type == typeof(int))
            {
                property.SetValue(target, value ?? default(int), null);
            }
            else if (type == typeof(int?))
            {
                property.SetValue(target, value ?? default(int?), null);
            }
            else if (type == typeof(long))
            {
                property.SetValue(target, value ?? default(long), null);
            }
            else if (type == typeof(long?))
            {
                property.SetValue(target, value ?? default(long?), null);
            }
            else if (type == typeof(bool))
            {
                property.SetValue(target, value ?? default(bool), null);
            }
            else if (type == typeof(bool?))
            {
                property.SetValue(target, value ?? default(bool?), null);
            }
            else if (type == typeof(double))
            {
                property.SetValue(target, value ?? default(double), null);
            }
            else if (type == typeof(double?))
            {
                property.SetValue(target, value ?? default(double?), null);
            }
            else if (type == typeof(decimal))
            {
                property.SetValue(target, value ?? default(decimal), null);
            }
            else if (type == typeof(decimal?))
            {
                property.SetValue(target, value ?? default(decimal?), null);
            }
            else if (type == typeof(float))
            {
                property.SetValue(target, value ?? default(float), null);
            }
            else if (type == typeof(float?))
            {
                property.SetValue(target, value ?? default(float?), null);
            }
        }
    }
}
