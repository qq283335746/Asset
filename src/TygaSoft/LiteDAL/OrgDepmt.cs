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
            return _db.OrgDepmts.FindAll().ToList();
        }

        public OrgDepmtInfo GetModel(string code, string name)
        {
            if (!string.IsNullOrEmpty(code)) return _db.OrgDepmts.FindOne(m => m.Coded.Equals(code));
            else return _db.OrgDepmts.FindOne(m => m.Named.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public OrgDepmtInfo GetModel(Guid id)
        {
            return _db.OrgDepmts.FindById(id);
        }

        public int Insert(OrgDepmtInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.OrgDepmts.Insert(model);

            return 1;
        }

        public int InsertByOutput(OrgDepmtInfo model)
        {
            _db.OrgDepmts.Insert(model);

            return 1;
        }

        public bool IsExistChild(Guid Id)
        {
            bool isExist = _db.OrgDepmts.Exists(m => m.ParentId.Equals(Id));
            if (isExist) return true;
            return _db.UserInOrg.Exists(m => m.OrgId.Equals(Id));
        }

        public bool IsExistCode(string code, Guid Id)
        {
            return _db.OrgDepmts.Exists(m => Id.Equals(Guid.Empty) ? m.Coded.Equals(code) : m.Coded.Equals(code) && !m.Id.Equals(Id));
        }

        public int Update(OrgDepmtInfo model)
        {
            _db.OrgDepmts.Update(model);

            return 1;
        }
    }
}
