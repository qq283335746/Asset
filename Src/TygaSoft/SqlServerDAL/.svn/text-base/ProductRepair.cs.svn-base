using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class ProductRepair
    {
        #region IProduct Member

        public IList<ProductRepairExtendInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            sb.Append(@"select count(*) from ProductRepair pr
                        left join Product p on p.Id = pr.ProductId
                        left join Category c on c.Id = p.CategoryId
                        left join Category c2 on c2.Id = c.ParentId
                        left join OrgDepmt od1 on od1.Id = p.UseDepmtId
                        left join OrgDepmt od2 on od2.Id = p.MgrDepmtId
                        left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ProductRepairExtendInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by pr.RecordDate desc) as RowNumber,
			          p.AppCode '1',p.UserId '2',p.DepmtId '3',p.Id '4',p.CategoryId '5',p.Coded '6',p.Named '7',p.Barcode '8',p.SpecModel '9',p.Qty '10',p.Price '11',p.Amount '12',p.MeterUnit '13',p.PieceQty '14',
                      p.Pattr '15',p.SourceFrom '16',p.Supplier '17',p.BuyDate '18',p.EnableDate '19',p.UseDateLimit '20',p.UseDepmtId '21',p.UsePersonName '22',p.MgrDepmtId '23',p.StoragePlaceId '24',p.Remark '25',p.Status '26',p.Sort '27',p.RecordDate '28',p.LastUpdatedDate '29'
					  ,c.Coded '30',c.Named '31',c2.Coded '32',c2.Named '33',od1.Coded '34' ,od1.Named '35',od2.Coded '36' ,od2.Named '37',sp.Coded '38' ,sp.Named '39' 
                      ,u.UserName '40',od3.Named '41',pr.Id '42',pr.ProductId '43',pr.RecordDate '44',pr.LastUpdatedDate '45'
                      from ProductRepair pr
                      left join Product p on p.Id = pr.ProductId
                      left join Category c on c.Id = p.CategoryId
                      left join Category c2 on c2.Id = c.ParentId
                      left join OrgDepmt od1 on od1.Id = p.UseDepmtId
                      left join OrgDepmt od2 on od2.Id = p.MgrDepmtId
                      left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      left join {0}aspnet_Users u on u.UserId = pr.UserId
                      left join OrgDepmt od3 on od3.Id = pr.OrgId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<ProductRepairExtendInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var productInfo = new ProductInfo();
                        productInfo.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        productInfo.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        productInfo.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        productInfo.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        productInfo.CategoryId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        productInfo.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        productInfo.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        productInfo.Barcode = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        productInfo.SpecModel = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        productInfo.Qty = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        productInfo.Price = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        productInfo.Amount = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                        productInfo.MeterUnit = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        productInfo.PieceQty = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                        productInfo.Pattr = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        productInfo.SourceFrom = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        productInfo.Supplier = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        productInfo.BuyDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);
                        productInfo.EnableDate = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        productInfo.UseDateLimit = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        productInfo.UseDepmtId = reader.IsDBNull(21) ? Guid.Empty : reader.GetGuid(21);
                        productInfo.UsePersonName = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        productInfo.MgrDepmtId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        productInfo.StoragePlaceId = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
                        productInfo.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        productInfo.Status = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        productInfo.Sort = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                        productInfo.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        productInfo.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                        productInfo.CategoryCode = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                        productInfo.CategoryName = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
                        productInfo.CategoryParentCode = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
                        productInfo.CategoryParentName = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
                        productInfo.UseOrgCode = reader.IsDBNull(34) ? string.Empty : reader.GetString(34);
                        productInfo.UseOrgName = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
                        productInfo.MgrOrgCode = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
                        productInfo.MgrOrgName = reader.IsDBNull(37) ? string.Empty : reader.GetString(37);
                        productInfo.StoragePlaceCode = reader.IsDBNull(38) ? string.Empty : reader.GetString(38);
                        productInfo.StoragePlaceName = reader.IsDBNull(39) ? string.Empty : reader.GetString(39);
                        productInfo.SBuyDate = productInfo.BuyDate.ToString("yyyy-MM-dd").Replace("1754-01-01", string.Empty);
                        productInfo.StatusName = EnumHelper.GetName(typeof(EnumProductStatus), productInfo.Status);
                        productInfo.SRecordDate = productInfo.RecordDate.ToString("yyyy-MM-dd");

                        var productRepairInfo = new ProductRepairInfo();
                        productRepairInfo.UserName = reader.IsDBNull(40) ? string.Empty : reader.GetString(40);
                        productRepairInfo.OrgName = reader.IsDBNull(41) ? string.Empty : reader.GetString(41);
                        productRepairInfo.Id = reader.IsDBNull(42) ? Guid.Empty : reader.GetGuid(42);
                        productRepairInfo.ProductId = reader.IsDBNull(43) ? Guid.Empty : reader.GetGuid(43);
                        productRepairInfo.RecordDate = reader.IsDBNull(44) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(44);
                        productRepairInfo.LastUpdatedDate = reader.IsDBNull(45) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(45);

                        list.Add(new ProductRepairExtendInfo { ProductInfo = productInfo, ProductRepairInfo = productRepairInfo });
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
