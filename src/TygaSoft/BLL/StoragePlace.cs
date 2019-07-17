using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class StoragePlace
    {
        #region StoragePlace Member

        public IList<StoragePlaceInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms) 
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public DataSet GetExportData(string sqlWhere, params SqlParameter[] cmdParms) 
        {
            return dal.GetExportData(sqlWhere, cmdParms);
        }

        public string GetRndCode(int n) 
        {
            var rnd = new Random();
            while (true) 
            {
                var s = (rnd.NextDouble() * int.MaxValue).ToString().Substring(0,n);
                if (!IsExistCode(s, Guid.Empty)) return s;
            }
        }

        public bool IsExistCode(string code, Guid Id) 
        {
            return dal.IsExistCode(code, Id);
        }

        public StoragePlaceInfo GetModel(string code, string name)
        {
            return dal.GetModel(code, name);
        }

        #endregion
    }
}
