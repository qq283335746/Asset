using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class Applications
    {
        #region Applications Member

        public Guid GetAspnetAppId(string appName)
        {
            return dal.GetAspnetAppId(appName);
        }

        //public object GetApplicationId(string appName)
        //{
        //    return dal.GetApplicationId(appName);
        //}

        #endregion
    }
}
