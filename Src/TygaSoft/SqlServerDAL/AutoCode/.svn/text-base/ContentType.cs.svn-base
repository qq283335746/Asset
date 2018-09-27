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
    public partial class ContentType : IContentType
    {
        #region IContentType Member

        public int Insert(ContentTypeInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ContentType (AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@DepmtId,@ParentId,@Coded,@Named,@Step,@FlagName,@Sort,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Step",SqlDbType.VarChar,1000), 
new SqlParameter("@FlagName",SqlDbType.VarChar,20), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,100), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.DepmtId;
            parms[3].Value = model.ParentId;
            parms[4].Value = model.Coded;
            parms[5].Value = model.Named;
            parms[6].Value = model.Step;
            parms[7].Value = model.FlagName;
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(ContentTypeInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ContentType (Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@DepmtId,@ParentId,@Coded,@Named,@Step,@FlagName,@Sort,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Step",SqlDbType.VarChar,1000), 
new SqlParameter("@FlagName",SqlDbType.VarChar,20), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,100), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.ParentId;
            parms[5].Value = model.Coded;
            parms[6].Value = model.Named;
            parms[7].Value = model.Step;
            parms[8].Value = model.FlagName;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.RecordDate;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ContentTypeInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update ContentType set AppCode = @AppCode,UserId = @UserId,DepmtId = @DepmtId,ParentId = @ParentId,Coded = @Coded,Named = @Named,Step = @Step,FlagName = @FlagName,Sort = @Sort,Remark = @Remark,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Step",SqlDbType.VarChar,1000), 
new SqlParameter("@FlagName",SqlDbType.VarChar,20), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,100), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.ParentId;
            parms[5].Value = model.Coded;
            parms[6].Value = model.Named;
            parms[7].Value = model.Step;
            parms[8].Value = model.FlagName;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.RecordDate;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from ContentType where Id = @Id ");
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
                sb.Append(@"delete from ContentType where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ContentTypeInfo GetModel(Guid id)
        {
            ContentTypeInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 AppCode,UserId,DepmtId,Id,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate 
			            from ContentType
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
                        model = new ContentTypeInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ParentId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Step = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.FlagName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                    }
                }
            }

            return model;
        }

        public IList<ContentTypeInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from ContentType ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ContentTypeInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          AppCode,UserId,DepmtId,Id,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate
					  from ContentType ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.ParentId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Step = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.FlagName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Sort = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Remark = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.RecordDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.LastUpdatedDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentTypeInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           AppCode,UserId,DepmtId,Id,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate
					   from ContentType ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.ParentId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Step = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.FlagName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Sort = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Remark = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.RecordDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.LastUpdatedDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentTypeInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select AppCode,UserId,DepmtId,Id,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate
                        from ContentType ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ParentId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Step = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.FlagName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ContentTypeInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select AppCode,UserId,DepmtId,Id,ParentId,Coded,Named,Step,FlagName,Sort,Remark,RecordDate,LastUpdatedDate 
			            from ContentType
					    order by Sort ");

            IList<ContentTypeInfo> list = new List<ContentTypeInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ContentTypeInfo model = new ContentTypeInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ParentId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Step = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.FlagName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
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
