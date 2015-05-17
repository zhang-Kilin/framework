using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration
{
    [Serializable]
    public class DatabaseConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("databaseCollection", IsRequired = true)]
        [ConfigurationCollection(typeof(DatabaseConfigurationElement), AddItemName = "add")]
        public DatabaseConfigurationElementCollection DatabaseCollection
        {
            get
            {
                return (DatabaseConfigurationElementCollection)base["databaseCollection"];
            }
            set
            {
                base["databaseCollection"] = value;
            }
        }
    }
}
