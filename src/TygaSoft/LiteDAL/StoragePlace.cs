using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class StoragePlace : IStoragePlace
    {
        private LiteDbContext _db;

        public StoragePlace()
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

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StoragePlaceInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StoragePlaceInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StoragePlaceInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StoragePlaceInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<StoragePlaceInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public StoragePlaceInfo GetModel(Guid id)
        {
            return _db.StoragePlaces.FindById(id);
        }

        public StoragePlaceInfo GetModel(string code, string name)
        {
            return _db.StoragePlaces.FindOne(m => !string.IsNullOrEmpty(code) ? m.Coded.Equals(code) : m.Named.Equals(name));
        }

        public int Insert(StoragePlaceInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(StoragePlaceInfo model)
        {
            throw new NotImplementedException();
        }

        public bool IsExistCode(string code, Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return _db.StoragePlaces.Exists(m => m.Coded.Equals(code));
            }
            else
            {
                return _db.StoragePlaces.Exists(m => m.Coded.Equals(code) && m.Id != id);
            }
        }

        public int Update(StoragePlaceInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
