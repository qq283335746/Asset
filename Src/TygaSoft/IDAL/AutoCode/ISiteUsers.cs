using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteUsers
    {
        #region ISiteUsers Member

        int Insert(SiteUsersInfo model);

        int InsertByOutput(SiteUsersInfo model);

        int Update(SiteUsersInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        SiteUsersInfo GetModel(Guid id);

        IList<SiteUsersInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteUsersInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteUsersInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteUsersInfo> GetList();

        #endregion
    }
}
