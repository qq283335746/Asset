using System;
using System.ServiceModel;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.Service")]
    public interface IService
    {
        #region 打印、条码

        [OperationContract(Name = "GetBarcodeTemplateInfoByDefault")]
        ResResultModel GetBarcodeTemplateInfoByDefault();

        [OperationContract(Name = "GetBarcodeFormats")]
        ResResultModel GetBarcodeFormats();

        [OperationContract(Name = "GetBarcodeBrowser")]
        ResResultModel GetBarcodeBrowser(BarcodeTemplateFmModel model);

        [OperationContract(Name = "GetBarcodeTemplateList")]
        ResResultModel GetBarcodeTemplateList(ListModel model);

        [OperationContract(Name = "SaveBarcodeTemplate")]
        ResResultModel SaveBarcodeTemplate(BarcodeTemplateFmModel model);

        [OperationContract(Name = "DeleteBarcodeTemplate")]
        ResResultModel DeleteBarcodeTemplate(string itemAppend);

        [OperationContract(Name = "SaveSetDefault")]
        ResResultModel SaveSetDefault(BarcodeTemplateFmModel model);

        #endregion

        #region 盘点

        [OperationContract(Name = "GetPandianInfo")]
        ResResultModel GetPandianInfo(string id);

        [OperationContract(Name = "GetPandianList")]
        ResResultModel GetPandianList(ListModel model);

        [OperationContract(Name = "DeletePandian")]
        ResResultModel DeletePandian(string itemAppend);

        [OperationContract(Name = "SavePandian")]
        ResResultModel SavePandian(PandianAssetFmModel model);

        [OperationContract(Name = "GetPandianAssetList")]
        ResResultModel GetPandianAssetList(ListModel model);

        [OperationContract(Name = "DeletePandianAsset")]
        ResResultModel DeletePandianAsset(string pandianId, string itemAppend);

        [OperationContract(Name = "SavePandianAssetResult")]
        ResResultModel SavePandianAssetResult(string pandianId);

        #endregion

        #region 基础数据

        #region 存放地点管理

        [OperationContract(Name = "GetCbbStoragePlace")]
        ResResultModel GetCbbStoragePlace();

        [OperationContract(Name = "GetStoragePlaceList")]
        ResResultModel GetStoragePlaceList(ListModel model);

        [OperationContract(Name = "SaveStoragePlace")]
        ResResultModel SaveStoragePlace(StoragePlaceFmModel model);

        [OperationContract(Name = "DeleteStoragePlace")]
        ResResultModel DeleteStoragePlace(string itemAppend);

        #endregion

        #region 内容类别

        [OperationContract(Name = "GetContentTypeTree")]
        ResResultModel GetContentTypeTree();

        [OperationContract(Name = "SaveContentType")]
        ResResultModel SaveContentType(ContentTypeFmModel model);

        [OperationContract(Name = "DeleteContentType")]
        ResResultModel DeleteContentType(Guid Id);

        #endregion

        #region 内容明细

        //[OperationContract(Name = "GetContentDetailList")]
        //ResResultModel GetContentDetailList(ListModel model);

        //[OperationContract(Name = "SaveContentDetail")]
        //ResResultModel SaveContentDetail(ContentDetailFmModel model);

        //[OperationContract(Name = "DeleteContentDetail")]
        //ResResultModel DeleteContentDetail(string itemAppend);

        #endregion

        #region 内容文件

        //[OperationContract(Name = "GetContentFilesByContentId")]
        //ResResultModel GetContentFilesByContentId(object contentId);

        //[OperationContract(Name = "DeleteContentFile")]
        //ResResultModel DeleteContentFile(object Id);

        #endregion

        #endregion

        #region 资产分类管理

        [OperationContract(Name = "GetCategoryTree")]
        ResResultModel GetCategoryTree();

        [OperationContract(Name = "SaveCategory")]
        ResResultModel SaveCategory(CategoryFmModel model);

        [OperationContract(Name = "DeleteCategory")]
        ResResultModel DeleteCategory(Guid Id);

        #endregion

        #region 资产管理

        [OperationContract(Name = "GetProductRepairs")]
        ResResultModel GetProductRepairs(ListModel model);

        [OperationContract(Name = "SaveProductRepair")]
        ResResultModel SaveProductRepair(ProductRepairFmModel model);

        [OperationContract(Name = "DeleteProductRepair")]
        ResResultModel DeleteProductRepair(string itemAppend);

        [OperationContract(Name = "GetProductList")]
        ResResultModel GetProductList(ListModel model);

        [OperationContract(Name = "SaveProduct")]
        ResResultModel SaveProduct(ProductFmModel model);

        [OperationContract(Name = "DeleteProduct")]
        ResResultModel DeleteProduct(string itemAppend);

        #endregion

        #region 组织机构管理

        [OperationContract(Name = "GetOrgDepmtTree")]
        ResResultModel GetOrgDepmtTree();

        [OperationContract(Name = "SaveOrgDepmt")]
        ResResultModel SaveOrgDepmt(OrgFmModel model);

        [OperationContract(Name = "DeleteOrgDepmt")]
        ResResultModel DeleteOrgDepmt(Guid Id);

        #endregion

        #region 员工管理

        [OperationContract(Name = "GetStaffList")]
        ResResultModel GetStaffList(ListModel model);

        [OperationContract(Name = "SaveStaff")]
        ResResultModel SaveStaff(StaffFmModel model);

        [OperationContract(Name = "DeleteStaff")]
        ResResultModel DeleteStaff(string itemAppend);

        #endregion

        #region 数据字典

        [OperationContract(Name = "GetDicsTree")]
        ResResultModel GetDicsTree();

        [OperationContract(Name = "SaveDics")]
        ResResultModel SaveDics(DicsFmModel model);

        [OperationContract(Name = "DeleteDics")]
        ResResultModel DeleteDics(Guid Id);

        #endregion

        #region 系统管理

        [OperationContract(Name = "GetFeatureUserInfo")]
        ResResultModel GetFeatureUserInfo(string username, string typeName);

        [OperationContract(Name = "SaveFeatureUser")]
        ResResultModel SaveFeatureUser(FeatureUserFmModel model);

        #endregion

        #region 图片、文件管理

        [OperationContract(Name = "GetSitePictureList")]
        ResResultModel GetSitePictureList(ListModel model);

        [OperationContract(Name = "DeleteSitePicture")]
        ResResultModel DeleteSitePicture(string itemAppend);

        #endregion

    }
}
