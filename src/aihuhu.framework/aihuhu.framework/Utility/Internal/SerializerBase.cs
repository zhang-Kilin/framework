using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Utility
{
    internal abstract class SerializerBase
    {   
        internal abstract string Serialize(object obj);

        internal abstract string Serialize<T>(T obj);

        internal abstract string Serialize(object obj, string namespaces);

        internal abstract string Serialize<T>(T obj, string namespaces);

        internal abstract object Deserialize(Type type, string serializeStr);

        internal abstract T Deserialize<T>(string serializeStr);

    }
}
