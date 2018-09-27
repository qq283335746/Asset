using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ICustomer
    {
        #region ICustomer Member

        DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms);

        IList<CustomerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
