angular.module('ngTygaSoft.services.Sys', [])

.factory('$tygasoftSys', function ($http, $ionicLoading, $ionicPopup, $ionicModal, ionicDatePicker, $tygasoftLS, $tygasoftMC, $tygasoftCommon) {

    var ts = {};

    ts.InitEvent = function ($scope) {
        $scope.onSave = function () {
            ts.OnSave($scope);
        }
    };

    ts.InitData = function ($scope) {
        var sServerUrl = $tygasoftLS.Get("ServiceUrl", "");
        if (!sServerUrl || sServerUrl == '') $tygasoftLS.Set("ServiceUrl", "http://115.28.5.84/asset");
        $scope.ModelData = { "ServerIP": "", "ServerPort": "80", "ServiceUrl": "" + sServerUrl + "" };
    };

    ts.OnSave = function ($scope) {
        if (!$scope.ModelData.ServiceUrl || $scope.ModelData.ServiceUrl == '') {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_EmptyError, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        $ionicPopup.confirm({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_SaveConfirm, cancelText: $tygasoftMC.MC.Btn_CancelText, okText: $tygasoftMC.MC.Btn_OkText }).then(function (r) {
            if (r) {
                $tygasoftLS.Set("ServiceUrl", $scope.ModelData.ServiceUrl);
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Response_Ok, okText: $tygasoftMC.MC.Btn_OkText });
                //var dlgShow = $ionicPopup.show({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Waiting });
                //setTimeout(function () {
                //    dlgShow.close();
                //    window.location = '#/app/Home';
                //}, 1000);
            }
        })
    }

    return ts;
});