<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddStaff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建/编辑员工信息</title>
</head>
<body>
    <form id="dlgFm" runat="server"> 
        <div class="row">
            <span class="rl w100"><span class="cr">*</span>员工编号：</span>
            <div class="fl w740">
                <input tabindex="1" id="txtCode" class="easyui-textbox" data-options="required:true" style="width: 100%;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl w100"><span class="cr">*</span>姓名：</span>
            <div class="fl w740">
                <input tabindex="2" id="txtName" class="easyui-textbox" data-options="required:true" style="width: 100%;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl w100"><span class="cr">*</span>登录密码：</span>
            <div class="fl w740">
                <input tabindex="3" id="txtPsw" class="easyui-textbox" data-options="required:true,validType:'psw'" style="width: 100%;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl w100">电子邮箱：</span>
            <div class="fl w740">
                <input tabindex="4" id="txtEmail" class="easyui-textbox" data-options="validType:'email'" style="width: 100%;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl w100">手机号：</span>
            <div class="fl w740">
                <input tabindex="5" id="txtPhone" class="easyui-textbox" data-options="validType:'mobilePhone'" style="width: 100%;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl w100">排序：</span><div class="fl">
                <input tabindex="6" id="txtSort" class="easyui-textbox w200" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row mt10"><span class="rl w100">备注：</span>
            <div class="fl w740">
                <input tabindex="7" id="txtRemark" class="easyui-textbox" data-options="multiline:true" style="width: 100%;height:80px;" />
            </div>
        </div>
        <span class="clr"></span>

        <input type="hidden" id="hId" />
        <input type="hidden" id="hOrgId" />
    </form>
</body>
</html>
