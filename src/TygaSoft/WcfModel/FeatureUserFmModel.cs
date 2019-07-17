using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "FeatureUserFmModel")]
    public class FeatureUserFmModel
    {
        [DataMember]
        public string FeatureId { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
