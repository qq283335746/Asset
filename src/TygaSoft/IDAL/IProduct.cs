using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IProduct
    {
        #region IProduct Member

        string GetRndBarcode();

        string GetRndCode(string prefix,int n);

        IList<ProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms);

        ProductInfo GetModel(string barcode);

        bool IsExist(string code, string name);

        #endregion
    }
}
