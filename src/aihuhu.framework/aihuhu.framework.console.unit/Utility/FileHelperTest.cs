using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.Utility
{
    [FrameworkTest]
    public class FileHelperTest
    {
        [MethodTest]
        public void ResolvePathTest()
        {
            string path = "C:\\Users\\Kilin\\test.txt";
            string expect = FileHelper.ResolvePath(path, "user.xml");
            if (expect != "C:\\Users\\Kilin\\user.xml")
            {
                throw new UnitTestException("FileHelper.ResolvePath(path, \"user.xml\")");
            }
            Console.WriteLine(expect);
        }

        [MethodTest]
        public void ResolvePathTest1()
        {
            string path = "C:\\Users\\Kilin\\Haha\\";
            string expect = FileHelper.ResolvePath(path, "user.xml");
            if (expect != "C:\\Users\\Kilin\\Haha\\user.xml")
            {
                throw new UnitTestException("FileHelper.ResolvePath(path, \"user.xml\")");
            }
            Console.WriteLine(expect);
        }
    }
}
