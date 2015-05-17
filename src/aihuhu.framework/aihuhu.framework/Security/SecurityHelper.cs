using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Security
{
    public static class SecurityHelper
    {
        public static string DESEncrypt(string str)
        {
            ISecurity provider = SecurityFactory.GetSecurityProvider(SecurityType.DES);
            return provider.Encrypt(str);
        }

        public static string DESDecrypt(string encryptStr)
        {
            ISecurity provider = SecurityFactory.GetSecurityProvider(SecurityType.DES);
            return provider.Decrypt(encryptStr);
        }

        public static string AESEncrypt(string str)
        {
            ISecurity provider = SecurityFactory.GetSecurityProvider(SecurityType.AES);
            return provider.Encrypt(str);
        }

        public static string AESDecrypt(string encryptStr)
        {
            ISecurity provider = SecurityFactory.GetSecurityProvider(SecurityType.AES);
            return provider.Decrypt(encryptStr);
        }

        public static string MD5(string str)
        {
            ISecurity provider = SecurityFactory.GetSecurityProvider(SecurityType.MD5);
            return provider.Encrypt(str);
        }

        public static string SHA1(string str)
        {
            ISecurity provider = SecurityFactory.GetSecurityProvider(SecurityType.SHA1);
            return provider.Encrypt(str);
        }
    }
}
