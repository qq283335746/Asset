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
    public partial class Staff : IStaff
    {
        #region IStaff Member

        public int Insert(StaffInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Staff (UserId,AppCode,Coded,Named,Phone,Sort,Remark,RecordDate,LastUpdatedDate)
			            values
						(@UserId,@AppCode,@Coded,@Named,@Phone,@Sort,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,50), 
new SqlParameter("@Phone",SqlDbType.VarChar,11), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,100), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.Phone;
            parms[5].Value = model.Sort;
            parms[6].Value = model.Remark;
            parms[7].Value = model.RecordDate;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StaffInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Staff set AppCode = @AppCode,Coded = @Coded,Named = @Named,Phone = @Phone,Sort = @Sort,Remark = @Remark,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where UserId = @UserId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,50), 
new SqlParameter("@Phone",SqlDbType.VarChar,11), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@Remark",SqlDbType.NVarChar,100), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.Phone;
            parms[5].Value = model.Sort;
            parms[6].Value = model.Remark;
            parms[7].Value = model.RecordDate;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid userId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Staff where UserId = @UserId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;

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
                sb.Append(@"delete from Staff where UserId = @UserId" + n + " ;");
                SqlParameter parm = new SqlParameter("@UserId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StaffInfo GetModel(Guid userId)
        {
            StaffInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 UserId,AppCode,Coded,Named,Phone,Sort,Remark,RecordDate,LastUpdatedDate 
			            from Staff
						where UserId = @UserId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new StaffInfo();
                        model.UserId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.Coded = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Named = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Phone = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Sort = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.Remark = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.RecordDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.IsDBNull(8) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public IList<StaffInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Staff ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StaffInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          UserId,AppCode,Coded,Named,Phone,Sort,Remark,RecordDate,LastUpdatedDate
					  from Staff ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StaffInfo> list = new List<StaffInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StaffInfo model = new StaffInfo();
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Coded = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Phone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Sort = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.Remark = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.RecordDate = reader.IsDBNull(8) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(8);
                        model.LastUpdatedDate = reader.IsDBNull(9) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StaffInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           UserId,AppCode,Coded,Named,Phone,Sort,Remark,RecordDate,LastUpdatedDate
					   from Staff ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StaffInfo> list = new List<StaffInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StaffInfo model = new StaffInfo();
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Coded = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Phone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Sort = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.Remark = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.RecordDate = reader.IsDBNull(8) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(8);
                        model.LastUpdatedDate = reader.IsDBNull(9) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StaffInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select UserId,AppCode,Coded,Named,Phone,Sort,Remark,RecordDate,LastUpdatedDate
                        from Staff ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<StaffInfo> list = new List<StaffInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StaffInfo model = new StaffInfo();
                        model.UserId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.Coded = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Named = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Phone = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Sort = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.Remark = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.RecordDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.IsDBNull(8) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StaffInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select UserId,AppCode,Coded,Named,Phone,Sort,Remark,RecordDate,LastUpdatedDate 
			            from Staff
					    order by Sort ");

            IList<StaffInfo> list = new List<StaffInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StaffInfo model = new StaffInfo();
                        model.UserId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.Coded = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Named = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Phone = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Sort = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.Remark = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.RecordDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.IsDBNull(8) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
