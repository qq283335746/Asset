using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class SiteMenusAccess : ISiteMenusAccess
    {
        private LiteDbContext _db;

        public SiteMenusAccess()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid applicationId, Guid accessId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusAccessInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusAccessInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusAccessInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusAccessInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public SiteMenusAccessInfo GetModel(Guid applicationId, Guid accessId)
        {
            return _db.SiteMenusAccess.FindOne(m => m.ApplicationId.Equals(applicationId) && m.AccessId.Equals(accessId));
        }

        public int Insert(SiteMenusAccessInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.SiteMenusAccess.Insert(model);

            return 1;
        }

        public int Update(SiteMenusAccessInfo model)
        {
            _db.SiteMenusAccess.Update(model);

            return 1;
        }
    }
}
