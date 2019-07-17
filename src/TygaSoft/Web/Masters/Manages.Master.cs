using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using TygaSoft.CustomProvider;
using TygaSoft.Model;
using TygaSoft.SysUtility;
using TygaSoft.WebUtility;

namespace TygaSoft.Web.Masters
{
    public partial class Manages : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            Control ctl = this.LoadControl("~/WebUserControls/UCMenu.ascx");
            ctl.ID = "UCMenu";
            phUc.Controls.Add(ctl);

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var Profile = new CustomProfileCommon();
                var userProfileInfo = JsonConvert.DeserializeObject<UserProfileInfo>(Profile.UserInfo);
                if(userProfileInfo != null)
                {
                    lbSiteTitle.InnerText = userProfileInfo.SiteTitle;
                    if (!string.IsNullOrWhiteSpace(userProfileInfo.SiteLogo) && File.Exists(userProfileInfo.SiteLogo.Replace(WebCommon.GetSiteAppName(),"~")))
                    {
                        imgLogo.Src = userProfileInfo.SiteLogo;
                        imgLogo.Visible = true;
                    }
                    LoginStatus lsUser = lvUser.FindControl("lsUser") as LoginStatus;
                    if (!string.IsNullOrWhiteSpace(userProfileInfo.CultureName) && userProfileInfo.CultureName.ToLower() == "en-us")
                    {
                        lsUser.LogoutText = "[Sign Out]";
                        LoginName lnUser = lvUser.FindControl("lnUser") as LoginName;
                        lnUser.FormatString = "Welcome，{0}";
                    }
                    if (!string.IsNullOrEmpty(userProfileInfo.SiteCode)) lbAppId.InnerText = userProfileInfo.SiteCode;
                    if (userProfileInfo.SiteCode == Auth.AppCode)
                    {
                        lsUser.Visible = false;
                        Literal ltrRegister = lvUser.FindControl("ltrRegister") as Literal;
                        ltrRegister.Text = string.Format("<a href=\"{0}/login.html\" style=\"color:#FFF;\">[登录]</a><a href=\"{0}/register.html\" style=\"color:#FFF;\">[注册]</a>", WebCommon.GetSiteAppName().TrimEnd('/'));
                    }
                }
            }
        }
    }
}