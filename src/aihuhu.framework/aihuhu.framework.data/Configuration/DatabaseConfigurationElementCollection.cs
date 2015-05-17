using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration
{
    public class DatabaseConfigurationElementCollection : ConfigurationElementCollection
    {
        public DatabaseConfigurationElement this[string name]
        {
            get
            {
                return (DatabaseConfigurationElement)base.BaseGet(name);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseConfigurationElement)element).Name;
        }
    }
}
