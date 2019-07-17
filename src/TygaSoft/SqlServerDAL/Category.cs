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
    public partial class Category
    {
        #region ICategory Member

        public CategoryInfo GetModel(string code,string name)
        {
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(name)) return null;

            CategoryInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 AppCode,UserId,DepmtId,Id,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate 
			            from Category
						where 1=1 ");
            if (!string.IsNullOrEmpty(code)) sb.Append("and Coded = @Coded ");
            else sb.Append("and Named = @Coded ");
            var parm = new SqlParameter("@Coded", !string.IsNullOrEmpty(code) ? code : name);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new CategoryInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ParentId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Step = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.RecordDate = reader.IsDBNull(10) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(10);
                        model.LastUpdatedDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        public bool IsExistCode(string code, Guid Id)
        {
            var cmdText = "";
            SqlParameter[] parms = new SqlParameter[1];
            if (Id.Equals(Guid.Empty))
            {
                cmdText = @"select 1 from [Category] where Coded = @Coded ";
                parms[0] = new SqlParameter("@Coded", SqlDbType.VarChar, 36);
                parms[0].Value = code;
            }
            else
            {
                cmdText = @"select 1 from [Category] where Coded = @Coded and Id <> @Id ";
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
            var cmdText = @"select 1 from Category
                            where exists(select 1 from Category where ParentId = @Id)
                            or exists(select 1 from Product where CategoryId = @Id) ";
            var parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Id;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
