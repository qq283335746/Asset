using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "DicsFmModel")]
    public partial class DicsFmModel
    {
        [DataMember]
        public string AppCode { get; set; }

        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public object ParentId { get; set; }

        [DataMember]
        public string Step { get; set; }

        [DataMember]
        public string FlagName { get; set; }

        [DataMember]
        public int Sort { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
