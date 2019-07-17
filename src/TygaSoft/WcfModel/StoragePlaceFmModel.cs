using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "StoragePlaceFmModel")]
    public class StoragePlaceFmModel
    {
        [DataMember]
        public string AppCode { get; set; }

        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }
    }
}
