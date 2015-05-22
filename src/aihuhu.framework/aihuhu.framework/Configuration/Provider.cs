using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Configuration
{
    public class ProviderCollection
    {
        private Dictionary<string, Provider> m_dic = new Dictionary<string, Provider>(50);
        private List<string> m_keys = new List<string>(50);

        public Provider this[string name]
        {
            get
            {
                return m_dic.ContainsKey(name) ? m_dic[name] : null;
            }
        }

        public Provider this[int index]
        {
            get
            {
                if (index >= 0
                    && index < m_keys.Count)
                {
                    return m_dic[m_keys[index]];
                }
                return null;
            }
        }

        internal void Add(Provider item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (!m_dic.ContainsKey(item.Name))
            {
                m_keys.Add(item.Name);
            }
            m_dic[item.Name] = item;

        }

        public int Count
        {
            get
            {
                return m_dic.Count;
            }
        }

        public IEnumerator<Provider> GetEnumerator()
        {
            return m_dic.Values.GetEnumerator();
        }

    }

    public class Provider
    {
        internal Provider(ProviderConfigurationElement ele)
        {
            if (ele == null)
            {
                throw new ArgumentNullException("ele");
            }
            this.Name = ele.Name;
            if (!string.IsNullOrWhiteSpace(ele.Type))
            {
                this.Type = Type.GetType(ele.Type);
            }
        }

        public string Name
        {
            get;
            private set;
        }

        public Type Type
        {
            get;
            private set;
        }
    }
}
