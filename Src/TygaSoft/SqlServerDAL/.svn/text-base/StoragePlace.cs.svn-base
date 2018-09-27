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
    public partial class StoragePlace
    {
        #region IStoragePlace Member

        public IList<StoragePlaceInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StoragePlaceInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Coded) as RowNumber,
			          Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate
					  from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<StoragePlaceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StoragePlaceInfo model = new StoragePlaceInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.RecordDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.IsDBNull(8) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(100);
            sb.Append(@"select Coded '存放地点编码',Named '存放地点'
                        from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Coded ");

            return SqlHelper.ExecuteDataset(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public bool IsExistCode(string code, Guid Id)
        {
            string cmdText;
            SqlParameter[] parms = new SqlParameter[1];
            if (Id.Equals(Guid.Empty))
            {
                cmdText = @"select 1 from [StoragePlace] where Coded = @Coded ";
                parms[0] = new SqlParameter("@Coded", SqlDbType.VarChar, 36);
                parms[0].Value = code;
            }
            else
            {
                cmdText = @"select 1 from [StoragePlace] where Coded = @Coded and Id <> @Id ";
                Array.Resize(ref parms, 2);
                parms[0] = new SqlParameter("@Coded", SqlDbType.VarChar, 36);
                parms[0].Value = code;
                parms[1] = new SqlParameter("@Id", Id);
            }

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        public StoragePlaceInfo GetModel(string code, string name)
        {
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(name)) return null;

            StoragePlaceInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate 
			            from StoragePlace
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
                        model = new StoragePlaceInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Coded = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.RecordDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);
                        model.LastUpdatedDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        #endregion
    }
}
