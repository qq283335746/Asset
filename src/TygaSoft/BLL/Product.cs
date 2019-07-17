using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class Product
    {
        #region Product Member

        public string GetRndBarcode() 
        {
            return dal.GetRndBarcode();
        }

        public string GetRndCode(string prefix,int n) 
        {
            return dal.GetRndCode(prefix,n);
        }

        public IList<ProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms) 
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<ProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms) 
        {
            return dal.GetExportData(sqlWhere, cmdParms);
        }

        public ProductInfo GetModel(string barcode) 
        {
            return dal.GetModel(barcode);
        }

        public bool IsExist(string code, string name) 
        {
            return dal.IsExist(code, name);
        }

        #endregion
    }
}
