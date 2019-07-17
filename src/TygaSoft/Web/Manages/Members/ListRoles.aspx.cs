using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.SysUtility;

namespace TygaSoft.Web.Manages.Members
{
    public partial class ListRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
            
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            SiteRoles bll = new SiteRoles();
            var roleList = bll.GetAspnetList(Membership.ApplicationName, "",null);
            List<UserRoleInfo> list = new List<UserRoleInfo>();
            string[] items = Roles.GetAllRoles();
            foreach (string item in items)
            {
                var model = new UserRoleInfo();
                model.RoleId = roleList.First(m => m.Named == item).Id;
                model.RoleName = item;

                list.Add(model);
            }

            rpData.DataSource = list;
            rpData.DataBind();
        }
    }
}