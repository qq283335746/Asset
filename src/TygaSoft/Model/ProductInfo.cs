using System;

namespace TygaSoft.Model
{
    public partial class ProductInfo
    {
        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string CategoryParentCode { get; set; }

        public string CategoryParentName { get; set; }

        public string UseOrgCode { get; set; }

        public string UseOrgName { get; set; }

        public string MgrOrgCode { get; set; }

        public string MgrOrgName { get; set; }

        public string StoragePlaceCode { get; set; }

        public string StoragePlaceName { get; set; }

        public string SBuyDate { get; set; }

        public string StatusName { get; set; }

        public string SRecordDate { get; set; }

        public string UserName { get; set; }
    }
}
