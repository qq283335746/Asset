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
    public partial class SiteUsers : ISiteUsers
    {
        #region ISiteUsers Member

        public int Insert(SiteUsersInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteUsers (ApplicationId,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate)
			            values
						(@ApplicationId,@Coded,@Named,@LowerName,@MobileAlias,@IsAnonymous,@LastActivityDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,20),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,256),
new SqlParameter("@MobileAlias",SqlDbType.NVarChar,16),
new SqlParameter("@IsAnonymous",SqlDbType.Bit),
new SqlParameter("@LastActivityDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ApplicationId;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.LowerName;
            parms[4].Value = model.MobileAlias;
            parms[5].Value = model.IsAnonymous;
            parms[6].Value = model.LastActivityDate;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(SiteUsersInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteUsers (Id,ApplicationId,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate)
			            values
						(@Id,@ApplicationId,@Coded,@Named,@LowerName,@MobileAlias,@IsAnonymous,@LastActivityDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,20),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,256),
new SqlParameter("@MobileAlias",SqlDbType.NVarChar,16),
new SqlParameter("@IsAnonymous",SqlDbType.Bit),
new SqlParameter("@LastActivityDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.LowerName;
            parms[5].Value = model.MobileAlias;
            parms[6].Value = model.IsAnonymous;
            parms[7].Value = model.LastActivityDate;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SiteUsersInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update SiteUsers set ApplicationId = @ApplicationId,Coded = @Coded,Named = @Named,LowerName = @LowerName,MobileAlias = @MobileAlias,IsAnonymous = @IsAnonymous,LastActivityDate = @LastActivityDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,20),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@LowerName",SqlDbType.NVarChar,256),
new SqlParameter("@MobileAlias",SqlDbType.NVarChar,16),
new SqlParameter("@IsAnonymous",SqlDbType.Bit),
new SqlParameter("@LastActivityDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.LowerName;
            parms[5].Value = model.MobileAlias;
            parms[6].Value = model.IsAnonymous;
            parms[7].Value = model.LastActivityDate;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from SiteUsers where Id = @Id ");
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
                sb.Append(@"delete from SiteUsers where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public SiteUsersInfo GetModel(Guid id)
        {
            SiteUsersInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 ApplicationId,Id,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate 
			            from SiteUsers
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
                        model = new SiteUsersInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.MobileAlias = reader.GetString(5);
                        model.IsAnonymous = reader.GetBoolean(6);
                        model.LastActivityDate = reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public IList<SiteUsersInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteUsers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteUsersInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          ApplicationId,Id,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate
					  from SiteUsers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteUsersInfo> list = new List<SiteUsersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteUsersInfo model = new SiteUsersInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.Id = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.LowerName = reader.GetString(5);
                        model.MobileAlias = reader.GetString(6);
                        model.IsAnonymous = reader.GetBoolean(7);
                        model.LastActivityDate = reader.GetDateTime(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteUsersInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           ApplicationId,Id,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate
					   from SiteUsers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteUsersInfo> list = new List<SiteUsersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteUsersInfo model = new SiteUsersInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.Id = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.LowerName = reader.GetString(5);
                        model.MobileAlias = reader.GetString(6);
                        model.IsAnonymous = reader.GetBoolean(7);
                        model.LastActivityDate = reader.GetDateTime(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteUsersInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select ApplicationId,Id,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate
                        from SiteUsers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<SiteUsersInfo> list = new List<SiteUsersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteUsersInfo model = new SiteUsersInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.MobileAlias = reader.GetString(5);
                        model.IsAnonymous = reader.GetBoolean(6);
                        model.LastActivityDate = reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteUsersInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select ApplicationId,Id,Coded,Named,LowerName,MobileAlias,IsAnonymous,LastActivityDate,LastUpdatedDate 
			            from SiteUsers
					    order by LastUpdatedDate desc ");

            IList<SiteUsersInfo> list = new List<SiteUsersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteUsersInfo model = new SiteUsersInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.LowerName = reader.GetString(4);
                        model.MobileAlias = reader.GetString(5);
                        model.IsAnonymous = reader.GetBoolean(6);
                        model.LastActivityDate = reader.GetDateTime(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
