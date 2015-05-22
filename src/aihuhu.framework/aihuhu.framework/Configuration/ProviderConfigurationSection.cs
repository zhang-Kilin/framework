using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Configuration
{
    public class ProviderConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderConfigurationElementCollection Providers
        {
            get
            {
                return (ProviderConfigurationElementCollection)base["providers"];
            }
            set
            {
                base["providers"] = value;
            }
        }
    }
}
