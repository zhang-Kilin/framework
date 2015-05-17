using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Security
{
    public interface ISecurity
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string Encrypt(string str);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        string Decrypt(string encryptStr);
    }
}
