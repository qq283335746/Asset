using LiteDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class Product : IProduct
    {
        private LiteDbContext _db;

        public Product()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.Products.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach(Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            DataSet ds = new DataSet("products");
            DataTable dt = new DataTable("products");
            dt.Columns.Add("资产编码", typeof(string));
            dt.Columns.Add("资产名称", typeof(string));
            dt.Columns.Add("规格型号", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("单价", typeof(string));
            dt.Columns.Add("金额", typeof(string));
            dt.Columns.Add("计量单位", typeof(string));
            dt.Columns.Add("资产属性", typeof(string));
            dt.Columns.Add("资产来源", typeof(string));
            dt.Columns.Add("供应商", typeof(string));
            dt.Columns.Add("购入日期", typeof(string));
            dt.Columns.Add("启用日期", typeof(string));
            dt.Columns.Add("使用期限", typeof(string));
            dt.Columns.Add("使用人", typeof(string));
            dt.Columns.Add("资产分类编码", typeof(string));
            dt.Columns.Add("资产分类", typeof(string));
            dt.Columns.Add("使用部门编码", typeof(string));
            dt.Columns.Add("使用部门", typeof(string));
            dt.Columns.Add("实物管理部门编码", typeof(string));
            dt.Columns.Add("实物管理部门", typeof(string));
            dt.Columns.Add("存放地点编码", typeof(string));
            dt.Columns.Add("存放地点", typeof(string));

            Query query = null;
            LiteQueryModel queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);
            if (!string.IsNullOrEmpty(queryModel.UserRule))
            {
                query = Query.Where("DepmtId", m => queryModel.UserRule.Contains(m));
            }

            IEnumerable<ProductInfo> datas = null;
            if(query == null)
            {
                datas = _db.Products.FindAll();
            }
            else
            {
                datas = _db.Products.Find(query);
            }

            if(datas != null)
            {
                foreach(ProductInfo p in datas)
                {
                    AppendFullInfo(p);

                    DataRow dr = dt.NewRow();
                    dr["资产编码"] = p.Coded;
                    dr["资产名称"] = p.Named;
                    dr["规格型号"] = p.SpecModel;
                    dr["数量"] = p.Qty;
                    dr["单价"] = p.Price;
                    dr["金额"] = p.Amount;
                    dr["计量单位"] = p.MeterUnit;
                    dr["资产属性"] = p.Pattr;
                    dr["资产来源"] = p.SourceFrom;
                    dr["供应商"] = p.Supplier;
                    dr["购入日期"] = p.BuyDate.ToString("yyyy-MM-dd");
                    dr["启用日期"] = p.EnableDate;
                    dr["使用期限"] = p.UseDateLimit;
                    dr["使用人"] = p.UsePersonName;
                    dr["资产分类编码"] = p.CategoryCode;
                    dr["资产分类"] = p.CategoryName;
                    dr["使用部门编码"] = p.UseOrgCode;
                    dr["使用部门"] = p.UseOrgName;
                    dr["实物管理部门编码"] = p.MgrOrgCode;
                    dr["实物管理部门"] = p.MgrOrgName;
                    dr["存放地点编码"] = p.StoragePlaceCode;
                    dr["存放地点"] = p.StoragePlaceName;

                    dt.Rows.Add(dr);
                }
            }

            ds.Tables.Add(dt);

            return ds;
        }

        public IList<ProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            LiteQueryModel queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            Query query = null;
            if (!queryModel.MgrDepmtId.Equals(Guid.Empty))
            {
                query = Query.And(Query.EQ("MgrDepmtId", queryModel.MgrDepmtId));
            }
            if (!queryModel.CategoryId.Equals(Guid.Empty))
            {
                query = Query.And(Query.EQ("CategoryId", queryModel.CategoryId));
            }
            if (!queryModel.UseDepmtId.Equals(Guid.Empty))
            {
                query = Query.And(Query.EQ("UseDepmtId", queryModel.CategoryId));
            }
            if (!queryModel.StoragePlaceId.Equals(Guid.Empty))
            {
                query = Query.And(Query.EQ("StoragePlaceId", queryModel.CategoryId));
            }
            if (queryModel.StartDate != DateTime.MinValue && queryModel.EndDate != DateTime.MinValue)
            {
                query = Query.And(Query.Between("BuyDate", queryModel.StartDate, queryModel.EndDate));
            }
            else
            {
                if (queryModel.StartDate != DateTime.MinValue)
                {
                    query = Query.And(Query.GTE("BuyDate", queryModel.StartDate));
                }
                if (queryModel.EndDate != DateTime.MinValue)
                {
                    query = Query.And(Query.LT("BuyDate", queryModel.EndDate));
                }
            }

            if(!string.IsNullOrEmpty(queryModel.UserRule))
            {
                query = Query.And(Query.Where("MgrDepmtId", m => queryModel.UserRule.Contains(m)));
            }

            if(query != null)
            {
                return _db.Products.Find(query).ToList();
            }
            else
            {
                return _db.Products.FindAll().ToList();
            }
        }

        public IList<ProductInfo> GetList()
        {
            return _db.Products.FindAll().ToList();
        }

        public IList<ProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            LiteQueryModel queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            Query query = null;
            if (!queryModel.MgrDepmtId.Equals(Guid.Empty))
            {
                query = Query.And(Query.EQ("MgrDepmtId", queryModel.MgrDepmtId));
            }
            if (!string.IsNullOrEmpty(queryModel.UserRule))
            {
                query = Query.And(Query.Where("MgrDepmtId", m => queryModel.UserRule.Contains(m)));
            }
            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                query = Query.And(Query.Or(Query.Contains("Named", queryModel.Keyword), Query.Contains("Coded", queryModel.Keyword)));
            }

            IEnumerable<ProductInfo> datas = null;

            if (query != null)
            {
                datas = _db.Products.Find(query);
            }
            else
            {
                datas = _db.Products.FindAll();
            }

            totalRecords = datas.Count();
            IEnumerable<ProductInfo> pageDatas = datas.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            foreach(ProductInfo p in pageDatas)
            {
                AppendFullInfo(p);
            }
            return pageDatas.ToList();
        }

        public IList<ProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public ProductInfo GetModel(Guid id)
        {
            return _db.Products.FindById(id);
        }

        public ProductInfo GetModel(string barcode)
        {
            return _db.Products.FindOne(m => m.Barcode.Equals(barcode));
        }

        public string GetRndBarcode()
        {
            while (true)
            {
                Thread.Sleep(1);
                var rnd = new Random();
                var sBarcode = (rnd.NextDouble() * int.MaxValue).ToString();

                if (!_db.Products.Exists(m => m.Barcode.Equals(sBarcode)))
                {
                    return sBarcode;
                }
            }
        }

        public string GetRndCode(string prefix, int n)
        {
            var len = prefix.Length;
            var max = n - len;
            while (true)
            {
                Thread.Sleep(1);
                var rnd = new Random();
                var sb = new StringBuilder(10);
                for (var i = 0; i < max; i++)
                {
                    sb.Append(rnd.Next(10));
                }
                var sCode = string.Format("{0}{1}", prefix, sb.ToString().PadLeft(max, '0'));

                if (!_db.Products.Exists(m => m.Barcode.Equals(sCode)))
                {
                    return sCode;
                }
            }
        }

        public int Insert(ProductInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.Products.Insert(model);

            return 1;
        }

        public int InsertByOutput(ProductInfo model)
        {
            _db.Products.Insert(model);

            return 1;
        }

        public bool IsExist(string code, string name)
        {
            return _db.Products.Exists(m => !string.IsNullOrEmpty(code) ? m.Coded.Equals(code) : m.Named.Equals(name));
        }

        public int Update(ProductInfo model)
        {
            _db.Products.Update(model);

            return 1;
        }

        public void AppendFullInfo(ProductInfo p)
        {
            CategoryInfo categoryInfo = _db.Categories.FindById(p.CategoryId);
            CategoryInfo parentCategoryInfo = _db.Categories.FindById(categoryInfo.ParentId);
            OrgDepmtInfo useOrgDepmtInfo = _db.OrgDepmts.FindById(p.UseDepmtId);
            OrgDepmtInfo mgrOrgDepmtInfo = _db.OrgDepmts.FindById(p.MgrDepmtId);
            StoragePlaceInfo storagePlaceInfo = _db.StoragePlaces.FindById(p.StoragePlaceId);
            p.CategoryCode = categoryInfo.Coded;
            p.CategoryParentCode = parentCategoryInfo.Coded;
            p.CategoryName = categoryInfo.Named;
            p.CategoryParentName = parentCategoryInfo.Named;
            p.UseOrgCode = useOrgDepmtInfo.Coded;
            p.UseOrgName = useOrgDepmtInfo.Named;
            p.MgrOrgCode = mgrOrgDepmtInfo.Coded;
            p.MgrOrgName = mgrOrgDepmtInfo.Named;
            p.StoragePlaceCode = storagePlaceInfo.Coded;
            p.StoragePlaceName = storagePlaceInfo.Named;
        }
    }
}
