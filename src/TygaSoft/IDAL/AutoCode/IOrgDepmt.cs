using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrgDepmt
    {
        #region IOrgDepmt Member

        int Insert(OrgDepmtInfo model);

        int InsertByOutput(OrgDepmtInfo model);

        int Update(OrgDepmtInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrgDepmtInfo GetModel(Guid id);

        IList<OrgDepmtInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrgDepmtInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrgDepmtInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrgDepmtInfo> GetList();

        #endregion
    }
}
