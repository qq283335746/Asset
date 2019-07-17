using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ContentTypeInfo
    {
        public ContentTypeInfo() { }

        public ContentTypeInfo(string appCode, Guid userId, Guid depmtId, Guid id, Guid parentId, string coded, string named, string step, string flagName, int sort, string remark, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.AppCode = appCode;
            this.UserId = userId;
            this.DepmtId = depmtId;
            this.Id = id;
            this.ParentId = parentId;
            this.Coded = coded;
            this.Named = named;
            this.Step = step;
            this.FlagName = flagName;
            this.Sort = sort;
            this.Remark = remark;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public Guid DepmtId { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string Step { get; set; }
        public string FlagName { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
