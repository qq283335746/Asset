using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StaffInfo
    {
        public StaffInfo() { }

        public StaffInfo(Guid userId, string appCode, string coded, string named, string phone, int sort, string remark, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.UserId = userId;
            this.AppCode = appCode;
            this.Coded = coded;
            this.Named = named;
            this.Phone = phone;
            this.Sort = sort;
            this.Remark = remark;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string AppCode { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string Phone { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
