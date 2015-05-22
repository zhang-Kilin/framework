using aihuhu.framework.Logging;
using aihuhu.framework.web.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web
{
    /// <summary>
    /// 当前环境运行上下文
    /// </summary>
    public class WebContext : ILogin, IPromission, ICache
    {
        [ThreadStatic]
        private static WebContext m_Context = new WebContext();

        private WebContext()
        {
            m_IsLogin = false;
            m_User = null;
        }
        /// <summary>
        /// 当前用户是否处于登录状态
        /// </summary>
        private bool m_IsLogin;
        /// <summary>
        /// 当前用户实例
        /// </summary>
        private User m_User;

        public bool IsLogin
        {
            get
            {
                return this.m_IsLogin;
            }
        }

        public object this[string key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.Add(key, value);
            }
        }

        public static WebContext Current
        {
            get
            {
                return m_Context;
            }
        }

        public bool Login(string userName, string password, out User loginUser)
        {
            if (ProviderInitializer.LoginProvider == null)
            {
                throw new ConfigurationErrorsException("can not find the instance class of interface ILogin. pls check your configFile.");
            }
            this.m_IsLogin = ProviderInitializer.LoginProvider.Login(userName, password, out loginUser);
            this.m_User = loginUser;
            return this.m_IsLogin;
        }

        public void Loginout(string userName)
        {
            if (ProviderInitializer.LoginProvider == null)
            {
                throw new ConfigurationErrorsException("can not find the instance class of interface ILogin. pls check your configFile.");
            }
            this.m_IsLogin = false;
            this.m_User = null;
            ProviderInitializer.LoginProvider.Loginout(userName);
        }

        public bool CheckPromission(params string[] key)
        {
            if (ProviderInitializer.PromissionProvider == null)
            {
                throw new ConfigurationErrorsException("can not find the instance class of interface IPromission. pls check your configFile.");
            }
            return ProviderInitializer.PromissionProvider.CheckPromission(key);
        }

        public void Add(string key, object value)
        {
            ProviderInitializer.CacheProvider.Add(key, value);
        }

        public void Add(string key, object value, DateTime expires)
        {
            ProviderInitializer.CacheProvider.Add(key, value, expires);
        }

        public object Get(string key)
        {
            return ProviderInitializer.CacheProvider.Get(key);
        }

        public object Remove(string key)
        {
            return ProviderInitializer.CacheProvider.Remove(key);
        }
    }
}
