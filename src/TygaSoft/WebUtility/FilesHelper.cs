using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using TygaSoft.Model;
using TygaSoft.SysUtility;

namespace TygaSoft.WebUtility
{
    public class FilesHelper
    {
        public static readonly string FileRoot = ConfigurationManager.AppSettings["FilesRoot"];

        public static string GetRndUrl(string dir, string fileName) 
        {
            var sRnd = string.Empty;
            if (Regex.IsMatch(Path.GetFileNameWithoutExtension(fileName), MC.Re_IsCode)) sRnd = Path.GetFileNameWithoutExtension(fileName);
            else sRnd = new Common().GetRndCode();
            return string.Format("{0}/{1}/{2}", FileRoot.Trim('~'), dir, fileName);
        }

        public static string GetRndFile(string dir, string fileName) 
        {
            var sRnd = string.Empty;
            if (Regex.IsMatch(Path.GetFileNameWithoutExtension(fileName), MC.Re_IsCode)) sRnd = Path.GetFileNameWithoutExtension(fileName);
            else sRnd = new Common().GetRndCode();
            var url = string.Format("{0}/{1}/{2}", FileRoot, dir, fileName);
            return ToFullPath(url);
        }

        public static string GetRndFile(string dir, string ext, DateTime currTime)
        {
            var sRnd = new Common().GetRndCode();
            var dirPath = HttpContext.Current.Server.MapPath(string.Format("{0}/{1}/{2}", FileRoot, dir, currTime.ToString("yyyyMM")));
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            var subDirs = Directory.GetDirectories(dirPath);
            if (subDirs != null && subDirs.Length > 0)
            {
                var q = subDirs.Where(m => m == sRnd);
                if (q != null && q.Any()) sRnd += q.Count();
            }
            var rndPath = Path.Combine(dirPath, sRnd);
            if (!Directory.Exists(rndPath)) Directory.CreateDirectory(rndPath);

            return Path.Combine(rndPath, string.Format("{0}{1}", sRnd, ext.ToLower()));
        }

        public static string GetRandomFolder(string dir, DateTime currTime,bool isOldRemove)
        {
            var currDir = string.Format("{0}/{1}/{2}/{3}", FileRoot, dir, currTime.ToString("yyyyMM"), (new Random().NextDouble() * int.MaxValue).ToString().PadLeft(10, '0'));
            var fullPath = HttpContext.Current.Server.MapPath(currDir);
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

            return dir;
        }

        public static string CreateDateTimeString()
        {
            //确保产生的字符串唯一性，使用线程休眠
            Thread.Sleep(2);
            Random random = new Random(); ;
            return DateTime.Now.ToString("yyyyMMddHHmmssffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + random.Next(0, 9999).ToString().PadLeft(4, '0');
        }

        public static string GetFormatDateTime()
        {
            Thread.Sleep(2);
            return DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToFullPath(string url) 
        {
            if(string.IsNullOrEmpty(url)) return string.Empty;
            if(!url.StartsWith("~/")) url = "~/"+url.Trim('/');
            var fullPath = HttpContext.Current.Server != null ? HttpContext.Current.Server.MapPath(url) : System.Web.Hosting.HostingEnvironment.MapPath(url);
            if (!Directory.Exists(Path.GetDirectoryName(fullPath))) Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            return fullPath;
        }

        public static string ToHostUrl(string url)
        {
            if (!string.IsNullOrEmpty(url) && !Regex.IsMatch(url, @"http(s)?://(.)*")) return string.Format("{0}{1}", GlobalConfig.AppName, url);
            return url;
        }

        public static string ToVirtualUrl(string fullPath)
        {
            return fullPath.Remove(0, fullPath.LastIndexOf("Files", StringComparison.Ordinal) - 1).Replace("\\", "/");
        }
    }
}
