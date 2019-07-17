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

        int Insert(StoragePlaceInfo model);

        int InsertByOutput(StoragePlaceInfo model);

        int Update(StoragePlaceInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        StoragePlaceInfo GetModel(Guid id);

        IList<StoragePlaceInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StoragePlaceInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StoragePlaceInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StoragePlaceInfo> GetList();

        #endregion
    }
}
