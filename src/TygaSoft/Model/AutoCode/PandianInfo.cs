using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class PandianInfo
    {
        public PandianInfo() { }

        public PandianInfo(Guid id, string appCode, Guid userId, Guid depmtId, string named, int totalQty, bool isDown, Guid mgrDepmtId, int sort, string remark, int status, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.AppCode = appCode;
            this.UserId = userId;
            this.DepmtId = depmtId;
            this.Named = named;
            this.TotalQty = totalQty;
            this.IsDown = isDown;
            this.MgrDepmtId = mgrDepmtId;
            this.Sort = sort;
            this.Remark = remark;
            this.Status = status;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public Guid DepmtId { get; set; }
        public string Named { get; set; }
        public int TotalQty { get; set; }
        public bool IsDown { get; set; }
        public Guid MgrDepmtId { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
