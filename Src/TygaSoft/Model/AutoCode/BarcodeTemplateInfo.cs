using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class BarcodeTemplateInfo
    {
        public BarcodeTemplateInfo() { }

        public BarcodeTemplateInfo(Guid id, Guid userId, string title, string html, string attr, bool isDefault, string typeName, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.Title = title;
            this.Html = html;
            this.Attr = attr;
            this.IsDefault = isDefault;
            this.TypeName = typeName;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Html { get; set; }
        public string Attr { get; set; }
        public bool IsDefault { get; set; }
        public string TypeName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
