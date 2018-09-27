using System;
using System.IO;
using System.ServiceModel;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.PdaService")]
    public interface IPda
    {
        [OperationContract(Name = "GetHelloWord")]
        string GetHelloWord();

        [OperationContract(Name = "Login")]
        ResResultModel Login(LoginFmModel model);

        #region 盘点

        [OperationContract(Name = "GetPandianList")]
        ResResultModel GetPandianList(PdaPandianModel model);

        [OperationContract(Name = "GetPandianAssetList")]
        ResResultModel GetPandianAssetList(PdaPandianAssetModel model);

        [OperationContract(Name = "GetPandianAssetByBarcode")]
        ResResultModel GetPandianAssetByBarcode(string appKey, string userName, object pandianId, string barcode);

        [OperationContract(Name = "GetPanYingBarcode")]
        ResResultModel GetPanYingBarcode();

        [OperationContract(Name = "SavePandianDown")]
        ResResultModel SavePandianDown(PdaPandianFmModel model);

        [OperationContract(Name = "SavePandianAsset")]
        ResResultModel SavePandianAsset(PdaPandianAssetFmModel model);

        #endregion

        #region 资产分类

        [OperationContract(Name = "GetCategoryTree")]
        ResResultModel GetCategoryTree(AuthFmModel model);

        #endregion

        #region 资产

        [OperationContract(Name = "GetProducts")]
        ResResultModel GetProducts(ListModel model);

        #endregion

        #region 组织机构

        [OperationContract(Name = "GetOrgDepmtTree")]
        ResResultModel GetOrgDepmtTree(AuthFmModel model);

        #endregion

        #region 存放地点管理

        [OperationContract(Name = "GetCbbStoragePlace")]
        ResResultModel GetCbbStoragePlace(LoginFmModel model);

        #endregion
    }
}
