using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TygaSoft.SysException;
using TygaSoft.DBUtility;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.BLL;
using TygaSoft.CustomProvider;
using TygaSoft.SysUtility;
using TygaSoft.WebUtility;
using TygaSoft.Converter;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {
        #region 打印、条码

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetBarcodeTemplateInfoByDefault()
        {
            try
            {
                var bll = new BarcodeTemplate();
                var oldInfo = bll.GetModelByDefault();
                if (oldInfo == null) return ResResult.Response(false, MC.M_BarcodeTemplateDefaultNotExistError, "");

                return ResResult.Response(true, "", JsonConvert.SerializeObject(oldInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetBarcodeFormats()
        {
            try
            {
                var data = BarcodeHelper.GetBarcodeFormats();

                return ResResult.Response(true, "", JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetBarcodeBrowser(BarcodeTemplateFmModel model)
        {
            try
            {
                var barcodeInfo = new BarcodeInfo(model.Barcode, model.BarcodeFormat, model.Width, model.Height, model.Margin, "");

                barcodeInfo.ImageUrl = new TempFolder().GetTempUrl(string.Format("{0}.jpg", model.Barcode));

                //barcodeInfo.ImageUrl = FilesHelper.GetRndFile("Barcodes", string.Format("{0}.jpg", model.Barcode));
                ZxingHelper.CreateBarcode(FilesHelper.ToFullPath(barcodeInfo.ImageUrl), barcodeInfo);
                return ResResult.Response(true, "", FilesHelper.ToVirtualUrl(barcodeInfo.ImageUrl));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetBarcodeTemplateList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                if (!string.IsNullOrWhiteSpace(model.TypeName))
                {
                    sqlWhere = new StringBuilder(100);
                    sqlWhere.Append("and TypeName = @TypeName ");
                    var parm = new SqlParameter("@TypeName", SqlDbType.NVarChar, 20);
                    parm.Value = model.TypeName;
                    parms = new ParamsHelper();
                    parms.Add(parm);
                }

                var bll = new BarcodeTemplate();
                var list = bll.GetList(model.PageIndex, model.PageSize, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + list.Count + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveBarcodeTemplate(BarcodeTemplateFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                if (Id.Equals(Guid.Empty)) model.Id = Guid.NewGuid();
                var userId = WebCommon.GetUserId();

                BarcodeTemplateInfo modelInfo = null;

                if (model.TypeName == "Barcode")
                {
                    var barcodeInfo = new BarcodeInfo(model.Barcode, model.BarcodeFormat, model.Width, model.Height, model.Margin, "");
                    barcodeInfo.ImageUrl = FilesHelper.GetRndUrl("Barcodes", string.Format("{0}.jpg", model.Id));
                    var toFile = FilesHelper.ToFullPath(barcodeInfo.ImageUrl);
                    if (!File.Exists(toFile)) ZxingHelper.CreateBarcode(toFile, barcodeInfo);
                    modelInfo = new BarcodeTemplateInfo(Guid.Parse(model.Id.ToString()), userId, model.Title, model.Html, JsonConvert.SerializeObject(barcodeInfo), model.IsDefault, model.TypeName, DateTime.Now);
                }
                else
                {
                    modelInfo = new BarcodeTemplateInfo(Guid.Parse(model.Id.ToString()), userId, model.Title, model.Html, HttpUtility.UrlDecode(model.Attr), model.IsDefault, model.TypeName, DateTime.Now);
                }

                var bll = new BarcodeTemplate();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.新增, true);
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteBarcodeTemplate(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.删除, true);

                var bll = new BarcodeTemplate();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveSetDefault(BarcodeTemplateFmModel model)
        {
            try
            {
                if (model.Id.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");

                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.编辑, true);

                var bll = new BarcodeTemplate();
                var effect = bll.SetDefault(Guid.Parse(model.Id.ToString()), model.IsDefault, model.TypeName);
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 盘点

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetPandianInfo(string id)
        {
            try
            {
                var pandianId = Guid.Empty;
                Guid.TryParse(id, out pandianId);
                if (pandianId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var bll = new Pandian();
                var parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm.Value = pandianId;
                var pandianInfo = bll.GetModelByJoin("and Id = @Id", parm);
                if (pandianInfo == null) return ResResult.Response(false, MC.GetString(MC.Params_Data_NotExist, id), "");

                return ResResult.Response(true, "", JsonConvert.SerializeObject(pandianInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;

                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();
                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    sqlWhere.Append("and (pd.Named like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 256);
                    parm.Value = "%" + model.Keyword + "%";
                    parms.Add(parm);
                }
                Auth.CreateSearchItem(ref sqlWhere, ref parms, new string[] { "DepmtId" });

                var bll = new Pandian();
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());
                if (totalRecord == 0) return ResResult.Response(true, "", "{\"total\":0,\"rows\":[]}");

                var totals = bll.GetTotal();

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"footer\":[{\"TotalAll\":" + totals[0] + ",\"TotalFinish\":" + totals[1] + ",\"TotalNotFinish\":" + totals[2] + "}]}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandian(PandianAssetFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何可保存的数据", "");
                if (string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "盘点单名称不能为空字符串", "");

                var userId = WebCommon.GetUserId();
                var depmtId = new Staff().GetOrgId(userId);
                var currTime = DateTime.Now;

                #region 查询条件

                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.StartBuyDate)) DateTime.TryParse(model.StartBuyDate, out startDate);
                if (!string.IsNullOrWhiteSpace(model.EndBuyDate)) DateTime.TryParse(model.EndBuyDate, out endDate);
                if (startDate > endDate) return ResResult.Response(false, "购入日期的开始时间不能大于结束时间", "");
                var mgrOrgId = Guid.Empty;
                if(!string.IsNullOrEmpty(model.MgrDepmtId)) Guid.TryParse(model.MgrDepmtId.ToString(),out mgrOrgId);
                if (mgrOrgId.Equals(Guid.Empty)) mgrOrgId = depmtId;

                var sqlWhere = new StringBuilder(1000);
                var parms = new ParamsHelper();
                SqlParameter parm = null;

                if (!(HttpContext.Current.User.IsInRole("Administrators") || HttpContext.Current.User.IsInRole("System")))
                {
                    var Profile = new CustomProfileCommon();
                    var sUserRule = Profile.UserRule;
                    if (string.IsNullOrEmpty(sUserRule)) return ResResult.Response(false, MC.M_DataRightInvalidError, "");
                    var userRules = sUserRule.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
                    var orgBll = new OrgDepmt();
                    var mgrOrgInfo = orgBll.GetModel(mgrOrgId);
                    if (mgrOrgInfo == null) return ResResult.Response(false, MC.GetString(MC.Params_Data_NotExist, mgrOrgId.ToString()), "");

                    if (!userRules.Contains(mgrOrgId.ToString())) return ResResult.Response(false, MC.GetString(MC.P_OrgRightInvalidError, mgrOrgInfo.Named), "");
                    if (userRules.Length > 0 && userRules[userRules.Length - 1] != mgrOrgId.ToString())
                    {
                        sqlWhere.Append("and MgrDepmtId = @MgrDepmtId ");
                        parm = new SqlParameter("@MgrDepmtId", SqlDbType.UniqueIdentifier);
                        parm.Value = mgrOrgId;
                        parms.Add(parm);
                    }
                    sqlWhere.AppendFormat("and CHARINDEX(convert(varchar(36),MgrDepmtId),'{0}') > 0 ", sUserRule);
                }

                #region 日期时间段

                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                    sqlWhere.Append("and (BuyDate between @StartDate and @EndDate) ");
                    parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                    parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = endDate;
                    parms.Add(parm);
                }
                else
                {
                    if (startDate != DateTime.MinValue)
                    {
                        sqlWhere.Append("and (BuyDate >= @StartDate) ");
                        parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        parm.Value = startDate;
                        parms.Add(parm);
                    }
                    if (endDate != DateTime.MinValue)
                    {
                        endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                        sqlWhere.Append("and (BuyDate <= @EndDate) ");
                        parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        parm.Value = endDate;
                        parms.Add(parm);
                    }
                }

                #endregion

                var categoryId = Guid.Empty;
                var useDepmtId = Guid.Empty;
                var mgrDepmtId = Guid.Empty;
                if (!string.IsNullOrEmpty(model.CategoryId) && Guid.TryParse(model.CategoryId.ToString(), out categoryId))
                {
                    sqlWhere.Append("and CategoryId = @CategoryId ");
                    parm = new SqlParameter("@CategoryId", SqlDbType.UniqueIdentifier);
                    parm.Value = categoryId;
                    parms.Add(parm);
                }
                if (!string.IsNullOrEmpty(model.UseDepmtId) && Guid.TryParse(model.UseDepmtId.ToString(), out useDepmtId))
                {
                    sqlWhere.Append("and UseDepmtId = @UseDepmtId ");
                    parm = new SqlParameter("@UseDepmtId", SqlDbType.UniqueIdentifier);
                    parm.Value = useDepmtId;
                    parms.Add(parm);
                }
                var storagePlaceId = Guid.Empty;
                if (!string.IsNullOrEmpty(model.StoragePlaceId) && Guid.TryParse(model.StoragePlaceId.ToString(), out storagePlaceId))
                {
                    sqlWhere.Append("and StoragePlaceId = @StoragePlaceId ");
                    parm = new SqlParameter("@StoragePlaceId", SqlDbType.UniqueIdentifier);
                    parm.Value = storagePlaceId;
                    parms.Add(parm);
                }

                #endregion

                var pBll = new Product();
                var pList = pBll.GetList(sqlWhere.ToString(), parms.ToArray());
                if (pList == null || pList.Count == 0)
                {
                    if (!model.IsConfirm) return ResResult.Response((int)ResCode.确认, "您所选范围没有资产，是否创建盘点单?", "");
                }

                var modelInfo = new PandianInfo(Guid.NewGuid(), model.AppCode, userId, depmtId, model.Named.Trim(), pList.Count, false, mgrDepmtId.Equals(Guid.Empty) ? depmtId : mgrDepmtId, 0, model.Remark, (int)EnumPandianStatus.新建, currTime, currTime);

                var pdBll = new Pandian();
                var pdaBll = new PandianAsset();
                int effect = 0;

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    effect = pdBll.InsertByOutput(modelInfo);

                //    foreach (var item in pList)
                //    {
                //        var pdaInfo = new PandianAssetInfo(modelInfo.Id, item.Id, model.AppCode, userId, depmtId, 0, string.Empty, 0,currTime,currTime);

                //        effect += pdaBll.Insert(pdaInfo);
                //    }

                //    scope.Complete();
                //}

                effect = pdBll.InsertByOutput(modelInfo);

                foreach (var item in pList)
                {
                    var pdaInfo = new PandianAssetInfo(modelInfo.Id, item.Id, model.AppCode, userId, depmtId, Guid.Empty, Guid.Empty, Guid.Empty, string.Empty, 0, string.Empty, (int)EnumStatus.新建, currTime, currTime);

                    effect += pdaBll.Insert(pdaInfo);
                }

                if (effect < 1) return ResResult.Response(false, "操作失败，原因：可能是由于数据连接异常", "");

                return ResResult.Response(true, "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeletePandian(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var arr = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr == null && arr.Length == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var bll = new Pandian();
                var effct = 0;
                foreach (var item in arr)
                {
                    var id = Guid.Parse(item);
                    if (bll.IsExistChildren(id))
                    {
                        return ResResult.Response(false, MC.GetString(MC.Params_DeleteExistChildError, "资产"), "");
                    }
                    effct += bll.Delete(id);
                }

                if (effct < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianAssetList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;

                var pandianId = Guid.Empty;
                if (!Guid.TryParse(model.ParentId.ToString(), out pandianId)) return ResResult.Response(false, "请求参数值“" + model.ParentId + "”不正确", "");

                var sqlWhere = new StringBuilder(100);
                var parms = new ParamsHelper();

                sqlWhere.Append("and PandianId = @PandianId");
                var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                parm.Value = pandianId;
                parms.Add(parm);

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
        public ResResultModel DeletePandianAsset(string pandianId, string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var arr = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var gId = Guid.Empty;
                if (!string.IsNullOrEmpty(pandianId)) Guid.TryParse(pandianId, out gId);
                if (gId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var pdBll = new Pandian();

                var pandianInfo = pdBll.GetModel(gId);
                if (pandianInfo == null) return ResResult.Response(false, MC.GetString(MC.Params_Data_NotExist, pandianId), "");
                if (pandianInfo.Status != (int)EnumPandianStatus.新建) return ResResult.Response(false, MC.GetString(MC.M_DeletePandianAssetError, pandianId), "");

                var pdaBll = new PandianAsset();
                var pBll = new Product();
                var effct = 0;

                var pdaList = pdaBll.GetListByPandianId(gId);

                foreach (var item in arr)
                {
                    var assetId = Guid.Parse(item);
                    var pdaInfo = pdaList.First(m => m.AssetId.Equals(assetId));
                    effct += pdaBll.Delete(gId, assetId);
                    if (pdaInfo.Status == (int)EnumPandianAssetStatus.盘盈) pBll.Delete(assetId);
                }

                if (effct < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianAssetResult(string pandianId)
        {
            try
            {
                var gId = Guid.Empty;
                if (!string.IsNullOrEmpty(pandianId)) Guid.TryParse(pandianId, out gId);
                if (gId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var pdBll = new Pandian();
                var pdInfo = pdBll.GetModel(gId);

                var pdaBll = new PandianAsset();
                var list = pdaBll.GetListByPandianId(gId);
                if (list == null || list.Count == 0) return ResResult.Response(true, MC.M_Save_Ok, "");


                var pBll = new Product();
                var effect = 0;
                var isError = false;

                foreach (var model in list)
                {
                    if (model.Status == (int)EnumPandianAssetStatus.完成) continue;

                    var pModel = pBll.GetModel(model.AssetId);
                    if (pModel == null)
                    {
                        pdaBll.Delete(gId, model.AssetId);
                        continue;
                    }

                    if (model.Status == (int)EnumPandianAssetStatus.盘盈)
                    {
                        pModel.IsDisable = false;
                    }
                    else
                    {
                        if (!model.LastUseDepmtId.Equals(Guid.Empty)) pModel.UseDepmtId = model.LastUseDepmtId;
                        if (!model.LastMgrDepmtId.Equals(Guid.Empty)) pModel.MgrDepmtId = model.LastMgrDepmtId;
                        if (!model.LastStoragePlaceId.Equals(Guid.Empty)) pModel.StoragePlaceId = model.LastStoragePlaceId;
                        if (!string.IsNullOrEmpty(model.LastUsePerson)) pModel.UsePersonName = model.LastUsePerson;
                        if (model.Status == (int)EnumPandianAssetStatus.未盘点) 
                        {
                            model.Status = (int)EnumPandianAssetStatus.盘亏;
                            pdaBll.Update(model);
                        }
                    }
                    pModel.Status = (int)EnumPandianAssetStatus.完成;

                    effect += pBll.Update(pModel);
                }

                pdInfo.Status = (int)EnumPandianStatus.已完成;
                pdBll.Update(pdInfo);

                if (effect < 1)
                {
                    if (isError) return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 基础数据

        #region 存放地点管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetCbbStoragePlace()
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

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStoragePlaceList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new StoragePlace();
                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();

                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    sqlWhere.Append("and (Named like @Keyword or Coded like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 256);
                    parm.Value = "%" + model.Keyword + "%";
                    parms.Add(parm);
                }

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveStoragePlace(StoragePlaceFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                var userId = WebCommon.GetUserId();
                var orgId = new Staff().GetOrgId(userId);
                var currTime = DateTime.Now;

                var modelInfo = new StoragePlaceInfo(Id, model.AppCode, userId, orgId, model.Coded, model.Named, currTime, currTime);

                var bll = new StoragePlace();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    var oldInfo = bll.GetModel(Id);
                    if (oldInfo == null) return ResResult.Response(false, MC.M_DataEmpty, "");
                    modelInfo.RecordDate = oldInfo.RecordDate;
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteStoragePlace(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new StoragePlace();

                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 资产分类管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetCategoryTree()
        {
            try
            {
                var bll = new Category();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveCategory(CategoryFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                if (string.IsNullOrWhiteSpace(model.Named) || string.IsNullOrWhiteSpace(model.AppCode)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);
                var currTime = DateTime.Now;
                var bll = new Category();
                int effect = 0;

                if (bll.IsExistCode(model.Coded, Id))
                {
                    return ResResult.Response(false, MC.GetString(MC.Params_CodeExistError, model.Coded), Id);
                }

                var modelInfo = new CategoryInfo(Id, model.AppCode, WebCommon.GetUserId(), Guid.Empty, parentId, model.Coded, model.Named, model.Step.Trim(','), model.Sort, model.Remark, currTime, currTime);
                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.新增, true);
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.Step = modelInfo.Id.ToString() + "," + modelInfo.Step;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteCategory(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                }

                var bll = new Category();
                if (bll.IsExistChild(Id)) return ResResult.Response(false, MC.M_DeleteTreeNodeError, null);

                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.删除, true);
                return ResResult.Response(bll.Delete(Id) > 0, "", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", null);
            }
        }

        #endregion

        #region 资产管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetProductRepairs(ListModel model)
        {
            try
            {
                var userId = WebCommon.GetUserId();
                var depmtId = new Staff().GetOrgId(userId);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();

                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    sqlWhere.Append("and (p.Named like @Keyword or p.Coded like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 256);
                    parm.Value = "%" + model.Keyword + "%";
                    parms.Add(parm);
                }

                int totalRecord = 0;
                var bll = new ProductRepair();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
                var data = new DataGrid<ProductRepairExtendInfo>();
                data.total = totalRecord;
                data.rows = list;

                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                return ResResult.Response(true, "", JsonConvert.SerializeObject(data, jsonSettings));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveProductRepair(ProductRepairFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                Guid productId = Guid.Empty;
                Guid.TryParse(model.ProductId, out productId);
                if (productId.Equals(Guid.Empty)) return ResResult.Response(false, MC.GetString(MC.P_SelectedInvalidError, "资产"), "");
                var userId = WebCommon.GetUserId();
                var orgId = new Staff().GetOrgId(userId);
                var status = (int)EnumProductStatus.待维修;
                if (!string.IsNullOrEmpty(model.StatusName)) status = EnumHelper.GetValue(typeof(EnumProductStatus), model.StatusName);
                var currTime = DateTime.Now;
                var modelInfo = new ProductRepairInfo(Id, model.AppCode, userId, orgId, productId,currTime, currTime);

                var pBll = new Product();
                var productInfo = pBll.GetModel(productId);
                if (productInfo == null) return ResResult.Response(false, MC.GetString(MC.Params_Data_NotExist, productId.ToString()), "");

                var bll = new ProductRepair();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    var oldInfo = bll.GetModel(Id);
                    if (oldInfo == null) return ResResult.Response(false, MC.Data_NotExist, "");
                    modelInfo.RecordDate = oldInfo.RecordDate;
                    modelInfo.LastUpdatedDate = currTime;
                    effect = bll.Update(modelInfo);
                }

                productInfo.Status = status;
                productInfo.LastUpdatedDate = currTime;
                pBll.Update(productInfo);

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteProductRepair(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new ProductRepair();

                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetProductList(ListModel model)
        {
            try
            {
                var userId = WebCommon.GetUserId();
                var depmtId = new Staff().GetOrgId(userId);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                var sqlWhere = new StringBuilder(300);
                var parms = new ParamsHelper();

                if (!(HttpContext.Current.User.IsInRole("Administrators") || HttpContext.Current.User.IsInRole("System")))
                {
                    var Profile = new CustomProfileCommon();
                    var sUserRule = Profile.UserRule;
                    var userRules = sUserRule.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (userRules.Length > 0 && userRules[userRules.Length - 1] != depmtId.ToString())
                    {
                        sqlWhere.Append("and MgrDepmtId = @MgrDepmtId ");
                        var parm = new SqlParameter("@MgrDepmtId", SqlDbType.UniqueIdentifier);
                        parm.Value = depmtId;
                        parms.Add(parm);
                    }
                    sqlWhere.AppendFormat("and CHARINDEX(convert(varchar(36),p.MgrDepmtId),'{0}') > 0 ", sUserRule);
                }

                if (!string.IsNullOrEmpty(model.Keyword))
                {
                    sqlWhere.Append("and (p.Named like @Keyword or p.Coded like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 256);
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

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveProduct(ProductFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                Guid categoryId = Guid.Empty;
                if (model.CategoryId != null) Guid.TryParse(model.CategoryId.ToString(), out categoryId);
                if (categoryId.Equals(Guid.Empty)) return ResResult.Response(false, MC.GetString(MC.P_SelectedInvalidError, "资产分类"), "");

                Guid useDepmtId = Guid.Empty;
                if (model.UseDepmtId != null) Guid.TryParse(model.UseDepmtId.ToString(), out useDepmtId);
                Guid mgrDepmtId = Guid.Empty;
                if (model.MgrDepmtId != null) Guid.TryParse(model.MgrDepmtId.ToString(), out mgrDepmtId);
                Guid storagePlaceId = Guid.Empty;
                if (model.StoragePlaceId != null) Guid.TryParse(model.StoragePlaceId.ToString(), out storagePlaceId);
                var buyDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.BuyDate)) DateTime.TryParse(model.BuyDate, out buyDate);
                if (buyDate == DateTime.MinValue) buyDate = DateTime.Parse("1754-01-01");
                var userId = WebCommon.GetUserId();
                var orgId = new Staff().GetOrgId(userId);
                var status = (int)EnumProductStatus.正常;
                if (!string.IsNullOrEmpty(model.StatusName)) status = EnumHelper.GetValue(typeof(EnumProductStatus), model.StatusName);
                var currTime = DateTime.Now;
                var modelInfo = new ProductInfo(model.AppCode, userId, orgId, Id, categoryId, model.Coded, model.Named, string.Empty, model.SpecModel, model.Qty, model.Price, model.Amount, model.MeterUnit, model.PieceQty, model.Pattr, model.SourceFrom, model.Supplier, buyDate, model.EnableDate, model.UseDateLimit, useDepmtId, model.UsePersonName, mgrDepmtId, storagePlaceId, model.Remark, status, model.Sort, false, currTime, currTime);

                var categoryInfo = new Category().GetModel(categoryId);
                if (categoryInfo == null) return ResResult.Response(false, MC.GetString(MC.Params_Data_NotExist, "资产分类"), "");

                var bll = new Product();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    modelInfo.Coded = bll.GetRndCode(categoryInfo.Coded, 8);
                    modelInfo.Barcode = modelInfo.Coded;
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    var oldInfo = bll.GetModel(Id);
                    if (oldInfo == null) return ResResult.Response(false, MC.Data_NotExist, "");
                    if (string.IsNullOrEmpty(oldInfo.Coded))
                    {
                        modelInfo.Coded = bll.GetRndCode(categoryInfo.Coded, 8);
                        modelInfo.Barcode = modelInfo.Coded;
                    }
                    if (string.IsNullOrEmpty(modelInfo.Barcode)) modelInfo.Barcode = modelInfo.Coded;
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteProduct(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new Product();

                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 内容类别

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetContentTypeTree()
        {
            try
            {
                var bll = new ContentType();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveContentType(ContentTypeFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                if (string.IsNullOrWhiteSpace(model.Named) || string.IsNullOrWhiteSpace(model.AppCode)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);
                var currTime = DateTime.Now;
                var bll = new ContentType();
                int effect = 0;

                if (bll.IsExistCode(model.Coded, Id))
                {
                    return ResResult.Response(false, MC.GetString(MC.Params_CodeExistError, model.Coded), Id);
                }

                var modelInfo = new ContentTypeInfo(model.AppCode, WebCommon.GetUserId(), Guid.Empty, Id, parentId, model.Coded, model.Named, model.Step.Trim(','), model.FlagName, model.Sort, model.Remark, currTime, currTime);
                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.新增, true);
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.Step = modelInfo.Id.ToString() + "," + modelInfo.Step;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteContentType(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                }

                var bll = new ContentType();
                if (bll.IsExistChild(Id)) return ResResult.Response(false, MC.M_DeleteTreeNodeError, null);

                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.删除, true);
                return ResResult.Response(bll.Delete(Id) > 0, "", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", null);
            }
        }

        #endregion

        #region 内容明细

        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //public ResResultModel GetContentDetailList(ListModel model)
        //{
        //    try
        //    {
        //        if (model.PageIndex < 1) model.PageIndex = 1;
        //        if (model.PageSize < 1) model.PageSize = 10;
        //        int totalRecord = 0;
        //        var bll = new ContentDetail();

        //        IList<ContentDetailInfo> list = null;

        //        StringBuilder sqlWhere = null;
        //        ParamsHelper parms = null;
        //        if (!string.IsNullOrWhiteSpace(model.Keyword))
        //        {
        //            sqlWhere = new StringBuilder(1000);
        //            parms = new ParamsHelper();
        //            sqlWhere.Append("and (p.Title+p.Keyword+p.Descr) like @Keyword ");
        //            parms.Add(new SqlParameter("@Keyword", "%" + model.Keyword + "%"));
        //        }
        //        var contentTypeId = Guid.Empty;
        //        if (model.ParentId != null && Guid.TryParse(model.ParentId.ToString(), out contentTypeId))
        //        {
        //            list = bll.GetListByContentType(model.PageIndex, model.PageSize, out totalRecord, contentTypeId, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
        //        }
        //        else
        //        {
        //            list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
        //        }

        //        return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResResult.Response(false, ex.Message, "");
        //    }
        //}

        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //public ResResultModel SaveContentDetail(ContentDetailFmModel model)
        //{
        //    try
        //    {
        //        if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
        //        if (string.IsNullOrWhiteSpace(model.AppCode) || string.IsNullOrWhiteSpace(model.Title)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
        //        Guid Id = Guid.Empty;
        //        if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
        //        Guid contentTypeId = Guid.Empty;
        //        if (model.ContentTypeId != null) Guid.TryParse(model.ContentTypeId.ToString(), out contentTypeId);
        //        var userId = WebCommon.GetUserId();
        //        var currTime = DateTime.Now;
        //        var modelInfo = new ContentDetailInfo(model.AppCode, Id, userId, contentTypeId, model.Title,model.Keyword,model.Descr,model.ContentText,model.Openness,model.Sort,currTime,currTime);

        //        var bll = new ContentDetail();
        //        int effect = -1;

        //        if (Id.Equals(Guid.Empty))
        //        {
        //            modelInfo.Id = Guid.NewGuid();
        //            effect = bll.InsertByOutput(modelInfo);
        //        }
        //        else
        //        {
        //            effect = bll.Update(modelInfo);
        //        }
        //        if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

        //        return ResResult.Response(true, "", modelInfo.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResResult.Response(false, ex.Message, "");
        //    }
        //}

        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //public ResResultModel DeleteContentDetail(string itemAppend)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
        //        var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //        var bll = new ContentDetail();

        //        if (bll.DeleteBatch((IList<object>)items.ToList<object>()))
        //        {
        //            foreach(var item in items)
        //            {
        //                DeleteContentFile(item);
        //            }
        //        }

        //        return ResResult.Response(true, MC.M_Save_Ok, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResResult.Response(false, ex.Message, "");
        //    }
        //}

        #endregion

        #region 内容文件

        //[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //public ResResultModel GetContentFilesByContentId(object contentId)
        //{
        //    try
        //    {
        //        var sContentId = Guid.Empty;
        //        if (contentId != null) Guid.TryParse(contentId.ToString(), out sContentId);
        //        if(sContentId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
        //        var bll = new ContentFile();

        //        var list = bll.GetListByJoin(string.Format("and ContentId = '{0}'",contentId), null);

        //        return ResResult.Response(true, "", "{\"total\":" + list.Count + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResResult.Response(false, ex.Message, "");
        //    }
        //}

        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //public ResResultModel DeleteContentFile(object Id)
        //{
        //    try
        //    {
        //        var id = Guid.Empty;
        //        if (Id != null) Guid.TryParse(Id.ToString(), out id);
        //        if(id.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");

        //        var bll = new ContentFile();
        //        var model = bll.GetModel(id);
        //        if(model != null)
        //        {
        //            if(bll.Delete(id) > 0)
        //            {
        //                var dir = Path.GetDirectoryName(FilesHelper.GetFullPath(model.FileUrl));
        //                Task.Factory.StartNew(() =>
        //                {
        //                    Directory.Delete(dir, true);
        //                });
        //            }
        //        }

        //        return ResResult.Response(true, MC.M_Save_Ok, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        return ResResult.Response(false, ex.Message, "");
        //    }
        //}

        #endregion

        #endregion

        #region 组织机构管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetOrgDepmtTree()
        {
            try
            {
                var bll = new OrgDepmt();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;
                //Auth.CreateSearchItem(ref sqlWhere, ref parms, new string[] { "Id" });
                return ResResult.Response(true, "", bll.GetTreeJson(sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray()));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrgDepmt(OrgFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                if (string.IsNullOrWhiteSpace(model.Coded) || string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);
                var currTime = DateTime.Now;
                var bll = new OrgDepmt();
                int effect = 0;

                if (bll.IsExistCode(model.Coded, Id))
                {
                    return ResResult.Response(false, MC.GetString(MC.Params_CodeExistError, model.Coded), Id);
                }

                var modelInfo = new OrgDepmtInfo(Id, model.AppCode, WebCommon.GetUserId(), Guid.Empty, parentId, model.Coded, model.Named, model.Step.Trim(','), model.Sort, model.Remark, currTime, currTime);
                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.新增, true);
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.Step = modelInfo.Id.ToString() + "," + modelInfo.Step;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrgDepmt(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                }

                var bll = new OrgDepmt();
                if (bll.IsExistChild(Id)) return ResResult.Response(false, MC.M_DeleteTreeNodeError, null);

                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.删除, true);
                return ResResult.Response(bll.Delete(Id) > 0, "", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", null);
            }
        }

        #endregion

        #region 员工管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStaffList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new Staff();

                IList<StaffInfo> list = null;

                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sqlWhere = new StringBuilder(1000);
                    parms = new ParamsHelper();
                    sqlWhere.Append("and (s.Coded+s.Phone) like @Keyword ");
                    parms.Add(new SqlParameter("@Keyword", "%" + model.Keyword + "%"));
                }
                var orgId = Guid.Empty;
                if (model.ParentId != null && Guid.TryParse(model.ParentId.ToString(), out orgId))
                {
                    list = bll.GetListByOrg(model.PageIndex, model.PageSize, out totalRecord, orgId, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
                }
                else
                {
                    list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
                }

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveStaff(StaffFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Coded)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                Guid Id = Guid.Empty;
                if (model.UserId != null) Guid.TryParse(model.UserId.ToString(), out Id);
                Guid orgId = Guid.Empty;
                if (model.OrgId != null) Guid.TryParse(model.OrgId.ToString(), out orgId);
                //var userId = WebCommon.GetUserId();
                var currTime = DateTime.Now;
                var modelInfo = new StaffInfo(Id, model.AppCode, model.Coded, model.UserName, model.Phone, model.Sort, model.Remark, currTime, currTime);
                modelInfo.OrgId = orgId;

                var bll = new Staff();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MembershipCreateStatus status;
                    MembershipUser user;
                    string[] roles = { "Users" };
                    user = Membership.GetUser(model.Coded);
                    if (user != null) return ResResult.Response(false, MC.GetString(MC.Login_ExistName, model.Coded), null);
                    user = Membership.CreateUser(model.Coded, model.Password, model.Email, null, null, true, out status);
                    if (user == null) return ResResult.Response(false, MC.M_Save_Error, null);
                    Roles.AddUserToRoles(model.Coded, roles);
                    modelInfo.UserId = Guid.Parse(user.ProviderUserKey.ToString());
                    bll.InsertOrgStaff(modelInfo);
                    effect = 1;
                }
                else
                {
                    MembershipUser user;
                    user = Membership.GetUser(model.Coded);
                    user.Email = model.Email;
                    Membership.UpdateUser(user);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", modelInfo.UserId);
            }
            catch (CustomException ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteStaff(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new Staff();
                var userId = Guid.Parse(items[0]);
                var user = Membership.GetUser(userId);
                if (user != null) Membership.DeleteUser(user.UserName, true);
                if (bll.DeleteStaff(userId) < 1)
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 数据字典

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetDicsTree()
        {
            try
            {
                var bll = new Dics();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveDics(DicsFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                if (string.IsNullOrWhiteSpace(model.Named) || string.IsNullOrWhiteSpace(model.AppCode)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);
                var currTime = DateTime.Now;
                var bll = new Dics();
                int effect = 0;

                if (bll.IsExistCode(model.Coded, Id))
                {
                    return ResResult.Response(false, MC.GetString(MC.Params_CodeExistError, model.Coded), Id);
                }

                var modelInfo = new DicsInfo(model.AppCode, WebCommon.GetUserId(), Guid.Empty, Id, parentId, model.Coded, model.Named, model.Step.Trim(','), model.FlagName, model.Sort, model.Remark, currTime, currTime);
                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.新增, true);
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.Step = modelInfo.Id.ToString() + "," + modelInfo.Step;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteDics(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                }

                var bll = new Dics();
                if (bll.IsExistChild(Id)) return ResResult.Response(false, MC.M_DeleteTreeNodeError, null);

                MenusDataProxy.ValidateAccess((int)EnumOperationAccess.删除, true);
                return ResResult.Response(bll.Delete(Id) > 0, "", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", null);
            }
        }

        #endregion

        #region 系统管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetFeatureUserInfo(string username, string typeName)
        {
            try
            {
                var bll = new FeatureUser();
                var fuInfo = bll.GetModel(SecurityService.GetUserId(username), typeName);
                if (fuInfo == null) fuInfo = new FeatureUserInfo();
                return ResResult.Response(true, "", JsonConvert.SerializeObject(fuInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveFeatureUser(FeatureUserFmModel model)
        {
            try
            {
                var featureId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(model.FeatureId)) Guid.TryParse(model.FeatureId, out featureId);
                var userId = SecurityService.GetUserId(model.UserName);

                var fuBll = new FeatureUser();
                fuBll.DoFeatureUser(userId, featureId, model.TypeName, true);

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 图片、文件管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetSitePictureList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new SitePicture();

                var list = bll.GetCbbList(model.PageIndex, model.PageSize, out totalRecord, model.Keyword);
                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteSitePicture(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var bll = new SitePicture();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion
    }
}
