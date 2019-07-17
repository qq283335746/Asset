var Dics = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadTree();
    },
    SelectNode: null,
    LoadTree: function () {
        var t = $("#treeCt");
        var postData = {};
        var url = Common.AppName + '/Services/Service.svc/GetDicsTree';
        Common.Ajax(url, postData, 'GET', '', true, true, function (result) {
            t.tree('loadData', JSON.parse(result.Data));
        });
    },
    OnLoadSuccess: function (node, data) {
        var t = $(this);
        if (Dics.SelectNode) {
            Dics.ExpandParent(t, Dics.SelectNode);
        }
    },
    OnTreeSelect: function (node) {
        Dics.SelectNode = node;
    },
    Add: function () {
        if (!Dics.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = Dics.SelectNode;
        if ($("body").find("#dlgDics").length == 0) {
            $("body").append("<div id=\"dlgDics\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgDics").dialog({
            title: '新建数据字典信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Dics.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgDics').dialog('close');
                }
            }],
            href: '/Asset/u/tdics.html',
            onLoad: function () {
                var data = {};
                data.ParentName = node.text;
                data.ParentId = node.id;
                data.Id = '';
                Dics.SetFm(data);
            }
        })
        return false;
    },
    Edit: function () {
        if (!Dics.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = Dics.SelectNode;
        var t = $("#treeCt");
        var pNode = t.tree('getParent', node.target);
        if ($("body").find("#dlgDics").length == 0) {
            $("body").append("<div id=\"dlgDics\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgDics").dialog({
            title: '编辑数据字典信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Dics.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgDics').dialog('close');
                }
            }],
            href: '/Asset/u/tdics.html',
            onLoad: function () {
                var jData = node.attributes;
                jData.ParentName = pNode.attributes.Named;
                Dics.SetFm(node.attributes);
            }
        })
        return false;
    },
    Del: function () {
        if (!Dics.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = Dics.SelectNode;
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"appCode": "' + Common.GetAppId() + '","Id": "' + node.id + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteDics";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    var t = $("#treeCt");
                    t.tree('remove', node.target);
                });
            }
        });
    },
    Save: function () {
        var contarner = $('#dlgDics');
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
        Dics.GetStep(t, currentNode);
        var step = Dics.Step;
        Dics.Step = "";

        var postData = '{"model":{"AppCode":"' + appCode + '","Id":"' + id + '","ParentId":"' + parentId + '","Coded":"' + coded + '","Named":"' + named + '","Step":"' + step + '","Sort":"' + sort + '","Remark":"' + remark + '"}';
        var url = Common.AppName + "/Services/Service.svc/SaveDics";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgDics").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                Dics.LoadTree();
            }, 700);
        });
    },
    SetFm: function (data) {
        //console.log('data--' + JSON.stringify(data));
        var contarner = $('#dlgDics');
        contarner.find('#hId').val(data.Id);
        contarner.find('#lbParentName').text(data.ParentName);
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
                Dics.ExpandParent(t, pNode);
            }
        }
    },
    Step: "",
    GetStep: function (t, node) {
        if (node) {
            Dics.Step += node.id + ",";
            var pNode = t.tree('getParent', node.target);
            if (pNode) {
                Dics.GetStep(t, pNode, Dics.Step);
            }
        }
    }
}