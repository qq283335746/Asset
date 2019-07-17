using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ContentDetailFmModel")]
    public partial class ContentDetailFmModel
    {
        [DataMember]
        public string AppCode { get; set; }

        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object ContentTypeId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Keyword { get; set; }

        [DataMember]
        public string Descr { get; set; }

        [DataMember]
        public string ContentText { get; set; }

        [DataMember]
        public byte Openness { get; set; }

        [DataMember]
        public int Sort { get; set; }
    }
}
