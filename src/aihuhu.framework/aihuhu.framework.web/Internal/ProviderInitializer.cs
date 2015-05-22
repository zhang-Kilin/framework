using aihuhu.framework.Configuration;
using aihuhu.framework.web.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.web.Internal
{
    static class ProviderInitializer
    {
        private static ICache m_CacheProvider;
        private static ILogin m_LoginProvider;
        private static IPromission m_PromissionProvider;

        static ProviderInitializer()
        {
            if (ConfigurationManager.Providers != null
                && ConfigurationManager.Providers.Count > 0)
            {
                Provider provider = null;
                for (int i = 0; i < ConfigurationManager.Providers.Count; i++)
                {
                    provider = ConfigurationManager.Providers[i];
                    m_CacheProvider = CreateInstance<ICache>(provider);
                    m_LoginProvider = CreateInstance<ILogin>(provider);
                    m_PromissionProvider = CreateInstance<IPromission>(provider);
                }
            }
            if (m_CacheProvider == null)
            {
                if (RuntimeCacheStore.Support())
                {
                    m_CacheProvider = new RuntimeCacheStore();
                }
                else
                {
                    m_CacheProvider = new MemoryCacheStore();
                }
            }
        }

        internal static ICache CacheProvider
        {
            get
            {
                return m_CacheProvider;
            }
        }

        internal static ILogin LoginProvider
        {
            get
            {
                return m_LoginProvider;
            }
        }

        internal static IPromission PromissionProvider
        {
            get
            {
                return m_PromissionProvider;
            }
        }

        private static TInterface CreateInstance<TInterface>(Provider provider)
            where TInterface : class
        {
            Type type = typeof(TInterface);
            Type[] interfaces = null;
            TInterface result = null;
            if (provider != null
                        && provider.Type != null)
            {
                interfaces = provider.Type.FindInterfaces((t, obj) =>
                {
                    return object.Equals(t, obj);
                }, type);
                if (interfaces != null
                    && interfaces.Length > 0)
                {
                    result = Activator.CreateInstance(provider.Type) as TInterface;
                }
            }
            return result;
        }
    }
}
