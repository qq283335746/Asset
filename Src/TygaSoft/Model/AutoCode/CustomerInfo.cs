using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class CustomerInfo
    {
        public CustomerInfo() { }

        public CustomerInfo(Guid id, string appCode, Guid userId, string coded, string named, string shortName, string contactMan, string contactPhone, string telPhone, string fax, string postCode, string address, string cityName, string tradeName, DateTime cooperateTime, DateTime agreementTimeout, decimal joinPrice, string discountAbout, string payWay, string staffCode, string remark, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.AppCode = appCode;
            this.UserId = userId;
            this.Coded = coded;
            this.Named = named;
            this.ShortName = shortName;
            this.ContactMan = contactMan;
            this.ContactPhone = contactPhone;
            this.TelPhone = telPhone;
            this.Fax = fax;
            this.PostCode = postCode;
            this.Address = address;
            this.CityName = cityName;
            this.TradeName = tradeName;
            this.CooperateTime = cooperateTime;
            this.AgreementTimeout = agreementTimeout;
            this.JoinPrice = joinPrice;
            this.DiscountAbout = discountAbout;
            this.PayWay = payWay;
            this.StaffCode = staffCode;
            this.Remark = remark;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public string AppCode { get; set; }
        public Guid UserId { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string ShortName { get; set; }
        public string ContactMan { get; set; }
        public string ContactPhone { get; set; }
        public string TelPhone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string TradeName { get; set; }
        public DateTime CooperateTime { get; set; }
        public DateTime AgreementTimeout { get; set; }
        public decimal JoinPrice { get; set; }
        public string DiscountAbout { get; set; }
        public string PayWay { get; set; }
        public string StaffCode { get; set; }
        public string Remark { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
