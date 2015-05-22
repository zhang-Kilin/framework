using aihuhu.framework.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aihuhu.framework.data.Configuration.Exports
{
    internal sealed class DatabaseFileWatcher
    {
        private static readonly List<FileSystemWatcher> m_WatchList = new List<FileSystemWatcher>(50);

        internal static void Watch(string filePath)
        {
            if (string.IsNullOrWhiteSpace(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath)
                || !FileHelper.Contains(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, filePath))
            {
                string directory = Path.GetDirectoryName(filePath);
                string fileName = Path.GetFileName(filePath);
                FileSystemWatcher watcher = new FileSystemWatcher(directory, fileName);
                watcher.EnableRaisingEvents = true;
                watcher.Changed += watcher_Changed;
                m_WatchList.Add(watcher);
            }
        }

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            m_WatchList.Clear();
            CommandConfigurationManager.Refresh();
        }
    }
}
