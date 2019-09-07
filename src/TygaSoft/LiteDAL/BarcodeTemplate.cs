using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class BarcodeTemplate : IBarcodeTemplate
    {
        private LiteDbContext _db;

        public BarcodeTemplate()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            _db.BarcodeTemplates.Delete(id);

            return 1;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach(Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public IList<BarcodeTemplateInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<BarcodeTemplateInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            BarcodeTemplateInfo model = ConvertToModel.ToModel<BarcodeTemplateInfo>(cmdParms);
            IEnumerable<BarcodeTemplateInfo> datas = _db.BarcodeTemplates.FindAll();
            if (!string.IsNullOrEmpty(model.TypeName)) datas = datas.Where(m => m.TypeName.Equals(model.TypeName));

            return datas.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public IList<BarcodeTemplateInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<BarcodeTemplateInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public BarcodeTemplateInfo GetModel(Guid id)
        {
            return _db.BarcodeTemplates.FindOne(m => m.Id.Equals(id));
        }

        public BarcodeTemplateInfo GetModelByDefault()
        {
            return _db.BarcodeTemplates.FindOne(m => m.IsDefault);
        }

        public int Insert(BarcodeTemplateInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(BarcodeTemplateInfo model)
        {
            _db.BarcodeTemplates.Insert(model);
            return 1;
        }

        public int SetDefault(Guid Id, bool isDefault, string typeName)
        {
            BarcodeTemplateInfo oldInfo = _db.BarcodeTemplates.FindById(Id);
            if (oldInfo == null) return 0;

            oldInfo.IsDefault = isDefault;
            oldInfo.LastUpdatedDate = DateTime.Now;

            if (isDefault)
            {
                IEnumerable<BarcodeTemplateInfo> oldInfos = _db.BarcodeTemplates.Find(m => !m.Id.Equals(oldInfo.Id) && m.TypeName.Equals(typeName) && m.IsDefault);
                foreach(BarcodeTemplateInfo item in oldInfos)
                {
                    item.IsDefault = false;
                    _db.BarcodeTemplates.Update(item);
                }
                
            }

            return 1;
        }

        public int Update(BarcodeTemplateInfo model)
        {
            _db.BarcodeTemplates.Update(model);

            return 1;
        }
    }
}
