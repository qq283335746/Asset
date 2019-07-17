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
    public partial class StoragePlace : IStoragePlace
    {
        #region IStoragePlace Member

        public int Insert(StoragePlaceInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StoragePlace (AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@DepmtId,@Coded,@Named,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.DepmtId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.Named;
            parms[5].Value = model.RecordDate;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(StoragePlaceInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StoragePlace (Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@DepmtId,@Coded,@Named,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.Coded;
            parms[5].Value = model.Named;
            parms[6].Value = model.RecordDate;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StoragePlaceInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update StoragePlace set AppCode = @AppCode,UserId = @UserId,DepmtId = @DepmtId,Coded = @Coded,Named = @Named,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.Coded;
            parms[5].Value = model.Named;
            parms[6].Value = model.RecordDate;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from StoragePlace where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from StoragePlace where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StoragePlaceInfo GetModel(Guid id)
        {
            StoragePlaceInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate 
			            from StoragePlace
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
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

        public IList<StoragePlaceInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StoragePlaceInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate
					  from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StoragePlaceInfo> list = new List<StoragePlaceInfo>();

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

        public IList<StoragePlaceInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate
					   from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StoragePlaceInfo> list = new List<StoragePlaceInfo>();

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

        public IList<StoragePlaceInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate
                        from StoragePlace ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<StoragePlaceInfo> list = new List<StoragePlaceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StoragePlaceInfo model = new StoragePlaceInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Coded = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.RecordDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);
                        model.LastUpdatedDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StoragePlaceInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,AppCode,UserId,DepmtId,Coded,Named,RecordDate,LastUpdatedDate 
			            from StoragePlace
					    order by LastUpdatedDate desc ");

            IList<StoragePlaceInfo> list = new List<StoragePlaceInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StoragePlaceInfo model = new StoragePlaceInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Coded = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.RecordDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);
                        model.LastUpdatedDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
