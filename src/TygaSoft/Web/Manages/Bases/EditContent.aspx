<%@ Page Title="内容管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditContent.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.EditContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'west',split:true" style="width: 300px;">
            <div class="mtb10">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ContentType.Add()">新建</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ContentType.Edit()">编辑</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ContentType.Del()">删除</a>
            </div>
            <div id="mmTree" class="easyui-menu" style="width: 120px;">
                <div onclick="ContentType.Add()" data-options="iconCls:'icon-add'">添加</div>
                <div onclick="ContentType.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
                <div onclick="ContentType.Del()" data-options="iconCls:'icon-remove'">删除</div>
            </div>
            <ul id="treeCt" class="easyui-tree" data-options="animate:true,onLoadSuccess:ContentType.OnLoadSuccess,onSelect:ContentType.OnTreeSelect"></ul>
        </div>
        <div data-options="region:'center'" style="padding: 5px;">
            <div id="dgContentDetailToolbar" style="padding:3px 5px 1px 0;display: none;">
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ContentDetail.Add()">新建</a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ContentDetail.Edit()">编辑</a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ContentDetail.Del()">删除</a>
                <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'" style="display:none;">导入/导出</a>
                <div class="fr">
                    <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ContentDetail.OnSearch" style="width: 250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <div id="mmExcel" style="width: 150px;">
                <div onclick="ContentDetail.OnImport()">批量导入</div>
                <div onclick="ContentDetail.OnExport()">导出</div>
            </div>
            <table id="dgContentDetail" class="easyui-datagrid" title="内容列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,striped:true,toolbar:'#dgContentDetailToolbar'">
                <thead>
                    <tr>
                        <th data-options="field:'Id',checkbox:true"></th>
                        <th data-options="field:'Title',width:100">标题</th>
                        <th data-options="field:'FileCount',width:100">文件数</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <div id="dlgContentType" style="width: 540px; height: 300px; padding: 10px;"></div>

    <script type="text/javascript" src="/Asset/Scripts/DlgFiles.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/ContentType.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/ContentDetail.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                ContentType.LoadTree();
                ContentDetail.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }

        })


    </script>

</asp:Content>
