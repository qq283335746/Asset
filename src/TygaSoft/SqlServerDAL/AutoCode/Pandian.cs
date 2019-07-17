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
    public partial class Pandian : IPandian
    {
        #region IPandian Member

        public int Insert(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Pandian (AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@DepmtId,@Named,@TotalQty,@IsDown,@MgrDepmtId,@Sort,@Remark,@Status,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@TotalQty",SqlDbType.Int), 
new SqlParameter("@IsDown",SqlDbType.Bit), 
new SqlParameter("@MgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.DepmtId;
            parms[3].Value = model.Named;
            parms[4].Value = model.TotalQty;
            parms[5].Value = model.IsDown;
            parms[6].Value = model.MgrDepmtId;
            parms[7].Value = model.Sort;
            parms[8].Value = model.Remark;
            parms[9].Value = model.Status;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Pandian (Id,AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@DepmtId,@Named,@TotalQty,@IsDown,@MgrDepmtId,@Sort,@Remark,@Status,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@TotalQty",SqlDbType.Int), 
new SqlParameter("@IsDown",SqlDbType.Bit), 
new SqlParameter("@MgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.Named;
            parms[5].Value = model.TotalQty;
            parms[6].Value = model.IsDown;
            parms[7].Value = model.MgrDepmtId;
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.Status;
            parms[11].Value = model.RecordDate;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Pandian set AppCode = @AppCode,UserId = @UserId,DepmtId = @DepmtId,Named = @Named,TotalQty = @TotalQty,IsDown = @IsDown,MgrDepmtId = @MgrDepmtId,Sort = @Sort,Remark = @Remark,Status = @Status,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@TotalQty",SqlDbType.Int), 
new SqlParameter("@IsDown",SqlDbType.Bit), 
new SqlParameter("@MgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.Named;
            parms[5].Value = model.TotalQty;
            parms[6].Value = model.IsDown;
            parms[7].Value = model.MgrDepmtId;
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.Status;
            parms[11].Value = model.RecordDate;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Pandian where Id = @Id ");
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
                sb.Append(@"delete from Pandian where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public PandianInfo GetModel(Guid id)
        {
            PandianInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate 
			            from Pandian
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
                        model = new PandianInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.TotalQty = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.IsDown = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
                        model.MgrDepmtId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Status = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                    }
                }
            }

            return model;
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			          Id,AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate
					  from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.TotalQty = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.IsDown = reader.IsDBNull(7) ? false : reader.GetBoolean(7);
                        model.MgrDepmtId = reader.IsDBNull(8) ? Guid.Empty : reader.GetGuid(8);
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

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			           Id,AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate
					   from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.TotalQty = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.IsDown = reader.IsDBNull(7) ? false : reader.GetBoolean(7);
                        model.MgrDepmtId = reader.IsDBNull(8) ? Guid.Empty : reader.GetGuid(8);
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

        public IList<PandianInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate
                        from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Status,Sort ");

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.TotalQty = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.IsDown = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
                        model.MgrDepmtId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Status = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,AppCode,UserId,DepmtId,Named,TotalQty,IsDown,MgrDepmtId,Sort,Remark,Status,RecordDate,LastUpdatedDate 
			            from Pandian
					    order by Status,Sort ");

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.TotalQty = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.IsDown = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
                        model.MgrDepmtId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Status = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
