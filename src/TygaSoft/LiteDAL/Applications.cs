using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;
using Yibi.LiteMembershipProvider;

namespace Yibi.LiteDAL
{
    public class Applications: IApplications
    {
        private LiteDbContext _db;

        public Applications()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public Guid GetAspnetAppId(string appName)
        {
            Yibi.LiteMembershipProvider.Entities.ApplicationsInfo appInfo = _db.Applications.FindOne(m => m.Name.Equals(appName, StringComparison.OrdinalIgnoreCase));
            return appInfo == null ? Guid.NewGuid() : appInfo.Id;
        }

        public IList<ApplicationsInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicationsInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicationsInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicationsInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public ApplicationsInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(ApplicationsInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(ApplicationsInfo model)
        {
            throw new NotImplementedException();
        }

        public int Update(ApplicationsInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
