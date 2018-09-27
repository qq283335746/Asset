using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.SysUtility;
using TygaSoft.WebUtility;
using TygaSoft.CustomProvider;
using TygaSoft.SysException;

namespace TygaSoft.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
            else
            {
                OnLogin();
            }
        }

        private void Bind()
        {
            var userName = string.Empty;
            try
            {
                if (Request.Cookies["UserInfo"] != null && !string.IsNullOrEmpty(Request.Cookies["UserInfo"].Value))
                {
                    var sLoginInfo = Request.Cookies["UserInfo"].Value;
                    AESEncrypt aes = new AESEncrypt();
                    var loginInfo = JsonConvert.DeserializeObject<LoginInfo>(aes.DecryptString(sLoginInfo));
                    userName = loginInfo.UserName;
                }
            }
            catch (Exception ex)
            {
                Response.Cookies["UserInfo"].Value = null;
                MessageBox.Messager(this.Page, Page.Controls[0], ex.Message, MC.AlertTitle_Sys_Info);
            }
            ltrMyData.Text = "<div id=\"myData\" style=\"display:none;\">{\"UserName\":\"" + userName + "\"}</div>";
        }

        private void OnLogin()
        {
            var errMsg = string.Empty;
            var fromLoginUrl = string.Empty;
            try
            {
                string userName = Request.Form["txtUserName"];
                string psw = Request.Form["txtPsw"];
                string sVc = Request.Form["txtVc"];

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(psw)) throw new ArgumentException(MC.Login_InvalidAccount);
                if (string.IsNullOrWhiteSpace(sVc)) throw new ArgumentException(MC.Login_InvalidVC);

                bool isRemember = Request.Form["cbRememberMe"] == "1";

                userName = userName.Trim();
                psw = psw.Trim();
                sVc = sVc.Trim();

                var cookie = Request.Cookies["Asset_LoginVc"];
                if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value)) throw new ArgumentException(MC.Login_InvalidVCCookie);
                string validCode = cookie.Value;

                AESEncrypt aes = new AESEncrypt();

                if (sVc.ToLower() != aes.DecryptString(validCode).ToLower()) throw new ArgumentException(MC.Login_InvalidVC);
                if (!Regex.IsMatch(psw, MC.PasswordStrengthRegularExpression)) throw new ArgumentException(MC.Login_InvalidPassword);

                #region 不使用wcf身份认证服务

                string userData = string.Empty;

                MembershipUser userInfo = Membership.GetUser(userName);
                if (!Membership.ValidateUser(userName, psw))
                {
                    new EnumMembershipCreateStatus(userInfo);
                }

                userData = userInfo.ProviderUserKey.ToString();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout),
                        true, userData, FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                //登录成功，则

                //bool isPersistent = true;
                //bool isRemember = true;
                //bool isAuto = false;
                //double d = 100;
                //if (cbRememberMe.Checked) isAuto = true;
                //自动登录 设置时间为7天
                //if (isAuto) d = 10080;

                #endregion

                if (isRemember)
                {
                    var loginInfo = new LoginInfo(userName, DateTime.Now);
                    var sUserInfo = aes.EncryptString(JsonConvert.SerializeObject(loginInfo));
                    Response.Cookies.Add(new HttpCookie("UserInfo", sUserInfo));
                }
                else
                {
                    Response.Cookies.Add(new HttpCookie("UserInfo", ""));
                }

                fromLoginUrl = FormsAuthentication.GetRedirectUrl(userName, true);
            }
            catch (CustomException ex)
            {
                errMsg = ex.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Messager(this.Page, Page.Controls[0], errMsg, MC.AlertTitle_Sys_Info);
                return;
            }
            Response.Redirect("~/u/t.html");
            //if (!string.IsNullOrEmpty(fromLoginUrl)) Response.Redirect(fromLoginUrl);

            //FormsAuthentication.RedirectFromLoginPage(userName, true);//使用此行会清空ticket中的userData ？！！！
        }
    }
}