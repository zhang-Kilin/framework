using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm.Internal
{
    internal class PropertyWriter
    {
        internal static void Writer<T>(T model, IDataReader reader)
        {

        }

        /// <summary>
        /// 使用新对象刷新model里的属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">待刷新的model</param>
        /// <param name="expectModel">期望刷新后的model</param>
        internal static void Refresh<T>(T model, T expectModel)
            where T : ModelBase
        {
            KeyValuePair<PropertyInfo, ColumnNameAttribute>[] properties = PropertyCacheManager.GetPropertyMapping(typeof(T));
            if (properties == null)
            {
                return;
            }
            PropertyInfo property;
            ColumnNameAttribute attr = null;
            model.BeginWrite();
            try
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    property = properties[i].Key;
                    attr = properties[i].Value;
                    if (property.CanRead && property.CanWrite)
                    {
                        property.SetValue(model, property.GetValue(expectModel, null), null);
                    }
                }
            }
            finally
            {
                model.EndWrite();
            }
        }


    }
}
