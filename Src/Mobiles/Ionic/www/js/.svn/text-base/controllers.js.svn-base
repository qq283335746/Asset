angular.module('starter.controllers', [])

.controller('AppCtrl', function ($scope, $tygasoftMenu) {
    $scope.$on('$ionicView.beforeEnter', function (e) {
        $tygasoftMenu.GetHomeMenus($scope);
    });
})
.controller('MenuCtrl', function ($scope, $tygasoftMenu) {
    $scope.LoginData = {};
    $scope.$on('$ionicView.beforeEnter', function (e) {
        $tygasoftMenu.BeforeEnter($scope);
    });
    $tygasoftMenu.GetMenus($scope);
    $scope.onTo = function (index) {
        $tygasoftMenu.OnTo($scope, index);
    };
})
.controller('SysCtrl', function ($scope, $tygasoftSys) {
    $tygasoftSys.InitEvent($scope);
    $tygasoftSys.InitData($scope);
})
.controller('LoginCtrl', function ($scope, $tygasoftLogin) {
    $tygasoftLogin.InitEvent($scope);
    $tygasoftLogin.InitData($scope);
})
.controller('PandianCtrl', function ($scope, $tygasoftPandian) {

    $scope.$on('$ionicView.enter', function (e) {
        $tygasoftPandian.Init($scope);
    });

    $scope.onPandian = function (item) {
        $tygasoftPandian.OnPandian($scope, item)
    }
})
.controller('PandianAssetCtrl', function ($scope, $stateParams, $tygasoftPandianAsset) {
    $scope.PandianInfo = JSON.parse($stateParams.item);
    $scope.PandianInfo.TotalQty = 0;

    $tygasoftPandianAsset.InitEvent($scope);

    $scope.$on('$ionicView.enter', function (e) {
        $tygasoftPandianAsset.InitData($scope);
    });

    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})
.controller('ProductCtrl', function ($scope, $tygasoftProduct) {
    $tygasoftProduct.InitEvent($scope);

    $scope.$on('$ionicView.enter', function (e) {
        $tygasoftProduct.InitData($scope);
    });

    $scope.toggleGroup = function (group) {
        if ($scope.isGroupShown(group)) {
            $scope.shownGroup = null;
        } else {
            $scope.shownGroup = group;
        }
    };
    $scope.isGroupShown = function (group) {
        return $scope.shownGroup === group;
    };
})
;
