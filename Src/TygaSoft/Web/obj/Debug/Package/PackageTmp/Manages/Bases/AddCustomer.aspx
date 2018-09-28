<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddCustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑客户信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row-fl">
            <span class="rl w110"><span class="cr">*</span> 客户编码：</span>
            <div class="fl"><input tabindex="1" id ="txtCoded" class="easyui-validatebox txt w200" data-options="required:true" /></div>
        </div> 
        <div class="row-fl"><span class="rl w110"><span class="cr">*</span> 客户名称：</span>
            <div class="fl"><input tabindex="2" id ="txtNamed" class="easyui-validatebox txt w200" data-options="required:true" /></div>
        </div> 
        <div class="row-fl"><span class="rl w110"><span class="cr">*</span> 客户简称：</span>
            <div class="fl"><input tabindex="3" id ="txtShortName" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w110">联系人：</span>
            <div class="fl"><input tabindex="4" id ="txtContactMan" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w110">电话：</span>
            <div class="fl"><input tabindex="5" id ="txtTelPhone" class="easyui-validatebox txt w200" data-options="validType:'telPhone'" /></div>
        </div>  
        <div class="row-fl"><span class="rl w110">地址：</span>
            <div class="fl"><input tabindex="6" id ="txtAddress" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w110">城市：</span>
            <div class="fl"><input tabindex="7" id ="txtCityName" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w110">行业：</span>
            <div class="fl"><input tabindex="8" id ="txtTradeName" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w110">合作时间：</span>
            <div class="fl"><input tabindex="9" id ="txtCooperateTime" class="easyui-datebox txt w200" style="height:20px;" /></div>
        </div>
        <div class="row-fl"><span class="rl w110">协议到期时间：</span>
            <div class="fl"><input tabindex="10" id ="txtAgreementTimeout" class="easyui-datebox txt w200" style="height:20px;" /></div></div> 
        <div class="row-fl"><span class="rl w110">合作价格：</span>
            <div class="fl"><input tabindex="11" id ="txtJoinPrice" class="easyui-validatebox txt w200" /></div>
        </div> 
        <div class="row-fl"><span class="rl w110">优惠：</span>
            <div class="fl"><input tabindex="12" id ="txtDiscountAbout" class="easyui-validatebox txt w200" /></div>
        </div> 
        <div class="row-fl"><span class="rl w110">付款方式：</span>
            <div class="fl"><input tabindex="13" id ="txtPayWay" class="txt w200" /></div></div> 
        <div class="row-fl"><span class="rl w110">服务工号：</span>
            <div class="fl"><input tabindex="14" id ="txtStaffCode" class="txt w200" /></div></div> 
        <input type="hidden" id="hId" />
        <span class="clr"></span>
        <div class="row mt10">
            <span class="rl w110">备注：</span>
            <div class="fl">
                <input tabindex="15" id="txtRemark" class="easyui-textbox txt" data-options="multiline:true" style="width:512px;" />
            </div>
        </div>
    </form>
</body>
</html>
