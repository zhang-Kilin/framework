using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<object, List<MethodInfo>> instances = InstanceManager.InitAllInstances();
            foreach (object instance in instances.Keys)
            {
                foreach (MethodInfo method in instances[instance])
                {
                    method.Invoke(instance, null);
                }
            }
        }
    }
}
