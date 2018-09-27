using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IPandian
    {
        #region IPandian Member

        int UpdateIsDown(object Id);

        int[] GetTotal();

        IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        PandianInfo GetModelByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        bool IsExistChildren(Guid id);

        #endregion
    }
}
