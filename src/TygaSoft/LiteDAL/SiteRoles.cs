using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class SiteRoles : ISiteRoles
    {
        private LiteDbContext _db;

        public SiteRoles()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public List<SiteRolesInfo> GetAspnetList(string appName, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);
            IEnumerable<SiteRolesInfo> siteRoles = null;

            var appid = new Applications().GetAspnetAppId(appName);
            if (!string.IsNullOrEmpty(queryModel.SqlIn))
            {
                siteRoles = _db.SiteRoles.Find(m => m.ApplicationId.Equals(appid) && queryModel.SqlIn.Contains(m.Named));
            }
            else
            {
                siteRoles = _db.SiteRoles.Find(m => m.ApplicationId.Equals(appid));
            }

            return siteRoles.ToList();
        }

        public SiteRolesInfo GetAspnetModel(string appName, string name)
        {
            var appid = new Applications().GetAspnetAppId(appName);

            return _db.SiteRoles.FindOne(m => m.ApplicationId.Equals(appid) && m.Named.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public int UpdateAspnetRoles(SiteRolesInfo model)
        {
            var oldInfo = _db.SiteRoles.FindById(model.Id);
            if(oldInfo != null)
            {
                oldInfo.Named = model.Named;

                return _db.SiteRoles.Update(oldInfo) ? 1 : 0;
            }

            return 0;
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<SiteRolesInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteRolesInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteRolesInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteRolesInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public SiteRolesInfo GetModel(string name)
        {
            throw new NotImplementedException();
        }

        public SiteRolesInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(SiteRolesInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(SiteRolesInfo model)
        {
            throw new NotImplementedException();
        }

        public int Update(SiteRolesInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
