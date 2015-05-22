using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.Configuration
{
    public class ConfigurationManager
    {
        private static FrameworkSectionGroupConfiguration m_SectionGroup;
        private static ProviderCollection m_ProviderCollection;
        private static string m_ConfigurationFile;
        private static System.Configuration.Configuration m_SystemConfig;

        static ConfigurationManager()
        {
            string configPath = m_ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (!File.Exists(configPath))
            {
                return;
            }
            System.Configuration.Configuration config = m_SystemConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new System.Configuration.ExeConfigurationFileMap
            {
                ExeConfigFilename = configPath
            }, System.Configuration.ConfigurationUserLevel.None);

            m_SectionGroup = config.GetSectionGroup("aihuhu.framework") as FrameworkSectionGroupConfiguration;

            ProviderConfigurationSection providerSection = null;
            if (m_SectionGroup != null)
            {
                for (int i = 0; i < m_SectionGroup.Sections.Count; i++)
                {
                    providerSection = m_SectionGroup.Sections[i] as ProviderConfigurationSection;
                    if (providerSection != null)
                    {
                        break;
                    }
                }
            }

            if (providerSection == null)
            {
                providerSection = System.Configuration.ConfigurationManager.GetSection("aihuhu.framework.provider") as ProviderConfigurationSection;
            }

            InitProviders(providerSection);
        }

        private static void InitProviders(ProviderConfigurationSection providerSection)
        {
            if (providerSection == null)
            {
                return;
            }
            if (providerSection.Providers != null
                && providerSection.Providers.Count > 0)
            {
                m_ProviderCollection = new ProviderCollection();
                for (int i = 0; i < providerSection.Providers.Count; i++)
                {
                    m_ProviderCollection.Add(new Provider(providerSection.Providers[i]));
                }
            }
        }

        public static string ConfigurationFile
        {
            get
            {
                return m_ConfigurationFile;
            }
        }

        public static System.Configuration.ConfigurationSection GetSection(string sectionName)
        {
            System.Configuration.ConfigurationSection section = null;
            if (m_SectionGroup != null)
            {
                section = m_SectionGroup.Sections[sectionName];
            }

            if (section == null)
            {
                section = System.Configuration.ConfigurationManager.GetSection(sectionName) as System.Configuration.ConfigurationSection;
            }
            return section;
        }

        public static System.Configuration.ConfigurationSectionGroup GetSectionGroup(string groupName)
        {
            System.Configuration.ConfigurationSectionGroup group = null;
            if (m_SectionGroup != null)
            {
                group = m_SectionGroup.SectionGroups[groupName];
            }
            if (group == null
                && m_SystemConfig != null)
            {
                group = m_SystemConfig.GetSectionGroup(groupName);
            }
            return group;
        }

        public static NameValueCollection AppSettings
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings;
            }
        }

        public static System.Configuration.ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings;
            }
        }

        public static FrameworkSectionGroupConfiguration SectionGroup
        {
            get
            {
                return m_SectionGroup;
            }
        }

        public static ProviderCollection Providers
        {
            get
            {
                return m_ProviderCollection;
            }
        }
    }
}
