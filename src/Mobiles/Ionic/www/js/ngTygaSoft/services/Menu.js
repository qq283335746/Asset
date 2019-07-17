angular.module('ngTygaSoft.services.Menu', [])
.factory('$tygasoftMenu', function ($timeout, $ionicHistory, $ionicSideMenuDelegate, $ionicLoading, $ionicPopup, $tygasoftLS, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.BeforeEnter = function ($scope) {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        $scope.LoginData.IsLogin = loginInfo.LoginId != '';
        $ionicSideMenuDelegate.canDragContent($scope.LoginData.IsLogin);
        if (!$scope.LoginData.IsLogin) {
            if ($ionicHistory.currentView().stateId == 'app.SysSet') $ionicHistory.nextViewOptions({ disableAnimate: true, disableBack: false, historyRoot: false });
            else {
                $ionicHistory.nextViewOptions({ disableAnimate: true, disableBack: true, historyRoot: true });
                window.location = '#/app/Login';
            }
        }
    }

    ts.GetMenus = function ($scope) {
        $scope.MenuItems = [{ "Id": 99, "Name": "设置", "icon": "ion-ios-gear-outline", "Url": "#/app/SysSet" }, { "Id": 98, "Name": "切换账号", "icon": "ion-ios-loop", "Url": "#/app/Login" }, { "Id": 97, "Name": "退出", "icon": "ion-power" }];
    };

    ts.GetHomeMenus = function ($scope) {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        //alert(loginInfo.LoginId);
        if (loginInfo.LoginId != '') {
            $scope.HomeMenuItems = [{ "Id": 1, "Name": "资产盘点", "Src": "img/icons/home-rfid.png", "Url": "#/app/Pandian" }, { "Id": 2, "Name": "资产查询", "Src": "img/icons/home-kccx.png", "Url": "#/app/Product" }];
        }
    };

    ts.OnTo = function ($scope,index) {
        var item = $scope.MenuItems[index];
        if (!item.Url || item.Url == '') {
            switch (item.Name) {
                case "退出":
                    ts.ExitApp();
                    break;
                default:
                    break;
            }
        }
        else {
            window.location = item.Url;
        }
        $ionicSideMenuDelegate.toggleLeft();
    };

    ts.CheckVersion = function () {
        var timespan = (new Date("2017-10-25")) - (new Date());
        var totalDays = Math.floor(timespan / (24 * 3600 * 1000));
        if (totalDays < 1) {
            return;
            setInterval(function () {
                alert('当前版本已过期，请联系我们解锁！');
            }, 1000);
        }
    };

    ts.ExitApp = function () {
        $ionicPopup.confirm({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_ExitApp_Content, cancelText: $tygasoftMC.MC.Btn_CancelText, okText: $tygasoftMC.MC.Btn_OkText }).then(function (res) {
            if (res) {
                $tygasoftLS.Set('LoginId', '');
                ionic.Platform.exitApp();
            }
        })
    };

    return ts;
});