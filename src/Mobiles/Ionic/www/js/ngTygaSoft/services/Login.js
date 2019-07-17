angular.module('ngTygaSoft.services.Login', [])

.factory('$tygasoftLogin', function ($q, $http, $ionicModal, $ionicHistory, $ionicLoading, $ionicPopup, $tygasoftMC, $tygasoftCommon, $tygasoftLS) {

    var ts = {};

    ts.InitEvent = function ($scope) {
        $scope.onSysSet = function () {
            window.location = "#/app/SysSet";
        };
        $scope.doLogin = function () {
            ts.DoLogin($scope);
        };
        //if (!ts.IsLogin()) {
        //    $ionicHistory.clearHistory();
        //    $ionicHistory.clearCache();
        //};
    };

    ts.InitData = function ($scope) {
        if (ts.IsLogin()) {
            var loginInfo = ts.GetLoginInfo();
            $scope.LoginData.UserName = loginInfo.LoginId;
        }
        else {
            $scope.LoginData.UserName = "张三";
            $scope.LoginData.Password = "123456";
        }
    };

    ts.IsLogin = function () {
        var loginInfo = ts.GetLoginInfo();
        return loginInfo.LoginId && loginInfo.LoginId != '';
    };

    ts.GetLoginInfo = function () {
        var loginInfo = {};
        var jDeviceInfo = $tygasoftLS.GetObject('DeviceInfo');
        var loginId = $tygasoftLS.Get('LoginId', '');
        loginInfo.Platform = jDeviceInfo.Platform;
        loginInfo.DeviceId = jDeviceInfo.UUID;
        loginInfo.LoginId = loginId;
        return loginInfo;
    };

    ts.DoLogin = function ($scope) {
        var sUserName = $scope.LoginData.UserName;
        var sPassword = $scope.LoginData.Password;
        if ((!sUserName || sUserName == '') || (!sPassword || sPassword == '')) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Login_EmptyError, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        var loginInfo = ts.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '","UserName":"' + sUserName + '","Password":"' + sPassword + '"}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/Login";
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function () {
            $tygasoftLS.Set('LoginId', sUserName);
            ts.ToHome();
        });
    };

    ts.ToHome = function () {
        window.location = '#/app/Home';
    };

    ts.SetRootView = function () {
        $ionicHistory.nextViewOptions({
            disableAnimate: true,
            disableBack: true,
            historyRoot: true
        });
        window.location = '#/app/Home';
    };

    return ts;
});