using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SiteMenusAccessInfo
    {
        public SiteMenusAccessInfo() { }

        public SiteMenusAccessInfo(Guid id, Guid applicationId, Guid accessId, string operationAccess, string accessType)
        {
            this.Id = id;
            this.ApplicationId = applicationId;
            this.AccessId = accessId;
            this.OperationAccess = operationAccess;
            this.AccessType = accessType;
        }

        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid AccessId { get; set; }
        public string OperationAccess { get; set; }
        public string AccessType { get; set; }
    }
}
