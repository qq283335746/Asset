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
    public class Customer: ICustomer
    {
        private LiteDbContext _db;

        public Customer()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid id)
        {
            return _db.Customers.Delete(id) ? 1 : 0;
        }

        public bool DeleteBatch(IList<object> list)
        {
            foreach(Guid id in list)
            {
                this.Delete(id);
            }

            return true;
        }

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<CustomerInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<CustomerInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<CustomerInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<CustomerInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<CustomerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public CustomerInfo GetModel(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(CustomerInfo model)
        {
            throw new NotImplementedException();
        }

        public int InsertByOutput(CustomerInfo model)
        {
            throw new NotImplementedException();
        }

        public int Update(CustomerInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
