using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaPandianAssetItemModel")]
    public class PdaPandianAssetItemModel
    {
        [DataMember]
        public string AssetId { get; set; }

        [DataMember]
        public string AssetName { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        [DataMember]
        public string SpecModel { get; set; }

        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public string CategoryId { get; set; }

        [DataMember]
        public string UseDepmtId { get; set; }

        [DataMember]
        public string MgrDepmtId { get; set; }

        [DataMember]
        public string StoreLocationId { get; set; }

        [DataMember]
        public string UsePerson { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public int Status { get; set; }
    }
}
