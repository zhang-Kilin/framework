using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration
{
    [Serializable]
    public class DatabaseConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        //[StringValidator(MinLength = 1, MaxLength = 50)]
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

        [ConfigurationProperty("connection", IsRequired = true)]
        public string Connection
        {
            get
            {
                return (string)base["connection"];
            }
            set
            {
                base["connection"] = value;
            }
        }

        [ConfigurationProperty("provider", DefaultValue = DatabaseProviderEnum.sql, IsRequired = false)]
        public DatabaseProviderEnum Provider
        {
            get
            {
                return (DatabaseProviderEnum)base["provider"];
            }
            set
            {
                base["provider"] = value;
            }
        }

        [ConfigurationProperty("encrypt", IsRequired = false, DefaultValue = true)]
        public bool Encrypt
        {
            get
            {
                return (bool)base["encrypt"];
            }
            set
            {
                base["encrypt"] = value;
            }
        }
    }
}
