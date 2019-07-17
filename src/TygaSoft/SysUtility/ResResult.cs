using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using TygaSoft.SysUtility;
using TygaSoft.WcfModel;

namespace TygaSoft.SysUtility
{
    public class ResResult
    {
        public static ResResultModel Response(bool isOk, string msg, params object[] data)
        {
            return new ResResultModel { ResCode = isOk ? (int)ResCode.成功 : (int)ResCode.失败, Msg = msg, Data = data == null ? "" : data[0] };
        }

        public static ResResultModel Response(int resCode, string msg, params object[] data)
        {
            return new ResResultModel { ResCode = resCode, Msg = msg, Data = data == null ? "" : data[0] };
        }

        public static string ResJsonString(bool isOk, string msg, params object[] data)
        {
            return JsonConvert.SerializeObject(new ResResultModel { ResCode = isOk ? (int)ResCode.成功 : (int)ResCode.失败, Msg = msg, Data = data == null ? "" : data[0] });
        }

        public static string ResJsonString(ResResultModel model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
