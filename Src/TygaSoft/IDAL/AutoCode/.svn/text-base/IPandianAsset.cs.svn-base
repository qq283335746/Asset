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

        int Insert(PandianAssetInfo model);

        int Update(PandianAssetInfo model);

        int Delete(Guid pandianId, Guid assetId);

        bool DeleteBatch(IList<object> list);

        PandianAssetInfo GetModel(Guid pandianId, Guid assetId);

        IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianAssetInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianAssetInfo> GetList();

        #endregion
    }
}
