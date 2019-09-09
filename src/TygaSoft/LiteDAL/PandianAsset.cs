using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class PandianAsset : IPandianAsset
    {
        private LiteDbContext _db;

        public PandianAsset()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid pandianId, Guid assetId)
        {
            return _db.PandianAssets.Delete(m => m.PandianId.Equals(pandianId) && m.AssetId.Equals(assetId));
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianAssetInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianAssetInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<PandianAssetInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianAssetInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public PandianAssetInfo GetModel(Guid pandianId, Guid assetId)
        {
            return _db.PandianAssets.FindOne(m => m.PandianId.Equals(pandianId) && m.AssetId.Equals(assetId));
        }

        public int[] GetTotal(object pandianId)
        {
            throw new NotImplementedException();
        }

        public int Insert(PandianAssetInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.PandianAssets.Insert(model);

            return 1;
        }

        public bool IsExist(string barcode)
        {
            ProductInfo productInfo = _db.Products.FindOne(m => m.Barcode.Equals(barcode));
            if (productInfo == null) return false;

            return _db.PandianAssets.Exists(m => m.AssetId.Equals(productInfo.Id));
        }

        public int Update(PandianAssetInfo model)
        {
            _db.PandianAssets.Update(model);

            return 1;
        }
    }
}
