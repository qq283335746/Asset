using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Yibi.LiteDAL
{
    public class ConvertToModel
    {
        public static T ToModel<T> (params SqlParameter[] cmdParms) where T:class,new()
        {
            if (cmdParms == null) return null;

            JObject o = new JObject();

            foreach (SqlParameter parm in cmdParms)
            {
                o.Add(parm.ParameterName, parm.Value.ToString());
            }

            return o.ToObject<T>();
        }
    }
}
