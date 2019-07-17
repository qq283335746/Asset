<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddProduct1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新增/编辑资产信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row-fl">
            <span class="rl w120"><span class="cr">*</span> 资产分类：</span>
            <div class="fl w288">
                <input id="cbtCategory" class="txt" data-options="required:true,prompt:'请选择资产类别'" style="width:100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130"><span class="cr">*</span> 资产编码：</span>
            <div class="fl w288">
                <input tabindex="2" id="txtCoded" class="easyui-textbox" data-options="prompt:'不填写则系统自动生成'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120"><span class="cr">*</span> 资产名称：</span>
            <div class="fl w288">
                <input tabindex="3" id="txtNamed" class="easyui-textbox" data-options="required:true" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">规格型号：</span>
            <div class="fl w288">
                <input tabindex="4" id="txtSpecModel" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">数量：</span>
            <div class="fl w288">
                <input tabindex="5" id="txtQty" class="easyui-textbox" data-options="validType:'int'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">单价：</span>
            <div class="fl w288">
                <input tabindex="6" id="txtPrice" class="easyui-textbox" data-options="validType:'price'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">金额：</span>
            <div class="fl w288">
                <input tabindex="7" id="txtAmount" class="easyui-textbox" data-options="validType:'price'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">计量单位：</span>
            <div class="fl w288">
                <input tabindex="11" id="txtMeterUnit" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl" style="display:none;">
            <span class="rl w120">件数：</span>
            <div class="fl w288">
                <input tabindex="12" id="txtPieceQty" class="easyui-textbox" data-options="validType:'int'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">资产属性：</span>
            <div class="fl w288">
                <input tabindex="13" id="txtPattr" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">资产来源：</span>
            <div class="fl w288">
                <input tabindex="14" id="txtSourceFrom" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">供应商：</span>
            <div class="fl w288">
                <input tabindex="15" id="txtSupplier" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">购入日期：</span>
            <div class="fl w288">
                <input tabindex="16" id="txtBuyDate" class="easyui-datebox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">启用日期：</span>
            <div class="fl w288">
                <input tabindex="17" id="txtEnableDate" class="easyui-datebox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">使用期限(月)：</span>
            <div class="fl w288">
                <input tabindex="18" id="txtUseDateLimit" class="easyui-textbox" data-options="validType:'int'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">使用部门：</span>
            <div class="fl w288">
                <input tabindex="19" id="cbtUseDepmt" data-options="prompt:'请选择使用部门'" style="width:100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">使用人：</span>
            <div class="fl w288">
                <input tabindex="20" id="txtUsePersonName" class="easyui-textbox" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">实物管理部门：</span>
            <div class="fl w288">
                <input tabindex="21" id="cbtMgrDepmt" data-options="prompt:'请选择实物管理部门'" style="width:100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">存放地点：</span>
            <div class="fl w288">
                <input tabindex="22" id="cbbStoragePlace" style="width:100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">排序：</span>
            <div class="fl w288">
                <input tabindex="23" id="txtSort" class="easyui-textbox" data-options="validType:'int'" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">状态：</span>
            <div class="fl w288">
                <input tabindex="24" id="txtStatus" class="easyui-textbox" data-options="editable:false" style="width: 100%;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w130">登记日期：</span>
            <div class="fl w288">
                <input tabindex="25" id="txtRegisterDate" class="easyui-textbox" data-options="editable:false" style="width: 100%;"/>
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w120">登记人：</span>
            <div class="fl w288">
                <input tabindex="25" id="txtRegisterUser" class="easyui-textbox" data-options="editable:false" style="width: 100%;" />
            </div>
        </div>
        <span class="clr"></span>
        <div class="row mt10">
            <span class="rl w120">备注：</span>
            <div class="fl" style="width:704px;">
                <input tabindex="29" id="txtRemark" class="easyui-textbox" data-options="multiline:true" style="width: 100%;height:80px;" />
            </div>
        </div>
        <span class="clr"></span>
        <input type="hidden" id="hId" />
<%--        <input type="hidden" id="hCategoryId" />
        <input type="hidden" id="hUseDepmtId" />
        <input type="hidden" id="hMgrDepmtId" />
        <input type="hidden" id="hStoragePlaceId" />--%>
    </form>
</body>
</html>
