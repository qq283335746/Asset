using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class Category: ICategory
    {
        private LiteDbContext _db;

        public Category()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            _db.Categories.Delete(id);

            return 1;
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            //CategoryInfo model = ConvertToModel.ToModel<CategoryInfo>(cmdParms);
            return _db.Categories.FindAll().ToList();
        }

        public IList<CategoryInfo> GetList()
        {
            return _db.Categories.FindAll().ToList();
        }

        public CategoryInfo GetModel(string code, string name)
        {
            return _db.Categories.FindOne(m => !string.IsNullOrEmpty(code) ? m.Coded.Equals(code) : m.Named.Equals(name));
        }

        public CategoryInfo GetModel(Guid id)
        {
            return _db.Categories.FindById(id);
        }

        public int Insert(CategoryInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.Categories.Insert(model);

            return 1;
        }

        public int InsertByOutput(CategoryInfo model)
        {
            _db.Categories.Insert(model);

            return 1;
        }

        public bool IsExistChild(Guid Id)
        {
            bool isExist = _db.Categories.Exists(m => m.ParentId.Equals(Id));
            if (isExist) return isExist;

            return _db.Products.Exists(m => m.CategoryId.Equals(Id));
        }

        public bool IsExistCode(string code, Guid Id)
        {
            if (Id.Equals(Guid.Empty))
            {
                return _db.Categories.FindOne(m => m.Coded.Equals(code)) != null;
            }
            else
            {
                return _db.Categories.FindOne(m => m.Coded.Equals(code) && !m.Id.Equals(Id)) != null;
            }
        }

        public int Update(CategoryInfo model)
        {
            _db.Categories.Update(model);

            return 1;
        }
    }
}
