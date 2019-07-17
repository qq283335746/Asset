<%@ Page Title="存放地点管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListStoragePlace.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.ListStoragePlace" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="StoragePlace.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="StoragePlace.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="StoragePlace.Del()"><span>删除</span></a>
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
        <div id="mmExcel" style="width:150px;">
            <div><a href="/asset/Files/Template/存放地点导入模板.xlsx">下载导入模板</a></div>
            <div onclick="StoragePlace.OnImport()">导入</div>
            <div onclick="StoragePlace.OnExport()">导出</div>
        </div>
	    <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:StoragePlace.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
		        <th data-options="field: 'Id', checkbox: true"></th> 
                <th data-options="field: 'Coded', width:100">存放地点编码</th> 
                <th data-options="field: 'Named', width:100">存放地点</th> 
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/Asset/Scripts/DlgFiles.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/StoragePlace.js"></script>
    <script type="text/javascript">
        $(function () {
            StoragePlace.Init();
        })
    </script>

</asp:Content>
