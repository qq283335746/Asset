<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProductRepair.aspx.cs" Inherits="TygaSoft.Web.Manages.Bases.AddProductRepair" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建/编辑信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
    <div>
        <div class="row">
          <span class="rl w100">选择资产：</span>
          <div class="fl" style="width:400px;">
             <input tabindex="1" id="cbgProduct" data-options="required:true,idField:'Id',textField: 'Named', fit:true, fitColumns:true,multiple:false,rownumbers:true,pagination:true,toolbar:'#dlgDgProductToolbar',panelWidth:720,panelHeight:380,columns: [[
                    { field: 'Coded', title: '资产编码', width: 100 },
                    { field: 'Named', title: '资产名称', width: 200 },
                    { field: 'SpecModel', title: '规格型号', width: 100 },
                    { field: 'StatusName', title: '状态', width: 80 }
             ]],onLoadSuccess:function(data){
                  var productId = $('#hProductId').val();
                  var productName = $('#hProductName').val();
                  var values = { 'Id': productId, 'Named': productName };
                 if (values && values != '') {
                     //console.log(values);
                     $('#cbgProduct').combogrid('setValue', values);
                 }
             }" />
          </div>
        </div>
        <div class="row mt10">
            <span class="rl w100">维修状态：</span>
            <div class="fl">
                <input tabindex="2" id="cbbProductStatus" data-options="required:true,validType:'select'" />
            </div>
        </div>
    </div>
    </form>

    <div id="dlgDgProductToolbar" style="padding:6px;">
        <div class="fr">
            <input id="txtDlgKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:DlgProduct.OnCbgSearch" style="width: 400px;" />
        </div>
        <span class="clr"></span>
    </div>
    <input type="hidden" id="hId" />
    <input type="hidden" id="hProductId" />
    <input type="hidden" id="hProductName" />

    <script type="text/javascript" src="/Asset/Scripts/Manages/Bases/DlgProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            DlgProduct.InitCbgProductTools();
            DlgProduct.LoadCbgProduct('cbgProduct', 1, 10);
            //DlgProduct.CbgProduct('cbgProduct', true, null,1,10);
        })
    </script>
</body>
</html>
