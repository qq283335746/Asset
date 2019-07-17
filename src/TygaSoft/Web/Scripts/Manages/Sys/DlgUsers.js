var DlgUsers = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadDg(1, 10);
    },
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dgDlgUsers");
        var postData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}}';
        var url = Common.AppName + "/Services/SecurityService.svc/GetUserList";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            console.log('result--' + JSON.stringify(result));
            if (result.ResCode != 1000) {
                $.messager.alert('提示', result.Msg, 'info');
                return false;
            }

            dg.datagrid('loadData', eval("(" + result.Data + ")"));
        });
    },
    OnDlg: function (clientId, targetId, targetTextId) {
        if ($("body").find("#dlgUsers").length == 0) {
            $("body").append("<div id=\"dlgUsers\"></div>");
        }
        var h = $(window).height();
        if (h > 520) h = 520;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 600) w = 600;
        else w = w * 0.95;
        $("#dlgUsers").dialog({
            title: '选择用户',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '/asset/u/dlgusers.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSelectUsers', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var rows = $("#dgDlgUsers").datagrid('getSelections');
                    if (!rows || rows.length < 1) {
                        $.messager.alert('错误提示', "请至少选择一行数据进行操作", 'error');
                        return false;
                    }
                    DlgUsers.SetDatagrid(rows, clientId, targetId, targetTextId);
                    $('#dlgUsers').dialog('close');
                }
            }, {
                id: 'btnCancelSelectUsers', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgUsers').dialog('close');
                }
            }]
        })
    },
    SetDatagrid: function (rows, clientId, targetId, targetTextId) {
        var userTextAppend = "";
        var userIdAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) {
                userTextAppend += ';';
                userIdAppend += '|';
            }
            userTextAppend += rows[i].UserName;
            userIdAppend += rows[i].Id;
        }

        var oldText = $.trim(targetTextId.val());
        if (oldText != "") {
            userTextAppend = oldText + ";" + userTextAppend;
        }
        targetTextId.val(userTextAppend);

        var oldId = $.trim(targetId.val());
        if (oldId != "") {
            userIdAppend = oldId + "|" + userIdAppend;
        }
        targetId.val(userIdAppend);

        clientId.textbox('setValue', userTextAppend);
    }
}