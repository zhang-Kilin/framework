using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Security
{
    internal class MD5EncryptProvider : ISecurity
    {
        public string Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("str");
            }
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
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
