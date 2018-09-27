var BarcodeTemplate = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.GetBarcodeFormats();
        this.LoadDg(1, 100);
    },
    LoadDg: function (pageIndex, pageSize) {
        var postData = '{"model":{"PageIndex":' + pageIndex + ',"PageSize":' + pageSize + ',"TypeName":"Barcode"}}';
        var url = Common.AppName + '/Services/Service.svc/GetBarcodeTemplateList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetBarcodeTemplateList--result--' + JSON.stringify(result));
            $("#dgBarcodeTemplate").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    SelectRow: null,
    OnSelect: function (index, row) {
        //console.log('OnSelect--row--' + JSON.stringify(row));
        var container = $('#dlgBarcodeTemplate');
        BarcodeTemplate.SelectRow = row;
        BarcodeTemplate.SetFm(JSON.parse(row.Attr));
        container.find('#lbtnSaveAs').linkbutton('enable');
        if (row.IsDefault) container.find('#lbtnSetDefault').linkbutton('disable');
        else container.find('#lbtnSetDefault').linkbutton('enable');
    },
    OnDlg: function (callBackFun) {
        var wh = Common.GetWh(960, 700);
        if ($("body").find("#BarcodeTemplate").length == 0) {
            $("body").append("<div id=\"dlgBarcodeTemplate\"></div>");
        }
        var dlg = $("#dlgBarcodeTemplate");
        dlg.dialog({
            title: '条码模板管理',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: Common.AppName + '/u/tbarcode.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSave', text: '确定', iconCls: 'icon-save', handler: function () {
                    if (typeof (eval(callBackFun)) == 'function') {
                        callBackFun(BarcodeTemplate.SelectRow);
                    }
                    dlg.dialog('close');
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('close');
                }
            }]
        })
    },
    GetBarcodeFormats: function () {
        var postData = '{"model":{"Id":""}}';
        var url = Common.AppName + '/Services/Service.svc/GetBarcodeFormats';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetBarcodeFormats--result.Data--' + result.Data);
            var jData = JSON.parse(result.Data);
            BarcodeTemplate.CbbBarcodeFormat(jData, null);
        });
    },
    CbbBarcodeFormat: function (jData, v) {
        if (!v || v == '') v = 'CODE_128';
        var cbb = $('#cbbBarcodeFormat');
        cbb.combobox({
            valueField: 'Key',
            textField: 'Value',
            data: jData,
            onLoadSuccess: function () {
                if (v != "") {
                    for (var i = 0; i < jData.length; i++) {
                        if (jData[i].Value == v) cbb.combobox('select', jData[i].Key);
                    }
                }
                else {
                    cbb.combobox('select', jData[0].Key);
                }
            }
        });
    },
    GetFm: function () {
        var isValid = $('#BarcodeTemplateFm').form('validate');
        if (!isValid) return null;
        var barcodeFormat = $('#cbbBarcodeFormat').combobox('getText');
        var width = $.trim($('#txtWidth').val()) == '' ? 200 : parseInt($('#txtWidth').val());
        var height = $.trim($('#txtHeight').val()) == '' ? 120 : parseInt($('#txtHeight').val());
        var barcode = $.trim($('#txtBarcode').val()) == '' ? '123456789' : $.trim($('#txtBarcode').val());
        var margin = $.trim($('#txtMargin').val()) == '' ? 0 : parseInt($('#txtMargin').val());
        return { BarcodeFormat: barcodeFormat, Width: width, Height: height, Barcode: barcode, Margin: margin, IsDefault: false };
    },
    SetFm: function (data) {
        $('#imgBarcodeBrowser').attr('src', Common.AppName + data.ImageUrl);
        $('#cbbBarcodeFormat').combobox('setValue', data.BarcodeFormat);
        $('#txtWidth').val(data.Width);
        $('#txtHeight').val(data.Height);
        $('#txtBarcode').val(data.Barcode);
        $('#txtMargin').val(data.Margin);
    },
    OnBrowser: function () {
        var postData = '{"model":' + JSON.stringify(BarcodeTemplate.GetFm()) + '}';
        var url = Common.AppName + '/Services/Service.svc/GetBarcodeBrowser';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            $('#imgBarcodeBrowser').attr('src', Common.AppName + result.Data);
        });
    },
    OnSave: function () {
        //console.log('OnSave--' + BarcodeTemplate.SelectRow);
        if (!BarcodeTemplate.SelectRow) {
            BarcodeTemplate.DlgTitle();
        }
        else {
            var jData = BarcodeTemplate.GetFm();
            if (!jData) return false;
            jData.Id = BarcodeTemplate.SelectRow.Id;
            jData.Title = BarcodeTemplate.SelectRow.Title;
            jData.IsDefault = BarcodeTemplate.SelectRow.IsDefault;
            BarcodeTemplate.Save(null, jData);
        }
    },
    OnSaveAsTemplate: function () {
        BarcodeTemplate.DlgTitle();
    },
    DlgTitle: function () {
        if ($("body").find("#dlgTitle").length == 0) {
            $("body").append('<div id="dlgTitle" style="padding:10px;"></div>');
        }
        var dlg = $("#dlgTitle");
        dlg.dialog({
            title: '保存模板',
            width: 400,
            height: 150,
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            content: '<form id="dlgTitleFm">标题：<input id="txtTitle" class="easyui-textbox" data-options="required:true" style="width:88%"></form>',
            buttons: [{
                id: 'btnSaveByDlgSave', text: '确定', iconCls: 'icon-save', handler: function () {
                    var isValid = $('#dlgTitleFm').form('validate');
                    if (!isValid) return false;

                    var jData = BarcodeTemplate.GetFm();
                    if (!jData) return false;
                    jData.Title = $.trim($('#txtTitle').val());

                    BarcodeTemplate.Save(dlg, jData);
                }
            }, {
                id: 'btnCancelByDlgSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('close');
                }
            }]
        })
    },
    Save: function (dlg, jData) {
        jData.TypeName = 'Barcode';
        var postData = '{"model":' + JSON.stringify(jData) + '}';
        var url = Common.AppName + '/Services/Service.svc/SaveBarcodeTemplate';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            if (dlg) dlg.dialog('close');
            BarcodeTemplate.LoadDg(1, 100);
            jeasyuiFun.show("温馨提示", "保存成功！");
        });

    },
    OnDel: function () {
        var dg = $("#dgBarcodeTemplate");
        var rows = dg.datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteBarcodeTemplate",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                    },
                    complete: function () {
                        $.messager.progress('close');
                    },
                    success: function (result) {
                        if (result.ResCode != 1000) {
                            $.messager.alert('系统提示', result.Msg, 'info');
                            return false;
                        }
                        jeasyuiFun.show("温馨提示", "操作成功！");
                        BarcodeTemplate.LoadDg(1, 100);
                    }
                });
            }
        });
    },
    OnSetDefault: function () {
        var dg = $("#dgBarcodeTemplate");
        var rows = dg.datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var jData = rows[0];
        jData.IsDefault = true;
        BarcodeTemplate.Save(null, JSON.stringify(jData));
    }
}