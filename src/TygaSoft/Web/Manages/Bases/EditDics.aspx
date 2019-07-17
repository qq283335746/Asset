<%@ Page Title="数据字典" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditDics.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.EditDics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="mtb10">
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="Dics.Add()">新建</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="Dics.Edit()">编辑</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="Dics.Del()">删除</a>
    </div>
    <div id="mmTree" class="easyui-menu" style="width: 120px;">
        <div onclick="Dics.Add()" data-options="iconCls:'icon-add'">添加</div>
        <div onclick="Dics.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
        <div onclick="Dics.Del()" data-options="iconCls:'icon-remove'">删除</div>
    </div>
    <ul id="treeCt" class="easyui-tree" data-options="animate:true,onLoadSuccess:Dics.OnLoadSuccess,onSelect:Dics.OnTreeSelect"></ul>

    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/Dics.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                Dics.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        })

    </script>

</asp:Content>
