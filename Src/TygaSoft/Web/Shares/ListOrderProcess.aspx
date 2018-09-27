<%@ Page Title="订单状态路由查询" Language="C#" MasterPageFile="~/Masters/Shares.Master" AutoEventWireup="true" CodeBehind="ListOrderProcess.aspx.cs" Inherits="TygaSoft.Web.Shares.ListOrderProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <div class="m-toolbar">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'请输入单号',buttonText:' 查 询 ',onClickButton:Orders.OnSearch" style="width:100%;" />
        </div>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="rownumbers:true,fit:true,fitColumns:true,singleSelect:true,border:true,showHeader:true,header:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'SRecordDate',width:120">操作时间</th>
                <th data-options="field: 'StepName',width:100">扫描类型</th>
            </tr>
        </thead>
    </table>

    <script src="/Asset/Scripts/Shares/Orders.js"></script>

</asp:Content>
