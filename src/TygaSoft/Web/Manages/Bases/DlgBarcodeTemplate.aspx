<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarcodeTemplate.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.DlgBarcodeTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>条码模板设定</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
    </form>
    <div class="easyui-layout" data-options="fit:true,border:false">
        <div data-options="region:'west',title:'模板',split:true,border:false" style="width:400px;">
            <div id="dgBarcodeTemplateToolbar">
                <a id="lbtnSetDefault" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="BarcodeTemplate.OnSetDefault()"><span>设为默认模板</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="BarcodeTemplate.OnDel()"><span>删除</span></a>
            </div>
            <table id="dgBarcodeTemplate" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:false,rownumbers:true,singleSelect:true,border:false,striped:true,toolbar:'#dgBarcodeTemplateToolbar',onSelect:BarcodeTemplate.OnSelect">
                <thead>
                    <tr>
		                <th data-options="field: 'Id', checkbox: true"></th> 
                        <th data-options="field: 'Title', width:300">标题</th> 
                        <th data-options="field: 'IsDefault', width:80,formatter: function(value,row,index){return value == true ? '是':'否';}">是否默认</th> 
                    </tr>
                </thead>
            </table>
        </div>
        <div data-options="region:'center',title:'条码参数设置'" style="padding:5px;">

            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="BarcodeTemplate.OnSave()"><span>保存</span></a>
            <a id="lbtnSaveAs" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save',disabled:true" onclick="BarcodeTemplate.OnSaveAsTemplate()"><span>另保存为模板</span></a>
            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-large-picture'" onclick="BarcodeTemplate.OnBrowser()"><span>预览</span></a>

            <form id="BarcodeTemplateFm">
                <div style="margin:0 auto; margin-bottom:10px; height:238px;padding:10px;border:1px solid #ddd;text-align:center;">
                    <img id="imgBarcodeBrowser" />
                </div>
                <span class="clr"></span>
                <div class="row-fl"><span class="rl"><span class="f-cr">*</span> 条码类型：</span>
                    <div class="fl">
                        <input id="cbbBarcodeFormat" data-options="editable:false" style="width:160px;height:22px;" />
                    </div>
                </div>
                <div class="row-fl"><span class="rl" style="width:60px;">宽：</span>
                    <div class="fl">
                        <input id="txtWidth" class="easyui-validatebox" data-options="validType:'intNotZero'" style="width:50px;" />
                    </div>
                </div>
                <div class="row-fl"><span class="rl" style="width:60px;">高：</span>
                    <div class="fl">
                        <input id="txtHeight" class="easyui-validatebox" data-options="validType:'intNotZero'" style="width:50px;" />
                    </div>
                </div>
                <span class=clr></span>
                <div class="row-fl"><span class="rl"><span class="f-cr">*</span> 条码内容：</span>
                    <div class="fl">
                        <input id="txtBarcode" placeholder="123456789" style="width:160px;" />
                    </div>
                </div>
                <div class="row-fl"><span class="rl" style="width:80px;">外边距：</span>
                    <div class="fl">
                        <input id="txtMargin" class="easyui-validatebox" data-options="validType:'int'" style="width:50px;" />
                    </div>
                </div>
                <span class="clr"></span>
            </form>
        </div>
    </div>

<%--    <script type="text/javascript" src="/asset/Scripts/Manages/Bases/BarcodeTemplate.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            BarcodeTemplate.Init();
        })
    </script>
</body>
</html>
