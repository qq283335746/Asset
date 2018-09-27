angular.module('ngTygaSoft.services.Pandian', [])

.factory('$tygasoftPandian', function ($state,$http, $ionicPopup, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.Init = function ($scope) {
        ts.GetList($scope, 1, 50);
    }

    ts.OnPandian = function ($scope, item) {
        if (item.IsDown) {
            var jRow = {Id:item.Id, Named: item.Named, UserName: item.UserName, TotalQty: item.TotalQty };
            //$state.go('app.PandianAsset', jRow);
            //console.log('#/app/pandianAsset/' + JSON.stringify(item) + '');
            window.location = '#/app/PandianAsset/' + JSON.stringify(jRow) + '';
        }
        else {
            item.IsDown = true;
            ts.SavePandianDown($scope, item);
        }
    };

    ts.GetList = function ($scope, pageIndex, pageSize) {
        $scope.Pandians = [];
        var loginInfo = $tygasoftLogin.GetLoginInfo();

        ts.GetPandians($scope, pageIndex, pageSize, loginInfo);
    };

    ts.GetPandians = function ($scope, pageIndex, pageSize, loginInfo) {
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/GetPandianList";
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}}';
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            if (typeof result.Data == 'string') {
                result.Data = JSON.parse(result.Data);
            }
            var jData = result.Data.rows;
            if (!jData || jData.length < 1) return;
            for (var i = 0; i < jData.length; i++) {
                var jRow = jData[i];
                jRow.Src = jRow.IsDown ? ' img/c_down_light.jpg' : ' img/c_down_dark.jpg';
                $scope.Pandians.push(jRow);
            }
            pageIndex++;
            ts.GetPandians($scope, pageIndex, pageSize, loginInfo);
        });
    };

    ts.SavePandianDown = function ($scope,item)
    {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '","Id":"' + item.Id + '"}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/SavePandianDown";
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            item.Src = item.IsDown ? ' img/c_down_light.jpg' : ' img/c_down_dark.jpg';
        });
    }

    return ts;
});