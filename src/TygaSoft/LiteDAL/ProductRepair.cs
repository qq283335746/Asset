using LiteDB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class ProductRepair : IProductRepair
    {
        private LiteDbContext _db;

        public ProductRepair()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.ProductRepairs.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach (Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public IList<ProductRepairInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ProductRepairInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ProductRepairInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ProductRepairInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<ProductRepairExtendInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            LiteQueryModel queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            Query query = null;
            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                query = Query.And(Query.Or(Query.Contains("Named", queryModel.Keyword), Query.Contains("Coded", queryModel.Keyword)));
            }

            IEnumerable<ProductRepairInfo> datas = null;
            if(query == null)
            {
                datas = _db.ProductRepairs.FindAll();
            }
            else
            {
                datas = _db.ProductRepairs.Find(query);
            }

            if(datas == null)
            {
                totalRecords = 0;

                return null;
            }

            totalRecords = datas.Count();

            IEnumerable<ProductRepairInfo> pageDatas = datas.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            List<ProductRepairExtendInfo> result = new List<ProductRepairExtendInfo>();
            foreach (ProductRepairInfo pr in pageDatas)
            {
                result.Add(ToProductRepairExtendInfo(pr));
            }

            return result;
        }

        public ProductRepairInfo GetModel(Guid id)
        {
            return _db.ProductRepairs.FindById(id);
        }

        public int Insert(ProductRepairInfo model)
        {
            model.Id = Guid.NewGuid();

            _db.ProductRepairs.Insert(model);

            return 1;
        }

        public int InsertByOutput(ProductRepairInfo model)
        {
            _db.ProductRepairs.Insert(model);

            return 1;
        }

        public int Update(ProductRepairInfo model)
        {
            _db.ProductRepairs.Update(model);

            return 1;
        }

        public ProductRepairExtendInfo ToProductRepairExtendInfo(ProductRepairInfo productRepairInfo)
        {
            ProductInfo productInfo = _db.Products.FindById(productRepairInfo.ProductId);

            return new ProductRepairExtendInfo { ProductInfo = new Product().AppendFullInfo(productInfo), ProductRepairInfo = productRepairInfo };
        }
    }
}
