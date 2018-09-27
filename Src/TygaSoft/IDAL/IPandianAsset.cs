using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IPandianAsset
    {
        #region IPandianAsset Member

        int[] GetTotal(object pandianId);

        bool IsExist(string barcode);

        IList<PandianAssetInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianAssetInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
