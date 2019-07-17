<%@ Page Title="角色管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListRoles.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.ListRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding: 5px;">
        <a id="lbtnAdd" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="currFun.Add()">新建</a>
        <a id="lbtnEdit" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="currFun.Edit()">编辑</a>
        <a id="lbtnDel" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="currFun.Del()">删除</a>
        <a id="lbtnRoleMenu" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="currFun.RoleMenu()">角色授权</a>
    </div>

    <table id="bindT" class="easyui-datagrid" data-options="rownumbers:true,fit:true,singleSelect:true,fitColumns:true,toolbar:'#toolbar',onSelect:currFun.OnSelect">
        <thead>
            <tr>
                <th data-options="field:'f0',checkbox:true"></th>
                <th data-options="field:'f1',width:200">角色名称</th>
                <th data-options="field:'f2',width:150">添加/移除用户</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rpData" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("RoleId")%> </td>
                        <td><%#Eval("RoleName") %></td>
                        <td>
                            <a href='amember.html?rName=<%#HttpUtility.UrlEncode(Eval("RoleName").ToString()) %>' class="abtn">管理</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <div id="dlg" class="easyui-dialog" title="新建角色" data-options="resizable:true,modal:true,closed:true,
buttons:[{
			text:'保存',iconCls:'icon-ok',
			handler:function(){currFun.Save();}
		},{
			text:'取消',iconCls:'icon-cancel',
			handler:function(){$('#dlg').dialog('close');}
		}]"
        style="width: 380px; height: 130px; padding: 10px;">
        <div class="row">
            <span class="fl rl w100"><b class="cr">*</b>角色名称：</span>
            <div class="fl">
                <input type="text" id="txtRolename" maxlength="50" tabindex="1" class="easyui-validatebox txt w200" data-options="required:true" />
            </div>
            <span class="clr"></span>
        </div>
    </div>

    <input type="hidden" id="hId" value="" />
    <script type="text/javascript" src="/Asset/Scripts/Manages/Members/Roles.js"></script>
    <script type="text/javascript">
        $(function () {
            currFun.Init();
        })
    </script>

</asp:Content>
