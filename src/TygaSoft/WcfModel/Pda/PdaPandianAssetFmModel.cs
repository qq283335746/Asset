using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaPandianAssetFmModel")]
    public class PdaPandianAssetFmModel : AuthFmModel
    {
        [DataMember]
        public string PandianId { get; set; }

        //[DataMember]
        //public string ItemList { get; set; }

        [DataMember]
        public List<PdaPandianAssetItemModel> ItemList { get; set; }
    }
}
