<%@ Page Title="资产清单" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ProductReport.aspx.cs" Inherits="TygaSoft.Web.Manages.Reports.ProductReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <div id="dgToolbar">
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
        <div id="mmExcel" style="width:150px;">
            <div onclick="Product.OnImport()">导入</div>
            <div onclick="Product.OnExport()">导出</div>
        </div>
        <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:Product.OnSearch" style="width: 250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'Coded', width:100">资产编码</th>
                <th data-options="field: 'Named', width:150">资产名称</th>
                <th data-options="field: 'CategoryCode', width:100">资产分类编码</th>
                <th data-options="field: 'CategoryName', width:150">资产分类</th>
                <th data-options="field: 'CategoryParentCode', width:100">资产大类编码</th>
                <th data-options="field: 'CategoryParentName', width:150">资产大类</th>
                <th data-options="field: 'SpecModel', width:100">规格型号</th>
                <th data-options="field: 'Qty', width:100">数量</th>
                <th data-options="field: 'Price', width:100">单价</th>
                <th data-options="field: 'Amount', width:100">金额</th>
                <th data-options="field: 'MeterUnit', width:100">计量单位</th>
                <th data-options="field: 'PieceQty', width:100,hidden:true">件数</th>
                <th data-options="field: 'Pattr', width:100">资产属性</th>
                <th data-options="field: 'SourceFrom', width:100">资产来源</th>
                <th data-options="field: 'Supplier', width:100">供应商</th>
                <th data-options="field: 'SBuyDate', width:100">购入日期</th>
                <th data-options="field: 'EnableDate', width:100">启用日期</th>
                <th data-options="field: 'UseDateLimit', width:100">使用期限</th>
                <th data-options="field: 'UseOrgCode', width:100">使用部门编码</th>
                <th data-options="field: 'UseOrgName', width:180">使用部门名称</th>
                <th data-options="field: 'UseUserName', width:100">使用人</th>
                <th data-options="field: 'MgrOrgCode', width:120">实物管理部门编码</th>
                <th data-options="field: 'MgrOrgName', width:180">实物管理部门名称</th>
                <th data-options="field: 'StoragePlaceCode', width:100">存放地点编码</th>
                <th data-options="field: 'StoragePlaceName', width:200">存放地点名称</th>
                <th data-options="field: 'UserName', width:100">登记人</th>
                <th data-options="field: 'SRecordDate', width:100">登记时间</th>
                <th data-options="field: 'StatusName', width:100">状态</th>
                <th data-options="field: 'Remark', width:100">备注</th>
            </tr>
        </thead>
    </table>
    <script type="text/javascript" src="/Asset/Scripts/DlgFiles.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/Product.js"></script>
    <script type="text/javascript">
        $(function () {
            Product.Init();
        })
    </script>
</asp:Content>
