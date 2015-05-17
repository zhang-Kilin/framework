using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aihuhu.framework.Utility
{
    public static class FileHelper
    {
        /// <summary>
        /// 获取文件的绝对路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string RootPath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            string path = filePath;
            if (Path.IsPathRooted(path))
            {
                return path;
            }
            //path = path.Replace('/','\\');
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        /// <summary>
        /// 根据基地址获取文件路径
        /// </summary>
        /// <param name="basePath">基地址</param>
        /// <param name="relativeFilePath">相对路径</param>
        /// <returns></returns>
        public static string ResolvePath(string basePath, string relativeFilePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentNullException("basePath");
            }
            if (string.IsNullOrWhiteSpace(relativeFilePath))
            {
                throw new ArgumentNullException("relativeFilePath");
            }
            //basePath = basePath.Replace('/','\\');
            if (!basePath.EndsWith("\\") && !basePath.EndsWith("/"))
            {
                basePath = Regex.Replace(basePath, @"[/\\][^/\\]+?$", string.Empty);
            }
            return Path.Combine(basePath, relativeFilePath);
        }
    }
}
