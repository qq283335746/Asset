
angular.module('ngTygaSoft.services.MC', [])

.factory('$tygasoftMC', function () {

    var ts = {};

    ts.GetString = function (s, m) {
        switch (s) {
            case 'Params_NotExist':
                return this.MC.Params_NotExist.replace('{0}', m);
            case 'Params_UploadCount':
                return this.MC.Params_UploadCount.replace('{0}', m);
            case 'Ex_Error':
                return this.MC.Ex_Error.replace('{0}', m);
            case 'Server_Data_TotalCount':
                return this.MC.Server_Data_TotalCount.replace('{0}', m);
            case 'Server_Data_ImportCount':
                return this.MC.Server_Data_ImportCount.replace('{0}', m);
            default:
                return "";
        }
    }

    ts.MC = {
        Params_NotExist: '“{0}”无效！',
        Params_UploadCount: '成功上传： {0} 条！',
        Ex_Error: '异常： {0}',
        Http_Err: '处理异常，请检查设备网络连接是否正常，稍后再重试！',
        Data_Empty: '暂无数据！',
        Data_Error: '数据存在异常，无法处理！',
        Data_ToServer_EmptyError: '无任何可上传的数据！',
        Data_SaveEmptyError: '无任何可保存的数据！',
        Server_Data_TotalCount: '获取到数据总条数： {0} 条',
        Btn_CancelText: '取消',
        Btn_OkText: '确定',
        Required_Password: '无效的密码，正确密码应由6-30位数字或字母组成！！',
        Required_CfmPassword: '前后两次输入密码不相等！',
        Required_Phone: '请正确输入您的手机号码！',
        Required_Login: '请先登录！',
        Required_PhonePsw_InvalidError: '请正确输入您的手机号码和密码（由6-30位数字或字母组成）！',
        Alert_Title: '提示',
        Confirm_Msg: '确定要执行此吗？',
        Confirm_Msg_Import: '确定要导入吗？',
        Confirm_Msg_Delete: '确定要删除吗？',
        Confirm_SaveToServer: '确定要上传到服务器吗？',
        Confirm_Export: '确定要导出数据吗？',
        Login_EmptyError: '用户名或密码不能为空字符串！',
        Login_InvalidError: '用户名或密码不正确！',
        Login_Ok: '登录成功，请稍后...',
        Response_Ok: '操作成功！',
        Response_Login_Ok: '登录成功！',
        Response_Register_Ok: '注册成功！',
        M_LoadingError: '数据加载异常！',
        M_ExError: '操作异常，请稍后再重试！',
        M_Login_Again: '您已登录，请勿重复操作！',
        M_Camera_Error: '请先拍照！',
        M_ExitApp: '再按一次离开',
        M_ExitApp_Content: '确定要退出吗？',
        M_Compare_Year: '开始年份不能大于结束年份，请正确选择年份！',
        M_Required_Error: '带有*符号的为必需项，请正确操作！',
        M_QtyInvalidError: '数量超出了范围！',
        M_QtyErrorConfirm: '数量超出了范围！是否继续？',
        M_TotalQtyInvalidError: '数量超出了范围！',
        M_PandianConfirm: '确定要保存并继续吗？',
        M_SaveConfirm: '确定要保存吗？',
        M_ServiceUrlEmpty: '您未设置接口地址，请先完成接口地址的设置！',
        M_EmptyError: '不能为空字符串！',
        M_Waiting: '请稍等...',
        M_Auth: '请先进行身份验证...',
        M_AuthOk: '恭喜您，认证成功！',
        M_AuthInvalidError: '该任务与身份认证信息不相符，属非法操作！',
        M_EmptyInvalidError: '请输入查询关键字！',
    };

    ts.DataStatus = {
        SysAdd: '预设',
        Import: '导入',
        Delete: '删除',
        Insert: '新增',
        Update: '修改',
        Scanning:'待扫描',
        Scanned: '已扫描',
        Finished: '已完成'
    };

    ts.EnumStep = {
        预收货 : 1, 收货 : 2, 上架 : 3, 发货 : 4
    };

    return ts;
});