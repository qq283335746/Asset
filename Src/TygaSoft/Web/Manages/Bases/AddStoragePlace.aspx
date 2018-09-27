<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStoragePlace.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddStoragePlace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增/编辑存放地点信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row">
            <span class="rl w110"><span class="cr">*</span> 存放地点编码：</span>
            <div class="fl">
                <input tabindex="4" id="txtCoded" class="easyui-validatebox w400" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl w110"><span class="cr">*</span> 存放地点：</span>
            <div class="fl">
                <input tabindex="5" id="txtNamed" class="easyui-validatebox w400" />
            </div>
        </div>

        <input type="hidden" id="hId" />
        <span class="clr"></span>
    </form>
</body>
</html>
