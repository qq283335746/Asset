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
    public partial class PandianAsset : IPandianAsset
    {
        #region IPandianAsset Member

        public int Insert(PandianAssetInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into PandianAsset (PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate)
			            values
						(@PandianId,@AssetId,@AppCode,@UserId,@DepmtId,@LastUseDepmtId,@LastMgrDepmtId,@LastStoragePlaceId,@LastUsePerson,@Sort,@Remark,@Status,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastUseDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastMgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastStoragePlaceId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastUsePerson",SqlDbType.NVarChar,20), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.PandianId;
            parms[1].Value = model.AssetId;
            parms[2].Value = model.AppCode;
            parms[3].Value = model.UserId;
            parms[4].Value = model.DepmtId;
            parms[5].Value = model.LastUseDepmtId;
            parms[6].Value = model.LastMgrDepmtId;
            parms[7].Value = model.LastStoragePlaceId;
            parms[8].Value = model.LastUsePerson;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.Status;
            parms[12].Value = model.RecordDate;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PandianAssetInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update PandianAsset set AppCode = @AppCode,UserId = @UserId,DepmtId = @DepmtId,LastUseDepmtId = @LastUseDepmtId,LastMgrDepmtId = @LastMgrDepmtId,LastStoragePlaceId = @LastStoragePlaceId,LastUsePerson = @LastUsePerson,Sort = @Sort,Remark = @Remark,Status = @Status,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where PandianId = @PandianId and AssetId = @AssetId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastUseDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastMgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastStoragePlaceId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@LastUsePerson",SqlDbType.NVarChar,20), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.PandianId;
            parms[1].Value = model.AssetId;
            parms[2].Value = model.AppCode;
            parms[3].Value = model.UserId;
            parms[4].Value = model.DepmtId;
            parms[5].Value = model.LastUseDepmtId;
            parms[6].Value = model.LastMgrDepmtId;
            parms[7].Value = model.LastStoragePlaceId;
            parms[8].Value = model.LastUsePerson;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.Status;
            parms[12].Value = model.RecordDate;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid pandianId, Guid assetId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from PandianAsset where PandianId = @PandianId and AssetId = @AssetId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = pandianId;
            parms[1].Value = assetId;

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
                sb.Append(@"delete from PandianAsset where PandianId = @PandianId" + n + " ;");
                SqlParameter parm = new SqlParameter("@PandianId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public PandianAssetInfo GetModel(Guid pandianId, Guid assetId)
        {
            PandianAssetInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate 
			            from PandianAsset
						where PandianId = @PandianId and AssetId = @AssetId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = pandianId;
            parms[1].Value = assetId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AssetId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.LastUseDepmtId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.LastMgrDepmtId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        model.LastStoragePlaceId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastUsePerson = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.Status = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        model.RecordDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.LastUpdatedDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                    }
                }
            }

            return model;
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianAssetInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			          PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate
					  from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AssetId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.AppCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.UserId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.DepmtId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.LastUseDepmtId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        model.LastMgrDepmtId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastStoragePlaceId = reader.IsDBNull(8) ? Guid.Empty : reader.GetGuid(8);
                        model.LastUsePerson = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Sort = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Remark = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.Status = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                        model.RecordDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.LastUpdatedDate = reader.IsDBNull(14) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			           PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate
					   from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AssetId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.AppCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.UserId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.DepmtId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.LastUseDepmtId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        model.LastMgrDepmtId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastStoragePlaceId = reader.IsDBNull(8) ? Guid.Empty : reader.GetGuid(8);
                        model.LastUsePerson = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Sort = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Remark = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.Status = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                        model.RecordDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.LastUpdatedDate = reader.IsDBNull(14) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate
                        from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Status,Sort ");

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AssetId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.LastUseDepmtId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.LastMgrDepmtId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        model.LastStoragePlaceId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastUsePerson = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.Status = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        model.RecordDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.LastUpdatedDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate 
			            from PandianAsset
					    order by Status,Sort ");

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AssetId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.LastUseDepmtId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.LastMgrDepmtId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        model.LastStoragePlaceId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastUsePerson = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.Status = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        model.RecordDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.LastUpdatedDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
