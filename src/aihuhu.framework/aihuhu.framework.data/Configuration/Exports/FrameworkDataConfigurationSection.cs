using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    public class FrameworkDataConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("databaseFilePath")]
        public string DatabaseFilePath
        {
            get
            {
                return (string)base["databaseFilePath"];
            }
            set
            {
                base["databaseFilePath"] = value;
            }
        }

        [ConfigurationProperty("commandFilePath")]
        public string CommandFilePath
        {
            get
            {
                return (string)base["commandFilePath"];
            }
            set
            {
                base["commandFilePath"] = value;
            }
        }
    }
}
