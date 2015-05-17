using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.TestModels
{
    [Serializable]
   public  class Emplooye
    {
        public string Name { get; set; }

        public int Age { get;set;}

        public string Department { get; set; }

        public decimal Salary { get; set; }
    }
}
