using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class PandianAsset
    {
        #region PandianAsset Member

        public DataTable GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var dt = new DataTable();
            dt.Columns.Add("盘点状态", typeof(string));
            dt.Columns.Add("资产类别编码", typeof(string));
            dt.Columns.Add("资产类别", typeof(string));
            dt.Columns.Add("资产编码", typeof(string));
            dt.Columns.Add("资产名称", typeof(string));
            dt.Columns.Add("规格型号", typeof(string));
            dt.Columns.Add("计量单位", typeof(string));
            dt.Columns.Add("使用部门编码", typeof(string));
            dt.Columns.Add("使用部门", typeof(string));
            dt.Columns.Add("实物管理部门编码", typeof(string));
            dt.Columns.Add("实物管理部门", typeof(string));
            dt.Columns.Add("存放地点编码", typeof(string));
            dt.Columns.Add("存放地点", typeof(string));
            dt.Columns.Add("使用人", typeof(string));
            dt.Columns.Add("账面数量", typeof(string));
            dt.Columns.Add("修改后使用部门", typeof(string));
            dt.Columns.Add("修改后实物管理部门", typeof(string));
            dt.Columns.Add("修改后使用人", typeof(string));
            dt.Columns.Add("修改后存放地点", typeof(string));
            dt.Columns.Add("盘点人", typeof(string));
            dt.Columns.Add("盘点备注", typeof(string));

            var totalRecords = 0;
            var list = GetListByJoin(1, 99999, out totalRecords, sqlWhere, cmdParms);
            foreach (var item in list) 
            {
                var row = dt.NewRow();
                row["盘点状态"] = item.StatusName;
                row["资产类别编码"] = item.CategoryCode;
                row["资产类别"] = item.CategoryName;
                row["资产编码"] = item.AssetCode;
                row["资产名称"] = item.AssetName;
                row["规格型号"] = item.SpecModel;
                row["计量单位"] = item.Unit;
                row["使用部门编码"] = item.UseDepmtCode;
                row["使用部门"] = item.UseDepmtName;
                row["实物管理部门编码"] = item.MgrDepmtCode;
                row["实物管理部门"] = item.MgrDepmtName;
                row["存放地点编码"] = item.StoragePlaceCode;
                row["存放地点"] = item.StoragePlaceName;
                row["使用人"] = item.UsePersonName;
                row["账面数量"] = item.Qty;
                row["修改后使用部门"] = item.LastUseDepmtName;
                row["修改后实物管理部门"] = item.LastMgrDepmtName;
                row["修改后使用人"] = item.LastUsePerson;
                row["修改后存放地点"] = item.LastStoragePlaceName;
                row["盘点人"] = item.UserName;
                row["盘点备注"] = item.Remark;

                dt.Rows.Add(row);
            }

            return dt;
        }

        public int[] GetTotal(object pandianId)
        {
            return dal.GetTotal(pandianId);
        }

        public bool IsExist(string barcode)
        {
            return dal.IsExist(barcode);
        }

        public IList<PandianAssetInfo> GetListByPandianId(object pandianId)
        {
            var sqlWhere = "and PandianId = @PandianId ";
            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId.ToString());

            return dal.GetList(sqlWhere, parm);
        }

        public IList<PandianAssetInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<PandianAssetInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        #endregion
    }
}
