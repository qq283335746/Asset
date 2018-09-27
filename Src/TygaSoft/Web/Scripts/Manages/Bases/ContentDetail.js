var ContentDetail = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var pager = $("#dgContentDetail").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ContentDetail.Load(pageNumber, pageSize);
            }
        });
        ContentDetail.Load(1, pager.pagination('options').pageSize);
    },
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var contentTypeId = null;
        if (ContentType.SelectNode) contentTypeId = ContentType.SelectNode.id;
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '", "ParentId": "' + contentTypeId + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetContentDetailList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetContentDetailList--result--' + JSON.stringify(result));
            $("#dgContentDetail").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        var pager = $("#dgContentDetail").datagrid('getPager');
        ContentDetail.Load(1, pager.pagination('options').pageSize);
    },
    Add: function () {
        if (!ContentType.SelectNode) {
            $.messager.alert('错误提示', '请选中一个分类节点再进行操作', 'error');
            return false;
        }
        ContentDetail.SelectRow = null;
        if ($("body").find("#dlgContentDetail").length == 0) {
            $("body").append("<div id=\"dlgContentDetail\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(960, 580);
        $("#dlgContentDetail").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '下一步', iconCls: 'icon-save', handler: function () {
                    ContentDetail.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgContentDetail').dialog('close');
                }
            }],
            href: '/Me/u/ycontent.html',
            onLoad: function () {
                var jData = {};
                jData.Id = '';
                jData.ContentTypeId = ContentType.SelectNode.id;
                ContentDetail.SetFm(jData);
                $('#tabsContent').tabs('disableTab', 1);
            }
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dgContentDetail").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        if ($("body").find("#dlgContentDetail").length == 0) {
            $("body").append("<div id=\"dlgContentDetail\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(960, 580);
        $("#dlgContentDetail").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    ContentDetail.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgContentDetail').dialog('close');
                }
            }],
            href: '/Me/u/ycontent.html',
            onLoad: function () {
                ContentDetail.SetFm(rows[0]);
                ContentDetail.LoadFiles(rows[0].Id);
            }
        })
        return false;
    },
    Del: function () {
        var rows = $("#dgContentDetail").datagrid('getSelections');
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
                var url = Common.AppName + "/Services/Service.svc/DeleteContentDetail";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        var pager = $("#dgContentDetail").datagrid('getPager');
                        ContentDetail.Load(1, pager.pagination('options').pageSize);
                    }, 700);
                });
            }
        });
    },
    Save: function () {
        if ($.trim($('#dlgContentDetail').next().find('a:first').find('.l-btn-text').text()) == '完成') {
            $("#dlgContentDetail").dialog('close');
            return false;
        }
        var contarner = $('#dlgContentDetail');
        var isValid = contarner.find('#baseFm').form('validate');
        if (!isValid) return false;
        var sTitle = $.trim(contarner.find('#txtTitle').textbox('getValue'));
        var sKeyword = $.trim(contarner.find('#txtKeyword').textbox('getValue'));
        var sDescr = $.trim(contarner.find('#txtDescr').textbox('getValue'));
        var sSort = $.trim(contarner.find('#txtSort').textbox('getValue'));
        if (sSort == '') sSort = 0;
        var sContent = $.trim(contarner.find('#txtContent').textbox('getValue'));
        var sId = $.trim(contarner.find("#hId").val());
        var sContentTypeId = $.trim(contarner.find("#hContentTypeId").val());
        var sOpenness = 0;

        var postData = '{"model":{ "AppCode": "' + Common.GetAppId() + '", "Id": "' + sId + '", "ContentTypeId": "' + sContentTypeId + '", "Title": "' + sTitle + '", "Keyword": "' + sKeyword + '", "Descr": "' + sDescr + '", "ContentText": "' + sContent + '", "Openness": "' + sOpenness + '", "Sort": "' + sSort + '" }}';
        var url = Common.AppName + "/Services/Service.svc/SaveContentDetail";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            setTimeout(function () {
                var pager = $("#dgContentDetail").datagrid('getPager');
                ContentDetail.Load(1, pager.pagination('options').pageSize);
            }, 700);
            if (sId == '') {
                contarner.find("#hId").val(result.Data);
                $('#tabsContent').tabs('enableTab', 1);
                $('#tabsContent').tabs('select', 1);
                $('#dlgContentDetail').next().find('a:first').find('.l-btn-text').text('完成');
            }
            else {
                jeasyuiFun.show("温馨提示", "操作成功！");
                $("#dlgContentDetail").dialog('close');
            }
        });
    },
    SetFm: function (data) {
        var contarner = $('#dlgContentDetail');
        contarner.find('#hId').val(data.Id);
        contarner.find('#hContentTypeId').val(data.ContentTypeId);
        contarner.find('#txtTitle').textbox('setValue', data.Title);
        contarner.find('#txtKeyword').textbox('setValue', data.Keyword);
        contarner.find('#txtDescr').textbox('setValue', data.Descr);
        contarner.find('#txtSort').textbox('setValue', data.Sort);
        contarner.find('#txtContent').textbox('setValue', data.ContentText);
    },
    LoadFiles: function (contentId) {
        var postData = { contentId: contentId };
        var url = Common.AppName + '/Services/Service.svc/GetContentFilesByContentId';
        Common.Ajax(url, postData, 'GET', '', true, true, function (result) {
            //console.log('LoadFiles--result--' + JSON.stringify(result));
            $("#dgFile").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    FDoBtns: function (value, row, index) {
        var sBtns = '';
        sBtns += '<a class="abtn mr10" Code="' + row.Id + '" onclick="ContentDetail.DeleteFile(this)">删除</a>';
        if ($.trim(row.ViewUrl) != '') {
            sBtns += '<a class="abtn mr10" target="_blank" href="' + Common.AppName + row.ViewUrl + '">打开</a>';
        }
        if ($.trim(row.FileUrl) != '') {
            sBtns += '<a class="abtn mr10" target="_blank" href="' + Common.AppName + row.FileUrl + '">下载</a>';
        }
        return sBtns;
    },
    DeleteFile: function (t) {
        var $this = $(t);
        var Id = $this.attr("Code");
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"Id": "' + Id + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteContentFile";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    var contarner = $('#dlgContentDetail');
                    var contentId = contarner.find("#hId").val();
                    ContentDetail.LoadFiles(contentId);
                    jeasyuiFun.show("温馨提示", "操作成功！");
                });
            }
        });
    },
    DlgUpload: function () {
        var contarner = $('#dlgContentDetail');
        var sContentId = $.trim(contarner.find("#hId").val());
        DlgFiles.Params = { ReqName: 'UploadContentFile', ContentId: sContentId, AppCode: Common.GetAppId() };
        DlgFiles.DlgUpload('ContentFile', function (result) {
            ContentDetail.LoadFiles(sContentId);
        })
    }
}