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
    public partial class SiteRoles : ISiteRoles
    {
        #region ISiteRoles Member

        public int Insert(SiteRolesInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteRoles (ApplicationId,Named,LowerName,LastUpdatedDate)
			            values
						(@ApplicationId,@Named,@LowerName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,50),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ApplicationId;
            parms[1].Value = model.Named;
            parms[2].Value = model.LowerName;
            parms[3].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(SiteRolesInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteRoles (Id,ApplicationId,Named,LowerName,LastUpdatedDate)
			            values
						(@Id,@ApplicationId,@Named,@LowerName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,50),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.Named;
            parms[3].Value = model.LowerName;
            parms[4].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SiteRolesInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update SiteRoles set ApplicationId = @ApplicationId,Named = @Named,LowerName = @LowerName,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,50),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.Named;
            parms[3].Value = model.LowerName;
            parms[4].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from SiteRoles where Id = @Id ");
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
                sb.Append(@"delete from SiteRoles where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public SiteRolesInfo GetModel(Guid id)
        {
            SiteRolesInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 ApplicationId,Id,Named,LowerName,LastUpdatedDate 
			            from SiteRoles
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
                        model = new SiteRolesInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.LowerName = reader.GetString(3);
                        model.LastUpdatedDate = reader.GetDateTime(4);
                    }
                }
            }

            return model;
        }

        public IList<SiteRolesInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteRoles ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteRolesInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          ApplicationId,Id,Named,LowerName,LastUpdatedDate
					  from SiteRoles ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteRolesInfo> list = new List<SiteRolesInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteRolesInfo model = new SiteRolesInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.Id = reader.GetGuid(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteRolesInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           ApplicationId,Id,Named,LowerName,LastUpdatedDate
					   from SiteRoles ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteRolesInfo> list = new List<SiteRolesInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteRolesInfo model = new SiteRolesInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.Id = reader.GetGuid(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.LastUpdatedDate = reader.GetDateTime(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteRolesInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select ApplicationId,Id,Named,LowerName,LastUpdatedDate
                        from SiteRoles ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<SiteRolesInfo> list = new List<SiteRolesInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteRolesInfo model = new SiteRolesInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.LowerName = reader.GetString(3);
                        model.LastUpdatedDate = reader.GetDateTime(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteRolesInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select ApplicationId,Id,Named,LowerName,LastUpdatedDate 
			            from SiteRoles
					    order by LastUpdatedDate desc ");

            IList<SiteRolesInfo> list = new List<SiteRolesInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteRolesInfo model = new SiteRolesInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.LowerName = reader.GetString(3);
                        model.LastUpdatedDate = reader.GetDateTime(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
