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
    public partial class SiteMembers
    {
        private static readonly ISiteMembers dal = DataAccess.CreateSiteMembers();

        #region SiteMembers Member

        public int Insert(SiteMembersInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(SiteMembersInfo model)
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

        public SiteMembersInfo GetModel(Guid userId)
        {
            return dal.GetModel(userId);
        }

        public IList<SiteMembersInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<SiteMembersInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<SiteMembersInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<SiteMembersInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
