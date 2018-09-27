using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaPandianAssetModel")]
    public class PdaPandianAssetModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public string PandianId { get; set; }

        [DataMember]
        public int Status { get; set; }
    }
}
