using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IApplications
    {
        #region IApplications Member

        int Insert(ApplicationsInfo model);

        int InsertByOutput(ApplicationsInfo model);

        int Update(ApplicationsInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        ApplicationsInfo GetModel(Guid id);

        IList<ApplicationsInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ApplicationsInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ApplicationsInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ApplicationsInfo> GetList();

        #endregion
    }
}
