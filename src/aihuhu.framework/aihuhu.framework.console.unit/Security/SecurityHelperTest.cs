using aihuhu.framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.console.unit.Security
{
    [FrameworkTest]
    public class SecurityHelperTest
    {
        [MethodTest]
        public void DESSecurityTest()
        {
            string str = "金胖子挂了";
            string encryptStr = SecurityHelper.DESEncrypt(str);

            string expect = SecurityHelper.DESDecrypt(encryptStr);

            if (str != expect)
            {
                throw new UnitTestException("失败：SecurityHelper.DESEncrypt(str);SecurityHelper.DESDecrypt(encryptStr);");
            }

            Console.WriteLine(encryptStr);
        }

        [MethodTest]
        public void AESSecurityTest()
        {
            string str = "金三胖挂了";
            string encryptStr = SecurityHelper.AESEncrypt(str);

            string expect = SecurityHelper.AESDecrypt(encryptStr);

            if (str != expect)
            {
                throw new UnitTestException("失败：SecurityHelper.DESEncrypt(str);SecurityHelper.DESDecrypt(encryptStr);");
            }

            Console.WriteLine("数据库连接串：{0}", encryptStr);
        }

        [MethodTest]
        public void MD5SecurityTest()
        {
            string str = "金胖子挂了";
            string encryptStr = SecurityHelper.MD5(str);
            if (encryptStr.Length != 32)
            {
                throw new UnitTestException("失败：SecurityHelper.MD5(str)");
            }
            Console.WriteLine(encryptStr);
        }

        [MethodTest]
        public void SHA1SecurityTest()
        {
            string str = "金胖子挂了吗减肥的就是了高科技的思考就过来看多少就是都i";
            string encryptStr = SecurityHelper.SHA1(str);
            if (encryptStr.Length != 40)
            {
                throw new UnitTestException("失败：SecurityHelper.MD5(str)");
            }
            Console.WriteLine(encryptStr);
        }
    }
}
