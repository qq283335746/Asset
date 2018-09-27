using System;

namespace TygaSoft.Model
{
    public partial class PandianAssetInfo
    {
        public string UserName { get; set; }

        public string StatusName { get; set; }

        public string AssetCode { get; set; }

        public string AssetName { get; set; }

        public string Barcode { get; set; }

        public string SpecModel { get; set; }

        public string Unit { get; set; }

        public int Qty { get; set; }

        public string PictureUrl { get; set; }

        public Guid CategoryId { get; set; }
        public Guid UseDepmtId { get; set; }
        public Guid MgrDepmtId { get; set; }
        public Guid StoragePlaceId { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string UseDepmtCode { get; set; }

        public string UseDepmtName { get; set; }

        public string MgrDepmtCode { get; set; }

        public string MgrDepmtName { get; set; }

        public string StoragePlaceCode { get; set; }

        public string StoragePlaceName { get; set; }

        public string UsePersonName { get; set; }

        public string LastUseDepmtName { get; set; }

        public string LastMgrDepmtName { get; set; }

        public string LastStoragePlaceName { get; set; }
       
    }
}
