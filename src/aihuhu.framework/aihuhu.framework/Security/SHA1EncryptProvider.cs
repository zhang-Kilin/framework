using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Security
{
    internal class SHA1EncryptProvider : ISecurity
    {
        public string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("str");
            }
            SHA1 sha1 = SHA1.Create();
            byte[] buffer = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string Decrypt(string encryptStr)
        {
            throw new NotImplementedException();
        }
    }
}
