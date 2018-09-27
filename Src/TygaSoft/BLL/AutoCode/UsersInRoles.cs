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
    public partial class UsersInRoles
    {
        private static readonly IUsersInRoles dal = DataAccess.CreateUsersInRoles();

        #region UsersInRoles Member

        public int Insert(UsersInRolesInfo model)
        {
            return dal.Insert(model);
        }

        public int Delete(Guid userId, Guid roleId)
        {
            return dal.Delete(userId, roleId);
        }

        public UsersInRolesInfo GetModel(Guid userId, Guid roleId)
        {
            return dal.GetModel(userId, roleId);
        }

        #endregion
    }
}
