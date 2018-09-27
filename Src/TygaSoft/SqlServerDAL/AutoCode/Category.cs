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
    public partial class Category : ICategory
    {
        #region ICategory Member

        public int Insert(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Category (AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@DepmtId,@ParentId,@Coded,@Named,@Step,@Sort,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Step",SqlDbType.VarChar,1000), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
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
            parms[7].Value = model.Sort;
            parms[8].Value = model.Remark;
            parms[9].Value = model.RecordDate;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Category (Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@DepmtId,@ParentId,@Coded,@Named,@Step,@Sort,@Remark,@RecordDate,@LastUpdatedDate)
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
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
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
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Category set AppCode = @AppCode,UserId = @UserId,DepmtId = @DepmtId,ParentId = @ParentId,Coded = @Coded,Named = @Named,Step = @Step,Sort = @Sort,Remark = @Remark,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
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
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
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
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Category where Id = @Id ");
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
                sb.Append(@"delete from Category where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public CategoryInfo GetModel(Guid id)
        {
            CategoryInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate 
			            from Category
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
                        model = new CategoryInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
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

        public IList<CategoryInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CategoryInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate
					  from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.ParentId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Step = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
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

        public IList<CategoryInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate
					   from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.ParentId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Step = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
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

        public IList<CategoryInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate
                        from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ParentId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Step = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.RecordDate = reader.IsDBNull(10) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(10);
                        model.LastUpdatedDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CategoryInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,AppCode,UserId,DepmtId,ParentId,Coded,Named,Step,Sort,Remark,RecordDate,LastUpdatedDate 
			            from Category
					    order by Sort ");

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ParentId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Step = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.RecordDate = reader.IsDBNull(10) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(10);
                        model.LastUpdatedDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
