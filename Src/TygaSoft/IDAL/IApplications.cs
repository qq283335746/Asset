using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IApplications
    {
        #region IApplications Member

        Guid GetAspnetAppId(string appName);

        //object GetApplicationId(string appName);

        #endregion
    }
}
