var currFun = {
    Init: function () {
        if (!Common.IsAdmin()) {
            $('#lbtnAdd').linkbutton('disable');
            $('#lbtnEdit').linkbutton('disable');
            $('#lbtnDel').linkbutton('disable');
            $('#lbtnRoleMenu').linkbutton('disable');
        }
    },
    OnSelect: function (index, row) {
        if (Common.IsAdmin()) {
            if (row.f1 == 'Administrators' || row.f1 == 'System' || row.f1 == 'Users' || row.f1 == 'Guest') {
                $('#lbtnEdit').linkbutton('disable');
                $('#lbtnDel').linkbutton('disable');
                if (row.f1 == 'Administrators') $('#lbtnRoleMenu').linkbutton('disable');
                else $('#lbtnRoleMenu').linkbutton('enable');
            }
            else {
                $('#lbtnEdit').linkbutton('enable');
                $('#lbtnDel').linkbutton('enable');
                $('#lbtnRoleMenu').linkbutton('enable');
            }
        }
    },
    Add: function () {
        $("#hId").val("");
        $("#txtRolename").val("");
        currFun.Url = Common.AppName + "/Services/SecurityService.svc/SaveRole";
        $('#dlg').dialog({ title: '新建角色' });
        $('#dlg').dialog('open');
    },
    Edit: function () {
        var cbl = $('#bindT').datagrid("getSelections");
        if (!cbl || (cbl.length != 1)) {
            $.messager.alert('错误提示', '请选择一行且仅一行数据进行编辑', 'error');
            return false;
        }

        $('#dlg').dialog({ title: '编辑角色' });
        currFun.Url = Common.AppName + "/Services/SecurityService.svc/SaveRole";
        $("#hId").val(cbl[0].f0);
        $("#txtRolename").val(cbl[0].f1);
        $('#dlg').dialog('open');

        return false;
    },
    Del: function () {
        var cbl = $('#bindT').datagrid("getSelections");
        if (!cbl || cbl.length == 0) {
            $.messager.alert('错误提示', '请至少选择一行数据再进行操作', 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var itemsAppend = "";
                for (var i = 0; i < cbl.length; i++) {
                    if (i > 0) itemsAppend += ",";
                    itemsAppend += cbl[i].f1;
                }

                var postData = '{"itemAppend":"' + itemsAppend + '"}';
                var url = Common.AppName + "/Services/SecurityService.svc/DelRole";
                Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
                    currFun.DelRow();
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    $('#dlg').dialog('close');
                });
            }
        });
    },
    Save: function () {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;

        var roleId = $.trim($("#hId").val());
        var roleName = $("#txtRolename").val();

        var postData = '{"model":{"RoleId":"' + roleId + '","RoleName":"' + roleName + '"}}';
        var url = currFun.Url;
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            if (roleId.length > 0) {
                $('#bindT').datagrid('updateRow', {
                    index: currFun.GetRowIndex(),
                    row: {
                        f1: roleName,
                        f2: "<a href=\"amember.html?rName=" + roleName + "\">分配用戶</a>"
                    }
                })
            }
            else {
                window.location = window.location.href;
            }

            jeasyuiFun.show("温馨提示", "保存成功！");
            $('#dlg').dialog('close');
        });
    },
    GetRowIndex: function () {
        var dg = $('#bindT');
        var row = dg.datagrid("getSelected");
        if (!row || row.length == 0) {
            return -1;
        }

        return dg.datagrid('getRowIndex', row);
    },
    DelRow: function () {
        var dg = $('#bindT');
        var cbl = dg.datagrid("getSelections");
        if (!cbl || cbl.length == 0) {
            $.messager.alert('错误提示', '请至少选择一行数据再进行操作', 'error');
            return false;
        }

        for (var i = 0; i < cbl.length; i++) {
            var rowIndex = dg.datagrid('getRowIndex', cbl[i]);
            dg.datagrid('deleteRow', rowIndex);
        }
    },
    RoleMenu: function () {
        var rows = $('#bindT').datagrid("getSelections");
        if (!rows || (rows.length != 1)) {
            $.messager.alert('错误提示', '请选择一行且仅一行数据进行编辑', 'error');
            return false;
        }
        window.location = "../u/rolemenu.html?allowRole=" + rows[0].f1 + "";
    }
}