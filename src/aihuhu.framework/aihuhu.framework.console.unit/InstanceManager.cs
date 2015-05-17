using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit
{
    public class InstanceManager
    {

        public static Dictionary<object, List<MethodInfo>> InitAllInstances()
        {
            Dictionary<object, List<MethodInfo>> results = new Dictionary<object, List<MethodInfo>>(200);

            Assembly assemby = typeof(InstanceManager).Assembly;
            Type[] types = assemby.GetTypes();
            List<MethodInfo> methodList = null;
            Type type = null;
            bool flag = false;
            object instance = null;

            for (int i = 0; i < types.Length; i++)
            {
                type = types[i];
                flag = false;
                Attribute attr = type.GetCustomAttribute(typeof(FrameworkTestAttribute), true);
                if (attr != null)
                {
                    methodList = new List<MethodInfo>(10);
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                    for (int j = 0; j < methods.Length; j++)
                    {
                        MethodInfo method = methods[j];
                        attr = method.GetCustomAttribute(typeof(MethodTestAttribute));
                        if (attr != null)
                        {
                            flag = true;
                            methodList.Add(method);
                        }
                    }

                    if (flag)
                    {
                        instance = Activator.CreateInstance(type);
                        results[instance] = methodList;
                    }
                }                
            }

            return results;
        }


    }
}
