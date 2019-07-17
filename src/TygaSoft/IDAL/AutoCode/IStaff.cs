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

        int Insert(StaffInfo model);

        int Update(StaffInfo model);

        int Delete(Guid userId);

        bool DeleteBatch(IList<object> list);

        StaffInfo GetModel(Guid userId);

        IList<StaffInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StaffInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StaffInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StaffInfo> GetList();

        #endregion
    }
}
