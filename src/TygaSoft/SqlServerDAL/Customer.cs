using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class Customer
    {
        #region ICustomer Member

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            sb.Append(@"select c.CityName '城市',c.Coded '客户编号',c.Named '客户名称',c.ContactMan '联系人',c.TelPhone '电话',c.Address '地址',c.TradeName '所属行业',CONVERT(varchar(36), c.CooperateTime, 23) '合作时间',CONVERT(varchar(36), c.AgreementTimeout, 23) '协议到期时间',c.JoinPrice '合作价格',c.DiscountAbout '优惠',c.PayWay '付款方式',c.StaffCode '开发服务对应人员工号',c.Remark '备注'
					      from Customer c
                         ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by RecordDate desc ");
            return SqlHelper.ExecuteDataset(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public IList<CustomerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CustomerInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate
					  from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        //model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Coded = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.ShortName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.ContactMan = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.ContactPhone = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.TelPhone = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Fax = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.PostCode = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.Address = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.CityName = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.TradeName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.CooperateTime = reader.IsDBNull(14) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(14);
                        model.AgreementTimeout = reader.IsDBNull(15) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(15);
                        model.JoinPrice = reader.IsDBNull(16) ? 0 : reader.GetDecimal(16);
                        model.DiscountAbout = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.PayWay = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.StaffCode = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.Remark = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.RecordDate = reader.IsDBNull(21) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(21);
                        model.LastUpdatedDate = reader.IsDBNull(22) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(22);

                        model.SCooperateTime = model.CooperateTime.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.CooperateTime.ToString("yyyy-MM-dd");
                        model.SAgreementTimeout = model.AgreementTimeout.ToString("yyyy-MM-dd") == "1754-01-01" ? "" : model.AgreementTimeout.ToString("yyyy-MM-dd");

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
