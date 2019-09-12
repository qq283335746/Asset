using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class SiteMenus : ISiteMenus
    {
        private LiteDbContext _db;

        public SiteMenus()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.SiteMenus.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            LiteQueryModel queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);
            if (!queryModel.ApplicationId.Equals(Guid.Empty))
            {
                return _db.SiteMenus.Find(m => m.ApplicationId.Equals(queryModel.ApplicationId)).ToList();
            }

            return _db.SiteMenus.FindAll().ToList();
        }

        public IList<SiteMenusInfo> GetList()
        {
            return _db.SiteMenus.FindAll().ToList();
        }

        public IList<SiteMenusInfo> GetListByParentName(string parentName)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMenusInfo> GetMenusAccess(string appName, string[] accessIds, bool isAdministrators)
        {
            throw new NotImplementedException();
        }

        public List<SiteMenusInfo> GetMenusAccess(Guid appId, Guid accessId, bool isAdministrators)
        {
            throw new NotImplementedException();
        }

        public SiteMenusInfo GetModel(Guid id)
        {
            return _db.SiteMenus.FindById(id);
        }

        public int Insert(SiteMenusInfo model)
        {
            model.Id = Guid.NewGuid();

            _db.SiteMenus.Insert(model);

            return 1;
        }

        public int InsertByOutput(SiteMenusInfo model)
        {
            _db.SiteMenus.Insert(model);

            return 1;
        }

        public int Update(SiteMenusInfo model)
        {
            _db.SiteMenus.Update(model);

            return 1;
        }
    }
}
