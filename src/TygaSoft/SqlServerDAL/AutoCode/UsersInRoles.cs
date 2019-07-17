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
    public partial class UsersInRoles : IUsersInRoles
    {
        #region IUsersInRoles Member

        public int Insert(UsersInRolesInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into UsersInRoles (UserId,RoleId)
			            values
						(@UserId,@RoleId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.RoleId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid userId, Guid roleId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from UsersInRoles where UserId = @UserId and RoleId = @RoleId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = roleId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public UsersInRolesInfo GetModel(Guid userId, Guid roleId)
        {
            UsersInRolesInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 UserId,RoleId 
			            from UsersInRoles
						where UserId = @UserId and RoleId = @RoleId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = roleId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new UsersInRolesInfo();
                        model.UserId = reader.GetGuid(0);
                        model.RoleId = reader.GetGuid(1);
                    }
                }
            }

            return model;
        }

        #endregion
    }
}
