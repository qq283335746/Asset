//angular.module('ngTygaSoft.services.Home', [])

//.factory('$tygasoftHome', function ($http, $ionicLoading, $ionicPopup, $ionicModal, ionicDatePicker, $tygasoftLS, $tygasoftMC, $tygasoftCommon) {

//    var ts = {};

//    ts.InitEvent = function () {

//    };

//    ts.InitData = function () {

//    };

//    ts.OnTo = function ($scope, index) {
//        var item = $scope.MenuItems[index];
//        if (!item.Url || item.Url == '') {
//            switch (item.Name) {
//                case "退出":
//                    ts.ExitApp();
//                    break;
//                default:
//                    break;
//            }
//        }
//        else {
//            window.location = item.Url;
//        }
//        $ionicSideMenuDelegate.toggleLeft();
//    };

//    ts.CheckVersion = function () {
//        var timespan = (new Date("2017-10-25")) - (new Date());
//        var totalDays = Math.floor(timespan / (24 * 3600 * 1000));
//        if (totalDays < 1) {
//            return;
//            setInterval(function () {
//                alert('当前版本已过期，请联系我们解锁！');
//            }, 1000);
//        }
//    };

//    ts.ExitApp = function () {
//        $ionicPopup.confirm({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_ExitApp_Content, cancelText: $tygasoftMC.MC.Btn_CancelText, okText: $tygasoftMC.MC.Btn_OkText }).then(function (res) {
//            if (res) {
//                $tygasoftLS.Set('LoginId', '');
//                ionic.Platform.exitApp();
//            }
//        })
//    };

//    return ts;
//});