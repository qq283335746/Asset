using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.SysUtility;

namespace Yibi.LiteDAL
{
    public class Pandian : IPandian
    {
        private LiteDbContext _db;

        public Pandian()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.Pandians.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<PandianInfo> GetList()
        {
            return _db.Pandians.FindAll().ToList();
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public PandianInfo GetModel(Guid id)
        {
            return _db.Pandians.FindById(id);
        }

        public PandianInfo GetModelByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            PandianInfo pandianInfo = ConvertToModel.ToModel<PandianInfo>(cmdParms);
            if (!pandianInfo.Id.Equals(Guid.Empty))
            {
                return _db.Pandians.FindById(pandianInfo.Id);
            }

            return null;
        }

        public int[] GetTotal()
        {
            List<int> totals = new List<int>();
            totals.Add(_db.Pandians.Count());
            totals.Add(_db.Pandians.Find(m => m.Status.Equals((int)EnumStatus.完成)).Count());
            totals.Add(_db.Pandians.Find(m => !m.Status.Equals((int)EnumStatus.完成)).Count());

            return totals.ToArray();
        }

        public int Insert(PandianInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.Pandians.Insert(model);

            return 1;
        }

        public int InsertByOutput(PandianInfo model)
        {
            _db.Pandians.Insert(model);

            return 1;
        }

        public bool IsExistChildren(Guid id)
        {
            return _db.PandianAssets.Exists(m => m.PandianId.Equals(id));
        }

        public int Update(PandianInfo model)
        {
            _db.Pandians.Update(model);

            return 1;
        }

        public int UpdateIsDown(object Id)
        {
            PandianInfo oldInfo = _db.Pandians.FindById((Guid)Id);
            if (oldInfo == null) return 0;

            oldInfo.IsDown = true;
            oldInfo.Status = (int)EnumPandianStatus.进行中;

            _db.Pandians.Update(oldInfo);

            return 1;
        }
    }
}
