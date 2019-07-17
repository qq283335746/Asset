using System;
using System.Configuration;
using System.Reflection;
using TygaSoft.IMessaging;

namespace TygaSoft.MessagingFactory
{
    public sealed class QueueAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["MsmqMessaging"];

        private QueueAccess() { }

        public static IRunQueue CreateRunQueue()
        {
            string className = path + ".RunQueue";
            return (IRunQueue)Assembly.Load(path).CreateInstance(className);
        }
    }
}
