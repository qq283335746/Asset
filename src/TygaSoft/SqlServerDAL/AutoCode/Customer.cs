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
    public partial class Customer : ICustomer
    {
        #region ICustomer Member

        public int Insert(CustomerInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Customer (AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@Coded,@Named,@ShortName,@ContactMan,@ContactPhone,@TelPhone,@Fax,@PostCode,@Address,@CityName,@TradeName,@CooperateTime,@AgreementTimeout,@JoinPrice,@DiscountAbout,@PayWay,@StaffCode,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,6),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,36),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@ShortName",SqlDbType.NVarChar,50),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@ContactPhone",SqlDbType.VarChar,20),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Fax",SqlDbType.VarChar,20),
new SqlParameter("@PostCode",SqlDbType.VarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,256),
new SqlParameter("@CityName",SqlDbType.NVarChar,30),
new SqlParameter("@TradeName",SqlDbType.NVarChar,50),
new SqlParameter("@CooperateTime",SqlDbType.DateTime),
new SqlParameter("@AgreementTimeout",SqlDbType.DateTime),
new SqlParameter("@JoinPrice",SqlDbType.Decimal),
new SqlParameter("@DiscountAbout",SqlDbType.NVarChar,50),
new SqlParameter("@PayWay",SqlDbType.NVarChar,30),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@Remark",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.ShortName;
            parms[5].Value = model.ContactMan;
            parms[6].Value = model.ContactPhone;
            parms[7].Value = model.TelPhone;
            parms[8].Value = model.Fax;
            parms[9].Value = model.PostCode;
            parms[10].Value = model.Address;
            parms[11].Value = model.CityName;
            parms[12].Value = model.TradeName;
            parms[13].Value = model.CooperateTime;
            parms[14].Value = model.AgreementTimeout;
            parms[15].Value = model.JoinPrice;
            parms[16].Value = model.DiscountAbout;
            parms[17].Value = model.PayWay;
            parms[18].Value = model.StaffCode;
            parms[19].Value = model.Remark;
            parms[20].Value = model.RecordDate;
            parms[21].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(CustomerInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Customer (Id,AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@Coded,@Named,@ShortName,@ContactMan,@ContactPhone,@TelPhone,@Fax,@PostCode,@Address,@CityName,@TradeName,@CooperateTime,@AgreementTimeout,@JoinPrice,@DiscountAbout,@PayWay,@StaffCode,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@AppCode",SqlDbType.Char,6),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,36),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@ShortName",SqlDbType.NVarChar,50),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@ContactPhone",SqlDbType.VarChar,20),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Fax",SqlDbType.VarChar,20),
new SqlParameter("@PostCode",SqlDbType.VarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,256),
new SqlParameter("@CityName",SqlDbType.NVarChar,30),
new SqlParameter("@TradeName",SqlDbType.NVarChar,50),
new SqlParameter("@CooperateTime",SqlDbType.DateTime),
new SqlParameter("@AgreementTimeout",SqlDbType.DateTime),
new SqlParameter("@JoinPrice",SqlDbType.Decimal),
new SqlParameter("@DiscountAbout",SqlDbType.NVarChar,50),
new SqlParameter("@PayWay",SqlDbType.NVarChar,30),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@Remark",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.Named;
            parms[5].Value = model.ShortName;
            parms[6].Value = model.ContactMan;
            parms[7].Value = model.ContactPhone;
            parms[8].Value = model.TelPhone;
            parms[9].Value = model.Fax;
            parms[10].Value = model.PostCode;
            parms[11].Value = model.Address;
            parms[12].Value = model.CityName;
            parms[13].Value = model.TradeName;
            parms[14].Value = model.CooperateTime;
            parms[15].Value = model.AgreementTimeout;
            parms[16].Value = model.JoinPrice;
            parms[17].Value = model.DiscountAbout;
            parms[18].Value = model.PayWay;
            parms[19].Value = model.StaffCode;
            parms[20].Value = model.Remark;
            parms[21].Value = model.RecordDate;
            parms[22].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(CustomerInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Customer set AppCode = @AppCode,UserId = @UserId,Coded = @Coded,Named = @Named,ShortName = @ShortName,ContactMan = @ContactMan,ContactPhone = @ContactPhone,TelPhone = @TelPhone,Fax = @Fax,PostCode = @PostCode,Address = @Address,CityName = @CityName,TradeName = @TradeName,CooperateTime = @CooperateTime,AgreementTimeout = @AgreementTimeout,JoinPrice = @JoinPrice,DiscountAbout = @DiscountAbout,PayWay = @PayWay,StaffCode = @StaffCode,Remark = @Remark,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@AppCode",SqlDbType.Char,6),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,36),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@ShortName",SqlDbType.NVarChar,50),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@ContactPhone",SqlDbType.VarChar,20),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Fax",SqlDbType.VarChar,20),
new SqlParameter("@PostCode",SqlDbType.VarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,256),
new SqlParameter("@CityName",SqlDbType.NVarChar,30),
new SqlParameter("@TradeName",SqlDbType.NVarChar,50),
new SqlParameter("@CooperateTime",SqlDbType.DateTime),
new SqlParameter("@AgreementTimeout",SqlDbType.DateTime),
new SqlParameter("@JoinPrice",SqlDbType.Decimal),
new SqlParameter("@DiscountAbout",SqlDbType.NVarChar,50),
new SqlParameter("@PayWay",SqlDbType.NVarChar,30),
new SqlParameter("@StaffCode",SqlDbType.VarChar,36),
new SqlParameter("@Remark",SqlDbType.NVarChar,256),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.Named;
            parms[5].Value = model.ShortName;
            parms[6].Value = model.ContactMan;
            parms[7].Value = model.ContactPhone;
            parms[8].Value = model.TelPhone;
            parms[9].Value = model.Fax;
            parms[10].Value = model.PostCode;
            parms[11].Value = model.Address;
            parms[12].Value = model.CityName;
            parms[13].Value = model.TradeName;
            parms[14].Value = model.CooperateTime;
            parms[15].Value = model.AgreementTimeout;
            parms[16].Value = model.JoinPrice;
            parms[17].Value = model.DiscountAbout;
            parms[18].Value = model.PayWay;
            parms[19].Value = model.StaffCode;
            parms[20].Value = model.Remark;
            parms[21].Value = model.RecordDate;
            parms[22].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Customer where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from Customer where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public CustomerInfo GetModel(Guid id)
        {
            CustomerInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate 
			            from Customer
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new CustomerInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
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
                    }
                }
            }

            return model;
        }

        public IList<CustomerInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
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
			          Id,AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate
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
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Coded = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.ShortName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.ContactMan = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ContactPhone = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.TelPhone = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Fax = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.PostCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.Address = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.CityName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.TradeName = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.CooperateTime = reader.IsDBNull(15) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(15);
                        model.AgreementTimeout = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.JoinPrice = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
                        model.DiscountAbout = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.PayWay = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.StaffCode = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.Remark = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                        model.RecordDate = reader.IsDBNull(22) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(22);
                        model.LastUpdatedDate = reader.IsDBNull(23) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(23);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CustomerInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate
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
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Coded = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.ShortName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.ContactMan = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ContactPhone = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.TelPhone = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Fax = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.PostCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.Address = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.CityName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.TradeName = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.CooperateTime = reader.IsDBNull(15) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(15);
                        model.AgreementTimeout = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.JoinPrice = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
                        model.DiscountAbout = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.PayWay = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.StaffCode = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.Remark = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                        model.RecordDate = reader.IsDBNull(22) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(22);
                        model.LastUpdatedDate = reader.IsDBNull(23) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(23);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CustomerInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate
                        from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CustomerInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,AppCode,UserId,Coded,Named,ShortName,ContactMan,ContactPhone,TelPhone,Fax,PostCode,Address,CityName,TradeName,CooperateTime,AgreementTimeout,JoinPrice,DiscountAbout,PayWay,StaffCode,Remark,RecordDate,LastUpdatedDate 
			            from Customer
					    order by LastUpdatedDate desc ");

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
