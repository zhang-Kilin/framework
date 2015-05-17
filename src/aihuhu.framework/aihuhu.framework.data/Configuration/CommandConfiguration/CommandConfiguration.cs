using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace aihuhu.framework.data.Configuration.CommandConfiguration
{
    [Serializable, XmlRoot("command")]
    public class CommandConfiguration
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("database")]
        public string Database { get; set; }

        [XmlAttribute("commandType")]
        public CommandType CommandType { get; set; }

        [XmlText, XmlElement("commandText")]
        public string CommandText { get; set; }

        [XmlElement("params")]
        public ParameterConfigurationCollection Parameters { get; set; }
    }
}
