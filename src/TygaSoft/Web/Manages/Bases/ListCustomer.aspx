<%@ Page Title="客户管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListCustomer.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.ListCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="Customer.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="Customer.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="Customer.Del()"><span>删除</span></a>
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
        <div id="mmExcel" style="width:150px;">
            <div onclick="Customer.OnExport()">导出</div>
        </div>
        <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:Customer.OnSearch" style="width: 250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'CityName', width:100">城市</th>
                <th data-options="field: 'Coded', width:100">客户编号</th>
                <th data-options="field: 'Named', width:180">客户名称</th>
                <th data-options="field: 'ContactMan', width:100">联系人</th>
                <th data-options="field: 'TelPhone', width:100">电话</th>
                <th data-options="field: 'Address', width:300">地址</th>
                <th data-options="field: 'TradeName', width:150">所属行业</th>
                <th data-options="field: 'SCooperateTime', width:100">合作时间</th>
                <th data-options="field: 'SAgreementTimeout', width:120">协议到期时间</th>
                <th data-options="field: 'JoinPrice', width:120">合作价格</th>
                <th data-options="field: 'DiscountAbout', width:120">优惠</th>
                <th data-options="field: 'PayWay', width:120">付款方式</th>
                <th data-options="field: 'StaffCode', width:180">开发服务对应人员工号</th>
                <th data-options="field: 'Remark', width:300">备注</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/Customer.js"></script>
    <script type="text/javascript">
        $(function () {
            Customer.Init();
        })
    </script>

</asp:Content>
