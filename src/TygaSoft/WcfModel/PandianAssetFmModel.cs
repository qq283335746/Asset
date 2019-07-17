using System;
using System.Runtime.Serialization;
namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PandianAssetFmModel")]
    public class PandianAssetFmModel
    {
        [DataMember]
        public string AppCode { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string StartBuyDate { get; set; }

        [DataMember]
        public string EndBuyDate { get; set; }

        [DataMember]
        public string CategoryId { get; set; }

        [DataMember]
        public string UseDepmtId { get; set; }

        [DataMember]
        public string MgrDepmtId { get; set; }

        [DataMember]
        public string StoragePlaceId { get; set; }

        [DataMember]
        public bool IsConfirm { get; set; }
    }
}
