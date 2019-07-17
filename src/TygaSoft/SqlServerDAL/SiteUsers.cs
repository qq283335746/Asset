using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class SiteUsers
    {
        #region ISiteUsers Member

        public bool IsExistUserName(string username,Guid Id)
        {
            var cmdText = @"select 1 from [SiteUsers] where Named = @Named and Id <> @Id ";
            SqlParameter[] parms = {
                new SqlParameter("@Named", SqlDbType.NVarChar, 50),
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = username;
            parms[1].Value = Id;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        public SiteUsersInfo GetModel(string username)
        {
            SiteUsersInfo model = null;

            var cmdText = @"select top 1 u.ApplicationId,u.Id,u.Coded,u.Named,u.LowerName,u.MobileAlias,u.IsAnonymous,u.LastActivityDate,u.LastUpdatedDate 
			                from SiteUsers u 
						    where Named = @Named ";
            var parm = new SqlParameter("@Named", SqlDbType.NVarChar, 50);
            parm.Value = username;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm))
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

        public SiteUsersInfo GetModelByJoin(string username,object Id)
        {
            SiteUsersInfo model = null;
            var sb = new StringBuilder(500);
            sb.Append(@"select top 1 u.ApplicationId,u.Id,u.Coded,u.Named,u.LowerName,u.MobileAlias,u.IsAnonymous,u.LastActivityDate,u.LastUpdatedDate 
                            ,m.Password,m.PasswordFormat,m.PasswordSalt,m.MobilePIN,m.Email,m.LoweredEmail,m.PasswordQuestion,m.PasswordAnswer,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,m.LastPasswordChangedDate,m.LastLockoutDate,m.FailedPasswordAttemptCount,m.FailedPasswordAttemptWindowStart,m.FailedPasswordAnswerAttemptCount,m.FailedPasswordAnswerAttemptWindowStart,m.Comment 
			                from SiteUsers u 
                            join SiteMembers m on m.UserId = u.Id
						    ");
            SqlParameter parm = null;
            if (!string.IsNullOrEmpty(username))
            {
                sb.Append("where Named = @Named");
                parm = new SqlParameter("@Named", SqlDbType.NVarChar, 50);
                parm.Value = username;
            }
            else if (Id is Guid)
            {
                sb.Append("where Id = @Id");
                parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(Id.ToString());
            }

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parm))
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
                        model.Password = reader.GetString(9);
                        model.PasswordFormat = reader.GetInt32(10);
                        model.PasswordSalt = reader.GetString(11);
                        model.MobilePIN = reader.GetString(12);
                        model.Email = reader.GetString(13);
                        model.LoweredEmail = reader.GetString(14);
                        model.PasswordQuestion = reader.GetString(15);
                        model.PasswordAnswer = reader.GetString(16);
                        model.IsApproved = reader.GetBoolean(17);
                        model.IsLockedOut = reader.GetBoolean(18);
                        model.CreateDate = reader.GetDateTime(19);
                        model.LastLoginDate = reader.GetDateTime(20);
                        model.LastPasswordChangedDate = reader.GetDateTime(21);
                        model.LastLockoutDate = reader.GetDateTime(22);
                        model.FailedPasswordAttemptCount = reader.GetInt32(23);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(24);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(25);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(26);
                        model.Comment = reader.GetString(27);
                    }
                }
            }

            return model;
        }

        public IList<SiteUsersInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteUsers u
                        join SiteMembers m on m.UserId = u.Id
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteUsersInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by u.LastUpdatedDate desc) as RowNumber,
			          u.ApplicationId,u.Id,u.Coded,u.Named,u.LowerName,u.MobileAlias,u.IsAnonymous,u.LastActivityDate,u.LastUpdatedDate
                      ,m.Password,m.PasswordFormat,m.PasswordSalt,m.MobilePIN,m.Email,m.LoweredEmail,m.PasswordQuestion,m.PasswordAnswer,m.IsApproved,m.IsLockedOut,m.CreateDate,m.LastLoginDate,m.LastPasswordChangedDate,m.LastLockoutDate,m.FailedPasswordAttemptCount,m.FailedPasswordAttemptWindowStart,m.FailedPasswordAnswerAttemptCount,m.FailedPasswordAnswerAttemptWindowStart,m.Comment 
					  from SiteUsers u 
                      join SiteMembers m on m.UserId = u.Id
                      ");
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

                        model.Password = reader.GetString(10);
                        model.PasswordFormat = reader.GetInt32(11);
                        model.PasswordSalt = reader.GetString(12);
                        model.MobilePIN = reader.GetString(13);
                        model.Email = reader.GetString(14);
                        model.LoweredEmail = reader.GetString(15);
                        model.PasswordQuestion = reader.GetString(16);
                        model.PasswordAnswer = reader.GetString(17);
                        model.IsApproved = reader.GetBoolean(18);
                        model.IsLockedOut = reader.GetBoolean(19);
                        model.CreateDate = reader.GetDateTime(20);
                        model.LastLoginDate = reader.GetDateTime(21);
                        model.LastPasswordChangedDate = reader.GetDateTime(22);
                        model.LastLockoutDate = reader.GetDateTime(23);
                        model.FailedPasswordAttemptCount = reader.GetInt32(24);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(25);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(26);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(27);
                        model.Comment = reader.GetString(28);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
