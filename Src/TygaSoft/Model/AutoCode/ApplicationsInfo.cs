using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ApplicationsInfo
    {
        public ApplicationsInfo() { }

        public ApplicationsInfo(Guid id, string coded, string named, string lowerName, string remark)
        {
            this.Id = id;
            this.Coded = coded;
            this.Named = named;
            this.LowerName = lowerName;
            this.Remark = remark;
        }

        public Guid Id { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string LowerName { get; set; }
        public string Remark { get; set; }
    }
}
