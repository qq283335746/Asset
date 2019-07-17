using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class Pandian
    {
        #region Pandian Member

        public int UpdateIsDown(object Id)
        {
            return dal.UpdateIsDown(Id);
        }

        public int[] GetTotal()
        {
            return dal.GetTotal();
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public PandianInfo GetModelByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetModelByJoin(sqlWhere, cmdParms);
        }

        public bool IsExistChildren(Guid id)
        {
            return dal.IsExistChildren(id);
        }

        #endregion
    }
}
