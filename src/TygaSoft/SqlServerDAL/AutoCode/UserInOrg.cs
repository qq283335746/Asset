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
    public partial class UserInOrg
    {
        #region IUserInOrg Member

        public int Insert(UserInOrgInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into UserInOrg (UserId,OrgId,AppCode)
			            values
						(@UserId,@OrgId,@AppCode)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OrgId;
            parms[2].Value = model.AppCode;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(UserInOrgInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update UserInOrg set AppCode = @AppCode 
			            where UserId = @UserId and OrgId = @OrgId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OrgId;
            parms[2].Value = model.AppCode;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid userId, Guid orgId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from UserInOrg where UserId = @UserId and OrgId = @OrgId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = orgId;

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
                sb.Append(@"delete from UserInOrg where UserId = @UserId" + n + " ;");
                SqlParameter parm = new SqlParameter("@UserId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public UserInOrgInfo GetModel(Guid userId, Guid orgId)
        {
            UserInOrgInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 AppCode,UserId,OrgId 
			            from UserInOrg
						where UserId = @UserId and OrgId = @OrgId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = orgId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new UserInOrgInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.OrgId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                    }
                }
            }

            return model;
        }

        public IList<UserInOrgInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from UserInOrg ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<UserInOrgInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by UserId) as RowNumber,
			          AppCode,UserId,OrgId
					  from UserInOrg ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UserInOrgInfo> list = new List<UserInOrgInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserInOrgInfo model = new UserInOrgInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.OrgId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserInOrgInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by UserId) as RowNumber,
			           AppCode,UserId,OrgId
					   from UserInOrg ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UserInOrgInfo> list = new List<UserInOrgInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserInOrgInfo model = new UserInOrgInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.OrgId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserInOrgInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select AppCode,UserId,OrgId
                        from UserInOrg ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by UserId ");

            IList<UserInOrgInfo> list = new List<UserInOrgInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserInOrgInfo model = new UserInOrgInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.OrgId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserInOrgInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select AppCode,UserId,OrgId 
			            from UserInOrg
					    order by UserId ");

            IList<UserInOrgInfo> list = new List<UserInOrgInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserInOrgInfo model = new UserInOrgInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.OrgId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
