using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IProductRepair
    {
        #region IProductRepair Member

        int Insert(ProductRepairInfo model);

		int InsertByOutput(ProductRepairInfo model);

        int Update(ProductRepairInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        ProductRepairInfo GetModel(Guid id);

        IList<ProductRepairInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductRepairInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductRepairInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductRepairInfo> GetList();

        #endregion
    }
}
