using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class BarcodeTemplate
    {
        #region IBarcodeTemplate Member

        public BarcodeTemplateInfo GetModelByDefault()
        {
            BarcodeTemplateInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,Title,Html,Attr,IsDefault,TypeName,LastUpdatedDate 
			            from BarcodeTemplate
						where IsDefault = @IsDefault ");
            SqlParameter[] parms = {
                                     new SqlParameter("@IsDefault",SqlDbType.Bit)
                                   };
            parms[0].Value = true;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new BarcodeTemplateInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.Title = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Html = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Attr = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.IsDefault = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
                        model.TypeName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.LastUpdatedDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public int SetDefault(Guid Id, bool isDefault,string typeName)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update BarcodeTemplate set IsDefault = @IsDefault,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");
            if(isDefault)
            {
                sb.Append(@";update BarcodeTemplate set IsDefault = @IsDefault2,LastUpdatedDate = @LastUpdatedDate 
			                 where Id <> @Id and TypeName = @TypeName
					      ");
            }
            

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@IsDefault",SqlDbType.Bit),
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                   };
            parms[0].Value = Id;
            parms[1].Value = isDefault;
            parms[2].Value = DateTime.Now;
            if (isDefault)
            {
                Array.Resize(ref parms, 5);
                parms[3] = new SqlParameter("@IsDefault2", SqlDbType.Bit);
                parms[3].Value = !isDefault;
                parms[4] = new SqlParameter("@TypeName", SqlDbType.NVarChar, 20);
                parms[4].Value = typeName;
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
