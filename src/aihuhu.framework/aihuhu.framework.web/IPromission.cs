using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web
{
    public interface IPromission
    {
        /// <summary>
        /// 检查当前用户是否拥有指定权限
        /// </summary>
        /// <param name="key">权限名</param>
        /// <returns></returns>
        bool CheckPromission(params string[] key);

    }
}
