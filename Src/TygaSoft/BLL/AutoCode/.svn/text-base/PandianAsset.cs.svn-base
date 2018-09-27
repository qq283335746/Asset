using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class PandianAsset
    {
        private static readonly IPandianAsset dal = DataAccess.CreatePandianAsset();

        #region PandianAsset Member

        public int Insert(PandianAssetInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(PandianAssetInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid pandianId, Guid assetId)
        {
            return dal.Delete(pandianId, assetId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public PandianAssetInfo GetModel(Guid pandianId, Guid assetId)
        {
            return dal.GetModel(pandianId, assetId);
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<PandianAssetInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<PandianAssetInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
