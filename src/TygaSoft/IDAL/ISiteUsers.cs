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

        bool IsExistUserName(string username, Guid Id);

        SiteUsersInfo GetModel(string username);

        SiteUsersInfo GetModelByJoin(string username, object Id);

        IList<SiteUsersInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
