var Product = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        this.InitForm();
    },
    InitData: function () {
        this.Load(1, 10);
    },
    InitForm: function () {
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Product.Load(pageNumber, pageSize);
            }
        });
        if (Product.Action == 'Add') {
            jeasyuiFun.cbt("cbtCategory", null, Common.AppName + "/Services/Service.svc/GetCategoryTree", '', null);
            jeasyuiFun.cbt("cbtUseDepmt", null, Common.AppName + "/Services/Service.svc/GetOrgDepmtTree", '', null);
            jeasyuiFun.cbt("cbtMgrDepmt", null, Common.AppName + "/Services/Service.svc/GetOrgDepmtTree", '', null);
            jeasyuiFun.cbb("cbbStoragePlace", null, Common.AppName + "/Services/Service.svc/GetCbbStoragePlace", '', null);
        }
    },
    Action: null,
    Load: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetProductList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            console.log('GetProductList--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
        Product.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    Add: function () {
        Product.Action = 'Add';
        if ($("body").find("#dlgProduct").length == 0) {
            $("body").append("<div id=\"dlgProduct\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(960, 550);
        $("#dlgProduct").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Product.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgProduct').dialog('close');
                }
            }],
            href: Common.AppName + '/u/tproduct.html',
            onLoad: function () {
                var jData = {};
                jData.Id = '';
                Product.SetFm(jData);
            }
        })
        return false;
    },
    Edit: function () {
        Product.Action = 'Edit';
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }

        if ($("body").find("#dlgProduct").length == 0) {
            $("body").append("<div id=\"dlgProduct\" style=\"padding:10px;\"></div>");
        }
        //var s = '';
        var wh = Common.GetWh(960, 550);
        $("#dlgProduct").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Product.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgProduct').dialog('close');
                }
            }],
            href: Common.AppName + '/u/tproduct.html',
            onLoad: function () {
                Product.SetFm(rows[0]);
            }
            //content:s
        })
        return false;
    },
    Del: function () {
        Product.Action = 'Del';
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"itemAppend": "' + itemAppend + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteProduct";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
                        Product.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var contarner = $('#dlgProduct');
        var isValid = contarner.find('#dlgFm').form('validate');
        if (!isValid) return false;
        var id = $.trim($("#hId").val());
        var categoryId = contarner.find('#cbtCategory').combotree('getValue');
        var useDepmtId = contarner.find("#cbtUseDepmt").combotree('getValue');
        var mgrDepmtId = contarner.find("#cbtMgrDepmt").combotree('getValue');
        var storagePlaceId = contarner.find('#cbbStoragePlace').combobox('getValue');
        var coded = $.trim(contarner.find("#txtCoded").textbox('getValue'));
        var named = $.trim(contarner.find("#txtNamed").textbox('getValue'));
        var specModel = $.trim(contarner.find("#txtSpecModel").textbox('getValue'));
        var qty = $.trim(contarner.find("#txtQty").textbox('getValue'));
        var price = $.trim(contarner.find("#txtPrice").textbox('getValue'));
        var amount = $.trim(contarner.find("#txtAmount").textbox('getValue'));
        var meterUnit = $.trim(contarner.find("#txtMeterUnit").textbox('getValue'));
        var pieceQty = $.trim(contarner.find("#txtPieceQty").textbox('getValue'));
        var pattr = $.trim(contarner.find("#txtPattr").textbox('getValue'));
        var sourceFrom = $.trim(contarner.find("#txtSourceFrom").textbox('getValue'));
        var supplier = $.trim(contarner.find("#txtSupplier").textbox('getValue'));
        var buyDate = $.trim(contarner.find("#txtBuyDate").datebox('getValue'));
        var enableDate = $.trim(contarner.find("#txtEnableDate").datebox('getValue'));
        var useDateLimit = $.trim(contarner.find("#txtUseDateLimit").textbox('getValue'));
        var usePersonName = $.trim(contarner.find("#txtUsePersonName").textbox('getValue'));
        var remark = $.trim(contarner.find("#txtRemark").textbox('getValue'));
        var registerDate = $.trim(contarner.find("#txtRegisterDate").textbox('getValue'));
        var registerUser = $.trim(contarner.find("#txtRegisterUser").textbox('getValue'));
        var status = $.trim(contarner.find("#txtStatus").textbox('getValue'));
        var sort = $.trim(contarner.find("#txtSort").textbox('getValue'));
        if (qty == '') qty = 0;
        if (price == '') price = 0;
        if (amount == '') amount = 0;
        if (pieceQty == '') pieceQty = 0;
        if (sort == '') sort = 0;

        var postData = '{"model":{"AppCode":"' + Common.GetAppId() + '","Id":"' + id + '","CategoryId":"' + categoryId + '","Coded":"' + coded + '","Named":"' + named + '","SpecModel":"' + specModel + '","Qty":"' + qty + '","Price":"' + price + '","Amount":"' + amount + '","MeterUnit":"' + meterUnit + '","PieceQty":"' + pieceQty + '","Pattr":"' + pattr + '","SourceFrom":"' + sourceFrom + '","Supplier":"' + supplier + '","BuyDate":"' + buyDate + '","EnableDate":"' + enableDate + '","UseDateLimit":"' + useDateLimit + '","UseDepmtId":"' + useDepmtId + '","UsePersonName":"' + usePersonName + '","MgrDepmtId":"' + mgrDepmtId + '","StoragePlaceId":"' + storagePlaceId + '","Remark":"' + remark + '","RegisterDate":"' + registerDate + '","RegisterUser":"' + registerUser + '","Status":"' + status + '","Sort":"' + sort + '"}';
        var url = Common.AppName + "/Services/Service.svc/SaveProduct";
        //        console.log('postData--' + postData);
        //        return false;
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgProduct").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                var pager = $("#dg").datagrid('getPager');
                Product.Load(pager.pagination('options').pageNumber, pager.pagination('options').pageSize);
            }, 700);
        });
    },
    SetFm: function (data) {
        var contarner = $('#dlgProduct');
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtCoded').textbox('setValue', data.Coded);
        contarner.find('#txtNamed').textbox('setValue', data.Named);
        contarner.find('#txtSpecModel').textbox('setValue', data.SpecModel);
        contarner.find('#txtQty').textbox('setValue', data.Qty);
        contarner.find('#txtPrice').textbox('setValue', data.Price);
        contarner.find('#txtAmount').textbox('setValue', data.Amount);
        contarner.find('#txtMeterUnit').textbox('setValue', data.MeterUnit);
        contarner.find('#txtPieceQty').textbox('setValue', data.PieceQty);
        contarner.find('#txtPattr').textbox('setValue', data.Pattr);
        contarner.find('#txtSourceFrom').textbox('setValue', data.SourceFrom);
        contarner.find('#txtSupplier').textbox('setValue', data.Supplier);
        contarner.find('#txtBuyDate').datebox('setValue', data.BuyDate);
        contarner.find('#txtEnableDate').datebox('setValue', data.EnableDate);
        contarner.find('#txtUseDateLimit').textbox('setValue', data.UseDateLimit);
        contarner.find('#txtUsePersonName').textbox('setValue', data.UsePersonName);
        contarner.find('#txtRemark').textbox('setValue', data.Remark);
        contarner.find('#txtRegisterDate').textbox('setValue', data.RegisterDate);
        contarner.find('#txtRegisterUser').textbox('setValue', data.RegisterUser);
        contarner.find('#txtStatus').textbox('setValue', data.Status);
        contarner.find('#txtSort').textbox('setValue', data.Sort);

        jeasyuiFun.cbt("cbtCategory", data.CategoryId, Common.AppName + "/Services/Service.svc/GetCategoryTree", '', null);
        jeasyuiFun.cbt("cbtUseDepmt", data.UseDepmtId, Common.AppName + "/Services/Service.svc/GetOrgDepmtTree", '', null);
        jeasyuiFun.cbt("cbtMgrDepmt", data.MgrDepmtId, Common.AppName + "/Services/Service.svc/GetOrgDepmtTree", '', null);
        jeasyuiFun.cbb("cbbStoragePlace", data.StoragePlaceId, Common.AppName + "/Services/Service.svc/GetCbbStoragePlace", '', null);
    },
    OnImport: function () {
        DlgFiles.Params = { ReqName: 'ImportProduct', AppCode: Common.GetAppId() };
        DlgFiles.DlgUpload('Product', function (result) {
            var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
            Product.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
        })
    },
    OnExport: function () {
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/asset/h/upload.html?ReqName=ExportProduct");
            }
        })
    },
    Print: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
//        var itemAppend = "";
//        for (var i = 0; i < rows.length; i++) {
//            if (i > 0) itemAppend += ",";
//            itemAppend += rows[i].Id;
//        }
        $.messager.confirm('提示', '确定要打印吗？', function (r) {
            if (r) {
                var data = {};
                var url = Common.AppName + "/Services/Service.svc/GetBarcodeTemplateInfoByDefault";
                Common.Ajax(url, data, 'GET', '', true, true, function (result) {
                    console.log('GetBarcodeTemplateInfoByDefault--' + JSON.stringify(result));
                    var jData = JSON.parse(result.Data);
                    
                })
            }
        })
    }
}