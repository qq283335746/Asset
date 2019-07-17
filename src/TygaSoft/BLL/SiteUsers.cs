using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;
using TygaSoft.SysUtility;

namespace TygaSoft.BLL
{
    public partial class SiteUsers
    {
        #region SiteUsers Member

        public bool IsExistUserName(string username, Guid Id)
        {
            return dal.IsExistUserName(username, Id);
        }

        public SiteUsersInfo GetModel(string username)
        {
            return dal.GetModel(username);
        }

        public SiteUsersInfo GetModelByJoin(string username, object Id)
        {
            return dal.GetModelByJoin(username, Id);
        }

        public IList<SiteUsersInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
