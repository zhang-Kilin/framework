using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public class User
    {
        public string UserName { get; set; }

        public string NickName { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string LastLoginIP { get; set; }

    }
}
