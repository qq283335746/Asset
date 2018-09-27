var DlgProduct = {
    Init: function () {
        this.InitEvent();
        this.InitForm();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        DlgProduct.LoadDg(1, 10);
    },
    InitForm: function () {
        //$("#dgDlgProduct").datagrid();
        var pager = $("#dgDlgProduct").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DlgProduct.LoadDg(pageNumber, pageSize);
            }
        });
    },
    Load: function (dg, pageIndex, pageSize) {
        var keyword = $("#txtDlgKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetProductList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetProductList--result--' + JSON.stringify(result));
            dg.datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
        DlgProduct.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    OnDlg: function () {
        if ($("body").find("#dlgProduct").length == 0) {
            $("body").append("<div id=\"dlgProduct\" style=\"padding:0;\"></div>");
        }
        var w = $(window).width();
        var h = $(window).height();
        if (w > 1024) w = 1024;
        else w = w * 0.94;
        if (h > 700) h = 700;
        else h = h * 0.94;
        $("#dlgProduct").dialog({
            title: '选择资产',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '/asset/u/dlgproduct.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveProduct', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var dg = $("#dgDlgProduct");
                    var rows = dg.datagrid('getSelections');
                    if (!rows || rows.length != 1) {
                        $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                        return false;
                    }
                    $("#hProductId").val(rows[0].Id);
                    $('#dlgProduct').dialog('close');
                    $("#txtProduct").textbox('setValue', rows[0].ProductCode);
                }
            }, {
                id: 'btnCancelSaveProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgProduct').dialog('close');
                }
            }]
        })
    },
    InitCbgProductTools: function () {
        $('#txtDlgKeyword').textbox();
    },
    LoadCbgProduct: function (cbgId, pageIndex, pageSize) {
        var cbg = $('#' + cbgId + '');
        cbg.combogrid();
        var dg = cbg.combogrid('grid');
        dg.datagrid('options').toolbar = '#dlgDgProductToolbar';
        DlgProduct.Load(dg, pageIndex, pageSize);
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DlgProduct.Load(dg, pageNumber, pageSize);
            }
        });
//        var values = $('#hProductId').val();
//        alert('values----' + values);
//        if (values && values != '') cbg.combogrid('setValue', values);
    },
    OnCbgSearch: function () {
        var cbg = $('#cbgProduct');
        var dg = cbg.combogrid('grid');
        var pagerOptions = jeasyuiFun.getDgPagerOptions(dg);
        DlgProduct.Load(dg, pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    CbgProduct: function (cbgId, isLoad, values, pageIndex, pageSize) {
        var cbg = $('#' + cbgId + '');
        if (!isLoad) {
            cbg.combogrid({
                readonly: true
            });
            if (values) cbg.combogrid('setValue', values);
        }
        else {
            var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '"}}';
            var url = Common.AppName + '/Services/Service.svc/GetProductList';
            Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
                //console.log('GetProductList--result--' + JSON.stringify(result));

                var wh = Common.GetWh(900, 500);
                cbg.combogrid({
                    panelWidth: wh[0] * 0.8,
                    panelHeight: 350,
                    fitColumns: true,
                    pagination: true,
                    columns: [[
                            { field: 'Coded', title: '资产编码', width: 100 },
                            { field: 'Named', title: '资产名称', width: 200 },
                            { field: 'SpecModel', title: '规格型号', width: 100 },
                            { field: 'StatusName', title: '状态', width: 80 }
                        ]],
                    data: JSON.parse(result.Data),
                    onLoadSuccess: function (data) {
                        if (values) cbg.combogrid('setValue', values);
                    },
                    onSelect: function (index, row) {

                    },
                    onSelectPage: function (pageNumber, pageSize) {
                        alert(pageNumber);
                    }
                });
            });
        }
        var pager = $('#cg').combogrid('grid').datagrid('getPager');
    }
}