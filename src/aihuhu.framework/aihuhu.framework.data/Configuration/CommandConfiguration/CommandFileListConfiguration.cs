using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace aihuhu.framework.data.Configuration.CommandConfiguration
{
    [Serializable, XmlRoot("commandFiles")]
    public class CommandFileListConfiguration
    {
        [XmlElement("file")]
        public List<CommandFileConfiguration> CommandFileList
        {
            get;
            set;
        }
    }
}
