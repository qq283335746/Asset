var ProductRepair = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.Load(1, 10);
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ProductRepair.Load(pageNumber, pageSize);
            }
        });
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetProductRepairs';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetProductRepairs--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        var pager = $("#dg").datagrid('getPager');
        ProductRepair.Load(1, pager.pagination('options').pageSize);
    },
    Add: function () {
        ProductRepair.SelectRow = null;
        if ($("body").find("#dlgProductRepair").length == 0) {
            $("body").append("<div id=\"dlgProductRepair\" style=\"padding:10px;\"></div>");
        }
        //var s = '';
        var wh = Common.GetWh(900, 500);
        $("#dlgProductRepair").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    ProductRepair.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgProductRepair').dialog('close');
                }
            }],
            href: '/asset/u/gproduct.html',
            onLoad: function () {
                var jData = {};
                jData.ProductRepairInfo = {};
                jData.ProductInfo = {};
                ProductRepair.SetFm(jData);
            }
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        //ProductRepair.SelectRow = rows[0];
        if ($("body").find("#dlgProductRepair").length == 0) {
            $("body").append("<div id=\"dlgProductRepair\" style=\"padding:10px;\"></div>");
        }
        //var s = '';
        var wh = Common.GetWh(780, 500);
        $("#dlgProductRepair").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    ProductRepair.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgProductRepair').dialog('close');
                }
            }],
            href: '/asset/u/gproduct.html',
            onLoad: function () {
                ProductRepair.SetFm(rows[0]);
            }
        })
        return false;
    },
    Del: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var isErr = false;
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].ProductInfo.Status > 0) {
                isErr = true;
                $.messager.alert('错误提示', "存在一个或多个资产正在维修处理中，无法删除！", 'error');
                return false;
            }
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].ProductRepairInfo.Id;
        }
        if (isErr) {
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"itemAppend": "' + itemAppend + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteProductRepair";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        var pager = $("#dg").datagrid('getPager');
                        ProductRepair.Load(1, pager.pagination('options').pageSize);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var id = $.trim($("#hId").val());
        var status = $.trim($("#cbbProductStatus").combobox('getText'));
        var productId = $.trim($("#cbgProduct").combogrid('getValue'));

        var postData = '{"model":{"Id":"' + id + '","AppCode":"' + Common.GetAppId() + '","ProductId":"' + productId + '","StatusName":"' + status + '"}';
        //                console.log(postData);
        //                return false;
        var url = Common.AppName + "/Services/Service.svc/SaveProductRepair";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgProductRepair").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                var pager = $("#dg").datagrid('getPager');
                ProductRepair.Load(1, pager.pagination('options').pageSize);
            }, 700);
        });
    },
    SetFm: function (data) {
        //console.log(JSON.stringify(data));
        var contarner = $('#dlgProductRepair');
        contarner.find('#hId').val(data.ProductRepairInfo.Id);
        contarner.find('#hProductId').val(data.ProductRepairInfo.ProductId);
        contarner.find('#hProductName').val(data.ProductInfo.Named);
        ProductRepair.CbbProductStatus(data.ProductInfo.Status);
        
        //$('#cbgProduct').combogrid('setValue', data.ProductRepairInfo.ProductId);
    },
    CbbProductStatus: function (v) {
        var jProductStatus = [{ "Id": 0, "Named": "正常" }, { "Id": 1, "Named": "待维修" }, { "Id": 2, "Named": "维修中" }, { "Id": 3, "Named": "已报废"}];
        jeasyuiFun.setCbb("cbbProductStatus", v, jProductStatus);
    }
}