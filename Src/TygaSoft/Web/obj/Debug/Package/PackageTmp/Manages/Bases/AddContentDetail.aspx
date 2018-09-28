<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContentDetail.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddContentDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建/编辑产品信息</title>
</head>
<body>
    <div id="tabsContent" class="easyui-tabs" data-options="fit:true">
        <div title="基本信息" style="padding: 20px; display: none;">
            <form id="baseFm">
                <div class="row">
                    <span class="rl w100"><span class="cr">*</span> 标题：</span>
                    <div class="fl w740">
                        <input tabindex="1" id="txtTitle" class="easyui-textbox" data-options="required:true" style="width: 100%;" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl w100">简介：</span>
                    <div class="fl w740">
                        <input tabindex="2" id="txtDescr" class="easyui-textbox" data-options="multiline:true,height:60" style="width: 100%;" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl w100">关键字：</span>
                    <div class="fl w740">
                        <input tabindex="3" id="txtKeyword" class="easyui-textbox" data-options="prompt:'搜索关键字（使用“,”分割）'" style="width: 100%;" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl w100">排序：</span><div class="fl">
                        <input tabindex="4" id="txtSort" class="easyui-textbox w200" data-options="validType:'int'" />
                    </div>
                </div>
                <div class="row mt10">
                    <span class="rl w100">内容：</span>
                    <div class="fl w740">
                        <input tabindex="5" id="txtContent" class="easyui-textbox" data-options="multiline:true,height:200" style="width: 100%;" />
                    </div>
                </div>
            </form>
        </div>
        <div title="文件管理" style="padding: 10px;">
            <div id="dgFileToolbar">
                <a id="abtnUpload" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ContentDetail.DlgUpload()">上传文件</a>
                <div class="fr" style="display:none;">
                    <input id="txtSKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ContentDetail.OnSearch" style="width: 250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <table id="dgFile" class="easyui-datagrid" data-options="fit:true,fitColumns:true,rownumbers:true,singleSelect:true,toolbar:'#dgFileToolbar'">
                <thead>
                    <tr>
                        <th data-options="field:'FileName',width:200">文件名</th>
                        <th data-options="field:'FileSize',width:60">文件大小</th>
                        <th data-options="field:'DoBtns',width:100,formatter:ContentDetail.FDoBtns">操作</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <input type="hidden" id="hId" />
    <input type="hidden" id="hContentTypeId" />
    <span class="clr"></span>
    <form id="dlgContentFm" runat="server"> </form>

</body>
</html>
