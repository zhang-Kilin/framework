using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.orm
{
    interface ISave
    {
        void Save<T>(Collection<T> collection) where T : ModelBase;
    }
}
