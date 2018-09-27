using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ProductRepairInfo
    {
        public ProductRepairInfo() { }

        public ProductRepairInfo(Guid id, string appCode, Guid userId, Guid orgId, Guid productId, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.AppCode = appCode;
            this.UserId = userId;
            this.OrgId = orgId;
            this.ProductId = productId;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public Guid OrgId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
