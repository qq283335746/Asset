using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaPandianFmModel")]
    public class PdaPandianFmModel : AuthFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string AppKey { get; set; }
    }
}
