using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IUsersInRoles
    {
        #region IUsersInRoles Member

        int Insert(UsersInRolesInfo model);

        int Delete(Guid userId, Guid roleId);

        UsersInRolesInfo GetModel(Guid userId, Guid roleId);

        #endregion
    }
}
