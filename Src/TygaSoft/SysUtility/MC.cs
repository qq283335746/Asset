using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysUtility
{
    public static class MC
    {
        public static string GetString(string strString)
        {
            return strString;
        }
        public static string GetString(string strString, string param1)
        {
            return string.Format(strString, param1);
        }
        public static string GetString(string strString, string param1, string param2)
        {
            return string.Format(strString, param1, param2);
        }
        public static string GetString(string strString, string param1, string param2, string param3)
        {
            return string.Format(strString, param1, param2, param3);
        }

        public const string M_Save_Ok = "操作成功！";
        public const string M_Save_Error = "操作失败，请稍后再重试！";
        public const string M_Order_NotExist = "请先保存单据信息！";
        public const string M_RuleInvalidError = "不符合规则，无法操作！";
        public const string M_QtyInvalidError = "数量超出了范围！";
        public const string M_NotExistDetailError = "未找到任何相关明细数据，请正确操作！";
        public const string M_NotConfigError = "未找到相关配置，请检查！";
        public const string M_SysDataChangedError = "当前操作包含系统预设的数据，不能更改系统预设的数据！";
        public const string M_StockProductInvalidError = "库存数据存在异常，请检查！";
        public const string M_ParamsInvalidError = "有“*”标识的为必填项，请检查！";
        public const string M_UploadFileNotExist = "未找到任何可上传的文件，请正确操作！";
        public const string M_UploadFileDataNotExist = "上传的文件内容不能为空字符串！";
        public const string M_DataEmpty = "未找到相关数据记录！";
        public const string M_DeleteTreeNodeError = "该节点下存在子节点或相关联数据，请先删除子节点或相关联数据再删除该节点！";
        public const string M_DeletePandianAssetError = "该数据已处理，不允许删除！";
        public const string M_BarcodeTemplateDefaultNotExistError = "未找到默认条码标签模板，请先设置默认模板！";
        public const string M_DataRightInvalidError = "您无操作权限！";

        public const string Re_IsCode = "^[a-zA-Z0-9]{1,36}$";

        public const string Params_CodeExistError = "代码“{0}”已经存在，请务必保持唯一值！";
        public const string Params_Data_NotExist = "“{0}”对应数据不存在或已被删除！";
        public const string Params_SwitchNameNotExist = "找不到名称为“{0}”！";
        public const string Params_ExistDetailError = "{0}存在明细信息，请先删除明细信息后再删除该数据！";
        public const string Params_SaveRoleAccessError = "不能对角色为“{0}”进行此操作！";
        public const string Params_SaveUserAccessError = "用户“{0}”属于“Administrators”的角色，说明该用户已具有“Administrators”的所有权限！";
        public const string Params_FeatureUserExistError = "用户“{0}”已分配，不能重复对一个用户多次分配！";
        public const string Params_UserNotExistError = "用户“{0}”不存在！";
        public const string Params_DeleteExistChildError = "请先删除其相关联的{0}信息再删除其信息！";
        public const string P_SelectedInvalidError = "请选择{0}！";
        public const string P_InvalidError = "{0}无效！";
        public const string P_OrgRightInvalidError = "操作失败，您没有操作“{0}”部门的权限！";

        public const string Data_ExistError = "已存在相同数据记录，请勿重复操作！";
        public const string Data_NotExist = "数据不存在或已被删除！";
        public const string Data_InvalidExist = "暂无数据，可能原因：当前登录用户无权限访问或数据不存在！";

        public const string AlertTitle_Info = "温馨提醒";
        public const string AlertTitle_Error = "错误提示";
        public const string AlertTitle_Sys_Info = "系统提示";
        public const string AlertTitle_Ex_Error = "异常提示";

        public const string Request_InvalidError = "非法操作，已禁止执行！";
        public const string Request_InvalidStaffBasic = "请先完善人员基本信息！";
        public const string Request_InvalidArgument = "获取{0}的值为空字符串或格式不正确，请检查！";
        public const string Request_NotExist = "{0}不存在或已被删除！";
        public const string Request_InvalidCompareToPassword = "前后输入密码不一致！";
        public const string Request_Params_InvalidError = "请求参数值不正确！";
        public const string Request_InvalidQty = "数量“{0}”超出了范围！";

        public const string Response_Ok = "调用成功！";

        public const string Role_InvalidError = "对不起，您木有操作权限，请联系管理员！";

        public const string Import_InvalidError = "带有“*”的列为必填项，请正确操作";
        public const string Import_NotDataError = "无任何可导入的数据，请下载导入模板并填写信息后再导入！";

        public const string Login_NotExist = "请先登录！";
        public const string Login_NotIsLockedOutError = "只有“已锁定”的用户才能执行此操作！";
        public const string Login_ExistName = "“{0}”已存在，请更换一个再重试！";
        public const string Login_InvalidAccount = "用户名或密码不正确！";
        public const string Login_InvalidVC = "验证码不正确！";
        public const string Login_InvalidVCCookie = "验证码不存在或已过期！";
        public const string Login_InvalidPsw = "密码不正确！";
        public const string Login_InvalidOldPsw = "当前密码不正确！";
        public const string Login_AccountLock = "您的账号已被锁定，请联系管理员先解锁后才能登录！";
        public const string Login_AccountAllow = "您的帐户尚未获得批准，请联系管理员！";
        public const string Login_InvalidPassword = "密码应由数字、字母、特殊字符（不包括“&”）组成，且是6-30位的字符串！";
        public const string PasswordStrengthRegularExpression = @"^[a-zA-Z0-9_\@\-\!\#\$\%\^\*\.\~]{6,30}$";
    }
}
