using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StoragePlaceInfo
    {
        public StoragePlaceInfo() { }

        public StoragePlaceInfo(Guid id, string appCode, Guid userId, Guid depmtId, string coded, string named, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.AppCode = appCode;
            this.UserId = userId;
            this.DepmtId = depmtId;
            this.Coded = coded;
            this.Named = named;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public Guid DepmtId { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
