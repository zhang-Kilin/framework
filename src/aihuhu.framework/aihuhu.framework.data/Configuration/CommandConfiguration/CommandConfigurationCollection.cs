using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace aihuhu.framework.data.Configuration.CommandConfiguration
{
    [Serializable,XmlRoot("commands")]
    public class CommandConfigurationCollection
    {
        [XmlElement("command")]
        public List<CommandConfiguration> CommandList { get; set; }
    }
}
