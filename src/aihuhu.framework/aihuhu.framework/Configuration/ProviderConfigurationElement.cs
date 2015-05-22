using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Configuration
{
    public class ProviderConfigurationElementCollection : ConfigurationElementCollection
    {

        public ProviderConfigurationElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as ProviderConfigurationElement;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderConfigurationElement)element).Type;
        }
    }


    public class ProviderConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsKey = true, IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)base["type"];
            }
            set
            {
                base["type"] = value;
            }
        }
    }
}
