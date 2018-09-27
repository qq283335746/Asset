<%@ Page Title="分类管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.EditCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="mtb10">
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="Category.Add()">新建</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="Category.Edit()">编辑</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="Category.Del()">删除</a>
        <a class="easyui-menubutton" data-options="menu:'#mmcExcel',iconCls:'icon-edit'">导入/导出</a>
    </div>
    <div id="mmTree" class="easyui-menu" style="width: 120px;">
        <div onclick="Category.Add()" data-options="iconCls:'icon-add'">添加</div>
        <div onclick="Category.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
        <div onclick="Category.Del()" data-options="iconCls:'icon-remove'">删除</div>
    </div>
    <div id="mmcExcel" style="width:150px;">
        <div><a href="/asset/Files/Template/资产分类导入模板.xlsx">下载导入模板</a></div>
        <div onclick="Category.OnImport()">导入</div>
        <div onclick="Category.OnExport()">导出</div>
    </div>
    <ul id="treeCt" class="easyui-tree" data-options="animate:true,onLoadSuccess:Category.OnLoadSuccess,onSelect:Category.OnTreeSelect"></ul>

    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/Category.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                Category.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        })

    </script>

</asp:Content>
