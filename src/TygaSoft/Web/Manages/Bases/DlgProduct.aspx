<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgProduct.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.DlgProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择资产</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
    </form>

    <div id="dgDlgProductToolbar">
        <div class="fr">
            <input id="txtDlgKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:DlgProduct.OnSearch" style="width: 250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dgDlgProduct" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgDlgProductToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'Coded', width:100">资产编码</th>
                <th data-options="field: 'Named', width:150">资产名称</th>
                <th data-options="field: 'SpecModel', width:100">规格型号</th>
                <th data-options="field: 'Qty', width:100">数量</th>
                <th data-options="field: 'Price', width:100">单价</th>
                <th data-options="field: 'Amount', width:100">金额</th>
                <th data-options="field: 'MeterUnit', width:100">计量单位</th>
                <th data-options="field: 'Pattr', width:100">资产属性</th>
                <th data-options="field: 'SourceFrom', width:100">资产来源</th>
                <th data-options="field: 'UseOrgCode', width:100">使用部门编码</th>
                <th data-options="field: 'UseOrgName', width:180">使用部门名称</th>
                <th data-options="field: 'UsePersonName', width:100">使用人</th>
                <th data-options="field: 'MgrOrgCode', width:120">实物管理部门编码</th>
                <th data-options="field: 'MgrOrgName', width:180">实物管理部门名称</th>
                <th data-options="field: 'StoragePlaceCode', width:100">存放地点编码</th>
                <th data-options="field: 'StoragePlaceName', width:200">存放地点名称</th>
            </tr>
        </thead>
    </table>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/DlgProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            DlgProduct.Init();
        })
    </script>
</body>
</html>
