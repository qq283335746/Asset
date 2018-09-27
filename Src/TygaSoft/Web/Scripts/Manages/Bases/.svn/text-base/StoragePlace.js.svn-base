var StoragePlace = {
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
                StoragePlace.Load(pageNumber, pageSize);
            }
        });
    },
    Action: null,
    Load: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetStoragePlaceList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetStoragePlaceList--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
        StoragePlace.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    Add: function () {
        StoragePlace.Action = 'Add';
        if ($("body").find("#dlgStoragePlace").length == 0) {
            $("body").append("<div id=\"dlgStoragePlace\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(570, 300);
        $("#dlgStoragePlace").dialog({
            title: '新增存放地点信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    StoragePlace.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStoragePlace').dialog('close');
                }
            }],
            href: Common.AppName + '/u/tstorage.html',
            onLoad: function () {
                var jData = {};
                jData.Id = '';
                StoragePlace.SetFm(jData);
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
        if ($("body").find("#dlgStoragePlace").length == 0) {
            $("body").append("<div id=\"dlgStoragePlace\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(570, 300);
        $("#dlgStoragePlace").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    StoragePlace.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStoragePlace').dialog('close');
                }
            }],
            href: Common.AppName + '/u/tstorage.html',
            onLoad: function () {
                StoragePlace.SetFm(rows[0]);
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
                var url = Common.AppName + "/Services/Service.svc/DeleteStoragePlace";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
                        StoragePlace.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;
        var id = $.trim($("#txtId").val());
        var coded = $.trim($("#txtCoded").val());
        var named = $.trim($("#txtNamed").val());

        var postData = '{"model":{"Id":"' + id + '","AppCode":"' + Common.GetAppId() + '","Coded":"' + coded + '","Named":"' + named + '"}';
        var url = Common.AppName + "/Services/Service.svc/SaveStoragePlace";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgStoragePlace").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
                StoragePlace.Load(1, pagerOptions.PageSize);
            }, 700);
        });
    },
    SetFm: function (data) {
        var contarner = $('#dlgStoragePlace');
        contarner.find('#hId').val(data.Id);
        contarner.find('#txtCoded').val(data.Coded);
        contarner.find('#txtNamed').val(data.Named);
    },
    OnImport: function () {
        DlgFiles.Params = { ReqName: 'ImportStoragePlace', AppCode: Common.GetAppId() };
        DlgFiles.DlgUpload('StoragePlace', function (result) {
            var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
            StoragePlace.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
        })
    },
    OnExport: function () {
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/asset/h/upload.html?ReqName=ExportStoragePlace");
            }
        })
    }
}