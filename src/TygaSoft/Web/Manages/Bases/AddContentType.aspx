<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContentType.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddContentType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建/编辑内容类型</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row">
            <span class="rl w100">所属上级：</span>
            <div class="fl">
                <span id="lbParentText"></span>
                <input type="hidden" id="hParentId" />
            </div>
        </div>
        <div class="row mt10"><span class="rl w100"><span class="cr">*</span> 代号：</span><div class="fl">
            <input tabindex="1" id="txtCoded" class="easyui-validatebox txt w200" data-options="required:true" /></div>
        </div>
        <div class="row mt10"><span class="rl w100"><span class="cr">*</span> 名称：</span><div class="fl">
            <input tabindex="2" id="txtNamed" class="easyui-validatebox txt w400" data-options="required:true" /></div>
        </div>
        <div class="row mt10">
            <span class="rl w100">排序：</span>
            <div class="fl">
                <input tabindex="3" id="txtSort" class="easyui-validatebox txt w200" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row mt10"><span class="rl w100">备注：</span>
            <div class="fl">
                <textarea tabindex="4" id="txtRemark" class="txt w400"></textarea>
            </div>
        </div>
        <input type="hidden" id="hId" />
        <span class="clr"></span>
    </form>
</body>
</html>
