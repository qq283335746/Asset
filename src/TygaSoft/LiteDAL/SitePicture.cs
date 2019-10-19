using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class SitePicture : ISitePicture
    {
        private LiteDbContext _db;

        public SitePicture()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.SiteMultis.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach (Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public IList<ComboboxInfo> GetCbbList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            IEnumerable<SitePictureInfo> sitePictures = null;

            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                sitePictures = _db.SitePictures.Find(m => m.FunType.Contains(queryModel.Keyword));
            }
            else
            {
                sitePictures = _db.SitePictures.FindAll();
            }
            if (sitePictures == null)
            {
                totalRecords = 0;
                return null;
            }

            totalRecords = sitePictures.Count();

            var datas = sitePictures.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var cbbDatas = new List<ComboboxInfo>();

            foreach (var item in datas)
            {
                cbbDatas.Add(new ComboboxInfo(item.Id.ToString(), string.Format("{0}{1}{2}", "/Asset", item.FileDirectory, item.FileName)));
            }

            return cbbDatas;
        }

        public IList<SitePictureInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SitePictureInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SitePictureInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<SitePictureInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public SitePictureInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public SitePictureInfo GetModel(string url)
        {
            throw new NotImplementedException();
        }

        public int Insert(SitePictureInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(SitePictureInfo model)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(object userId, string fileName, int fileSize)
        {
            throw new NotImplementedException();
        }

        public int Update(SitePictureInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
