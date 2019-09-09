using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class Pandian : IPandian
    {
        private LiteDbContext _db;

        public Pandian()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.Pandians.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public PandianInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public PandianInfo GetModelByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public int[] GetTotal()
        {
            throw new NotImplementedException();
        }

        public int Insert(PandianInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(PandianInfo model)
        {
            throw new NotImplementedException();
        }

        public bool IsExistChildren(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Update(PandianInfo model)
        {
            throw new NotImplementedException();
        }

        public int UpdateIsDown(object Id)
        {
            throw new NotImplementedException();
        }
    }
}
