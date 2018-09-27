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
    public partial class Staff
    {
        private static readonly IStaff dal = DataAccess.CreateStaff();

        #region Staff Member

        public int Insert(StaffInfo model)
        {
            return dal.Insert(model);
        }

		public int Update(StaffInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid userId)
        {
            return dal.Delete(userId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public StaffInfo GetModel(Guid userId)
        {
            return dal.GetModel(userId);
        }

        public IList<StaffInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<StaffInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<StaffInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<StaffInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
