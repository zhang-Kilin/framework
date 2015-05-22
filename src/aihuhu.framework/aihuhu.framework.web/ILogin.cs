using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web
{
    public interface ILogin
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>登录是否成功</returns>
        bool Login(string userName, string password,out User loginUser);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        void Loginout(string userName);
    }
}
