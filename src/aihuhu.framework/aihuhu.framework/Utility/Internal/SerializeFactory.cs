using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Utility
{
    public enum SerializeType
    { 
        Xml,
        JSON,
        Binary
    }

    class SerializeFactory
    {
        private static readonly SerializerBase m_XmlSerializer = new XmlSerializer();

        public static SerializerBase GetInstance(SerializeType serializeType)
        {
            SerializerBase serializer = null;
            switch (serializeType)
            {
                case SerializeType.JSON:
                    break;
                case SerializeType.Binary:
                    break;

                case SerializeType.Xml:
                default:
                    serializer = m_XmlSerializer;
                    break;
            }
            return serializer;
        }
    }
}
