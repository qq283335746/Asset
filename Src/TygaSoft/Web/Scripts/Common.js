var Common = {
    AppName: '/Asset',
    GetAppId: function () {
        var appCode = $.trim($("[id$=lbAppId]").text());
        if (appCode == '' || appCode == '000000') appCode = '100000';
        return appCode;
    },
    IsAdmin: function () {
        return $('[id$=lnUser]').text().indexOf('管理员') > -1;
    },
    GetWh: function (w, h) {
        var winw = $(window).width();
        var winh = $(window).height();
        if (w > 0) {
            if (winw < w) w = winw * 0.9;
        }
        else w = winw;
        if (h > 0) {
            if (winh < h) h = winh * 0.9;
        }
        else h = winh;

        return new Array(w, h);
    },
    GetMainWh: function (w, h) {
        var winw = $('#pageMain').width();
        var winh = $('#pageMain').height();
        if (w > 0) {
            if (winw < w) w = winw * 0.9;
        }
        else w = winw;
        if (h > 0) {
            if (winh < h) h = winh * 0.9;
        }
        else h = winh;

        return new Array(w, h);
    },
    FDate: function (value, row, index) {
        if (value.indexOf('1754-01-01') > -1) return '';
        return new Date(value.replace('T', ' ')).Format("yyyy-MM-dd");
    },
    FDateTime: function (value, row, index) {
        if (value.indexOf('1754-01-01') > -1) return '';
        if (value) return new Date(value.replace('T', ' ')).Format("yyyy-MM-dd hh:mm:ss");
        else return "";
    },
    FIsYes: function (value, row, index) {
        if (value) return '是';
        return '否';
    },
    FImg: function (value, row, index) {
        if (value && value != '') {
            if (value.indexOf('|') > -1) {
                var s = '';
                var picItems = value.split('|');
                var picLen = picItems.length;
                if (picLen > 1) {
                    return '<a code="' + value + '" onclick="OrderMake.ViewPic(this)"><span class="c-g">查看更多图片</span></a>'
                }
                else {
                    return '<img src="' + Common.AppName + '' + picItems[0] + '" alt="图片" width="100" height="50" code="' + value + '" onclick="OrderMake.ViewPic(this)" />';
                }
                //for (var i = 0; i < picLen; i++) {
                //    s += '<img src="' + Common.AppName + '' + picItems[i] + '" alt="图片" width="100" height="50" />';
                //}
                return s;
            }
            else return '<img src="' + Common.AppName + '' + value + '" alt="图片" width="100" height="50" code="' + value + '" onclick="OrderMake.ViewPic(this)" />';
        }
        else return '<img src="' + Common.AppName + '/Images/nopic.gif" alt="图片" width="100" height="50" code="' + value + '" onclick="OrderMake.ViewPic(this)" />';
    },
    FHighLight: function (value, row, index) {
        if (!value || value == '') return value;
        return '<span class="c-g">' + value + '</span>';
    },
    GetQueryString: function (key) {
        var arr = Common.GetQueryStringItems();
        return arr[key];
    },
    GetQueryStringItems: function () {
        var hashMap = {};
        var href = window.location.href;
        var queryStr = href.substr(href.indexOf('?') + 1);
        var arr = queryStr.split("&");
        var len = arr.length;
        //var sJson = "";
        if (len > 0) {
            for (var i = 0; i < len; i++) {
                //if(i>0) sJson+=','
                var item = arr[i];
                var itemArr = item.split("=");
                if (itemArr.length > 1) hashMap[itemArr[0]] = $.trim(itemArr[1]);
                else hashMap[itemArr[0]] = "";
            }
        }
        return hashMap;
    },
    OnProgressStart: function () {
        $('#dlgWaiting').dialog({
            closed: false,
            content: '<div class="datagrid-mask-msg" style="display:block;"></div>'
        });
    },
    OnProgressStop: function () {
        $("#dlgWaiting").dialog('destroy');
    },
    EnumMenuName: {
        UCMenuParentName: '首页'
    },
    Ajax: function (url, data, type, contentType, isProgress, isAlertErr, callback) {
        if (!type || type == '') type = "POST";
        if (!contentType || contentType == '') contentType = "application/json; charset=utf-8";
        if (!isProgress) isProgress = true;
        if (!isAlertErr) isAlertErr = true;
        $.ajax({
            url: url,
            type: type,
            data: data,
            contentType: contentType,
            cache: false,
            beforeSend: function () {
                if (isProgress) Common.OnProgressStart();
            },
            complete: function () {
                if (isProgress) Common.OnProgressStop();
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    if (isAlertErr) $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                if (typeof (eval(callback)) == 'function') {
                    callback(result);
                }
            }
        });
    },
    AjaxPost: function (url, data, callback) {
        try {
            Common.OnProgressStart();

            $.post(url, data, function (result) {
                Common.OnProgressStop();

                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }

                if (typeof (eval(callback)) == 'function') {
                    callback(result);
                }
            })
        }
        catch (e) {
            Common.OnProgressStop();
        }
    }
}