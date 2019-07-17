using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteRoles
    {
        #region ISiteRoles Member

        int Insert(SiteRolesInfo model);

        int InsertByOutput(SiteRolesInfo model);

        int Update(SiteRolesInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        SiteRolesInfo GetModel(Guid id);

        IList<SiteRolesInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteRolesInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteRolesInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteRolesInfo> GetList();

        #endregion
    }
}
