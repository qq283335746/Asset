<%@ Page Title="盘点结果" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListPandianAsset.aspx.cs" Inherits="TygaSoft.Web.Manages.Pandian.ListPandianAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding: 5px;">
        <ul class="h_ul">
            <li style="width:30%;padding-bottom:5px;">盘点单名称：<span id="lbPandianName"></span></li>
            <li style="width:30%;padding-bottom:5px;">盘点单状态：<span id="lbPandianStatus"></span></li>
            <li style="width:30%;padding-bottom:5px;">创建日期：<span id="lbPandianDate"></span></li>
        </ul>
        <span class="clr"></span>
        <ul id="detailPanel" class="h_ul">
            <li style="width:30%;color:#3c8dbc;">已盘（0 ）</li>
            <li style="width:30%;color:#3c763d;">盘盈（0）</li>
            <li style="width:30%;color:#a94442;">未盘（0）</li>
        </ul>
        <span class="clr"></span>
        <a id="abtnDel" name="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="PandianAsset.Del()">删除</a>
        <a id="abtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" onclick="PandianAsset.Save()">提交盘点结果</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="PandianAsset.OnExport()">导出</a>
    </div>

    <table id="dg" class="easyui-datagrid" title="" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:false,toolbar:'#toolbar',pageList:[1000],pageSize:1000">
        <thead>
            <tr>
                <th data-options="field:'AssetId',checkbox:true"></th>
                <th data-options="field:'StatusName',width:100">盘点状态</th>
                <th data-options="field:'Barcode',hidden:true,width:120">条形码</th>
                <th data-options="field:'PictureUrl',hidden:true,width:100">图片</th>
                <th data-options="field:'CategoryCode',width:100">资产类别编码</th>
                <th data-options="field:'CategoryName',width:150">资产类别</th>
                <th data-options="field:'AssetCode',width:130">资产编码</th>
                <th data-options="field:'AssetName',width:180">资产名称</th>
                <th data-options="field:'SpecModel',width:100">规格型号</th>
                <th data-options="field:'Unit',width:100">计量单位</th>
                <th data-options="field:'UseDepmtCode',width:100">使用部门编码</th>
                <th data-options="field:'UseDepmtName',width:150">使用部门</th>
                <th data-options="field:'MgrDepmtCode',width:120">实物管理部门编码</th>
                <th data-options="field:'MgrDepmtName',width:150">实物管理部门</th>
                <th data-options="field:'StoragePlaceCode',width:100">存放地点编码</th>
                <th data-options="field:'StoragePlaceName',width:250">存放地点</th>
                <th data-options="field:'UsePersonName',width:100">使用人</th>
                <th data-options="field:'Qty',width:80">账面数量</th>
                <th data-options="field:'LastUseDepmtName',width:150">修改后使用部门</th>
                <th data-options="field:'LastMgrDepmtName',width:150">修改后实物管理部门</th>
                <th data-options="field:'LastUsePerson',width:100">修改后使用人</th>
                <th data-options="field:'LastStoragePlaceName',width:150">修改后存放地点</th>
                <th data-options="field:'UserName',width:100">盘点人</th>
                <th data-options="field:'Remark',width:200">盘点备注</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/asset/Scripts/Manages/Pandian/PandianAsset.js"></script>

    <script type="text/javascript">
        $(function () {
            try {
                PandianAsset.Init();
            }
            catch (e) {
                $.messager.alert('异常提示', e.name + ": " + e.message, 'error');
            }
        })
    </script>

</asp:Content>
