<%@ Page Title="部门管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditOrgDepmt.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.EditOrgDepmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'west',split:true" style="width: 330px;">
            <div class="mtb10">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="OrgDepmt.Add()">新建</a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="OrgDepmt.Edit()">编辑</a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="OrgDepmt.Del()">删除</a>
                <a class="easyui-menubutton" data-options="menu:'#mmoExcel',iconCls:'icon-edit'">导入/导出</a>
                <div id="mmoExcel" style="width: 150px;">
                    <div><a href="/asset/Files/Template/组织机构导入模板.xlsx">下载导入模板</a></div>
                    <div onclick="OrgDepmt.OnImport()">导入</div>
                    <div onclick="OrgDepmt.OnExport()">导出</div>
                </div>
            </div>
            <div id="mmTree" class="easyui-menu" style="width: 120px;">
                <div onclick="OrgDepmt.Add()" data-options="iconCls:'icon-add'">添加</div>
                <div onclick="OrgDepmt.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
                <div onclick="OrgDepmt.Del()" data-options="iconCls:'icon-remove'">删除</div>
            </div>
            <ul id="treeCt" class="easyui-tree" data-options="animate:true,onLoadSuccess:OrgDepmt.OnLoadSuccess,onSelect:OrgDepmt.OnTreeSelect"></ul>
        </div>
        <div data-options="region:'center'" style="padding: 5px;">
            <div id="dgStaffToolbar" style="padding:3px 5px 1px 0;display: none;">
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="Staff.Add()">新建</a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="Staff.Edit()">编辑</a>
                <a class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="Staff.Del()">删除</a>
                <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'" style="display:none;">导入/导出</a>
                <div class="fr">
                    <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:Staff.OnSearch" style="width: 250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <div id="mmExcel" style="width: 150px;">
                <div onclick="Staff.OnImport()">导入</div>
                <div onclick="Staff.OnExport()">导出</div>
            </div>
            <table id="dgStaff" class="easyui-datagrid" title="用户列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,striped:true,toolbar:'#dgStaffToolbar'">
                <thead>
                    <tr>
                        <th data-options="field:'UserId',checkbox:true"></th>
                        <th data-options="field:'Coded',width:100">工号</th>
                        <th data-options="field:'Named',width:100">姓名</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <div id="dlgOrgDepmt" style="width: 540px; height: 300px; padding: 10px;"></div>

    <script type="text/javascript" src="/Asset/Scripts/DlgFiles.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/OrgDepmt.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/Staff.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                OrgDepmt.Init();
                Staff.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        })

    </script>

</asp:Content>
