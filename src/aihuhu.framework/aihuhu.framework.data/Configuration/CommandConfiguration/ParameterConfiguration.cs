using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace aihuhu.framework.data.Configuration.CommandConfiguration
{
    [Serializable,XmlRoot("params")]
    public class ParameterConfigurationCollection
    {
        [XmlElement("param")]
        public List<ParameterConfiguration> ParameterList { get; set; }
    }

    [Serializable, XmlRoot("param")]
    public class ParameterConfiguration
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("dataType")]
        public DbType DbType { get; set; }

        [XmlAttribute("size")]
        public int Size { get; set; }

        [XmlAttribute("direction")]
        public ParameterDirection Direction { get; set; }

        [XmlAttribute("defaultValue")]
        public string DefaultValue { get; set; }
    }
}
