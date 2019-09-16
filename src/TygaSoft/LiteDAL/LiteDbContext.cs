using System;
using LiteDB;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class LiteDbContext
    {
        public readonly LiteDatabase Context;

        public LiteDbContext(string filePath)
        {
            var db = new LiteDatabase(filePath);
            if (db != null)
                Context = db;
        }

        public LiteCollection<Yibi.LiteMembershipProvider.Entities.ApplicationsInfo> Applications => Context.GetCollection<Yibi.LiteMembershipProvider.Entities.ApplicationsInfo>("Applications");

        public LiteCollection<BarcodeTemplateInfo> BarcodeTemplates => Context.GetCollection<BarcodeTemplateInfo>("BarcodeTemplates");

        public LiteCollection<CategoryInfo> Categories => Context.GetCollection<CategoryInfo>("Categories");

        public LiteCollection<ContentTypeInfo> ContentTypes => Context.GetCollection<ContentTypeInfo>("ContentTypes");

        public LiteCollection<ProductInfo> Products => Context.GetCollection<ProductInfo>("Products");

        public LiteCollection<ProductRepairInfo> ProductRepairs => Context.GetCollection<ProductRepairInfo>("ProductRepairs");

        public LiteCollection<StoragePlaceInfo> StoragePlaces => Context.GetCollection<StoragePlaceInfo>("StoragePlaces");

        public LiteCollection<CustomerInfo> Customers => Context.GetCollection<CustomerInfo>("Customers");

        public LiteCollection<DicsInfo> Dics => Context.GetCollection<DicsInfo>("Dics");

        public LiteCollection<FeatureUserInfo> FeatureUsers => Context.GetCollection<FeatureUserInfo>("FeatureUsers");

        public LiteCollection<OrgDepmtInfo> OrgDepmts => Context.GetCollection<OrgDepmtInfo>("OrgDepmts");

        public LiteCollection<UserInOrgInfo> UserInOrg => Context.GetCollection<UserInOrgInfo>("UserInOrg");

        public LiteCollection<PandianInfo> Pandians => Context.GetCollection<PandianInfo>("Pandians");

        public LiteCollection<PandianAssetInfo> PandianAssets => Context.GetCollection<PandianAssetInfo>("PandianAssets");

        public LiteCollection<SiteMenusInfo> SiteMenus => Context.GetCollection<SiteMenusInfo>("SiteMenus");

        public LiteCollection<SiteMenusAccessInfo> SiteMenusAccess => Context.GetCollection<SiteMenusAccessInfo>("SiteMenusAccess");

        public LiteCollection<SiteMultiInfo> SiteMultis => Context.GetCollection<SiteMultiInfo>("SiteMultis");

        public LiteCollection<SitePictureInfo> SitePictures => Context.GetCollection<SitePictureInfo>("SitePictures");
    }
}
