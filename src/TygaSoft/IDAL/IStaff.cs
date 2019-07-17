using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStaff
    {
        #region IStaff Member

        Guid GetOrgId(Guid userid);

        StaffInfo GetStaffOrgInfo(Guid userid);

        IList<StaffInfo> GetListByOrg(int pageIndex, int pageSize, out int totalRecords, object orgId, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StaffInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        int DeleteStaff(Guid userid);

        void InsertOrgStaff(StaffInfo model);

        #endregion
    }
}
