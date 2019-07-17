using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class Product
    {
        #region IProduct Member

        public string GetRndBarcode() 
        {
            while (true) 
            {
                Thread.Sleep(1);
                var rnd = new Random();
                var sBarcode = (rnd.NextDouble() * int.MaxValue).ToString();
                var cmdText = @"select 1 from Product where Barcode = '" + sBarcode + "'";
                object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, null);
                if (obj == null) return sBarcode;
            }
        }

        public string GetRndCode(string prefix,int n)
        {
            var len = prefix.Length;
            var max = n-len;
            while (true)
            {
                Thread.Sleep(1);
                var rnd = new Random();
                var sb = new StringBuilder(10);
                for (var i = 0; i < max; i++) 
                {
                    sb.Append(rnd.Next(10));
                }
                var sCode = string.Format("{0}{1}", prefix, sb.ToString().PadLeft(max, '0'));
                var cmdText = @"select 1 from Product where Coded = '" + sCode + "'";
                object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, null);
                if (obj == null) return sCode;
            }
        }

        public IList<ProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            sb.Append(@"select count(*) from Product p
                        left join Category c on c.Id = p.CategoryId
                        left join Category c2 on c2.Id = c.ParentId
                        left join OrgDepmt od1 on od1.Id = p.UseDepmtId
                        left join OrgDepmt od2 on od2.Id = p.MgrDepmtId
                        left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by p.Status,p.Sort) as RowNumber,
			          p.AppCode '1',p.UserId '2',p.DepmtId '3',p.Id '4',p.CategoryId '5',p.Coded '6',p.Named '7',p.Barcode '8',p.SpecModel '9',p.Qty '10',p.Price '11',p.Amount '12',p.MeterUnit '13',p.PieceQty '14',
                      p.Pattr '15',p.SourceFrom '16',p.Supplier '17',p.BuyDate '18',p.EnableDate '19',p.UseDateLimit '20',p.UseDepmtId '21',p.UsePersonName '22',p.MgrDepmtId '23',p.StoragePlaceId '24',p.Remark '25',p.Status '26',p.Sort '27',p.RecordDate '28',p.LastUpdatedDate '29'
					  ,c.Coded '30',c.Named '31',c2.Coded '32',c2.Named '33',od1.Coded '34' ,od1.Named '35',od2.Coded '36' ,od2.Named '37',sp.Coded '38' ,sp.Named '39' 
                      ,u.UserName '40'
                      from Product p 
                      left join Category c on c.Id = p.CategoryId
                      left join Category c2 on c2.Id = c.ParentId
                      left join OrgDepmt od1 on od1.Id = p.UseDepmtId
                      left join OrgDepmt od2 on od2.Id = p.MgrDepmtId
                      left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      left join {0}aspnet_Users u on u.UserId = p.UserId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.CategoryId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Barcode = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SpecModel = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Qty = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Price = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.Amount = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                        model.MeterUnit = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.PieceQty = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                        model.Pattr = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.SourceFrom = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.Supplier = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.BuyDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);
                        model.EnableDate = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDateLimit = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.UseDepmtId = reader.IsDBNull(21) ? Guid.Empty : reader.GetGuid(21);
                        model.UsePersonName = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        model.MgrDepmtId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.StoragePlaceId = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
                        model.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        model.Status = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.Sort = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                        model.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        model.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                        model.CategoryCode = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                        model.CategoryName = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
                        model.CategoryParentCode = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
                        model.CategoryParentName = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
                        model.UseOrgCode = reader.IsDBNull(34) ? string.Empty : reader.GetString(34);
                        model.UseOrgName = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
                        model.MgrOrgCode = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
                        model.MgrOrgName = reader.IsDBNull(37) ? string.Empty : reader.GetString(37);
                        model.StoragePlaceCode = reader.IsDBNull(38) ? string.Empty : reader.GetString(38);
                        model.StoragePlaceName = reader.IsDBNull(39) ? string.Empty : reader.GetString(39);
                        model.SBuyDate = model.BuyDate.ToString("yyyy-MM-dd").Replace("1754-01-01",string.Empty);
                        model.StatusName = EnumHelper.GetName(typeof(EnumProductStatus), model.Status);
                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd");
                        model.UserName = reader.IsDBNull(40) ? string.Empty : reader.GetString(40);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            sb.AppendFormat(@"select row_number() over(order by p.Status,p.Sort) as RowNumber,
			          p.AppCode '1',p.UserId '2',p.DepmtId '3',p.Id '4',p.CategoryId '5',p.Coded '6',p.Named '7',p.Barcode '8',p.SpecModel '9',p.Qty '10',p.Price '11',p.Amount '12',p.MeterUnit '13',p.PieceQty '14',
                      p.Pattr '15',p.SourceFrom '16',p.Supplier '17',p.BuyDate '18',p.EnableDate '19',p.UseDateLimit '20',p.UseDepmtId '21',p.UsePersonName '22',p.MgrDepmtId '23',p.StoragePlaceId '24',p.Remark '25',p.Status '26',p.Sort '27',p.RecordDate '28',p.LastUpdatedDate '29'
					  ,c.Coded '30',c.Named '31',c2.Coded '32',c2.Named '33',od1.Coded '34' ,od1.Named '35',od2.Coded '36' ,od2.Named '37',sp.Coded '38' ,sp.Named '39' 
                      ,u.UserName '40'
                      from Product p 
                      left join Category c on c.Id = p.CategoryId
                      left join Category c2 on c2.Id = c.ParentId
                      left join OrgDepmt od1 on od1.Id = p.UseDepmtId
                      left join OrgDepmt od2 on od2.Id = p.MgrDepmtId
                      left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      left join {0}aspnet_Users u on u.UserId = p.UserId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            var list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.CategoryId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Barcode = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SpecModel = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Qty = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Price = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.Amount = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                        model.MeterUnit = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.PieceQty = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                        model.Pattr = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.SourceFrom = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.Supplier = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.BuyDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);
                        model.EnableDate = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDateLimit = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.UseDepmtId = reader.IsDBNull(21) ? Guid.Empty : reader.GetGuid(21);
                        model.UsePersonName = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        model.MgrDepmtId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.StoragePlaceId = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
                        model.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        model.Status = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.Sort = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                        model.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        model.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                        model.CategoryCode = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                        model.CategoryName = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
                        model.CategoryParentCode = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
                        model.CategoryParentName = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
                        model.UseOrgCode = reader.IsDBNull(34) ? string.Empty : reader.GetString(34);
                        model.UseOrgName = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
                        model.MgrOrgCode = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
                        model.MgrOrgName = reader.IsDBNull(37) ? string.Empty : reader.GetString(37);
                        model.StoragePlaceCode = reader.IsDBNull(38) ? string.Empty : reader.GetString(38);
                        model.StoragePlaceName = reader.IsDBNull(39) ? string.Empty : reader.GetString(39);
                        model.SBuyDate = model.BuyDate.ToString("yyyy-MM-dd").Replace("1754-01-01", string.Empty);
                        model.StatusName = EnumHelper.GetName(typeof(EnumProductStatus), model.Status);
                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd");
                        model.UserName = reader.IsDBNull(40) ? string.Empty : reader.GetString(40);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(100);
            sb.Append(@"select p.Coded '资产编码',p.Named '资产名称',p.SpecModel '规格型号',p.Qty '数量',p.Price '单价',p.Amount '金额',p.MeterUnit '计量单位',p.Pattr '资产属性',p.SourceFrom '资产来源',p.Supplier '供应商',CONVERT(varchar(100), p.BuyDate, 23) '购入日期',p.EnableDate '启用日期',p.UseDateLimit '使用期限',p.UsePersonName '使用人'
                        ,c.Coded '资产分类编码',c.Named '资产分类',od1.Coded '使用部门编码' ,od1.Named '使用部门',od2.Coded '实物管理部门编码' ,od2.Named '实物管理部门',sp.Coded '存放地点编码' ,sp.Named '存放地点'
                        from Product p 
                        left join Category c on c.Id = p.CategoryId
                        left join OrgDepmt od1 on od1.Id = p.UseDepmtId
                        left join OrgDepmt od2 on od2.Id = p.MgrDepmtId
                        left join StoragePlace sp on sp.Id = p.StoragePlaceId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by p.LastUpdatedDate ");

            return SqlHelper.ExecuteDataset(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        public ProductInfo GetModel(string barcode)
        {
            ProductInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 AppCode,UserId,DepmtId,Id,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate 
			            from Product
						where Barcode = @Barcode ");
            var parm = new SqlParameter("@Barcode", SqlDbType.VarChar, 36);
            parm.Value = barcode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.CategoryId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Barcode = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.SpecModel = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Qty = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Price = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                        model.Amount = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.MeterUnit = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.PieceQty = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                        model.Pattr = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.SourceFrom = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.Supplier = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.BuyDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.EnableDate = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.UseDateLimit = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDepmtId = reader.IsDBNull(20) ? Guid.Empty : reader.GetGuid(20);
                        model.UsePersonName = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                        model.MgrDepmtId = reader.IsDBNull(22) ? Guid.Empty : reader.GetGuid(22);
                        model.StoragePlaceId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.Remark = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                        model.Status = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                        model.Sort = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.IsDisable = reader.IsDBNull(27) ? false : reader.GetBoolean(27);
                        model.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        model.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                    }
                }
            }

            return model;
        }

        public bool IsExist(string code, string name)
        {
            if (string.IsNullOrEmpty(code) && string.IsNullOrEmpty(name)) return false;
            var sb = new StringBuilder(100);
            sb.Append(@"select 1 from Product where 1=1 ");
            if (!string.IsNullOrEmpty(code)) sb.Append("and Coded = @Coded ");
            else if(!string.IsNullOrEmpty(name)) sb.Append("and Named = @Coded ");
            var parm = new SqlParameter("@Coded", !string.IsNullOrEmpty(code)?code:name);

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parm);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
