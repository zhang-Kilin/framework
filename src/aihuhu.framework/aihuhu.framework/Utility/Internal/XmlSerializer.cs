using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemXmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace aihuhu.framework.Utility
{
    internal class XmlSerializer : SerializerBase
    {
        internal override string Serialize(object obj)
        {
            return Serialize(obj, null);
        }

        internal override string Serialize<T>(T obj)
        {
            return Serialize<T>(obj, null);
        }

        internal override string Serialize(object obj, string namespaces)
        {
            if (obj == null)
            {
                return null;
            }
            Type type = obj.GetType();
            SystemXmlSerializer ser = new SystemXmlSerializer(type);
            string xml = string.Empty;
            //namespaces = namespaces ?? string.Empty;
            System.Xml.Serialization.XmlSerializerNamespaces n = new System.Xml.Serialization.XmlSerializerNamespaces();
            if (!string.IsNullOrWhiteSpace(namespaces))
            {
                n.Add("nm", namespaces);
            }
            else
            {
                n.Add(string.Empty, string.Empty);
            }
            using (MemoryStream stream = new MemoryStream())
            {
                ser.Serialize(stream, obj, n);
                xml = Encoding.UTF8.GetString(stream.ToArray());
            }
            xml = xml.Replace("\0", "");
            return xml;
        }

        internal override string Serialize<T>(T obj, string namespaces)
        {
            if (object.Equals(obj, null))
            {
                return null;
            }
            Type type = typeof(T);
            string xml = string.Empty;
            //namespaces = namespaces ?? string.Empty;
            SystemXmlSerializer ser = new SystemXmlSerializer(type);
            System.Xml.Serialization.XmlSerializerNamespaces n = new System.Xml.Serialization.XmlSerializerNamespaces();
            if (!string.IsNullOrWhiteSpace(namespaces))
            {
                n.Add("nm", namespaces);
            }
            else
            {
                n.Add(string.Empty, string.Empty);
            }
            using (MemoryStream stream = new MemoryStream())
            {
                ser.Serialize(stream, obj, n);
                xml = Encoding.UTF8.GetString(stream.ToArray());
            }
            xml = xml.Replace("\0", "");
            return xml;
        }

        internal override object Deserialize(Type type, string serializeStr)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (string.IsNullOrWhiteSpace(serializeStr))
            {
                throw new ArgumentNullException("serializeStr");
            }
            object result = null;
            SystemXmlSerializer ser = new SystemXmlSerializer(type);
            byte[] buffer = Encoding.UTF8.GetBytes(serializeStr);
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                result = ser.Deserialize(stream);
            }

            return result;
        }

        internal override T Deserialize<T>(string serializeStr)
        {
            Type type = typeof(T);
            return (T)Deserialize(type, serializeStr);
        }

    }
}
