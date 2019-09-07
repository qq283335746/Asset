using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class Dics: IDics
    {
        private LiteDbContext _db;

        public Dics()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.Dics.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach(Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public IList<DicsInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<DicsInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<DicsInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<DicsInfo> GetList()
        {
            return _db.Dics.FindAll().ToList();
        }

        public DicsInfo GetModel(Guid id)
        {
            return _db.Dics.FindById(id);
        }

        public int Insert(DicsInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.Dics.Insert(model);

            return 1;
        }

        public int InsertByOutput(DicsInfo model)
        {
            _db.Dics.Insert(model);

            return 1;
        }

        public bool IsExistChild(Guid Id)
        {
            return _db.Dics.Exists(m => m.ParentId.Equals(Id));
        }

        public bool IsExistCode(string code, Guid Id)
        {
            if (Id.Equals(Guid.Empty))
            {
                return _db.Dics.Exists(m => m.Coded.Equals(code));
            }
            else
            {
                return _db.Dics.Exists(m => !m.Id.Equals(Id) && m.Coded.Equals(code));
            }
        }

        public int Update(DicsInfo model)
        {
            _db.Dics.Update(model);

            return 1;
        }
    }
}
