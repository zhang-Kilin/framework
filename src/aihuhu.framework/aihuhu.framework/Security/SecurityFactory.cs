using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Security
{
    internal enum SecurityType
    {
        DES,
        AES,
        MD5,
        SHA1
    }

    internal class SecurityFactory
    {
        private static readonly ISecurity m_DESSecurityInstance = new DESEncryptProvider();
        private static readonly ISecurity m_AESSecurityInstance = new AESEncryptProvider();
        private static readonly ISecurity m_MD5SecurityInstance = new MD5EncryptProvider();
        private static readonly ISecurity m_SHA1SecurityInstance = new SHA1EncryptProvider();

        public static ISecurity GetSecurityProvider(SecurityType securityType)
        {
            ISecurity provider = null;
            switch (securityType)
            {
                case SecurityType.AES:
                    provider = m_AESSecurityInstance;
                    break;
                case SecurityType.MD5:
                    provider = m_MD5SecurityInstance;
                    break;
                case SecurityType.SHA1:
                    provider = m_SHA1SecurityInstance;
                    break;
                case SecurityType.DES:
                default:
                    provider = m_DESSecurityInstance;
                    break;
            }
            return provider;
        }
    }
}
