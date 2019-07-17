using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SiteMembersInfo
    {
        public SiteMembersInfo() { }

        public SiteMembersInfo(Guid applicationId, Guid userId, string password, int passwordFormat, string passwordSalt, string mobilePIN, string email, string loweredEmail, string passwordQuestion, string passwordAnswer, bool isApproved, bool isLockedOut, DateTime createDate, DateTime lastLoginDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate, int failedPasswordAttemptCount, DateTime failedPasswordAttemptWindowStart, int failedPasswordAnswerAttemptCount, DateTime failedPasswordAnswerAttemptWindowStart, string comment)
        {
            this.ApplicationId = applicationId;
            this.UserId = userId;
            this.Password = password;
            this.PasswordFormat = passwordFormat;
            this.PasswordSalt = passwordSalt;
            this.MobilePIN = mobilePIN;
            this.Email = email;
            this.LoweredEmail = loweredEmail;
            this.PasswordQuestion = passwordQuestion;
            this.PasswordAnswer = passwordAnswer;
            this.IsApproved = isApproved;
            this.IsLockedOut = isLockedOut;
            this.CreateDate = createDate;
            this.LastLoginDate = lastLoginDate;
            this.LastPasswordChangedDate = lastPasswordChangedDate;
            this.LastLockoutDate = lastLockoutDate;
            this.FailedPasswordAttemptCount = failedPasswordAttemptCount;
            this.FailedPasswordAttemptWindowStart = failedPasswordAttemptWindowStart;
            this.FailedPasswordAnswerAttemptCount = failedPasswordAnswerAttemptCount;
            this.FailedPasswordAnswerAttemptWindowStart = failedPasswordAnswerAttemptWindowStart;
            this.Comment = comment;
        }

        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public int PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public string MobilePIN { get; set; }
        public string Email { get; set; }
        public string LoweredEmail { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public DateTime LastLockoutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }
        public string Comment { get; set; }
    }
}
