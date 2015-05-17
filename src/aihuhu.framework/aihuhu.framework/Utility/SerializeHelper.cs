using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Utility
{
    public static class SerializeHelper
    {
        /// <summary>
        /// 序列化为xml字符串
        /// </summary>
        /// <param name="obj">要进行序列化的对象</param>
        /// <returns></returns>
        public static string SerializeXml(object obj)
        {
            SerializerBase serializer = SerializeFactory.GetInstance(SerializeType.Xml);
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// 使用指定的命名空间进行序列化
        /// </summary>
        /// <param name="obj">要进行序列化的对象</param>
        /// <param name="namespaces">命名空间</param>
        /// <returns></returns>
        public static string SerializeXml(object obj, string namespaces)
        {
            SerializerBase serializer = SerializeFactory.GetInstance(SerializeType.Xml);
            return serializer.Serialize(obj, namespaces);
        }

        /// <summary>
        /// 序列化为xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXml<T>(T obj)
        {
            SerializerBase serializer = SerializeFactory.GetInstance(SerializeType.Xml);
            return serializer.Serialize<T>(obj);
        }

        /// <summary>
        /// 使用指定的命名空间进行序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        public static string SerializeXml<T>(T obj, string namespaces)
        {
            SerializerBase serializer = SerializeFactory.GetInstance(SerializeType.Xml);
            return serializer.Serialize<T>(obj, namespaces);
        }

        /// <summary>
        /// 将字符串反序列化为实体对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object DeserializeXml(Type type, string xml)
        {
            SerializerBase serializer = SerializeFactory.GetInstance(SerializeType.Xml);
            return serializer.Deserialize(type, xml);
        }

        /// <summary>
        /// 将字符串反序列化为实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeXml<T>(string xml)
        {
            SerializerBase serializer = SerializeFactory.GetInstance(SerializeType.Xml);
            return serializer.Deserialize<T>(xml);
        }
    }
}
