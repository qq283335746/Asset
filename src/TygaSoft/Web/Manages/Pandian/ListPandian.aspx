<%@ Page Title="盘点单" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListPandian.aspx.cs" Inherits="TygaSoft.Web.Manages.Pandian.ListPandian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding: 5px;">
        <ul class="h_ul">
            <li style="width:30%;color:#3c8dbc;padding-left:10px;">0 盘点单 (全部)</li>
            <li style="width:30%;color:#3c763d;">0 盘点单 (已完成)</li>
            <li style="width:30%;color:#a94442;">0 盘点单 (未完成)</li>
        </ul>
        <span class="clr"></span>
        <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="Pandian.Add()">新增</a>
        <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="Pandian.Del()">删除</a>
        <a id="abtnView" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" onclick="Pandian.OnView()">盘点详情</a>
        <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:Pandian.OnSearch" style="width: 250px;" />
        </div>
        <span class="clr"></span>
    </div>

    <table id="dgPandian" class="easyui-datagrid" title="" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'Named',width:150,formatter:Pandian.FormatName">盘点单名称</th>
                <th data-options="field:'UserName',width:100">创建人</th>
                <th data-options="field:'SCreateDate',width:100">创建时间</th>
                <th data-options="field:'StatusName',width:100">状态</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/asset/Scripts/Manages/Pandian/Pandian.js"></script>

    <script type="text/javascript">
        $(function () {
            Pandian.Init();
        })
    </script>

</asp:Content>
