using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class ContentType : IContentType
    {
        private LiteDbContext _db;

        public ContentType()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            _db.ContentTypes.Delete(id);

            return 1;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach(Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public IList<ContentTypeInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ContentTypeInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ContentTypeInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ContentTypeInfo> GetList()
        {
            return _db.ContentTypes.FindAll().ToList();
        }

        public ContentTypeInfo GetModel(Guid id)
        {
            return _db.ContentTypes.FindById(id);
        }

        public int Insert(ContentTypeInfo model)
        {
            model.Id = Guid.NewGuid();

            _db.ContentTypes.Insert(model);

            return 1;
        }

        public int InsertByOutput(ContentTypeInfo model)
        {
            _db.ContentTypes.Insert(model);

            return 1;
        }

        public bool IsExistChild(Guid Id)
        {
            return _db.ContentTypes.Exists(m => m.ParentId.Equals(Id));
        }

        public bool IsExistCode(string code, Guid Id)
        {
            if (Id.Equals(Guid.Empty))
            {
                return _db.ContentTypes.Exists(m => m.Coded.Equals(code));
            }
            else
            {
                return _db.ContentTypes.Exists(m => m.Coded.Equals(code) && !m.Id.Equals(Id));
            }
        }

        public int Update(ContentTypeInfo model)
        {
            _db.ContentTypes.Update(model);

            return 1;
        }
    }
}
