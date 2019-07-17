using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "CustomerFmModel")]
    public class CustomerFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string ContactMan { get; set; }

        [DataMember]
        public string ContactPhone { get; set; }

        [DataMember]
        public string TelPhone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string PostCode { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string CityName { get; set; }

        [DataMember]
        public string TradeName { get; set; }

        [DataMember]
        public string CooperateTime { get; set; }

        [DataMember]
        public string AgreementTimeout { get; set; }

        [DataMember]
        public string JoinPrice { get; set; }

        [DataMember]
        public string DiscountAbout { get; set; }

        [DataMember]
        public string PayWay { get; set; }

        [DataMember]
        public string StaffCode { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
