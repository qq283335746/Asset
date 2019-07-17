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
    public partial class SiteMembers : ISiteMembers
    {
        #region ISiteMembers Member

        public int Insert(SiteMembersInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteMembers (UserId,ApplicationId,Password,PasswordFormat,PasswordSalt,MobilePIN,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart,Comment)
			            values
						(@UserId,@ApplicationId,@Password,@PasswordFormat,@PasswordSalt,@MobilePIN,@Email,@LoweredEmail,@PasswordQuestion,@PasswordAnswer,@IsApproved,@IsLockedOut,@CreateDate,@LastLoginDate,@LastPasswordChangedDate,@LastLockoutDate,@FailedPasswordAttemptCount,@FailedPasswordAttemptWindowStart,@FailedPasswordAnswerAttemptCount,@FailedPasswordAnswerAttemptWindowStart,@Comment)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Password",SqlDbType.NVarChar,128),
new SqlParameter("@PasswordFormat",SqlDbType.Int),
new SqlParameter("@PasswordSalt",SqlDbType.NVarChar,128),
new SqlParameter("@MobilePIN",SqlDbType.NVarChar,16),
new SqlParameter("@Email",SqlDbType.NVarChar,256),
new SqlParameter("@LoweredEmail",SqlDbType.NVarChar,256),
new SqlParameter("@PasswordQuestion",SqlDbType.NVarChar,256),
new SqlParameter("@PasswordAnswer",SqlDbType.NVarChar,128),
new SqlParameter("@IsApproved",SqlDbType.Bit),
new SqlParameter("@IsLockedOut",SqlDbType.Bit),
new SqlParameter("@CreateDate",SqlDbType.DateTime),
new SqlParameter("@LastLoginDate",SqlDbType.DateTime),
new SqlParameter("@LastPasswordChangedDate",SqlDbType.DateTime),
new SqlParameter("@LastLockoutDate",SqlDbType.DateTime),
new SqlParameter("@FailedPasswordAttemptCount",SqlDbType.Int),
new SqlParameter("@FailedPasswordAttemptWindowStart",SqlDbType.DateTime),
new SqlParameter("@FailedPasswordAnswerAttemptCount",SqlDbType.Int),
new SqlParameter("@FailedPasswordAnswerAttemptWindowStart",SqlDbType.DateTime),
new SqlParameter("@Comment",SqlDbType.NText,1073741823)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.Password;
            parms[3].Value = model.PasswordFormat;
            parms[4].Value = model.PasswordSalt;
            parms[5].Value = model.MobilePIN;
            parms[6].Value = model.Email;
            parms[7].Value = model.LoweredEmail;
            parms[8].Value = model.PasswordQuestion;
            parms[9].Value = model.PasswordAnswer;
            parms[10].Value = model.IsApproved;
            parms[11].Value = model.IsLockedOut;
            parms[12].Value = model.CreateDate;
            parms[13].Value = model.LastLoginDate;
            parms[14].Value = model.LastPasswordChangedDate;
            parms[15].Value = model.LastLockoutDate;
            parms[16].Value = model.FailedPasswordAttemptCount;
            parms[17].Value = model.FailedPasswordAttemptWindowStart;
            parms[18].Value = model.FailedPasswordAnswerAttemptCount;
            parms[19].Value = model.FailedPasswordAnswerAttemptWindowStart;
            parms[20].Value = model.Comment;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SiteMembersInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update SiteMembers set ApplicationId = @ApplicationId,Password = @Password,PasswordFormat = @PasswordFormat,PasswordSalt = @PasswordSalt,MobilePIN = @MobilePIN,Email = @Email,LoweredEmail = @LoweredEmail,PasswordQuestion = @PasswordQuestion,PasswordAnswer = @PasswordAnswer,IsApproved = @IsApproved,IsLockedOut = @IsLockedOut,CreateDate = @CreateDate,LastLoginDate = @LastLoginDate,LastPasswordChangedDate = @LastPasswordChangedDate,LastLockoutDate = @LastLockoutDate,FailedPasswordAttemptCount = @FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart,Comment = @Comment 
			            where UserId = @UserId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Password",SqlDbType.NVarChar,128),
new SqlParameter("@PasswordFormat",SqlDbType.Int),
new SqlParameter("@PasswordSalt",SqlDbType.NVarChar,128),
new SqlParameter("@MobilePIN",SqlDbType.NVarChar,16),
new SqlParameter("@Email",SqlDbType.NVarChar,256),
new SqlParameter("@LoweredEmail",SqlDbType.NVarChar,256),
new SqlParameter("@PasswordQuestion",SqlDbType.NVarChar,256),
new SqlParameter("@PasswordAnswer",SqlDbType.NVarChar,128),
new SqlParameter("@IsApproved",SqlDbType.Bit),
new SqlParameter("@IsLockedOut",SqlDbType.Bit),
new SqlParameter("@CreateDate",SqlDbType.DateTime),
new SqlParameter("@LastLoginDate",SqlDbType.DateTime),
new SqlParameter("@LastPasswordChangedDate",SqlDbType.DateTime),
new SqlParameter("@LastLockoutDate",SqlDbType.DateTime),
new SqlParameter("@FailedPasswordAttemptCount",SqlDbType.Int),
new SqlParameter("@FailedPasswordAttemptWindowStart",SqlDbType.DateTime),
new SqlParameter("@FailedPasswordAnswerAttemptCount",SqlDbType.Int),
new SqlParameter("@FailedPasswordAnswerAttemptWindowStart",SqlDbType.DateTime),
new SqlParameter("@Comment",SqlDbType.NText,1073741823)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.Password;
            parms[3].Value = model.PasswordFormat;
            parms[4].Value = model.PasswordSalt;
            parms[5].Value = model.MobilePIN;
            parms[6].Value = model.Email;
            parms[7].Value = model.LoweredEmail;
            parms[8].Value = model.PasswordQuestion;
            parms[9].Value = model.PasswordAnswer;
            parms[10].Value = model.IsApproved;
            parms[11].Value = model.IsLockedOut;
            parms[12].Value = model.CreateDate;
            parms[13].Value = model.LastLoginDate;
            parms[14].Value = model.LastPasswordChangedDate;
            parms[15].Value = model.LastLockoutDate;
            parms[16].Value = model.FailedPasswordAttemptCount;
            parms[17].Value = model.FailedPasswordAttemptWindowStart;
            parms[18].Value = model.FailedPasswordAnswerAttemptCount;
            parms[19].Value = model.FailedPasswordAnswerAttemptWindowStart;
            parms[20].Value = model.Comment;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid userId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from SiteMembers where UserId = @UserId ");
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
                sb.Append(@"delete from SiteMembers where UserId = @UserId" + n + " ;");
                SqlParameter parm = new SqlParameter("@UserId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public SiteMembersInfo GetModel(Guid userId)
        {
            SiteMembersInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 ApplicationId,UserId,Password,PasswordFormat,PasswordSalt,MobilePIN,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart,Comment 
			            from SiteMembers
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
                        model = new SiteMembersInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Password = reader.GetString(2);
                        model.PasswordFormat = reader.GetInt32(3);
                        model.PasswordSalt = reader.GetString(4);
                        model.MobilePIN = reader.GetString(5);
                        model.Email = reader.GetString(6);
                        model.LoweredEmail = reader.GetString(7);
                        model.PasswordQuestion = reader.GetString(8);
                        model.PasswordAnswer = reader.GetString(9);
                        model.IsApproved = reader.GetBoolean(10);
                        model.IsLockedOut = reader.GetBoolean(11);
                        model.CreateDate = reader.GetDateTime(12);
                        model.LastLoginDate = reader.GetDateTime(13);
                        model.LastPasswordChangedDate = reader.GetDateTime(14);
                        model.LastLockoutDate = reader.GetDateTime(15);
                        model.FailedPasswordAttemptCount = reader.GetInt32(16);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(17);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(18);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(19);
                        model.Comment = reader.GetString(20);
                    }
                }
            }

            return model;
        }

        public IList<SiteMembersInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteMembers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteMembersInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by ) as RowNumber,
			          ApplicationId,UserId,Password,PasswordFormat,PasswordSalt,MobilePIN,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart,Comment
					  from SiteMembers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteMembersInfo> list = new List<SiteMembersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMembersInfo model = new SiteMembersInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.Password = reader.GetString(3);
                        model.PasswordFormat = reader.GetInt32(4);
                        model.PasswordSalt = reader.GetString(5);
                        model.MobilePIN = reader.GetString(6);
                        model.Email = reader.GetString(7);
                        model.LoweredEmail = reader.GetString(8);
                        model.PasswordQuestion = reader.GetString(9);
                        model.PasswordAnswer = reader.GetString(10);
                        model.IsApproved = reader.GetBoolean(11);
                        model.IsLockedOut = reader.GetBoolean(12);
                        model.CreateDate = reader.GetDateTime(13);
                        model.LastLoginDate = reader.GetDateTime(14);
                        model.LastPasswordChangedDate = reader.GetDateTime(15);
                        model.LastLockoutDate = reader.GetDateTime(16);
                        model.FailedPasswordAttemptCount = reader.GetInt32(17);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(18);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(19);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(20);
                        model.Comment = reader.GetString(21);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMembersInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by ) as RowNumber,
			           ApplicationId,UserId,Password,PasswordFormat,PasswordSalt,MobilePIN,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart,Comment
					   from SiteMembers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteMembersInfo> list = new List<SiteMembersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMembersInfo model = new SiteMembersInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.Password = reader.GetString(3);
                        model.PasswordFormat = reader.GetInt32(4);
                        model.PasswordSalt = reader.GetString(5);
                        model.MobilePIN = reader.GetString(6);
                        model.Email = reader.GetString(7);
                        model.LoweredEmail = reader.GetString(8);
                        model.PasswordQuestion = reader.GetString(9);
                        model.PasswordAnswer = reader.GetString(10);
                        model.IsApproved = reader.GetBoolean(11);
                        model.IsLockedOut = reader.GetBoolean(12);
                        model.CreateDate = reader.GetDateTime(13);
                        model.LastLoginDate = reader.GetDateTime(14);
                        model.LastPasswordChangedDate = reader.GetDateTime(15);
                        model.LastLockoutDate = reader.GetDateTime(16);
                        model.FailedPasswordAttemptCount = reader.GetInt32(17);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(18);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(19);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(20);
                        model.Comment = reader.GetString(21);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMembersInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select ApplicationId,UserId,Password,PasswordFormat,PasswordSalt,MobilePIN,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart,Comment
                        from SiteMembers ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by  ");

            IList<SiteMembersInfo> list = new List<SiteMembersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMembersInfo model = new SiteMembersInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Password = reader.GetString(2);
                        model.PasswordFormat = reader.GetInt32(3);
                        model.PasswordSalt = reader.GetString(4);
                        model.MobilePIN = reader.GetString(5);
                        model.Email = reader.GetString(6);
                        model.LoweredEmail = reader.GetString(7);
                        model.PasswordQuestion = reader.GetString(8);
                        model.PasswordAnswer = reader.GetString(9);
                        model.IsApproved = reader.GetBoolean(10);
                        model.IsLockedOut = reader.GetBoolean(11);
                        model.CreateDate = reader.GetDateTime(12);
                        model.LastLoginDate = reader.GetDateTime(13);
                        model.LastPasswordChangedDate = reader.GetDateTime(14);
                        model.LastLockoutDate = reader.GetDateTime(15);
                        model.FailedPasswordAttemptCount = reader.GetInt32(16);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(17);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(18);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(19);
                        model.Comment = reader.GetString(20);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMembersInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select ApplicationId,UserId,Password,PasswordFormat,PasswordSalt,MobilePIN,Email,LoweredEmail,PasswordQuestion,PasswordAnswer,IsApproved,IsLockedOut,CreateDate,LastLoginDate,LastPasswordChangedDate,LastLockoutDate,FailedPasswordAttemptCount,FailedPasswordAttemptWindowStart,FailedPasswordAnswerAttemptCount,FailedPasswordAnswerAttemptWindowStart,Comment 
			            from SiteMembers
					    order by  ");

            IList<SiteMembersInfo> list = new List<SiteMembersInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMembersInfo model = new SiteMembersInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Password = reader.GetString(2);
                        model.PasswordFormat = reader.GetInt32(3);
                        model.PasswordSalt = reader.GetString(4);
                        model.MobilePIN = reader.GetString(5);
                        model.Email = reader.GetString(6);
                        model.LoweredEmail = reader.GetString(7);
                        model.PasswordQuestion = reader.GetString(8);
                        model.PasswordAnswer = reader.GetString(9);
                        model.IsApproved = reader.GetBoolean(10);
                        model.IsLockedOut = reader.GetBoolean(11);
                        model.CreateDate = reader.GetDateTime(12);
                        model.LastLoginDate = reader.GetDateTime(13);
                        model.LastPasswordChangedDate = reader.GetDateTime(14);
                        model.LastLockoutDate = reader.GetDateTime(15);
                        model.FailedPasswordAttemptCount = reader.GetInt32(16);
                        model.FailedPasswordAttemptWindowStart = reader.GetDateTime(17);
                        model.FailedPasswordAnswerAttemptCount = reader.GetInt32(18);
                        model.FailedPasswordAnswerAttemptWindowStart = reader.GetDateTime(19);
                        model.Comment = reader.GetString(20);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
