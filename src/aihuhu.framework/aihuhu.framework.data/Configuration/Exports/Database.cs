using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    internal class Database
    {
        internal Database() { }

        public string Name
        {
            get;
            internal set;
        }

        public string Connection
        {
            get;
            internal set;
        }

        public DatabaseProviderEnum Provider
        {
            get;
            internal set;
        }

        public bool Encrypt
        {
            get;
            internal set;
        }
    }
}
