var ContentType = {
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
                ContentType.Load(pageNumber, pageSize);
            }
        });
    },
    SelectNode: null,
    Load: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetContentTypeList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetContentTypeList--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    LoadTree: function () {
        var t = $("#treeCt");
        var postData = {};
        var url = Common.AppName + '/Services/Service.svc/GetContentTypeTree';
        Common.Ajax(url, postData, 'GET', '', true, true, function (result) {
            //console.log('GetContentTypeTree--result--' + JSON.stringify(result));
            t.tree('loadData', JSON.parse(result.Data));
        });
    },
    OnLoadSuccess: function (node, data) {
        var t = $(this);
        if (ContentType.SelectNode) {
            ContentType.ExpandParent(t, ContentType.SelectNode);
        }
    },
    OnTreeSelect: function (node) {
        ContentType.SelectNode = node;
        var pageSize = 10;
        var pager = $("#dgContentDetail").datagrid('getPager');
        if(pager) pageSize = pager.pagination('options').pageSize;
        ContentDetail.Load(1, pageSize);
    },
    Add: function () {
        if (!ContentType.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = ContentType.SelectNode;
        if ($("body").find("#dlgContentType").length == 0) {
            $("body").append("<div id=\"dlgContentType\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgContentType").dialog({
            title: '新建信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    ContentType.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgContentType').dialog('close');
                }
            }],
            href: '/Asset/u/tcontent.html',
            onLoad: function () {
                var data = {};
                data.ParentText = node.text;
                data.ParentId = node.id;
                data.Id = '';
                ContentType.SetFm(data);
            }
        })
        return false;
    },
    Edit: function () {
        if (!ContentType.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = ContentType.SelectNode;
        var t = $("#treeCt");
        var pNode = t.tree('getParent', node.target);
        if ($("body").find("#dlgContentType").length == 0) {
            $("body").append("<div id=\"dlgContentType\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgContentType").dialog({
            title: '编辑分类信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    ContentType.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgContentType').dialog('close');
                }
            }],
            href: '/Asset/u/tcontent.html',
            onLoad: function () {
                var jData = node.attributes;
                jData.ParentText = pNode.attributes.Named;
                ContentType.SetFm(node.attributes);
            }
        })
        return false;
    },
    Del: function () {
        if (!ContentType.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = ContentType.SelectNode;
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"appCode": "' + Common.GetAppId() + '","Id": "' + node.id + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteContentType";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    var t = $("#treeCt");
                    t.tree('remove', node.target);
                });
            }
        });
    },
    Save: function () {
        var contarner = $('#dlgContentType');
        var isValid = contarner.find('#dlgFm').form('validate');
        if (!isValid) return false;
        var appCode = $.trim($("[id$=lbAppId]").text());
        var id = $.trim(contarner.find("#hId").val());
        var parentId = $.trim(contarner.find("#hParentId").val());
        var coded = $.trim(contarner.find("#txtCoded").val());
        var named = $.trim(contarner.find("#txtNamed").val());
        var sort = $.trim(contarner.find("#txtSort").val());
        var remark = $.trim(contarner.find("#txtRemark").val());
        if (sort == '') sort = 0;

        var t = $("#treeCt");
        var currentNode = t.tree('getSelected');
        ContentType.GetStep(t, currentNode);
        var step = ContentType.Step;
        ContentType.Step = "";

        var postData = '{"model":{"AppCode":"' + appCode + '","Id":"' + id + '","ParentId":"' + parentId + '","Coded":"' + coded + '","Named":"' + named + '","Step":"' + step + '","Sort":"' + sort + '","Remark":"' + remark + '"}';
        var url = Common.AppName + "/Services/Service.svc/SaveContentType";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgContentType").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                ContentType.LoadTree();
            }, 700);
        });
    },
    SetFm: function (data) {
        //console.log('data--' + JSON.stringify(data));
        var contarner = $('#dlgContentType');
        contarner.find('#hId').val(data.Id);
        contarner.find('#lbParentText').text(data.ParentText);
        contarner.find('#hParentId').val(data.ParentId);
        contarner.find('#txtCoded').val(data.Coded);
        contarner.find('#txtNamed').val(data.Named);
        contarner.find('#txtSort').val(data.Sort);
        contarner.find('#txtRemark').val(data.Remark);
    },
    ExpandParent: function (t, node) {
        var rootNode = t.tree('getRoot');
        var pNode = t.tree('find', node.attributes.ParentId);
        if (pNode) {
            if (rootNode && pNode.id != rootNode.id) {
                t.tree('expandAll', pNode.target);
                ContentType.ExpandParent(t, pNode);
            }
        }
    },
    Step: "",
    GetStep: function (t, node) {
        if (node) {
            ContentType.Step += node.id + ",";
            var pNode = t.tree('getParent', node.target);
            if (pNode) {
                ContentType.GetStep(t, pNode, ContentType.Step);
            }
        }
    }
}