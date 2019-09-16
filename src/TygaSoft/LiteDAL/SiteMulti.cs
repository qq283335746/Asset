using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class SiteMulti : ISiteMulti
    {
        private LiteDbContext _db;

        public SiteMulti()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.SiteMultis.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach(Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public string GetCode()
        {
            var total = _db.SiteMultis.Count();
            return string.Format("1{0}", (total + 1).ToString().PadLeft(5, '0'));
        }

        public IList<SiteMultiInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMultiInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMultiInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SiteMultiInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<SiteMultiInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            IEnumerable<SiteMultiInfo> siteMultis = null;

            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                siteMultis = _db.SiteMultis.Find(m => m.Coded.Contains(queryModel.Keyword) || m.Named.Contains(queryModel.Keyword));
            }
            else
            {
                siteMultis = _db.SiteMultis.FindAll();
            }
            if(siteMultis == null)
            {
                totalRecords = 0;
                return null;
            }

            totalRecords = siteMultis.Count();

            var datas = siteMultis.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            foreach (var item in datas)
            {
                var sitePictureInfo = _db.SitePictures.FindOne(m => (m.FileDirectory + m.FileName).Equals(item.SiteLogo));
                item.SiteLogoId = sitePictureInfo == null ? Guid.Empty : sitePictureInfo.Id;
            }

            return datas.ToList();
        }

        public SiteMultiInfo GetModel(Guid id)
        {
            return _db.SiteMultis.FindById(id);
        }

        public int Insert(SiteMultiInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.SiteMultis.Insert(model);

            return 1;
        }

        public int InsertByOutput(SiteMultiInfo model)
        {
            _db.SiteMultis.Insert(model);

            return 1;
        }

        public int Update(SiteMultiInfo model)
        {
            _db.SiteMultis.Update(model);

            return 1;
        }
    }
}
