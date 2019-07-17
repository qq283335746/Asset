using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class ContentType
    {
        #region IContentType Member

        public bool IsExistCode(string code, Guid Id)
        {
            var cmdText = "";
            SqlParameter[] parms = new SqlParameter[1];
            if (Id.Equals(Guid.Empty))
            {
                cmdText = @"select 1 from [ContentType] where Coded = @Coded ";
                parms[0] = new SqlParameter("@Coded", SqlDbType.VarChar, 36);
                parms[0].Value = code;
            }
            else
            {
                cmdText = @"select 1 from [ContentType] where Coded = @Coded and Id <> @Id ";
                Array.Resize(ref parms, 2);
                parms[0] = new SqlParameter("@Coded", SqlDbType.VarChar, 36);
                parms[0].Value = code;
                parms[1] = new SqlParameter("@Id", Id);
            }

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        public bool IsExistChild(Guid Id)
        {
            var cmdText = @"select 1 from ContentType
                            where exists(select 1 from ContentType where ParentId = @ContentTypeId)
                            or exists(select 1 from ContentDetail where ContentTypeId = @ContentTypeId) ";
            var parm = new SqlParameter("@ContentTypeId", SqlDbType.UniqueIdentifier);
            parm.Value = Id;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
