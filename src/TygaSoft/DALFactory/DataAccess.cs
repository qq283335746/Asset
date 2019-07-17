using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using TygaSoft.IDAL;

namespace TygaSoft.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        #region 打印、条码

        public static IBarcodeTemplate CreateBarcodeTemplate()
        {
            string className = path + ".BarcodeTemplate";
            return (IBarcodeTemplate)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 盘点

        public static IPandian CreatePandian()
        {
            string className = path + ".Pandian";
            return (IPandian)Assembly.Load(path).CreateInstance(className);
        }

        public static IPandianAsset CreatePandianAsset()
        {
            string className = path + ".PandianAsset";
            return (IPandianAsset)Assembly.Load(path).CreateInstance(className);
        } 

        #endregion

        #region 公共

        public static ISitePicture CreateSitePicture()
        {
            string className = path + ".SitePicture";
            return (ISitePicture)Assembly.Load(path).CreateInstance(className);
        }

        public static IFeatureUser CreateFeatureUser()
        {
            string className = path + ".FeatureUser";
            return (IFeatureUser)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 订单

        #endregion

        #region 基础数据

        public static ICustomer CreateCustomer()
        {
            string className = path + ".Customer";
            return (ICustomer)Assembly.Load(path).CreateInstance(className);
        }

        public static IProductRepair CreateProductRepair()
        {
            string className = path + ".ProductRepair";
            return (IProductRepair)Assembly.Load(path).CreateInstance(className);
        }

        public static ICategory CreateCategory()
        {
            string className = path + ".Category";
            return (ICategory)Assembly.Load(path).CreateInstance(className);
        }

        public static IProduct CreateProduct()
        {
            string className = path + ".Product";
            return (IProduct)Assembly.Load(path).CreateInstance(className);
        }

        public static IStoragePlace CreateStoragePlace()
        {
            string className = path + ".StoragePlace";
            return (IStoragePlace)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 数据字典

        public static IDics CreateDics()
        {
            string className = path + ".Dics";
            return (IDics)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 组织机构管理

        public static IOrgDepmt CreateOrgDepmt()
        {
            string className = path + ".OrgDepmt";
            return (IOrgDepmt)Assembly.Load(path).CreateInstance(className);
        }

        public static IStaff CreateStaff()
        {
            string className = path + ".Staff";
            return (IStaff)Assembly.Load(path).CreateInstance(className);
        } 

        #endregion

        #region 系统管理

        public static IContentType CreateContentType()
        {
            string className = path + ".ContentType";
            return (IContentType)Assembly.Load(path).CreateInstance(className);
        } 

        public static ISiteMulti CreateSiteMulti()
        {
            string className = path + ".SiteMulti";
            return (ISiteMulti)Assembly.Load(path).CreateInstance(className);
        }

        public static ISiteMenus CreateSiteMenus()
        {
            string className = path + ".SiteMenus";
            return (ISiteMenus)Assembly.Load(path).CreateInstance(className);
        }

        public static ISiteMenusAccess CreateSiteMenusAccess()
        {
            string className = path + ".SiteMenusAccess";
            return (ISiteMenusAccess)Assembly.Load(path).CreateInstance(className);
        }

        #endregion

        #region 成员资格

        public static ISiteUsers CreateSiteUsers()
        {
            string className = path + ".SiteUsers";
            return (ISiteUsers)Assembly.Load(path).CreateInstance(className);
        }
        public static ISiteMembers CreateSiteMembers()
        {
            string className = path + ".SiteMembers";
            return (ISiteMembers)Assembly.Load(path).CreateInstance(className);
        }
        public static ISiteRoles CreateSiteRoles()
        {
            string className = path + ".SiteRoles";
            return (ISiteRoles)Assembly.Load(path).CreateInstance(className);
        }
        public static IUsersInRoles CreateUsersInRoles()
        {
            string className = path + ".UsersInRoles";
            return (IUsersInRoles)Assembly.Load(path).CreateInstance(className);
        }
        public static IApplications CreateApplications()
        {
            string className = path + ".Applications";
            return (IApplications)Assembly.Load(path).CreateInstance(className);
        }

        #endregion
    }
}
