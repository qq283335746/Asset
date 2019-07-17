using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class UserInOrgInfo
    {
        public UserInOrgInfo() { }

        public UserInOrgInfo(string appCode, Guid userId, Guid orgId)
        {
            this.AppCode = appCode;
            this.UserId = userId;
            this.OrgId = orgId;
        }

        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public Guid OrgId { get; set; }
    }
}
