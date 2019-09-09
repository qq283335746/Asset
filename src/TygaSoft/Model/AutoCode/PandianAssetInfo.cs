using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class PandianAssetInfo
    {
        public PandianAssetInfo() { }

        public PandianAssetInfo(Guid pandianId, Guid assetId, string appCode, Guid userId, Guid depmtId, Guid lastUseDepmtId, Guid lastMgrDepmtId, Guid lastStoragePlaceId, string lastUsePerson, int sort, string remark, int status, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.PandianId = pandianId;
            this.AssetId = assetId;
            this.AppCode = appCode;
            this.UserId = userId;
            this.DepmtId = depmtId;
            this.LastUseDepmtId = lastUseDepmtId;
            this.LastMgrDepmtId = lastMgrDepmtId;
            this.LastStoragePlaceId = lastStoragePlaceId;
            this.LastUsePerson = lastUsePerson;
            this.Sort = sort;
            this.Remark = remark;
            this.Status = status;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid PandianId { get; set; }
        public Guid AssetId { get; set; }
        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public Guid DepmtId { get; set; }
        public Guid LastUseDepmtId { get; set; }
        public Guid LastMgrDepmtId { get; set; }
        public Guid LastStoragePlaceId { get; set; }
        public string LastUsePerson { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
