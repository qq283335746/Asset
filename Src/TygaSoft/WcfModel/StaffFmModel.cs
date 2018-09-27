using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "StaffFmModel")]
    public partial class StaffFmModel
    {
        [DataMember]
        public string AppCode { get; set; }

        [DataMember]
        public object OrgId { get; set; }

        [DataMember]
        public object UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public int Sort { get; set; }
    }
}
