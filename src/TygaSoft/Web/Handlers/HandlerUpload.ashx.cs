using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Transactions;
using System.Web.Configuration;
using TygaSoft.SysUtility;
using TygaSoft.DBUtility;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WebUtility;
using TygaSoft.SysException;
using TygaSoft.Converter;
using TygaSoft.CustomProvider;

namespace TygaSoft.Web.Handlers
{
    public class HandlerUpload : IHttpHandler
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
                    case "ImportStoragePlace":
                        OnImportStoragePlace(context);
                        break;
                    case "ExportStoragePlace":
                        OnExportStoragePlace(context);
                        break;
                    case "ImportOrgDepmt":
                        OnImportOrgDepmt(context);
                        break;
                    case "ExportOrgDepmt":
                        OnExportOrgDepmt(context);
                        break;
                    case "ImportCategory":
                        OnImportCategory(context);
                        break;
                    case "ExportCategory":
                        OnExportCategory(context);
                        break;
                    case "ImportProduct":
                        OnImportProduct(context);
                        break;
                    case "ExportProduct":
                        OnExportProduct(context);
                        break;
                    case "ExportPandianAsset":
                        OnExportPandianAsset(context);
                        break;
                    default:
                        throw new ArgumentException(MC.Request_Params_InvalidError);
                }
            }
            catch (CustomException ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        #region 导入导出

        /// <summary>
        /// 导入存放地点
        /// </summary>
        /// <param name="context"></param>
        private void OnImportStoragePlace(HttpContext context)
        {
            var files = context.Request.Files;
            if (files.Count == 0) throw new ArgumentException(MC.M_UploadFileNotExist);

            var appCode = context.Request.Form["AppCode"];
            var userId = WebCommon.GetUserId();
            var orgId = new Staff().GetOrgId(userId);

            var bll = new StoragePlace();
            int effect = 0;

            foreach (string item in files.AllKeys)
            {
                HttpPostedFile file = files[item];
                if (file == null || file.ContentLength == 0) continue;
                var dt = ExcelHelper.Import(file.InputStream);
                if (dt == null || dt.Rows.Count == 0) throw new ArgumentException(MC.M_UploadFileDataNotExist);
                var currTime = DateTime.Now;
                var id = Guid.Empty;

                var drc = dt.Rows;
                foreach (DataRow dr in drc)
                {
                    #region 请求参数集

                    string coded = string.Empty;
                    if (dr["存放地点编码"] != null) coded = dr["存放地点编码"].ToString().Trim();
                    string named = string.Empty;
                    if (dr["存放地点"] != null) named = dr["存放地点"].ToString().Trim();

                    if (!string.IsNullOrWhiteSpace(named))
                    {
                        if (string.IsNullOrWhiteSpace(coded))
                        {
                            coded = bll.GetRndCode(6);
                        }
                        if (bll.GetModel(null,named) != null) continue;
                        var currInfo = new StoragePlaceInfo(id, appCode, userId, orgId, coded, named, currTime, currTime);
                        effect += bll.Insert(currInfo);
                    }

                    #endregion
                }
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
            }
            else context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, effect));
        }

        /// <summary>
        /// 导出存放地点
        /// </summary>
        /// <param name="context"></param>
        private void OnExportStoragePlace(HttpContext context)
        {
            var bll = new StoragePlace();
            var ds = bll.GetExportData("", null);
            var dt = ds.Tables[0];
            HttpClientHelper.Export(context, dt);
        }

        /// <summary>
        /// 导入资产分类
        /// </summary>
        /// <param name="context"></param>
        private void OnImportCategory(HttpContext context)
        {
            var files = context.Request.Files;
            if (files.Count == 0) throw new ArgumentException(MC.M_UploadFileNotExist);

            var appCode = context.Request.Form["AppCode"];
            var userId = WebCommon.GetUserId();
            var orgId = new Staff().GetOrgId(userId);

            var bll = new Category();
            int effect = 0;

            var categories = bll.GetList();

            foreach (string item in files.AllKeys)
            {
                HttpPostedFile file = files[item];
                if (file == null || file.ContentLength == 0) continue;
                var dt = ExcelHelper.Import(file.InputStream);
                if (dt == null || dt.Rows.Count == 0) throw new ArgumentException(MC.M_UploadFileDataNotExist);
                var currTime = DateTime.Now;
                var sEmpty = string.Empty;

                var drc = dt.Rows;
                foreach (DataRow dr in drc)
                {
                    #region 请求参数集

                    string coded = string.Empty;
                    if (dr["资产分类编码"] != null) coded = dr["资产分类编码"].ToString().Trim();
                    string named = string.Empty;
                    if (dr["资产分类名称"] != null) named = dr["资产分类名称"].ToString().Trim();
                    string parentCode = string.Empty;
                    if (dr["所属上级分类编码"] != null) parentCode = dr["所属上级分类编码"].ToString().Trim();
                    string parentName = string.Empty;
                    if (dr["所属上级分类"] != null) parentName = dr["所属上级分类"].ToString().Trim();
                    int sort = 0;
                    if (dr["排序"] != null) int.TryParse(dr["排序"].ToString(), out sort);
                    if (string.IsNullOrWhiteSpace(coded) || string.IsNullOrWhiteSpace(named)) continue;

                    var id = Guid.NewGuid();
                    var parentId = Guid.Empty;
                    var parentInfo = bll.GetModel(parentCode, parentName);
                    var step = id.ToString();
                    var ids = new List<Guid>();
                    if (parentInfo != null)
                    {
                        parentId = parentInfo.Id;
                        bll.GetStep(categories, parentId, ref ids);
                        if (ids.Count > 0)
                        {
                            Array.Reverse(ids.ToArray());
                            step += "," + string.Join(",", ids.ToArray());
                        }
                    }

                    var currInfo = new CategoryInfo(id, appCode, userId, orgId, parentId, coded, named, step, sort, sEmpty, currTime, currTime);
                    effect += bll.Insert(currInfo);

                    #endregion
                }
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
            }
            else context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, effect));
        }

        /// <summary>
        /// 导出资产分类
        /// </summary>
        /// <param name="context"></param>
        private void OnExportCategory(HttpContext context)
        {
            var bll = new Category();
            var dt = bll.GetExportData("", null);
            HttpClientHelper.Export(context, dt);
        }

        /// <summary>
        /// 导入组织机构
        /// </summary>
        /// <param name="context"></param>
        private void OnImportOrgDepmt(HttpContext context)
        {
            var files = context.Request.Files;
            if (files.Count == 0) throw new ArgumentException(MC.M_UploadFileNotExist);

            var appCode = context.Request.Form["AppCode"];
            var userId = WebCommon.GetUserId();
            var orgId = new Staff().GetOrgId(userId);

            var bll = new OrgDepmt();
            int effect = 0;

            var categories = bll.GetList();

            foreach (string item in files.AllKeys)
            {
                HttpPostedFile file = files[item];
                if (file == null || file.ContentLength == 0) continue;
                var dt = ExcelHelper.Import(file.InputStream);
                if (dt == null || dt.Rows.Count == 0) throw new ArgumentException(MC.M_UploadFileDataNotExist);
                var currTime = DateTime.Now;
                var sEmpty = string.Empty;

                var drc = dt.Rows;
                foreach (DataRow dr in drc)
                {
                    #region 请求参数集

                    string coded = string.Empty;
                    if (dr["资产分类编码"] != null) coded = dr["资产分类编码"].ToString().Trim();
                    string named = string.Empty;
                    if (dr["资产分类名称"] != null) named = dr["资产分类名称"].ToString().Trim();
                    string parentCode = string.Empty;
                    if (dr["所属上级分类编码"] != null) parentCode = dr["所属上级分类编码"].ToString().Trim();
                    string parentName = string.Empty;
                    if (dr["所属上级分类"] != null) parentName = dr["所属上级分类"].ToString().Trim();
                    int sort = 0;
                    if (dr["排序"] != null) int.TryParse(dr["排序"].ToString(), out sort);
                    if (string.IsNullOrWhiteSpace(coded) || string.IsNullOrWhiteSpace(named)) continue;

                    var id = Guid.NewGuid();
                    var parentId = Guid.Empty;
                    if (string.IsNullOrWhiteSpace(parentCode)) parentCode = named;
                    var parentInfo = bll.GetModel(parentCode, parentName);
                    var step = id.ToString();
                    var ids = new List<Guid>();
                    if (parentInfo != null)
                    {
                        parentId = parentInfo.Id;
                        bll.GetStep(categories, parentId, ref ids);
                        if (ids.Count > 0)
                        {
                            Array.Reverse(ids.ToArray());
                            step += "," + string.Join(",", ids.ToArray());
                        }
                    }

                    var currInfo = new OrgDepmtInfo(id, appCode, userId, orgId, parentId, coded, named, step, sort, sEmpty, currTime, currTime);
                    effect += bll.Insert(currInfo);

                    #endregion
                }
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
            }
            else context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, effect));
        }

        /// <summary>
        /// 导出组织机构
        /// </summary>
        /// <param name="context"></param>
        private void OnExportOrgDepmt(HttpContext context)
        {
            var bll = new OrgDepmt();
            var dt = bll.GetExportData("", null);
            HttpClientHelper.Export(context, dt);
        }

        /// <summary>
        /// 导入资产信息
        /// </summary>
        /// <param name="context"></param>
        private void OnImportProduct(HttpContext context)
        {
            var files = context.Request.Files;
            if (files.Count == 0) throw new ArgumentException(MC.M_UploadFileNotExist);

            var appCode = context.Request.Form["AppCode"];
            var userId = WebCommon.GetUserId();
            var orgId = new Staff().GetOrgId(userId);

            var bll = new Product();
            var orgBll = new OrgDepmt();
            var spBll = new StoragePlace();
            var cBll = new Category();
            int effect = 0;
            var currTime = DateTime.Now;

            foreach (string item in files.AllKeys)
            {
                HttpPostedFile file = files[item];
                if (file == null || file.ContentLength == 0) continue;
                var dt = ExcelHelper.Import(file.InputStream);
                if (dt == null || dt.Rows.Count == 0) throw new CustomException(MC.M_UploadFileDataNotExist);
                //var id = Guid.Empty;

                var drc = dt.Rows;
                foreach (DataRow dr in drc)
                {
                    var currInfo = new ProductInfo();

                    #region 请求参数集

                    if (dr["资产分类编码"] != null) currInfo.CategoryCode = dr["资产分类编码"].ToString().Trim();
                    if (dr["资产分类"] != null) currInfo.CategoryName = dr["资产分类"].ToString().Trim();

                    if(string.IsNullOrWhiteSpace(currInfo.CategoryCode) || string.IsNullOrWhiteSpace(currInfo.CategoryName)) throw new CustomException(MC.GetString(MC.Request_InvalidArgument,"资产编码、资产名称"));
                    var oldCategoryInfo = cBll.GetModel(currInfo.CategoryCode,currInfo.CategoryName);
                    if(oldCategoryInfo == null) throw new CustomException(MC.GetString(MC.P_InvalidError,string.Format("资产分类编码“{0}”、资产分类为“{1}”",currInfo.CategoryCode,currInfo.CategoryName)));
                    currInfo.CategoryId = oldCategoryInfo.Id;

                    //if (dr["资产编码"] != null) currInfo.Coded = dr["资产编码"].ToString().Trim();
                    if (dr["资产名称"] != null) currInfo.Named = dr["资产名称"].ToString().Trim();
                    //if (bll.IsExist(currInfo.Coded, currInfo.Named)) continue;
                    var sCoded = bll.GetRndCode(oldCategoryInfo.Coded,8);
                    currInfo.Coded = sCoded;
                    var qty = 0;
                    if (dr["数量"] != null) int.TryParse(dr["数量"].ToString(),out qty);
                    currInfo.Qty = 1;
                    if(qty < 1) throw new CustomException(string.Format("数量为“{0}”",qty));
                    if (dr["规格型号"] != null) currInfo.SpecModel = dr["规格型号"].ToString().Trim();
                    var price = 0m;
                    if (dr["单价"] != null && decimal.TryParse(dr["单价"].ToString(),out price)) currInfo.Price = price;
                    var amount = 0m;
                    if (dr["金额"] != null && decimal.TryParse(dr["金额"].ToString(),out amount)) currInfo.Amount = amount;
                    if (dr["计量单位"] != null) currInfo.MeterUnit = dr["计量单位"].ToString().Trim();
                    var pieceQty = 0;
                    //if (dr["件数"] != null && int.TryParse(dr["件数"].ToString(),out pieceQty)) currInfo.PieceQty = pieceQty;
                    if (dr["资产属性"] != null) currInfo.Pattr = dr["资产属性"].ToString().Trim();
                    if (dr["资产来源"] != null) currInfo.SourceFrom = dr["资产来源"].ToString().Trim();
                    if (dr["供应商"] != null) currInfo.Supplier = dr["供应商"].ToString().Trim();
                    var buyDate = DateTime.MinValue;
                    if (dr["购入日期"] != null) DateTime.TryParse(dr["购入日期"].ToString(), out buyDate);
                    if (buyDate == DateTime.MinValue) buyDate = DateTime.Parse("1754-01-01");
                    currInfo.BuyDate = buyDate;  
                    var enableDate = DateTime.MinValue;
                    if (dr["启用日期"] != null && DateTime.TryParse(dr["启用日期"].ToString(),out enableDate)) currInfo.EnableDate = enableDate.ToString("yyyy-MM-dd");
                    if (dr["使用期限"] != null) currInfo.UseDateLimit = dr["使用期限"].ToString().Trim();
                    if (dr["使用部门"] != null) currInfo.UseOrgName = dr["使用部门"].ToString().Trim();
                    if (dr["使用部门编码"] != null) currInfo.UseOrgCode = dr["使用部门编码"].ToString().Trim();
                    if (!string.IsNullOrEmpty(currInfo.UseOrgCode) || !string.IsNullOrEmpty(currInfo.UseOrgName)) 
                    {
                        var useOrgInfo = orgBll.GetModel(currInfo.UseOrgCode, currInfo.UseOrgName);
                        if (useOrgInfo == null) throw new CustomException(MC.GetString(MC.P_InvalidError, string.Format("使用部门编码“{0}”、使用部门“{1}”", currInfo.UseOrgCode, currInfo.UseOrgName)));
                        currInfo.UseDepmtId = useOrgInfo.Id;
                    }

                    if (dr["实物管理部门"] != null) currInfo.MgrOrgName = dr["实物管理部门"].ToString().Trim();
                    if (dr["实物管理部门编码"] != null) currInfo.MgrOrgCode = dr["实物管理部门编码"].ToString().Trim();
                    if(!string.IsNullOrEmpty(currInfo.MgrOrgCode) || !string.IsNullOrEmpty(currInfo.MgrOrgName))
                    {
                        var mgrOrgInfo = orgBll.GetModel(currInfo.MgrOrgCode, currInfo.MgrOrgName);
                        if (mgrOrgInfo == null) throw new CustomException(MC.GetString(MC.P_InvalidError, string.Format("实物管理部门编码“{0}”、实物管理部门“{1}”", currInfo.MgrOrgCode, currInfo.MgrOrgName)));
                        currInfo.MgrDepmtId = mgrOrgInfo.Id;
                    }
                    
                    if (dr["存放地点"] != null) currInfo.StoragePlaceName = dr["存放地点"].ToString().Trim();
                    if (dr["存放地点编码"] != null) currInfo.StoragePlaceCode = dr["存放地点编码"].ToString().Trim();
                    if (!string.IsNullOrEmpty(currInfo.StoragePlaceCode) || !string.IsNullOrEmpty(currInfo.StoragePlaceName))
                    {
                        var storagePlaceInfo = spBll.GetModel(currInfo.StoragePlaceCode, currInfo.StoragePlaceName);
                        if (storagePlaceInfo == null) throw new CustomException(MC.GetString(MC.P_InvalidError, string.Format("存放地点编码“{0}”、存放地点“{1}”", currInfo.StoragePlaceCode, currInfo.StoragePlaceName)));
                        currInfo.StoragePlaceId = storagePlaceInfo.Id;
                    }

                    if (dr["使用人"] != null) currInfo.UsePersonName = dr["使用人"].ToString().Trim();

                    #endregion

                    currInfo.AppCode = appCode;
                    currInfo.UserId = userId;
                    currInfo.DepmtId = orgId;
                    currInfo.RecordDate = currTime;
                    currInfo.LastUpdatedDate = currTime;

                    effect += bll.Insert(currInfo);

                    if (qty > 1)
                    {
                        for (var i = 1; i < qty; i++)
                        {
                            currInfo.Coded = bll.GetRndCode(sCoded, 11);
                            effect += bll.Insert(currInfo);
                        }
                    }
                }
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
            }
            else context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, effect));
        }

        /// <summary>
        /// 导出存放地点
        /// </summary>
        /// <param name="context"></param>
        private void OnExportProduct(HttpContext context)
        {
            var sqlWhere = new StringBuilder(100);
            var parms = new ParamsHelper();
            SqlParameter parm = null;
            var Profile = new CustomProfileCommon();
            if (!(HttpContext.Current.User.IsInRole("Administrators") && HttpContext.Current.User.IsInRole("Administrators")) && !string.IsNullOrEmpty(Profile.UserRule))
            {
                sqlWhere.Append("and CHARINDEX(convert(varchar(36),p.DepmtId),'" + Profile.UserRule + "') > 0 ");
                parm = new SqlParameter("@UserRule", Profile.UserRule);
                parms.Add(parm);
            }
            var bll = new Product();
            var ds = bll.GetExportData(sqlWhere.ToString(), parms.ToArray());
            var dt = ds.Tables[0];
            HttpClientHelper.Export(context, dt);
        }

        /// <summary>
        /// 导出盘点结果
        /// </summary>
        /// <param name="context"></param>
        private void OnExportPandianAsset(HttpContext context)
        {
            var pandianId = context.Request.QueryString["pandianId"];
            var bll = new PandianAsset();
            var cmdText = "and pda.PandianId = @PandianId ";
            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId);
            var dt = bll.GetExportData(cmdText, parm);
            HttpClientHelper.Export(context, dt);
        }

        #endregion

        #region 私有方法

        private void FileValidated(HttpPostedFile file)
        {
            int fileSize = file.ContentLength;
            int uploadFileSize = int.Parse(WebConfigurationManager.AppSettings["UploadFileSize"]);
            if (fileSize > uploadFileSize) throw new ArgumentException("文件【" + file.FileName + "】大小超出字节" + uploadFileSize + "，无法上传，请正确操作！");
            if (!UploadFilesHelper.IsFileValidated(file.InputStream, fileSize))
            {
                new CustomException("上传了非法文件--" + file.FileName);
                throw new ArgumentException("文件【" + file.FileName + "】为受限制的文件，请正确操作！");
            }
        }

        private void CreateThumbnailImage(HttpContext context, ImagesHelper ih, string originalPath)
        {
            var ext = Path.GetExtension(originalPath);
            var rndFolder = Path.GetFileNameWithoutExtension(originalPath);
            string[] platformNames = Enum.GetNames(typeof(Platform));
            var index = 0;
            foreach (string name in platformNames)
            {
                string sizeAppend = WebConfigurationManager.AppSettings[name];
                string[] sizeArr = sizeAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sizeArr.Length; i++)
                {
                    string bmsPath = string.Format("{0}\\{1}_{2}{3}{4}", Path.GetDirectoryName(originalPath), rndFolder, index, i + 1, ext);
                    string[] wh = sizeArr[i].Split('*');

                    ih.CreateThumbnailImage(originalPath, bmsPath, int.Parse(wh[0]), int.Parse(wh[1]), "DB", ext);
                }
                index++;
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}