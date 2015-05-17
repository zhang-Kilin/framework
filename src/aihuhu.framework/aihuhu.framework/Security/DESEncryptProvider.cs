using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Security
{
    internal class DESEncryptProvider : ISecurity
    {
        private byte[] m_EncryptKeyBuffer;
        private byte[] m_EncryptHashCodeBuffer;

        internal DESEncryptProvider()
        {
            string key = framework.Properties.framework.ResourceManager.GetString("DESEncryptKey", CultureInfo.CurrentCulture);
            string hashCode = framework.Properties.framework.ResourceManager.GetString("DESEncryptHashCode", CultureInfo.CurrentCulture);
            this.m_EncryptKeyBuffer = Encoding.UTF8.GetBytes(key);
            this.m_EncryptHashCodeBuffer = Encoding.UTF8.GetBytes(hashCode);
        }

        public string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("str");
            }
            string result = null; 
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream,provider.CreateEncryptor(this.m_EncryptKeyBuffer,this.m_EncryptHashCodeBuffer), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(buffer, 0, buffer.Length);
                    cryptoStream.FlushFinalBlock();
                }
                result = Convert.ToBase64String(stream.ToArray());
            }
            return result;
        }

        public string Decrypt(string encryptStr)
        {
            if (string.IsNullOrEmpty(encryptStr))
            {
                throw new ArgumentNullException("str");
            }
            string result = null;
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = Convert.FromBase64String(encryptStr);
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(stream, provider.CreateDecryptor(this.m_EncryptKeyBuffer, this.m_EncryptHashCodeBuffer), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(buffer, 0, buffer.Length);
                    cryptoStream.FlushFinalBlock();
                }
                result = Encoding.UTF8.GetString(stream.ToArray());
            }
            return result;
        }
    }
}
