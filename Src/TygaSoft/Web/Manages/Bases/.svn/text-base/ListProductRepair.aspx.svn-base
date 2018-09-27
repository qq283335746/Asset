<%@ Page Title="资产维修管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListProductRepair.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.ListProductRepair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ProductRepair.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ProductRepair.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ProductRepair.Del()"><span>删除</span></a>
	    <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ProductRepair.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
		        <th data-options="field: 'ProductRepairInfo.Id', checkbox: true,formatter: function (value, row, index) {
                    return row.ProductRepairInfo.Id;
                }"></th>
                <th data-options="field: 'ProductInfo.Coded', width:100,formatter: function (value, row, index) {
                    return row.ProductInfo.Coded;
                }">资产编码</th>
                <th data-options="field: 'ProductInfo.Named', width:150,formatter: function (value, row, index) {
                    return row.ProductInfo.Named;
                }">资产名称</th>
                <th data-options="field: 'ProductInfo.StatusName', width:100,formatter: function (value, row, index) { 
                    return row.ProductInfo.StatusName;
                }">状态</th>
                <th data-options="field: 'ProductRepairInfo.UserName', width:100,formatter: function (value, row, index) { 
                    return row.ProductRepairInfo.UserName;
                }">操作人</th>
                <th data-options="field: 'ProductRepairInfo.LastUpdatedDate', width:100,formatter: function (value, row, index) { 
                    return row.ProductRepairInfo.LastUpdatedDate;
                }">操作时间</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/DlgProduct.js?v=20180123001"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/ProductRepair.js?v=20180123001"></script>
    <script type="text/javascript">
        $(function () {
            ProductRepair.Init();
        })
    </script>

</asp:Content>
