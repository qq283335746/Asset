using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskCA
{
    class Program
    {
        static void Main(string[] args)
        {
            var step = "82818fe6-57b7-4a39-9961-2102d916d251,8d6f62f7-72c2-4b50-bd60-7cb61a9021f5";
            var orgId = "8d6f62f7-72c2-4b50-bd60-7cb61a9021f5";
            var list = new List<string>();
            var arr = step.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            var index = arr.FindLastIndex(m=>m.ToLower()==orgId.ToLower());
            for(var i=0;i<=index;i++)
            {
                list.Add(arr[i]);
            }
            var s = string.Join(",", list);

            Console.Read();
        }
    }
}
