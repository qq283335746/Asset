
var ListMenus = {
    Init: function () {
        this.Load();
    },
    Container: $("#dlgMenus"),
    Url: "",
    Load: function () {
        var t = $("#treeCt");
        var url = Common.AppName + "/Services/SecurityService.svc/GetMenusTree";
        Common.Ajax(url, {}, 'GET', '', true, true, function (result) {
            t.tree({
                data: JSON.parse(result.Data),
                animate: true
//                onContextMenu: function (e, node) {
//                    e.preventDefault();
//                    $(this).tree('select', node.target);
//                    $('#mmTree').menu('show', {
//                        left: e.pageX,
//                        top: e.pageY
//                    });
//                }
            })
        })
    },
    Add: function () {
        //this.Url = Common.AppName + "/Services/SecurityService.svc/SaveMenus";
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var wh = Common.GetWh(600, 300);
        ListMenus.Container.dialog({
            title: "新建菜单导航",
            width: wh[0],
            height: wh[1],
            href: '/Asset/u/ymenu.html?action=add&Id=' + node.id + '',
            closed: false,
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveMenus', text: '保存', iconCls: 'icon-save', handler: function () {
                    ListMenus.Save();
                }
            }, {
                id: 'btnCancelSaveMenus', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgMenus').dialog('close');
                }
            }]
        });
    },
    Edit: function () {
        //this.Url = Common.AppName + "/Services/SecurityService.svc/SaveMenus";
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        $("#hCurrExpandNode").val(node.id);
        var wh = Common.GetWh(600, 300);
        ListMenus.Container.dialog({
            title: "编辑菜单导航",
            width: wh[0],
            height: wh[1],
            href: Common.AppName + '/u/ymenu.html?action=edit&Id=' + node.id + '',
            closed: false,
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveMenus', text: '保存', iconCls: 'icon-save', handler: function () {
                    ListMenus.Save();
                }
            }, {
                id: 'btnCancelSaveMenus', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgMenus').dialog('close');
                }
            }]
        });
    },
    Del: function () {
        var t = $("#treeCt");
        var node = t.tree('getSelected');

        if (!node) {
            $.messager.alert('错误提示', "请选中一个节点再进行操作", 'error');
            return false;
        }

        try {
            var childNodes = t.tree('getChildren', node.target);
            if (childNodes && childNodes.length > 0) {
                $.messager.alert('错误提示', "请先删除子节点再删除此节点", 'error');
                return false;
            }
        }
        catch (e) {
            $.messager.alert('错误提示', "请先删除子节点再删除此节点", 'error');
            return false;
        }

        if (node) {
            $.messager.confirm('温馨提醒', '确定要删除吗?', function (r) {
                if (r) {
                    var postData = '{"Id": "' + node.id + '" }';
                    var url = Common.AppName + "/Services/SecurityService.svc/DeleteMenus";
                    Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
                        jeasyuiFun.show("温馨提示", "保存成功！");
                        t.tree('remove', node.target);
                    })
                }
            });
        }
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;

        var Id = $.trim($("#hId").val());
        var parentId = $("#hParentId").val();
        var sTitle = $.trim($("#txtTitle").val());
        var sUrl = $.trim($("#txtUrl").val());
        var sDescr = $.trim($("#txtDescr").val());
        var sort = $.trim($("#txtSort").val());
        if (sort == "") sort = 0;

        var t = $('#treeCt');
        var selectedNode = t.tree('getSelected');
        ListMenus.IdStep = '';
        ListMenus.GetIdStep(t, selectedNode);
        var sIdStep = ListMenus.IdStep;

        var postData = '{"model":{"Id":"' + Id + '","ParentId":"' + parentId + '","Title":"' + sTitle + '","IdStep":"' + sIdStep + '","Url":"' + sUrl + '","Descr":"' + sDescr + '","Sort":"' + sort + '"}}';
        //console.log('postData--' + postData);
        //return false;

        var url = Common.AppName + "/Services/SecurityService.svc/SaveMenus";
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            jeasyuiFun.show("温馨提示", "保存成功！");
            var selected = t.tree('getSelected');
            if (selected.text == "请选择") {
                t.tree('update', {
                    target: selected.target,
                    id: result.Data,
                    text: sTitle
                });
                ListMenus.Container.dialog('close');
                return false;
            }
            var parentNode = t.tree('getParent', selected.target);
            if (Id == "") {
                t.tree('append', {
                    parent: selected.target,
                    data: [{
                        id: result.Data, text: sTitle, "attributes": {
                            "parentId": parentId
                        },
                    }]
                });
            }
            else {
                t.tree('update', {
                    target: selected.target,
                    text: sTitle
                });
            }
            ListMenus.Container.dialog('close');
        });
    },
    IdStep: '',
    GetIdStep: function (t, node) {
        //console.log('node--' + node.id + '--node2--');
        if (node) {
            if (ListMenus.IdStep != '') ListMenus.IdStep += ',';
            ListMenus.IdStep += node.id;
            var pNode = t.tree('getParent', node.target);
            ListMenus.GetIdStep(t, pNode);
        }
    }
}