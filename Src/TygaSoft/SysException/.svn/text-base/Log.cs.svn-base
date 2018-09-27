using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Util;

[assembly: TygaSoft.SysException.Log4netXmlConfiguratorAttribute(Watch = true)]
namespace TygaSoft.SysException
{
    public sealed class Log
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Log));
        //public static bool IsFatalEnabled => logger.IsFatalEnabled;
        //public static bool IsWarnEnabled => logger.IsWarnEnabled;
        //public static bool IsDebugEnabled => logger.IsDebugEnabled;
        //public static bool IsInfoEnabled => logger.IsInfoEnabled;
        //public static bool IsErrorEnabled => logger.IsErrorEnabled;

        public static void Info(string message)
        {
            Task.Factory.StartNew(() =>
            {
                logger.Info(message);
            }).Wait();
        }

        public static void InfoFormat(string format, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.InfoFormat(format, args);
            }).Wait();
        }

        public static void Info(string message, Exception exception)
        {
            Task.Factory.StartNew(() =>
            {
                logger.Info(message, exception);
            }).Wait();
        }

        public static void Error(string message)
        {
            Task.Factory.StartNew(() =>
            {
                logger.Error(message);
            }).Wait();
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.ErrorFormat(format, args);
            }).Wait();
        }

        public static void Error(string message, Exception exception)
        {
            Task.Factory.StartNew(() =>
            {
                logger.Error(message, exception);
            }).Wait();
        }
    }

    internal class Log4netXmlConfiguratorAttribute : XmlConfiguratorAttribute
    {
        public override void Configure(Assembly assembly, ILoggerRepository targetRepository)
        {
            base.ConfigFile = Path.Combine(Path.GetDirectoryName(assembly.CodeBase.Replace(@"file:///", "")), "Log4net.config");
            var arrayList = new List<object>();
            var logReceivedAdapter = new LogLog.LogReceivedAdapter(arrayList);
            try
            {
                //反射调用基类私有方法，完成配置，跳过本方法基类报错的地方
                var methods = typeof(XmlConfiguratorAttribute).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
                var method = methods.First(t => t.Name == "ConfigureFromFile" && t.GetParameters().Any(u => u.ParameterType == typeof(ILoggerRepository)));
                method.Invoke(this, new object[] { assembly, targetRepository });
            }
            finally
            {
                logReceivedAdapter.Dispose();
            }
            targetRepository.ConfigurationMessages = arrayList;
        }
    }
}
