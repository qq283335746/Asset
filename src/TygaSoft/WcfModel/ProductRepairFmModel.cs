using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ProductRepairFmModel")]
    public class ProductRepairFmModel
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string AppCode { get; set; }

        [DataMember]
        public string ProductId { get; set; }

        [DataMember]
        public string StatusName { get; set; }
    }
}
