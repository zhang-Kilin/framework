using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace aihuhu.framework.data.Configuration.CommandConfiguration
{
    [Serializable, XmlRoot("file")]
    public class CommandFileConfiguration
    {
        [XmlAttribute("path")]
        public string Path { get; set; }
    }
}
