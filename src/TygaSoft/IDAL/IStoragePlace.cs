using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStoragePlace
    {
        #region IStoragePlace Member

        IList<StoragePlaceInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms);

        bool IsExistCode(string code, Guid Id);

        StoragePlaceInfo GetModel(string code, string name);

        #endregion
    }
}
