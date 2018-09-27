angular.module('ngTygaSoft.services.Product', [])

.factory('$tygasoftProduct', function ($ionicPopup, $cordovaBarcodeScanner, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.InitEvent = function ($scope) {
        $scope.onSearch = function () {
            ts.GetProducts($scope);
        };
        $scope.onScan = function () {
            ts.OnScan($scope);
        };
    };

    ts.InitData = function ($scope) {
        $scope.DataModel = {};
    };

    ts.GetProducts = function ($scope) {
        var sKeyword = $scope.DataModel.Keyword;
        if (!sKeyword || sKeyword == '') {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_EmptyInvalidError, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '","Keyword":"' + sKeyword + '","PageIndex":1,"PageSize":1000}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/GetProducts";
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            if (typeof result.Data == 'string') {
                result.Data = JSON.parse(result.Data);
            }
            $scope.Products = result.Data.rows;
        });
    };

    ts.OnScan = function ($scope) {
        $cordovaBarcodeScanner.scan().then(function (result) {
            $scope.DataModel.Keyword = result.text;
            $scope.onSearch();
        }, function (err) {
            if (err != 1) {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: err, okText: $tygasoftMC.MC.Btn_OkText });
            }
        });
    };

    return ts;
});