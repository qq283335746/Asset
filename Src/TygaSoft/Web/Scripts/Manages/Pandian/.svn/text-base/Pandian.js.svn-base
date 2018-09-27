var Pandian = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var pager = $("#dgPandian").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Pandian.LoadDg(pageNumber, pageSize);
            }
        })
        Pandian.LoadDg(1, 10);
    },
    Container: $("#dlgPandian"),
    Action: null,
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dgPandian");
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + "/Services/Service.svc/GetPandianList";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
//            console.log(result.Data);
//            return false;
            if (typeof result.Data == 'string') result.Data = eval("(" + result.Data + ")");
            dg.datagrid('loadData', result.Data);
            var footerData = result.Data.footer;
            var totalAll = 0;
            var totalFinish = 0;
            var totalNotFinish = 0;
            if (footerData && footerData.length > 0) {
                totalAll = footerData[0].TotalAll;
                totalFinish = footerData[0].TotalFinish;
                totalNotFinish = footerData[0].TotalNotFinish;
            }
            var lis = $('#toolbar>ul>li');
            lis.eq(0).text("" + totalAll + " 盘点单 (全部)");
            lis.eq(1).text("" + totalFinish + " 盘点单 (已完成)");
            lis.eq(2).text("" + totalNotFinish + " 盘点单 (未完成)");
        });
    },
    OnSearch: function () {
        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgPandian"));
        Pandian.LoadDg(pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    FormatName: function (value, row, index) {
        return '<a href="/asset/u/ypandian.html?Id=' + row.Id + '" class="c_blue">' + value + '</a>';
    },
    Add: function () {
        Pandian.Action = 'Add';
        if ($("body").find("#dlgAddPandian").length == 0) {
            $("body").append("<div id=\"dlgAddPandian\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(880, 550);
        $("#dlgAddPandian").dialog({
            title: '新增盘点单信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: Common.AppName + '/u/tpandian.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '创建', iconCls: 'icon-save', handler: function () {
                    Pandian.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddPandian').dialog('close');
                }
            }],
            onLoad: function () {
                jeasyuiFun.cbt("cbtCategory", null, Common.AppName + "/Services/Service.svc/GetCategoryTree", '', null);
                jeasyuiFun.cbt("cbtUseDepmt", null, Common.AppName + "/Services/Service.svc/GetOrgDepmtTree", '', null);
                jeasyuiFun.cbt("cbtMgrDepmt", null, Common.AppName + "/Services/Service.svc/GetOrgDepmtTree", '', null);
                jeasyuiFun.cbb("cbbStoragePlace", null, Common.AppName + "/Services/Service.svc/GetCbbStoragePlace", '', null);
            }
        })
        return false;
    },
    OnView: function () {
        var rows = $("#dgPandian").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据再进行操作", 'error');
            return false;
        }

        window.location = Common.AppName + '/u/ypandian.html?Id=' + rows[0].Id + '';
    },
    Del: function () {
        var rows = $("#dgPandian").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"itemAppend":"' + itemAppend + '"}';
                var url = Common.AppName + "/Services/Service.svc/DeletePandian";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("提示", "操作成功！");
                    var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgPandian"));
                    Pandian.LoadDg(pagerOptions.PageIndex, pagerOptions.PageSize);
                });

            }
        });
    },
    Save: function () {
        var container = $("#dlgAddPandian");
        var isValid = container.find('#dlgFm').form('validate');
        if (!isValid) return false;
        var categoryId = container.find('#cbtCategory').combotree('getValue');
        var useDepmtId = container.find("#cbtUseDepmt").combotree('getValue');
        var mgrDepmtId = container.find("#cbtMgrDepmt").combotree('getValue');
        var storagePlaceId = container.find('#cbbStoragePlace').combobox('getValue');
        var named = $.trim(container.find('#txtName').textbox('getValue'));
        var remark = $.trim(container.find('#txtRemark').textbox('getValue'));
        var startBuyDate = container.find('#txtStartBuyDate').datebox('getValue');
        var endBuyDate = container.find('#txtEndBuyDate').datebox('getValue');
        var isConfirm = container.find('#hIsConfirm').val();
        var Id = $.trim($("#hId").val());

        var sData = '{"AppCode":"' + Common.GetAppId() + '","Id":"' + Id + '","Named":"' + named + '","Remark":"' + remark + '","StartBuyDate":"' + startBuyDate + '","EndBuyDate":"' + endBuyDate + '","CategoryId":"' + categoryId + '","UseDepmtId":"' + useDepmtId + '","MgrDepmtId":"' + mgrDepmtId + '","StoragePlaceId":"' + storagePlaceId + '","IsConfirm":"' + isConfirm + '"}';

        Pandian.SaveData(sData);
    },
    SaveData: function (sData) {
        var container = $("#dlgAddPandian");
        var postData = '{"model":' + sData + '}';
        var url = Common.AppName + "/Services/Service.svc/SavePandian";
        Common.Ajax(url, postData, "POST", "", true, false, function (result) {
            if (result.ResCode == 1002) {
                $.messager.confirm('系统提示', result.Msg, function (r) {
                    if (r) {
                        $('#hIsConfirm').val('true');
                        Pandian.Save();
                    }
                });

                return false;
            }

            if (result.ResCode != 1000) {
                $.messager.alert('系统提示', result.Msg, 'info');
                return false;
            }

            container.dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgPandian"));
                Pandian.LoadDg(pagerOptions.PageIndex, pagerOptions.PageSize);
            }, 1000);
        });

    }
}