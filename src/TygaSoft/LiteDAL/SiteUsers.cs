using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class SiteUsers: ISiteUsers
    {
        private LiteDbContext _db;

        public SiteUsers()
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

        public IList<SiteUsersInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteUsersInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteUsersInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteUsersInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<SiteUsersInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public SiteUsersInfo GetModel(string username)
        {
            throw new NotImplementedException();
        }

        public SiteUsersInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public SiteUsersInfo GetModelByJoin(string username, object Id)
        {
            throw new NotImplementedException();
        }

        public int Insert(SiteUsersInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(SiteUsersInfo model)
        {
            throw new NotImplementedException();
        }

        public bool IsExistUserName(string username, Guid Id)
        {
            throw new NotImplementedException();
        }

        public int Update(SiteUsersInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
