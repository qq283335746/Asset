var Staff = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var pager = $("#dgStaff").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Staff.Load(pageNumber, pageSize);
            }
        });
        Staff.Load(1, pager.pagination('options').pageSize);
    },
    Action: null,
    SelectRow: null,
    Load: function (pageIndex, pageSize) {
        var orgId = null;
        if (OrgDepmt.SelectNode) orgId = OrgDepmt.SelectNode.id;
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '", "ParentId": "' + orgId + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetStaffList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            //console.log('GetStaffList--result--' + JSON.stringify(result));
            $("#dgStaff").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgStaff"));
        Staff.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
    },
    Add: function () {
        Staff.Action = 'Add';
        if (!OrgDepmt.SelectNode) {
            $.messager.alert('提示', '请选中一个组织机构节点再进行操作', 'error');
            return false;
        }
        Staff.SelectRow = null;
        if ($("body").find("#dlgStaff").length == 0) {
            $("body").append("<div id=\"dlgStaff\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(960, 580);
        $("#dlgStaff").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Staff.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStaff').dialog('close');
                }
            }],
            href: Common.AppName + '/u/yorg.html',
            onLoad: function () {
                var jData = {};
                jData.Id = '';
                jData.OrgId = OrgDepmt.SelectNode.id;
                Staff.SetFm(jData);
            }
        })
        return false;
    },
    Edit: function () {
        Staff.Action = 'Edit';
        var rows = $("#dgStaff").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        if ($("body").find("#dlgStaff").length == 0) {
            $("body").append("<div id=\"dlgStaff\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(960, 580);
        $("#dlgStaff").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    Staff.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStaff').dialog('close');
                }
            }],
            href: Common.AppName + '/u/yorg.html',
            onLoad: function () {
                Staff.SetFm(rows[0]);
            }
        })
        return false;
    },
    Save: function () {
        var contarner = $('#dlgStaff');
        var isValid = contarner.find('#dlgFm').form('validate');
        if (!isValid) return false;
        var sCode = $.trim(contarner.find('#txtCode').textbox('getValue'));
        var sName = $.trim(contarner.find('#txtName').textbox('getValue'));
        var sPsw = $.trim(contarner.find('#txtPsw').textbox('getValue'));
        var sEmail = $.trim(contarner.find('#txtEmail').textbox('getValue'));
        var sPhone = $.trim(contarner.find('#txtPhone').textbox('getValue'));
        var sRemark = $.trim(contarner.find('#txtRemark').textbox('getValue'));
        var sSort = $.trim(contarner.find('#txtSort').textbox('getValue'));
        if (sSort == '') sSort = 0;
        if (sEmail == '') sEmail = "asset@asset.com";
        var sId = $.trim(contarner.find("#hId").val());
        var sOrgId = $.trim(contarner.find("#hOrgId").val());

        var postData = '{"model":{ "AppCode": "' + Common.GetAppId() + '", "UserId": "' + sId + '", "OrgId": "' + sOrgId + '", "UserName": "' + sName + '", "Password": "' + sPsw + '", "Email": "' + sEmail + '", "IsApproved": "' + true + '", "Coded": "' + sCode + '", "Phone": "' + sPhone + '", "Remark": "' + sRemark + '", "Sort": "' + sSort + '" }}';
        var url = Common.AppName + "/Services/Service.svc/SaveStaff";
        Common.Ajax(url, postData, "POST", "", true, true, function (result) {
            jeasyuiFun.show("提示", "操作成功！");
            var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgStaff"));
            Staff.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
            $("#dlgStaff").dialog('close');
        });
    },
    Del: function () {
        Staff.Action = 'Del';
        var rows = $("#dgStaff").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].UserId;
        }
        $.messager.confirm('提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = '{"itemAppend": "' + itemAppend + '" }';
                var url = Common.AppName + "/Services/Service.svc/DeleteStaff";
                Common.Ajax(url, postData, "POST", "", true, true, function (result) {
                    jeasyuiFun.show("提示", "操作成功！");
                    setTimeout(function () {
                        var pagerOptions = jeasyuiFun.getDgPagerOptions($("#dgStaff"));
                        Staff.Load(pagerOptions.PageIndex, pagerOptions.PageSize);
                    }, 700);
                });
            }
        });
    },
    SetFm: function (data) {
        console.log('data--' + JSON.stringify(data));
        var contarner = $('#dlgStaff');
        contarner.find('#hId').val(data.UserId);
        contarner.find('#hOrgId').val(data.OrgId);
        contarner.find('#txtCode').textbox('setValue', data.Coded);
        contarner.find('#txtName').textbox('setValue', data.Named);
        contarner.find('#txtEmail').textbox('setValue', data.Email ? data.Email.replace("asset@asset.com",""):"");
        contarner.find('#txtPhone').textbox('setValue', data.Phone);
        contarner.find('#txtSort').textbox('setValue', data.Sort);
        contarner.find('#txtaRemark').val(data.Remark);
        if (Staff.Action == 'Edit') {
            contarner.find('#txtPsw').textbox('disable');
            contarner.find('#txtPsw').textbox('disableValidation');
            contarner.find('#txtCode').textbox('disable');
            contarner.find('#txtCode').textbox('disableValidation');
            contarner.find('#txtName').textbox('disable');
            contarner.find('#txtName').textbox('disableValidation');
        }
    }
}