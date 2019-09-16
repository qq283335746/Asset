using Newtonsoft.Json;
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
            Guid applicationId = _db.Applications.FindOne(m => m.Name.Equals(appName, StringComparison.OrdinalIgnoreCase)).Id;
            IEnumerable<SiteMenusInfo> menus = _db.SiteMenus.Find(m => m.ApplicationId.Equals(applicationId)).OrderBy(m => m.Sort);

            if (isAdministrators)
            {
                foreach (SiteMenusInfo item in menus)
                {
                    item.IsView = true;
                    item.IsAdd = true;
                    item.IsEdit = true;
                    item.IsDelete = true;
                }

                return menus.ToList();
            }
            else
            {
                List<SiteMenusInfo> menuAccessDatas = new List<SiteMenusInfo>();

                IEnumerable<SiteMenusAccessInfo> menusAccessItems = _db.SiteMenusAccess.Find(m => m.ApplicationId.Equals(applicationId) && accessIds.Select(a => Guid.Parse(a)).Contains(m.AccessId));

                foreach (SiteMenusInfo model in menus)
                {
                    #region 权限控制

                    if (menusAccessItems != null && menusAccessItems.Any())
                    {
                        List<SiteMenusAccessItemInfo> maitems = null;

                        var qrmaList = menusAccessItems.Where(m => m.AccessType == "Roles");
                        if (qrmaList != null && qrmaList.Count() > 0)
                        {
                            foreach (var item in qrmaList)
                            {
                                maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(item.OperationAccess);
                                var maitemInfo = maitems.FirstOrDefault(m => Guid.Parse(m.MenuId.ToString()).Equals(model.Id));
                                model.IsView = maitemInfo == null ? false : maitemInfo.IsView;
                                model.IsAdd = maitemInfo == null ? false : maitemInfo.IsAdd;
                                model.IsEdit = maitemInfo == null ? false : maitemInfo.IsEdit;
                                model.IsDelete = maitemInfo == null ? false : maitemInfo.IsDelete;
                            }
                        }

                        var qumaInfo = menusAccessItems.FirstOrDefault(m => m.AccessType == "Users");
                        if (qumaInfo != null)
                        {
                            maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(qumaInfo.OperationAccess);
                            var maitemInfo = maitems.FirstOrDefault(m => Guid.Parse(m.MenuId.ToString()).Equals(model.Id));
                            if (maitemInfo != null)
                            {
                                if (maitemInfo.IsView) model.IsView = false;
                                if (maitemInfo.IsAdd) model.IsAdd = false;
                                if (maitemInfo.IsEdit) model.IsEdit = false;
                                if (maitemInfo.IsDelete) model.IsDelete = false;
                            }
                        }
                    }

                    if (model.IsView) menuAccessDatas.Add(model);

                    #endregion
                }

                return menuAccessDatas;
            }
        }

        public List<SiteMenusInfo> GetMenusAccess(Guid appId, Guid accessId, bool isAdministrators)
        {
            var list = new List<SiteMenusInfo>();
            var maInfo = _db.SiteMenusAccess.FindOne(m=>m.ApplicationId.Equals(appId) && m.AccessId.Equals(accessId));
            List<SiteMenusAccessItemInfo> maitems = null;
            if (maInfo != null) maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(maInfo.OperationAccess);

            var menus = _db.SiteMenus.Find(m => m.ApplicationId.Equals(appId));

            foreach(var model in menus)
            {
                if (isAdministrators)
                {
                    model.IsView = true;
                    model.IsAdd = true;
                    model.IsEdit = true;
                    model.IsDelete = true;
                }
                else
                {
                    if (maitems != null)
                    {
                        var maitemInfo = maitems.FirstOrDefault(m => Guid.Parse(m.MenuId.ToString()).Equals(model.Id));
                        model.IsView = maitemInfo == null ? false : maitemInfo.IsView;
                        model.IsAdd = maitemInfo == null ? false : maitemInfo.IsAdd;
                        model.IsEdit = maitemInfo == null ? false : maitemInfo.IsEdit;
                        model.IsDelete = maitemInfo == null ? false : maitemInfo.IsDelete;
                    }
                }

                list.Add(model);
            }

            return list;
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
