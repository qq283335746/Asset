using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
     [DataContract(Name = "AuthFmModel")]
    public class AuthFmModel
    {
        [DataMember]
        public string Platform { get; set; }

        [DataMember]
        public string Deviceid { get; set; }
    }
}
