using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteMembers
    {
        #region ISiteMembers Member

        int Insert(SiteMembersInfo model);

        int Update(SiteMembersInfo model);

        int Delete(Guid userId);

        bool DeleteBatch(IList<object> list);

        SiteMembersInfo GetModel(Guid userId);

        IList<SiteMembersInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMembersInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMembersInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMembersInfo> GetList();

        #endregion
    }
}
