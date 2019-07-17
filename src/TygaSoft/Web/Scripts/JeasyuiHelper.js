var jeasyuiFun = {
    show: function (title, msg) {
        $.messager.show({
            title: title,
            msg: msg,
            showType: 'slide',
            style: {
                right: '',
                top: document.body.scrollTop + document.documentElement.scrollTop,
                bottom: ''
            }
        });
    },
    getDgPagerOptions: function (dg) {
        var jData = {};
        var pager = dg.datagrid('getPager');
        jData.PageIndex = pager.pagination('options').pageNumber;
        jData.PageSize = pager.pagination('options').pageSize;
        return jData;
    },
    cbb: function (cbbId, v, url, data, callback) {
        Common.Ajax(url, data, "GET", "", true, true, function (result) {
            var jData = JSON.parse(result.Data);
            var cbb = $('#' + cbbId + '');
            cbb.combobox({
                valueField: 'Id',
                textField: 'Named',
                data: jData,
                onLoadSuccess: function () {
                    if (v && v != "") {
                        cbb.combobox('select', v);
                    }
                    else {
                        cbb.combobox('setValue', "请选择");
                    }
                }
            });
        });
    },
    cbt: function (cbtId, v, url, data, callback) {
        Common.Ajax(url, data, "GET", "", true, true, function (result) {
            var jData = JSON.parse(result.Data);
            var cbt = $('#' + cbtId + '');
            cbt.combotree({
                data: jData,
                onLoadSuccess: function () {
                    if (v && v != "") {
                        cbt.combotree('setValue', v);
                    }
                }
            });
        });
    },
    setCbb: function (cbbId, v, data) {
        if (typeof data == 'string') {
            data = JSON.parse(data);
        }
        var cbb = $('#' + cbbId + '');
        cbb.combobox({
            valueField: 'Id',
            textField: 'Named',
            data: data,
            onLoadSuccess: function () {
                if (number = typeof (v)) cbb.combobox('select', v);
                else {
                    if (v && v != '') {
                        cbb.combobox('select', v);
                    }
                    else {
                        cbb.combobox('setValue', "请选择");
                    }
                }
            }
        });
    }
}