using System;
using System.Configuration;

namespace Yibi.LiteDAL
{
    public class ConnectionHelper
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["AspnetDbConnString"].ConnectionString;
    }
}
