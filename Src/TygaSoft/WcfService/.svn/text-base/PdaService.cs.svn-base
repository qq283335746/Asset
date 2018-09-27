using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.SysException;
using TygaSoft.SysUtility;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.WebUtility;
using TygaSoft.CustomProvider;
using TygaSoft.DBUtility;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PdaService : IPda
    {
        public string GetHelloWord()
        {
            return ResResult.ResJsonString(true, "", "Hello Word");
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel Login(LoginFmModel model)
        {
            try
            {
                Log.Info(string.Format("{0}--platform：{1}，deviceid：{2}，username：{3}，password：{4}", "ValidateUser", model.Platform, model.Deviceid, model.UserName, model.Password));

                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password)) return ResResult.Response(false, MC.Login_InvalidAccount, "");

                string userData = string.Empty;

                MembershipUser user = Membership.GetUser(model.UserName);
                if (!Membership.ValidateUser(model.UserName, model.Password))
                {
                    new EnumMembershipCreateStatus(user);
                }

                userData = user.ProviderUserKey.ToString();

                var ticket = new FormsAuthenticationTicket(1, model.UserName, DateTime.Now, DateTime.Now.AddMonths(1), true, userData, FormsAuthentication.FormsCookiePath);
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));

                return ResResult.Response(true, MC.Response_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #region 盘点

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianList(PdaPandianModel model)
        {
            try
            {
                var userId = WebCommon.GetUserId();
                if (userId.Equals(Guid.Empty)) return ResResult.Response((int)ResCode.未登录, MC.Login_NotExist, "");

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 10) model.PageSize = 10;
                int totalRecord = 0;

                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();

                Auth.CreateSearchItem(ref sqlWhere, ref parms, new string[] { "pd.DepmtId" });

                sqlWhere.AppendFormat("and pd.Status < {0} ", (int)EnumPandianStatus.已完成);

                if (model.PandianId != null)
                {
                    var pandianId = Guid.Empty;
                    Guid.TryParse(model.PandianId.ToString(), out pandianId);
                    if (!pandianId.Equals(Guid.Empty))
                    {
                        sqlWhere.Append("and Id = @PandianId ");
                        var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                        parm.Value = pandianId;
                        parms.Add(parm);
                    }
                }

                var bll = new Pandian();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());
                if (totalRecord == 0) return ResResult.Response(true, "", "{\"total\":0,\"rows\":[]}");

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianAssetList(PdaPandianAssetModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, "");

                var userId = WebCommon.GetUserId();
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 10) model.PageSize = 10;
                int totalRecord = 0;

                var pandianId = Guid.Empty;
                if (model.PandianId != null)
                {
                    Guid.TryParse(model.PandianId.ToString(), out pandianId);
                }

                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();
                SqlParameter parm = null;

                sqlWhere.AppendFormat("and pda.Status = {0} ", (int)EnumPandianAssetStatus.未盘点);
                if (!pandianId.Equals(Guid.Empty))
                {
                    sqlWhere.Append("and PandianId = @PandianId ");
                    parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                    parm.Value = pandianId;
                    parms.Add(parm);
                }

                var bll = new PandianAsset();
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());

                var totals = bll.GetTotal(pandianId);

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"footer\":[{\"TotalPan\":" + totals[0] + ",\"TotalYpan\":" + totals[1] + ",\"TotalNotPan\":" + totals[2] + "}]}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianAssetByBarcode(string appKey, string userName, object pandianId, string barcode)
        {
            try
            {
                var userId = WebCommon.GetUserId();

                if (string.IsNullOrWhiteSpace(barcode)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");

                var gId = Guid.Empty;
                if (pandianId != null) Guid.TryParse(pandianId.ToString(), out gId);
                if (gId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");

                var sqlWhere = @"and pd.Id = @PandianId and ais.Barcode = @Barcode ";
                SqlParameter[] parms = {
                    new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Barcode",SqlDbType.VarChar,36)
                };
                parms[0].Value = gId;
                parms[1].Value = barcode;

                var bll = new PandianAsset();
                var list = bll.GetListByJoin(sqlWhere, parms.ToArray());
                if (list == null || list.Count == 0) return ResResult.Response(false, MC.GetString(MC.Params_Data_NotExist, barcode));

                var item = list[0];

                return ResResult.Response(true, MC.Response_Ok, JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetPanYingBarcode()
        {
            try
            {
                return ResResult.Response(true, "", new Product().GetRndBarcode());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianDown(PdaPandianFmModel model)
        {
            try
            {
                var userId = WebCommon.GetUserId();
                if (userId.Equals(Guid.Empty)) return ResResult.Response((int)ResCode.未登录, MC.Login_NotExist, "");

                var gId = Guid.Empty;
                if (!Guid.TryParse(model.Id.ToString(), out gId)) return ResResult.Response(false, "参数不正确", "");

                var bll = new Pandian();
                if (bll.UpdateIsDown(gId) < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianAsset(PdaPandianAssetFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "请求参数集为空字符串", "");
                var userId = WebCommon.GetUserId();
                if (userId.Equals(Guid.Empty)) return ResResult.Response((int)ResCode.未登录, MC.Login_NotExist, "");
                var depmtId = new Staff().GetOrgId(userId);

                var pandianId = Guid.Empty;
                if (!Guid.TryParse(model.PandianId, out pandianId)) return ResResult.Response(false, "参数PandianId值为“" + model.PandianId + "”无效", "");

                var currTime = DateTime.Now;
                var gEmpty = Guid.Empty;
                var minDate = DateTime.Parse("1754-01-01");

                var pdaBll = new PandianAsset();
                var pBll = new Product();
                var pdBll = new Pandian();
                var effect = 0;

                foreach (var item in model.ItemList)
                {
                    var assetId = Guid.Empty;
                    if (!string.IsNullOrEmpty(item.AssetId)) Guid.TryParse(item.AssetId, out assetId);
                    var categoryId = Guid.Empty;
                    if (!string.IsNullOrEmpty(item.CategoryId)) Guid.TryParse(item.CategoryId, out categoryId);
                    var useDepmtId = Guid.Empty;
                    if (!string.IsNullOrEmpty(item.UseDepmtId)) Guid.TryParse(item.UseDepmtId, out useDepmtId);
                    var mgrDepmtId = Guid.Empty;
                    if (!string.IsNullOrEmpty(item.MgrDepmtId)) Guid.TryParse(item.MgrDepmtId, out mgrDepmtId);
                    var storeLocationId = Guid.Empty;
                    if (!string.IsNullOrEmpty(item.StoreLocationId)) Guid.TryParse(item.StoreLocationId, out storeLocationId);

                    ProductInfo productInfo = null; 
                    if (item.Status == (int)EnumPandianAssetStatus.盘盈)
                    {
                        #region 盘盈

                        productInfo = new ProductInfo(GlobalConfig.SiteCode, userId, depmtId, Guid.NewGuid(), categoryId, item.Barcode, item.AssetName, item.Barcode, item.SpecModel, 1, 0, 0, item.Unit, 0, string.Empty, string.Empty, string.Empty, minDate, "1754-01-01", string.Empty, useDepmtId, item.UsePerson, mgrDepmtId, storeLocationId, string.Empty, item.Status, 0, true, currTime, currTime);
                        effect += pBll.InsertByOutput(productInfo);
                        var pandianAssetInfo = new PandianAssetInfo(pandianId, productInfo.Id, productInfo.AppCode, productInfo.UserId, productInfo.DepmtId, gEmpty, gEmpty, gEmpty, string.Empty, 0, item.Remark, item.Status, currTime, currTime);
                        pdaBll.Insert(pandianAssetInfo);

                        #endregion
                    }
                    else
                    {
                        #region 非盘盈

                        productInfo = pBll.GetModel(assetId);
                        var pandianAssetInfo = pdaBll.GetModel(pandianId, assetId);
                        if (!useDepmtId.Equals(Guid.Empty) && !useDepmtId.Equals(productInfo.UseDepmtId)) pandianAssetInfo.LastUseDepmtId = useDepmtId;
                        if (!mgrDepmtId.Equals(Guid.Empty) && !mgrDepmtId.Equals(productInfo.MgrDepmtId)) pandianAssetInfo.LastMgrDepmtId = mgrDepmtId;
                        if (!storeLocationId.Equals(Guid.Empty) && !storeLocationId.Equals(productInfo.StoragePlaceId)) pandianAssetInfo.LastStoragePlaceId = storeLocationId;
                        if (!string.IsNullOrEmpty(item.UsePerson) && item.UsePerson != productInfo.UsePersonName) pandianAssetInfo.LastUsePerson = item.UsePerson;
                        pandianAssetInfo.Status = item.Status;

                        effect += pdaBll.Update(pandianAssetInfo);

                        #endregion
                    }
                }

                if (effect < 1) return ResResult.Response(false, "操作失败", "");

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex) 
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 资产分类

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCategoryTree(AuthFmModel model)
        {
            try
            {
                var bll = new Category();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message);
            }
        }

        #endregion

        #region 资产

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetProducts(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();
                //var Profile = new CustomProfileCommon();
                //if (!HttpContext.Current.User.IsInRole("Administrators") && !string.IsNullOrEmpty(Profile.UserRule))
                //{
                //    sqlWhere.Append("and CHARINDEX(convert(varchar(36),p.DepmtId),'" + Profile.UserRule + "') > 0 ");
                //}

                if (!string.IsNullOrEmpty(model.Keyword)) 
                {
                    sqlWhere.Append("and (p.Named like @Keyword or p.Coded like @Keyword) ");
                    var parm = new SqlParameter("@Keyword",SqlDbType.NVarChar,256);
                    parm.Value = "%" + model.Keyword + "%";
                    parms.Add(parm);
                }

                int totalRecord = 0;
                var bll = new Product();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 组织机构

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrgDepmtTree(AuthFmModel model)
        {
            try
            {
                var bll = new OrgDepmt();
                return ResResult.Response(true, "", bll.GetTreeJson(null,null));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 存放地点管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbbStoragePlace(LoginFmModel model)
        {
            try
            {
                var bll = new StoragePlace();
                var list = bll.GetList();
                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 私有

        //private void CreateAssetInStoreInfo(PdaPandianAssetItemModel item, ref ProductInfo model, ref PandianAssetInfo pdaModel)
        //{
        //    ProductInfo newModel = null;
        //    PandianAssetInfo newPdaModel = null;
        //    if (model != null) newModel = model;
        //    else newModel = new ProductInfo();
        //    if (pdaModel != null) newPdaModel = pdaModel;
        //    else newPdaModel = new PandianAssetInfo();

        //    var currTime = DateTime.Now;

        //    newModel.Barcode = item.Barcode;

        //    var gId = Guid.Empty;

        //    var categoryId = Guid.Empty;
        //    if (item.CategoryId != null) Guid.TryParse(item.CategoryId.ToString(), out categoryId);
        //    newModel.CategoryId = categoryId;
        //    newModel.Named = item.AssetName;
        //    newModel.SpecModel = item.SpecModel;
        //    newModel.MeterUnit = item.Unit;
        //    if (model == null) newModel.Price = 0;

        //    var useDepmtId = Guid.Empty;
        //    if (item.UseDepmtId != null) Guid.TryParse(item.UseDepmtId.ToString(), out useDepmtId);
        //    newPdaModel.LastUseDepmtId = useDepmtId;
        //    newModel.UseDepmtId = useDepmtId;

        //    var mgrDepmtId = Guid.Empty;
        //    if (item.MgrDepmtId != null) Guid.TryParse(item.MgrDepmtId.ToString(), out mgrDepmtId);
        //    newPdaModel.LastMgrDepmtId = mgrDepmtId;
        //    newModel.MgrDepmtId = mgrDepmtId;

        //    var storeLocationId = Guid.Empty;
        //    if (item.StoreLocationId != null) Guid.TryParse(item.StoreLocationId.ToString(), out storeLocationId);
        //    newPdaModel.LastStoragePlaceId = storeLocationId;
        //    newModel.StoragePlaceId = storeLocationId;

        //    if (model == null) newModel.BuyDate = currTime;
        //    newModel.UsePersonName = item.UsePerson;
        //    newPdaModel.LastUsePerson = item.UsePerson;

        //    if (model == null) newModel.UseDateLimit = currTime.AddMonths(12).ToString("yyyy-MM-dd");
        //    if (model == null) newModel.Supplier = "";
        //    newModel.Remark = item.Remark;
        //    newModel.LastUpdatedDate = currTime;
        //    newPdaModel.Status = EnumHelper.GetValue(typeof(EnumPandianAssetStatus), item.Status);
        //    newPdaModel.Remark = item.Remark;
        //    newPdaModel.LastUpdatedDate = currTime;
        //    newModel.LastUpdatedDate = currTime;

        //    model = newModel;
        //    pdaModel = newPdaModel;
        //}

        #endregion
    }
}
