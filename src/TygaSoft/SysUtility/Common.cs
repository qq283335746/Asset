using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TygaSoft.SysUtility
{
    public class Common
    {
        public static string GetQueryString(string queryString, string key)
        {
            if (queryString.LastIndexOf("?") > -1) queryString = queryString.Remove(0, queryString.LastIndexOf("?") + 1);
            var items = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            var item = items.FirstOrDefault(m => m.Contains(key));
            if (item != null)
            {
                var kvs = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (kvs.Length > 1) return kvs[1].Trim();
            }
            return string.Empty;
        }

        public string GetRndCode()
        {
            return (new Random().NextDouble() * int.MaxValue).ToString().PadLeft(10, '0');
        }
    }
}
