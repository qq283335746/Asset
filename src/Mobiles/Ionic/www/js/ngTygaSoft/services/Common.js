
angular.module('ngTygaSoft.services.Common', [])

.factory('$tygasoftCommon', function ($http, $ionicPopup, $ionicLoading, $tygasoftLS, $tygasoftMC) {

    var ts = {};

    ts.AppKey = '011de50b-216d-49c4-8836-8ba2f4c9e490';

    ts.ServerUrl = function () {
        var serviceUrl = $tygasoftLS.Get("ServiceUrl", "");
        if (serviceUrl && serviceUrl != '') {
            if (serviceUrl.indexOf('/Services/PdaService.svc') > -1) serviceUrl = serviceUrl.replace('/Services/PdaService.svc', "");
        }
        //if (!serviceUrl || serviceUrl == '') {
        //    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_ServiceUrlEmpty, okText: $tygasoftMC.MC.Btn_OkText }).then(function () {
        //        window.location = "#/app/SysSet";
        //    })
        //    return false;
        //}
        
        return serviceUrl;
    };

    ts.PageIndex = 1;
    ts.PageSize = 20;

    ts.GetSplitValue = function (s, k) {
        var arr = s.split('|');
        for (var i = 0; i < arr.length; i++) {
            var key = "" + k + "=";
            if (arr[i].indexOf(key) > -1) {
                return arr[i].replace(key, "");
            }
        }
        return "";
    };
    ts.IsMobilePhone = function (s) {
        var reg = /^0{0,1}(13[0-9]|15[0-9]|18[0-9])[0-9]{8}$/;

    };
    ts.CurrentDate = function () {
        var s = "";
        var date = new Date();
        s += date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        if (m < 10) s += "-" + "0" + m + "";
        else s += "-" + m;
        if (d < 10) s += "-" + "0" + d + "";
        else s += "-" + d;

        return s;
    };
    ts.GetDate = function (sDate, sType, n) {
        var s = "";
        var date = new Date(sDate);
        s += date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        if (sType == "month") {
            m = m + n;
            if (m < 10) s += "-" + "0" + m + "";
            else s += "-" + m;
            if (d < 10) s += "-" + "0" + d + "";
            else s += "-" + d;
        }
        else if (sType == "day") {
            if (m < 10) s += "-" + "0" + m + "";
            else s += "-" + m;
            d = d + n;
            if (d < 10) s += "-" + "0" + d + "";
            else s += "-" + d;
        }

        return s;
    };
    ts.FDate = function (value) {
        if (value == '') return new Date().Format("yyyy-MM-dd");
        return new Date(value.replace('T', ' ')).Format("yyyy-MM-dd");
    };
    ts.FDateTime = function (value) {
        if (value == '') return new Date().Format("yyyy-MM-dd hh:mm:ss");
        return new Date(value.replace('T', ' ')).Format("yyyy-MM-dd hh:mm:ss");
    };
    ts.FTime = function (value) {
        var s = '';
        if (value == '') {
            var date = new Date();
            var h = date.getHours();
            if (h < 10) s += '0' + h;
            else s += h;
            s += ':';
            var m = date.getMinutes();
            if (m < 10) s += '0' + m;
            else s += m;
        }
        else {
            var date = new Date(value * 1000);
            var h = date.getUTCHours();
            if (h < 10) s += '0' + h;
            else s += h;
            s += ':';
            var m = date.getUTCMinutes();
            if (m < 10) s += '0' + m;
            else s += m;
        }

        return s;
    };
    ts.FMonth = function () {
        var s = '';
        var date = new Date();
        s += date.getFullYear();
        var m = date.getMonth()+1;
        if (m < 10) s += "-0" + m;
        else s += "-" + m;
        return s;
    };
    ts.GetRndOrderCode = function (max) {
        return new Date().Format("yyyyMMddhhmmss") + Math.round(Math.random() * max);
    };
    ts.GetBarcode = function () {
        var now = new Date();
        var year = now.getFullYear();           //年
        var month = now.getMonth() + 1;     //月
        var day = now.getDate();            //日
        var hh = now.getHours();            //时
        var mm = now.getMinutes();          //分

        var s = '';
        s = year.toString().substr(2,4) + month.toString().PadLeft(2, '0') + day.toString().PadLeft(2, '0') + hh.toString().PadLeft(2, '0') + mm.toString().PadLeft(2, '0') + ts.GetRndNum(4);
        return s;
    };
    ts.GetRndNum = function (max) {
        var s = '';
        var index = 0;
        while (index < max) {
            var rndn = Math.round(Math.random() * 10);
            if (rndn < 10) {
                s += rndn;
                index++;
            }
        }
        return s;
    };

    ts.String = {
        IsNullOrWhiteSpace: function (s) {
            if (s) {
                if (s.replace(/^\s+|\s+$/g, "") != "") return false;
            }
            return true;
        },
        Trim: function (s) {
            return s.replace(/^\s+|\s+$/g, "");
        }
    };

    ts.DoHttp = function (url, data, httpMethod, contentType, isProgress, isAlertErr, callback) {
        if (typeof data != 'string') {
            data = JSON.stringify(data);
        }
        var serverUrl = ts.ServerUrl();
        if (url.indexOf(serverUrl) == -1) url = serverUrl + url;
        if (!httpMethod || httpMethod == '') httpMethod = 'POST';
        if (!contentType || contentType == '') contentType = "application/json; charset=utf-8";
        if (isProgress) $ionicLoading.show();
        $http.defaults.headers.post['Content-Type'] = contentType;
        $http({
            method: httpMethod,
            url: url,
            data: data,

        }).then(function (res) {
            if (isProgress) $ionicLoading.hide();
            var result = res.data;
            if (typeof result == 'string') {
                result = JSON.parse(result);
            }
            if (result.ResCode != 1000) {
                if (isAlertErr) $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: result.Msg, okText: $tygasoftMC.MC.Btn_OkText });
                if (result.ResCode == 1003) {
                    setTimeout(function () {
                        window.location = '#/app/Login';
                    }, 700);
                }
                return false;
            }
            if (typeof (eval(callback)) == 'function') {
                callback(result);
            }
        }, function (err) {
            if (isProgress) $ionicLoading.hide();
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Http_Err, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        });
    };

    return ts;
});