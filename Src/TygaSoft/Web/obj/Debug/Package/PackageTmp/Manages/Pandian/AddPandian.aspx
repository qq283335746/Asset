<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPandian.aspx.cs" Inherits="TygaSoft.Web.Manages.Pandian.AddPandian" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建盘点单</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row mt10">
            <span class="rl w110"><span class="cr">*</span>盘点单名称：</span>
            <div class="fl w700">
                <input tabindex="1" id="txtName" class="easyui-textbox" data-options="required:true" style="width: 100%;" />
            </div>
        </div>
<%--        <div class="row mt10">
            <span class="rl w110"><span class="cr">*</span>分配用户：</span>
            <div class="fl w700">
                <input tabindex="1" id="txtAllowUsers" name="AllowUsers" class="easyui-textbox mtxt" data-options="required:true,prompt:'分配给用户',missingMessage:'请选择用户',invalidMessage:'请选择用户', icons:[{
                    iconCls:'icon-search',    
                    handler: function(e){
                            DlgUsers.OnDlg($('#txtAllowUsers'),$('#hUserIdAppend'),$('#hUserTextAppend'));
                        }
                    },{
                        iconCls:'icon-remove',
		                handler: function(e){
			                $(e.data.target).textbox('clear');
                            $('#hUserIdAppend').val('');
                            $('#hUserTextAppend').val('');
		                }
                    }]" style="width:100%;" />

            </div>
        </div>--%>
        <div class="row mt10">
            <span class="rl w110">备注：</span>
            <div class="fl w700">
                <input tabindex="3" id="txtRemark" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl" style="width:30px;">&nbsp;</span>
            <div class="fl" style="width:780px;">
                <div id="tabAddPandian" class="easyui-tabs" style="margin-top:10px;">
                    <div title="盘点范围" style="padding:20px;">
                        <div class="row">
                            <span class="rl w130">购入日期：</span>
                            <div class="fl">
                                <input tabindex="4" id="txtStartBuyDate" name="StartBuyDate" class="easyui-datebox w179" data-options="editable:true" />
                            </div>
                            <span class="fl">--</span>
                            <div class="fl">
                                <input tabindex="5" id="txtEndBuyDate" name="EndBuyDate" class="easyui-datebox w179" data-options="editable:true" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl w130">使用部门：</span>
                            <div class="fl w408">
                                <input tabindex="6" id="cbtUseDepmt" data-options="prompt:'请选择使用部门'" style="width:100%;" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl w130">实物管理部门：</span>
                            <div class="fl w408">
                                <input tabindex="7" id="cbtMgrDepmt" data-options="prompt:'请选择实物管理部门'" style="width:100%;" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl w130">资产分类：</span>
                            <div class="fl w408">
                                <input tabindex="8" id="cbtCategory" class="txt" data-options="prompt:'请选择资产类别'" style="width:100%;" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl w130">存放地点：</span>
                            <div class="fl w408">
                                <input tabindex="9" id="cbbStoragePlace" style="width:100%;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <input type="hidden" id="hId" />
        <input type="hidden" id="hIsConfirm" value="false" />
        
    </form>
</body>
</html>
