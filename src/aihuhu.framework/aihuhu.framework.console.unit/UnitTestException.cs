using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit
{
    public class UnitTestException : ApplicationException
    {
        public UnitTestException() : base() { }

        public UnitTestException(string message) : base(message) { }
    }
}
