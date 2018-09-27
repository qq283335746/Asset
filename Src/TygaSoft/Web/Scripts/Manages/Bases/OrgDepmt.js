var OrgDepmt = {
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
        var url = Common.AppName + '/Services/Service.svc/GetOrgDepmtTree';
        Common.Ajax(url, postData, 'GET', '', true, true, function (result) {
            t.tree('loadData', JSON.parse(result.Data));
        });
    },
    OnLoadSuccess: function (node, data) {
        var t = $(this);
        if (OrgDepmt.SelectNode) {
            OrgDepmt.ExpandParent(t, OrgDepmt.SelectNode);
        }
    },
    OnTreeSelect: function (node) {
        OrgDepmt.SelectNode = node;
        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgStaff"));
        Staff.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    Add: function () {
        if (!OrgDepmt.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = OrgDepmt.SelectNode;
        if ($("body").find("#dlgOrgDepmt").length == 0) {
            $("body").append("<div id=\"dlgOrgDepmt\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgOrgDepmt").dialog({
            title: '新建分类信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrgDepmt.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrgDepmt').dialog('close');
                }
            }],
            href: Common.AppName + '/u/torg.html',
            onLoad: function () {
                var data = {};
                data.ParentName = node.text;
                data.ParentId = node.id;
                data.Id = '';
                OrgDepmt.SetFm(data);
            }
        })
        return false;
    },
    Edit: function () {
        if (!OrgDepmt.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = OrgDepmt.SelectNode;
        var t = $("#treeCt");
        var pNode = t.tree('getParent', node.target);
        if ($("body").find("#dlgOrgDepmt").length == 0) {
            $("body").append("<div id=\"dlgOrgDepmt\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(600, 400);
        $("#dlgOrgDepmt").dialog({
            title: '编辑分类信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    OrgDepmt.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgOrgDepmt').dialog('close');
                }
            }],
            href: Common.AppName + '/u/torg.html',
            onLoad: function () {
                var jData = node.attributes;
                jData.ParentName = pNode ? pNode.attributes.Named:"";
                OrgDepmt.SetFm(node.attributes);
            }
        })
        return false;
    },
    Del: function () {
        if (!OrgDepmt.SelectNode) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var node = OrgDepmt.SelectNode;
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"appCode": "' + Common.GetAppId() + '","Id": "' + node.id + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteOrgDepmt";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    var t = $("#treeCt");
                    t.tree('remove', node.target);
                });
            }
        });
    },
    Save: function () {
        var contarner = $('#dlgOrgDepmt');
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
        OrgDepmt.GetStep(t, currentNode);
        var step = OrgDepmt.Step;
        OrgDepmt.Step = "";

        var postData = '{"model":{"AppCode":"' + appCode + '","Id":"' + id + '","ParentId":"' + parentId + '","Coded":"' + coded + '","Named":"' + named + '","Step":"' + step + '","Sort":"' + sort + '","Remark":"' + remark + '"}';
        var url = Common.AppName + "/Services/Service.svc/SaveOrgDepmt";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            $("#dlgOrgDepmt").dialog('close');
            jeasyuiFun.show("温馨提示", "操作成功！");
            setTimeout(function () {
                OrgDepmt.LoadTree();
            }, 700);
        });
    },
    SetFm: function (data) {
        //console.log('data--' + JSON.stringify(data));
        var contarner = $('#dlgOrgDepmt');
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
                OrgDepmt.ExpandParent(t, pNode);
            }
        }
    },
    Step: "",
    GetStep: function (t, node) {
        if (node) {
            OrgDepmt.Step += node.id + ",";
            var pNode = t.tree('getParent', node.target);
            if (pNode) {
                OrgDepmt.GetStep(t, pNode, OrgDepmt.Step);
            }
        }
    },
    OnImport: function () {
        DlgFiles.Params = { ReqName: 'ImportOrgDepmt', AppCode: Common.GetAppId() };
        DlgFiles.DlgUpload('OrgDepmt', function (result) {
            var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dg"));
            OrgDepmt.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
        })
    },
    OnExport: function () {
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/asset/h/upload.html?ReqName=ExportOrgDepmt");
            }
        })
    }
}