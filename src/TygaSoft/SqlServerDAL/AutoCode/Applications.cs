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
    public partial class Applications : IApplications
    {
        #region IApplications Member

        public int Insert(ApplicationsInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Applications (Coded,Named,LowerName,Remark)
			            values
						(@Coded,@Named,@LowerName,@Remark)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Coded",SqlDbType.Char,6),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,256)
                                   };
            parms[0].Value = model.Coded;
            parms[1].Value = model.Named;
            parms[2].Value = model.LowerName;
            parms[3].Value = model.Remark;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(ApplicationsInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Applications (Id,Coded,Named,LowerName,Remark)
			            values
						(@Id,@Coded,@Named,@LowerName,@Remark)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.Char,6),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,256)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.LowerName;
            parms[4].Value = model.Remark;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ApplicationsInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Applications set Coded = @Coded,Named = @Named,LowerName = @LowerName,Remark = @Remark 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.Char,6),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,256)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.LowerName;
            parms[4].Value = model.Remark;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Applications where Id = @Id ");
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
                sb.Append(@"delete from Applications where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ApplicationsInfo GetModel(Guid id)
        {
            ApplicationsInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,Coded,Named,LowerName,Remark 
			            from Applications
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
                        model = new ApplicationsInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.LowerName = reader.GetString(3);
                        model.Remark = reader.GetString(4);
                    }
                }
            }

            return model;
        }

        public IList<ApplicationsInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Applications ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ApplicationsInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by ) as RowNumber,
			          Id,Coded,Named,LowerName,Remark
					  from Applications ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ApplicationsInfo> list = new List<ApplicationsInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ApplicationsInfo model = new ApplicationsInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.Remark = reader.GetString(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ApplicationsInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by ) as RowNumber,
			           Id,Coded,Named,LowerName,Remark
					   from Applications ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ApplicationsInfo> list = new List<ApplicationsInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ApplicationsInfo model = new ApplicationsInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.Remark = reader.GetString(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ApplicationsInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,Coded,Named,LowerName,Remark
                        from Applications ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by  ");

            IList<ApplicationsInfo> list = new List<ApplicationsInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ApplicationsInfo model = new ApplicationsInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.LowerName = reader.GetString(3);
                        model.Remark = reader.GetString(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ApplicationsInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,Coded,Named,LowerName,Remark 
			            from Applications
					    order by  ");

            IList<ApplicationsInfo> list = new List<ApplicationsInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ApplicationsInfo model = new ApplicationsInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.LowerName = reader.GetString(3);
                        model.Remark = reader.GetString(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
