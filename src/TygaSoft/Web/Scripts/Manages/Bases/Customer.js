var Customer = {
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
                Customer.Load(pageNumber, pageSize);
            }
        });
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var jSearchItem = Customer.GetSearchItem();
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + jSearchItem.Keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetCustomerList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetCustomerList--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        Customer.Load(1, 10);
    },
    GetSearchItem: function () {
        var keyword = $("#txtKeyword").textbox('getValue');
        var data = { "Keyword": keyword };
        return data;
    },
    Add: function () {
        Customer.SelectRow = null;
        if ($("body").find("#dlgAddCustomer").length == 0) {
            $("body").append("<div id=\"dlgAddCustomer\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(720, 450);
        $("#dlgAddCustomer").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Customer.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCustomer').dialog('close');
                }
            }],
            href: Common.AppName + '/Asset/u/tcustomer.html'
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        Customer.SelectRow = rows[0];
        if ($("body").find("#dlgAddCustomer").length == 0) {
            $("body").append("<div id=\"dlgAddCustomer\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(720, 450);
        $("#dlgAddCustomer").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Customer.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCustomer').dialog('close');
                }
            }],
            href: Common.AppName + '/u/tcustomer.html',
            onLoad: function () {
                Customer.SetEdit(rows[0]);
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
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"itemAppend": "' + itemAppend + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteCustomer";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        Customer.Load(1, 10);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var Id = $.trim($("#hId").val());
        var coded = $.trim($("#txtCoded").val());
        var named = $.trim($("#txtNamed").val());
        var shortName = $.trim($("#txtShortName").val());
        var contactMan = $.trim($("#txtContactMan").val());
        var contactPhone = "";
        var telPhone = $.trim($("#txtTelPhone").val());
        var fax = "";
        var postCode = "";
        var address = $.trim($("#txtAddress").val());
        var cityName = $.trim($("#txtCityName").val());
        var tradeName = $.trim($("#txtTradeName").val());
        var cooperateTime = $("#txtCooperateTime").datebox('getValue');
        var agreementTimeout = $("#txtAgreementTimeout").datebox('getValue');
        var joinPrice = $.trim($("#txtJoinPrice").val());
        var discountAbout = $.trim($("#txtDiscountAbout").val());
        var payWay = $.trim($("#txtPayWay").val());
        var staffCode = $.trim($("#txtStaffCode").val());
        var remark = $.trim($("#txtRemark").val());

        var postData = '{"model":{"Id": "' + Id + '","Coded": "' + coded + '", "Named": "' + named + '", "ShortName": "' + shortName + '", "ContactMan": "' + contactMan + '", "ContactPhone": "' + contactPhone + '", "TelPhone": "' + telPhone + '", "Fax": "' + fax + '", "PostCode": "' + postCode + '", "Address": "' + address + '", "CityName": "' + cityName + '", "TradeName": "' + tradeName + '", "CooperateTime": "' + cooperateTime + '","AgreementTimeout":"' + agreementTimeout + '","JoinPrice":"' + joinPrice + '","DiscountAbout":"' + discountAbout + '", "PayWay": "' + payWay + '", "StaffCode": "' + staffCode + '", "Remark": "' + remark + '" }}';
        var url = Common.AppName + "/Services/Service.svc/SaveCustomer";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgAddCustomer").dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                Customer.Load(1, 10);
            }, 700);
        });
    },
    SetEdit: function (data) {
        var contarner = $('#dlgAddCustomer');
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtCoded').val(data.Coded);
        contarner.find('#txtNamed').val(data.Named);
        contarner.find('#txtShortName').val(data.ShortName);
        contarner.find('#txtContactMan').val(data.ContactMan);
        contarner.find('#txtContactPhone').val(data.ContactPhone);
        contarner.find('#txtTelPhone').val(data.TelPhone);
        contarner.find('#txtFax').val(data.Fax);
        contarner.find('#txtPostCode').val(data.PostCode);
        contarner.find('#txtAddress').val(data.Address);
        contarner.find('#txtCityName').val(data.CityName);
        contarner.find('#txtTradeName').val(data.TradeName);
        contarner.find('#txtCooperateTime').datebox('setValue', data.SCooperateTime);
        contarner.find('#txtAgreementTimeout').datebox('setValue', data.SAgreementTimeout);
        contarner.find('#txtJoinPrice').val(data.JoinPrice);
        contarner.find('#txtDiscountAbout').val(data.DiscountAbout);
        contarner.find('#txtPayWay').val(data.PayWay);
        contarner.find('#txtStaffCode').val(data.StaffCode);
        contarner.find('#txtRemark').textbox('setValue', data.Remark);
    },
    OnExport: function () {
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                var jSearchItem = Customer.GetSearchItem();
                var s = 'Keyword=' + jSearchItem.Keyword + '';
                window.open(Common.AppName + "/h/content.html?ReqName=ExportCustomer&" + s + "");
            }
        })
    }
}