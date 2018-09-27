using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SiteUsersInfo
    {
        public SiteUsersInfo() { }

        public SiteUsersInfo(Guid applicationId, Guid id, string coded, string named, string lowerName, string mobileAlias, bool isAnonymous, DateTime lastActivityDate, DateTime lastUpdatedDate)
        {
            this.ApplicationId = applicationId;
            this.Id = id;
            this.Coded = coded;
            this.Named = named;
            this.LowerName = lowerName;
            this.MobileAlias = mobileAlias;
            this.IsAnonymous = isAnonymous;
            this.LastActivityDate = lastActivityDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ApplicationId { get; set; }
        public Guid Id { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string LowerName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
