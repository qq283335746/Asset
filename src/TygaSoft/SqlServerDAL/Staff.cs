using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class Staff
    {
        #region IStaff Member

        public Guid GetOrgId(Guid userid) 
        {
            var cmdText = @"select top 1 OrgId from UserInOrg uo where uo.UserId = @UserId ";
            var parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            parm.Value = userid;
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        return reader.GetGuid(0);
                    }
                }
            }
            return Guid.Empty;
        }

        public StaffInfo GetStaffOrgInfo(Guid userid)
        {
            StaffInfo model = null;
            var cmdText = @"select top 1 o.Id,o.Step
                            from Staff s 
                            join UserInOrg uo on uo.UserId = s.UserId
                            join OrgDepmt o on o.Id = uo.OrgId
                            where s.UserId = @UserId 
                          ";
            var parm = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            parm.Value = userid;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new StaffInfo();
                        model.OrgId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.OrgStep = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                    }
                }
            }
            return model;
        }

        public IList<StaffInfo> GetListByOrg(int pageIndex, int pageSize, out int totalRecords, object orgId, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            sb.AppendFormat(@"select count(*) from Staff s 
                        left join UserInOrg uo on s.AppCode = uo.AppCode and s.UserId = uo.UserId
                        join
                        (
                            select o.Id from OrgDepmt o where CHARINDEX('{0}', o.Step) > 0
                        )
                        c on c.Id = uo.OrgId
                        left join {1}aspnet_Users u on u.UserId = s.UserId
                        left join {1}aspnet_Membership m on m.UserId = u.UserId
                      ", orgId,GlobalConfig.Dbo);

            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StaffInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by s.Sort) as RowNumber,
			          s.UserId,s.AppCode,s.Coded,s.Named,s.Phone,s.Sort,s.Remark
                      ,u.UserName,m.Email
                      from Staff s
                        left join UserInOrg uo on s.AppCode = uo.AppCode and s.UserId = uo.UserId
                        join
                        (
                            select o.Id from OrgDepmt o where CHARINDEX('{0}', o.Step) > 0
                        )
                        c on c.Id = uo.OrgId
                        left join {1}aspnet_Users u on u.UserId = s.UserId
                        left join {1}aspnet_Membership m on m.UserId = u.UserId
                      ", orgId, GlobalConfig.Dbo);

            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<StaffInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new StaffInfo();
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Coded = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Phone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Sort = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.Remark = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.UserName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Email = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StaffInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.AppendFormat(@"select count(*) from Staff s 
                              left join UserInOrg uo on s.AppCode = uo.AppCode and s.UserId = uo.UserId
                              left join {0}aspnet_Users u on u.UserId = s.UserId
                              left join {0}aspnet_Membership m on m.UserId = u.UserId
                           ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StaffInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by s.Sort) as RowNumber,
			          s.UserId,s.AppCode,s.Coded,s.Named,s.Phone,s.Sort,s.Remark
                      ,u.UserName,m.Email,uo.OrgId
					  from Staff s 
                      left join UserInOrg uo on s.AppCode = uo.AppCode and s.UserId = uo.UserId
                      left join {0}aspnet_Users u on u.UserId = s.UserId
                      left join {0}aspnet_Membership m on m.UserId = u.UserId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<StaffInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new StaffInfo();
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Coded = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Phone = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Sort = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.Remark = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.UserName = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Email = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.OrgId = reader.IsDBNull(10) ? Guid.Empty : reader.GetGuid(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public int DeleteStaff(Guid userid)
        {
            var sb = new StringBuilder(250);
            sb.Append(@"delete from Staff where UserId = @UserId; ");
            sb.Append(@"delete from UserInOrg where UserId = @UserId;");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userid;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public void InsertOrgStaff(StaffInfo model)
        {
            Insert(model);
            var userInOrgInfo = new UserInOrgInfo(model.AppCode, model.UserId, model.OrgId);
            new UserInOrg().Insert(userInOrgInfo);
        }

        #endregion
    }
}
