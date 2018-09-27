using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class PandianAsset
    {
        #region IPandianAsset Member

        public int[] GetTotal(object pandianId)
        {
            var cmdText = string.Format(@"select count(1) as Total from PandianAsset where PandianId = @PandianId and Status = {0} 
                union all select count(1) as Total from PandianAsset where PandianId = @PandianId and Status = {1} 
                union all select count(1) as Total from PandianAsset where PandianId = @PandianId and Status = {2} "
                , (int)EnumPandianAssetStatus.已盘点, (int)EnumPandianAssetStatus.盘盈, (int)EnumPandianAssetStatus.未盘点);

            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId.ToString());

            var list = new List<int>(3);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
                    }
                }
            }

            return list.ToArray();
        }

        public bool IsExist(string barcode)
        {
            var cmdText = @"select 1 from Pandian pd 
                            join PandianAsset pda on pda.PandianId = pd.Id
                            join Product p on p.Id = pda.AssetId
                            where p.Barcode = @Barcode
                            ";
            var parm = new SqlParameter("@Barcode", SqlDbType.VarChar, 36);
            parm.Value = barcode;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        public IList<PandianAssetInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(@"select count(1)
                            from PandianAsset pda  
                          left join Pandian pd on pda.PandianId = pd.Id
                          left join Product p on p.Id = pda.AssetId
                          left join Category c on c.Id = p.CategoryId
                          left join {0}aspnet_Users u on u.UserId = pd.UserId
                          left join OrgDepmt orgd1 on orgd1.Id = p.UseDepmtId
                          left join OrgDepmt orgd2 on orgd2.Id = p.MgrDepmtId
                          left join StoragePlace sp on sp.Id = p.StoragePlaceId
                          left join OrgDepmt orgd11 on orgd11.Id = pda.LastUseDepmtId
                          left join OrgDepmt orgd22 on orgd22.Id = pda.LastMgrDepmtId
                          left join StoragePlace sp2 on sp2.Id = pda.LastStoragePlaceId
                     ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianAssetInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by pda.LastUpdatedDate desc) as RowNumber,
                      pd.Id '1', pd.Named '2',pd.TotalQty '3'
                        ,pda.AssetId '4', pda.Status '5',pda.Remark '6', pda.LastUseDepmtId '7',pda.LastMgrDepmtId '8', pda.LastStoragePlaceId '9',pda.LastUsePerson '10'
                        ,p.CategoryId '11',p.Coded '12',p.Named '13',p.SpecModel '14',p.Qty '15',p.Price '16',p.Amount '17',p.MeterUnit '18',p.PieceQty '19',p.Pattr '20',p.SourceFrom '21',p.Supplier '22',p.BuyDate '23',p.EnableDate '24',p.UseDateLimit '25'
                        ,p.UseDepmtId '26',p.UsePersonName '27',p.MgrDepmtId '28',p.StoragePlaceId '29',p.Remark '30',p.Status '31',p.Sort '32',p.RecordDate '33'
                        ,c.Coded '34', c.Named '35',orgd1.Coded '36',orgd1.Named '37',orgd2.Coded '38',orgd2.Named '39',sp.Coded '40',sp.Named '41',orgd11.Coded '42',orgd11.Named '43',orgd22.Coded '44',orgd22.Named '45',sp2.Coded '46',sp2.Named '47'
                        ,u.UserName '48'
                      from PandianAsset pda  
                      left join Pandian pd on pda.PandianId = pd.Id
                      left join Product p on p.Id = pda.AssetId
                      left join Category c on c.Id = p.CategoryId
                      left join {0}aspnet_Users u on u.UserId = pd.UserId
                      left join OrgDepmt orgd1 on orgd1.Id = p.UseDepmtId
                      left join OrgDepmt orgd2 on orgd2.Id = p.MgrDepmtId
                      left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      left join OrgDepmt orgd11 on orgd11.Id = pda.LastUseDepmtId
                      left join OrgDepmt orgd22 on orgd22.Id = pda.LastMgrDepmtId
                      left join StoragePlace sp2 on sp2.Id = pda.LastStoragePlaceId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AssetId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.AssetCode = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.AssetName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.SpecModel = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.Unit = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.LastUseDepmtId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastMgrDepmtId = reader.IsDBNull(8) ? Guid.Empty : reader.GetGuid(8);
                        model.LastStoragePlaceId = reader.IsDBNull(9) ? Guid.Empty : reader.GetGuid(9);
                        model.LastUseDepmtName = reader.IsDBNull(43) ? string.Empty : reader.GetString(43);
                        model.LastMgrDepmtName = reader.IsDBNull(45) ? string.Empty : reader.GetString(45);
                        model.LastStoragePlaceName = reader.IsDBNull(47) ? string.Empty : reader.GetString(47);
                        model.LastUsePerson = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.Status = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.CategoryId = reader.IsDBNull(11) ? Guid.Empty : reader.GetGuid(11);
                        model.UseDepmtId = reader.IsDBNull(26) ? Guid.Empty : reader.GetGuid(26);
                        model.MgrDepmtId = reader.IsDBNull(28) ? Guid.Empty : reader.GetGuid(28);
                        model.StoragePlaceId = reader.IsDBNull(29) ? Guid.Empty : reader.GetGuid(29);
                        model.CategoryCode = reader.IsDBNull(34) ? string.Empty : reader.GetString(34);
                        model.CategoryName = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
                        model.UseDepmtCode = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
                        model.UseDepmtName = reader.IsDBNull(37) ? string.Empty : reader.GetString(37);
                        model.MgrDepmtCode = reader.IsDBNull(38) ? string.Empty : reader.GetString(38);
                        model.MgrDepmtName = reader.IsDBNull(39) ? string.Empty : reader.GetString(39);
                        model.StoragePlaceCode = reader.IsDBNull(40) ? string.Empty : reader.GetString(40);
                        model.StoragePlaceName = reader.IsDBNull(41) ? string.Empty : reader.GetString(41);
                        model.UsePersonName = reader.IsDBNull(27) ? string.Empty : reader.GetString(27);
                        model.Qty = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                        model.UserName = reader.IsDBNull(48) ? string.Empty : reader.GetString(48);
                        model.Remark = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.StatusName = EnumHelper.GetName(typeof(EnumPandianAssetStatus), model.Status);
                        model.Barcode = model.AssetCode;

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder();

            sb.AppendFormat(@"row_number() over(order by pda.LastUpdatedDate desc) as RowNumber, pd.Id '1', pd.Named '2',pd.TotalQty '3'
			          ,pda.AssetId '4', pda.Status '5',pda.Remark '6', pda.LastUseDepmtId '7',pda.LastMgrDepmtId '8', pda.LastStoragePlace '9',pda.LastUsePerson '10'
                      ,p.CategoryId '11',p.Coded '12',p.Named '13',p.SpecModel '14',p.Qty '15',p.Price '16',p.Amount '17',p.MeterUnit '18',p.PieceQty '19',p.Pattr '20',p.SourceFrom '21',p.Supplier '22',p.BuyDate '23'
                      ,p.EnableDate '24',p.UseDateLimit '25',p.UseDepmtId '26',p.UsePersonName '27',p.MgrDepmtId '28',p.StoragePlaceId '29',p.Remark '30',p.Status '31',p.Sort '32',p.RecordDate '33'
					  ,c.Coded '34', c.Named '35',orgd1.Coded '36',orgd1.Named '37',orgd2.Coded '38',orgd2.Named '39',sp.Coded '40',sp.Named '41',orgd11.Coded '42',orgd11.Named '43',orgd22.Coded '44',orgd22.Named '45',sp2.Coded '46',sp2.Named '47'
                      ,u.UserName '48'
                      from PandianAsset pda  
                      left join Pandian pd on pda.PandianId = pd.Id
                      left join Product p on p.Id = pda.AssetId
                      left join Category c on c.Id = p.CategoryId
                      left join {0}aspnet_Users u on u.UserId = pd.UserId
                      left join OrgDepmt orgd1 on orgd1.Id = p.UseDepmtId
                      left join OrgDepmt orgd2 on orgd.Id = p.MgrDepmtId
                      left join StoragePlace sp on sp.Id = p.StoragePlaceId
                      left join OrgDepmt orgd11 on orgd11.Id = pda.LastUseDepmtId
                      left join OrgDepmt orgd22 on orgd22.Id = pda.LastMgrDepmtId
                      left join StoragePlace sp2 on sp2.Id = pda.StoragePlaceId
                      ",GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            var list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new PandianAssetInfo();
                        model.AssetId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.StatusName = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.CategoryCode = reader.IsDBNull(34) ? string.Empty : reader.GetString(34);
                        model.CategoryName = reader.IsDBNull(35) ? string.Empty : reader.GetString(35);
                        model.AssetName = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.SpecModel = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.Unit = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.UseDepmtCode = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
                        model.UseDepmtName = reader.IsDBNull(37) ? string.Empty : reader.GetString(37);
                        model.MgrDepmtCode = reader.IsDBNull(38) ? string.Empty : reader.GetString(38);
                        model.MgrDepmtName = reader.IsDBNull(39) ? string.Empty : reader.GetString(39);
                        model.StoragePlaceCode = reader.IsDBNull(40) ? string.Empty : reader.GetString(40);
                        model.StoragePlaceName = reader.IsDBNull(41) ? string.Empty : reader.GetString(41);
                        model.UsePersonName = reader.IsDBNull(27) ? string.Empty : reader.GetString(27);
                        model.Qty = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                        model.LastUseDepmtName = reader.IsDBNull(42) ? string.Empty : reader.GetString(42);
                        model.LastMgrDepmtName = reader.IsDBNull(43) ? string.Empty : reader.GetString(45);
                        model.LastUsePerson = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.LastUseDepmtName = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.LastStoragePlaceName = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.UserName = reader.IsDBNull(48) ? string.Empty : reader.GetString(48);
                        model.Remark = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public PandianAssetInfo GetModel(Guid pandianId)
        {
            PandianAssetInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 PandianId,AssetId,AppCode,UserId,DepmtId,LastUseDepmtId,LastMgrDepmtId,LastStoragePlaceId,LastUsePerson,Sort,Remark,Status,RecordDate,LastUpdatedDate 
			            from PandianAsset
						where PandianId = @PandianId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = pandianId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PandianAssetInfo();
                        model.PandianId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AssetId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.LastUseDepmtId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.LastMgrDepmtId = reader.IsDBNull(6) ? Guid.Empty : reader.GetGuid(6);
                        model.LastStoragePlaceId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);
                        model.LastUsePerson = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Sort = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Remark = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.Status = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                        model.RecordDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.LastUpdatedDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                    }
                }
            }

            return model;
        }

        #endregion
    }
}
