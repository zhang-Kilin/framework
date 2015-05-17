using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    internal class ParameterCollection : IDictionary<string, Parameter>,ICloneable
    {
        private IDictionary<string, Parameter> m_Map = new Dictionary<string, Parameter>();

        public void Add(string key, Parameter value)
        {
            this.m_Map.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return m_Map.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get
            {
                return m_Map.Keys;
            }
        }

        public bool Remove(string key)
        {
            return m_Map.Remove(key);
        }

        public bool TryGetValue(string key, out Parameter value)
        {
            return m_Map.TryGetValue(key, out value);
        }

        public ICollection<Parameter> Values
        {
            get
            {
                return m_Map.Values;
            }
        }

        public Parameter this[string key]
        {
            get
            {
                if (this.ContainsKey(key))
                {
                    return m_Map[key];
                }
                return null;
            }
            set
            {
                m_Map[key] = value;
            }
        }

        public void Add(KeyValuePair<string, Parameter> item)
        {
            m_Map.Add(item);
        }

        public void Clear()
        {
            m_Map.Clear();
        }

        public bool Contains(KeyValuePair<string, Parameter> item)
        {
            return m_Map.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, Parameter>[] array, int arrayIndex)
        {
            m_Map.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return m_Map.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(KeyValuePair<string, Parameter> item)
        {
            return m_Map.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, Parameter>> GetEnumerator()
        {
            return m_Map.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_Map.GetEnumerator();
        }

        public object Clone()
        {
            ParameterCollection collection = new ParameterCollection();
            foreach (string key in this.Keys)
            {
                Parameter parameter = (Parameter)this[key].Clone();
                collection.Add(parameter.Name, parameter);
            }
            return collection;
        }
    }
}
