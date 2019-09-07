using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class OrgDepmt : IOrgDepmt
    {
        private LiteDbContext _db;

        public OrgDepmt()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.OrgDepmts.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<OrgDepmtInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<OrgDepmtInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<OrgDepmtInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<OrgDepmtInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public OrgDepmtInfo GetModel(string code, string name)
        {
            throw new NotImplementedException();
        }

        public OrgDepmtInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(OrgDepmtInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(OrgDepmtInfo model)
        {
            throw new NotImplementedException();
        }

        public bool IsExistChild(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool IsExistCode(string code, Guid Id)
        {
            throw new NotImplementedException();
        }

        public int Update(OrgDepmtInfo model)
        {
            _db.OrgDepmts.Update(model);

            return 1;
        }
    }
}
