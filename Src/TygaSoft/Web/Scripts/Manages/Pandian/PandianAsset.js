var PandianAsset = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        PandianAsset.GetPandianInfo();
        var dg = $("#dg");
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                PandianAsset.LoadDg(pageNumber, pageSize);
            }
        })
        var pagerOptions = jeasyuiFun.getDgPagerOptions(dg);
        PandianAsset.LoadDg(1, pagerOptions.PageSize);
    },
    Container: $("#dlgPandianAsset"),
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dg");
        var Id = Common.GetQueryString("Id");
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "ParentId": "' + Id + '"}}';
        var url = Common.AppName + "/Services/Service.svc/GetPandianAssetList";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            //console.log(result.Data);
            if (typeof result.Data == 'string') result.Data = eval("(" + result.Data + ")");
            dg.datagrid('loadData', result.Data);
            var footerData = result.Data.footer;
            var lis = $('#detailPanel>li');
            lis.eq(0).text("已盘（" + footerData[0].TotalPan + "）");
            lis.eq(1).text("盘盈（" + footerData[0].TotalYpan + "）");
            lis.eq(2).text("未盘（" + footerData[0].TotalNotPan + "）");
        });
    },
    GetPandianInfo: function () {
        var data = { "id": "" + Common.GetQueryString("Id") + "" };
        var url = Common.AppName + "/Services/Service.svc/GetPandianInfo";
        Common.Ajax(url, data, 'GET', '', true, true, function (result) {
            //console.log('GetPandianInfo--' + JSON.stringify(result));
            var jData = JSON.parse(result.Data);
            if (jData.StatusName == "已完成") $('#abtnSave').linkbutton('disable');
            $('#lbPandianName').text(jData.Named);
            $('#lbPandianStatus').text(jData.StatusName);
            $('#lbPandianDate').text(jData.SCreateDate);
            if (jData.Status > 0) $('#abtnDel').linkbutton('disable');
        })
    },
    Save: function () {
        var dg = $("#dg");
        dg.datagrid('selectAll');
        var Id = Common.GetQueryString("Id");
        var postData = '{"pandianId":"' + Id + '"}';
        var url = Common.AppName + "/Services/Service.svc/SavePandianAssetResult";
        $.messager.confirm('提醒', '单据是否已全部处理完毕，且执行结案操作！（注：此处为不可逆操作）', function (r) {
            if (r) {
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("提示", "操作成功！");
                    setTimeout(function () {
                        var pagerOptions = jeasyuiFun.getDgPagerOptions(dg);
                        PandianAsset.GetPandianInfo();
                        PandianAsset.LoadDg(pagerOptions.PageIndex, pagerOptions.PageSize);
                        $('#abtnSave').linkbutton('disable');
                    }, 700);
                });
            }
        });
    },
    Del: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var pandianId = Common.GetQueryString("Id");
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].AssetId;
        }
        if (!pandianId || pandianId == "") {
            $.messager.alert('错误提示', "非法操作，已终止执行", 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"pandianId":"' + pandianId + '", "itemAppend":"' + itemAppend + '"}';
                var url = Common.AppName + "/Services/Service.svc/DeletePandianAsset";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("提示", "操作成功！");
                    var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
                    PandianAsset.LoadDg(pagerOptions.PageIndex, pagerOptions.PageSize);
                });

            }
        });
    },
    OnExport: function () {
        var pandianId = Common.GetQueryString("Id");
        if (!pandianId || $.trim(pandianId) == '') {
            $.messager.alert('错误提示', "未找到相应的盘点单ID", 'error');
            return false;
        }
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/asset/h/upload.html?ReqName=ExportPandianAsset&pandianId=" + pandianId + "");
            }
        })
    }
}