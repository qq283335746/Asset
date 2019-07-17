using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.BLL;
using TygaSoft.DBUtility;
using TygaSoft.Model;
using TygaSoft.SysUtility;
using TygaSoft.WebUtility;
using TygaSoft.CustomProvider;
using TygaSoft.Converter;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// HandlerContent 的摘要说明
    /// </summary>
    public class HandlerContent : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            try
            {
                string reqName = "";
                switch (context.Request.HttpMethod.ToUpper())
                {
                    case "GET":
                        reqName = context.Request.QueryString["ReqName"];
                        break;
                    case "POST":
                        reqName = context.Request.Form["ReqName"];
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrWhiteSpace(reqName)) return;
                reqName = reqName.Trim();

                switch (reqName)
                {
                    case "SaveMenuAccess":
                        SaveMenuAccess(context);
                        break;
                    default:
                        throw new ArgumentException(MC.Request_Params_InvalidError);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 条码

        //private void SaveBarcodeTemplate(HttpContext context)
        //{
        //    try
        //    {
        //        var isInsert = false;
        //        Guid Id = Guid.Empty;
        //        if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
        //        if (Id.Equals(Guid.Empty))
        //        {
        //            Id = Guid.NewGuid();
        //            isInsert = true;
        //        }
        //        var sTypeName = context.Request.Form["TypeName"].Trim();
        //        var userId = WebCommon.GetUserId();
        //        BarcodeTemplateInfo modelInfo = null;
        //        var sTitle = context.Request.Form["Title"].Trim();
        //        var sJContent = HttpUtility.UrlDecode(context.Request.Form["JContent"]).Trim();
        //        var isDefault = bool.Parse(context.Request.Form["IsDefault"]);

        //        if (string.IsNullOrWhiteSpace(sTitle) && string.IsNullOrWhiteSpace(sJContent) && string.IsNullOrWhiteSpace(sTypeName))
        //        {
        //            throw new ArgumentException(MC.M_RuleInvalidError);
        //        }

        //        if (sTypeName == "Barcode")
        //        {
        //            var sBarcode = context.Request.Form["Barcode"].Trim();
        //            var sBarcodeFormat = context.Request.Form["BarcodeFormat"].Trim();
        //            var width = int.Parse(context.Request.Form["Width"]);
        //            var height = int.Parse(context.Request.Form["Height"]);
        //            var margin = int.Parse(context.Request.Form["Margin"]);

        //            var barcodeInfo = new BarcodeInfo(sBarcode, sBarcodeFormat, width, height, margin, "");
        //           barcodeInfo.ImageUrl = FilesHelper.GetRndUrl("Barcodes",
        //            barcodeInfo.ImageUrl = ZxingHelper.CreateBarcode(barcodeInfo, Id.ToString(), true);
        //            modelInfo = new BarcodeTemplateInfo(Id, userId, sTitle, JsonConvert.SerializeObject(barcodeInfo), isDefault, sTypeName, DateTime.Now);
        //        }
        //        else
        //        {
        //            modelInfo = new BarcodeTemplateInfo(Id, userId, sTitle, sJContent, isDefault, sTypeName, DateTime.Now);
        //        }

        //        var bll = new BarcodeTemplate();
        //        int effect = -1;

        //        if (isInsert)
        //        {
        //            effect = bll.InsertByOutput(modelInfo);
        //        }
        //        else
        //        {
        //            effect = bll.Update(modelInfo);
        //        }
        //        if (effect < 1) context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));

        //        context.Response.Write(ResResult.ResJsonString(true, "", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
        //    }
        //}

        #endregion

        #region 系统管理

        private void SaveMenuAccess(HttpContext context)
        {
            if (!(HttpContext.Current.User.IsInRole("Administrators") || HttpContext.Current.User.IsInRole("System"))) throw new ArgumentException(MC.Role_InvalidError);

            var sRoleName = context.Request.Form["RoleName"];
            var sUserName = context.Request.Form["UserName"];
            var sMenuItemJson = context.Request.Form["MenuItemJson"];

            if (string.IsNullOrWhiteSpace(sMenuItemJson)) throw new ArgumentException(MC.Request_Params_InvalidError);
            sMenuItemJson = HttpUtility.UrlDecode(sMenuItemJson);
            if (string.IsNullOrWhiteSpace(sRoleName) && string.IsNullOrWhiteSpace(sUserName)) throw new ArgumentException(MC.Request_Params_InvalidError);
            List<SiteMenusAccessItemInfo> list = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(sMenuItemJson);
            var accessId = Guid.Empty;
            var isRole = !string.IsNullOrWhiteSpace(sRoleName);
            var accessType = isRole ? "Roles" : "Users";
            if (isRole)
            {
                if (sRoleName.ToLower() == "administrators") throw new ArgumentException(MC.GetString(MC.Params_SaveRoleAccessError, sRoleName));

                var roleBll = new SiteRoles();
                accessId = roleBll.GetAspnetModel(Membership.ApplicationName, sRoleName).Id;
            }
            else
            {
                if (Roles.GetRolesForUser(sUserName).Contains("administrators")) throw new ArgumentException(MC.GetString(MC.Params_SaveUserAccessError, sUserName));

                accessId = Guid.Parse(Membership.GetUser(sUserName).ProviderUserKey.ToString());
            }
            var menuBll = new SiteMenus();
            var maBll = new SiteMenusAccess();
            List<SiteMenusAccessItemInfo> maitems = null;
            var appId = new Applications().GetAspnetAppId(Membership.ApplicationName);
            var menusAccessInfo = maBll.GetModel(appId,accessId);
            if (menusAccessInfo != null) maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(menusAccessInfo.OperationAccess);
            else maitems = new List<SiteMenusAccessItemInfo>();

            foreach (var item in list)
            {
                var menuId = Guid.Parse(item.MenuId.ToString());

                var itemIndex = maitems.FindIndex(m => m.MenuId.Equals(menuId));
                if (itemIndex > -1) maitems[itemIndex] = item;
                else maitems.Add(item);
            }

            if (menusAccessInfo != null)
            {
                menusAccessInfo.OperationAccess = JsonConvert.SerializeObject(maitems);
                maBll.Update(menusAccessInfo);
            }
            else
            {
                menusAccessInfo = new SiteMenusAccessInfo(appId,accessId, JsonConvert.SerializeObject(maitems), accessType);
                maBll.Insert(menusAccessInfo);
            }

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        #endregion
    }
}