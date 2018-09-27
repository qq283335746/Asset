using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IDics
    {
        #region IDics Member

        int Insert(DicsInfo model);

        int InsertByOutput(DicsInfo model);

        int Update(DicsInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        DicsInfo GetModel(Guid id);

        IList<DicsInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<DicsInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<DicsInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<DicsInfo> GetList();

        #endregion
    }
}
