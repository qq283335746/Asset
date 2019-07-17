angular.module('ngTygaSoft.services.LS', [])

//本地存储数据
.factory('$tygasoftLS', ['$window', function ($window) {
    return {
        Set: function (key, value) {
            $window.localStorage[key] = value;
        },
        Get: function (key, defaultValue) {
            return $window.localStorage[key] || defaultValue;
        },
        SetObject: function (key, value) {
            $window.localStorage[key] = JSON.stringify(value);
        },
        GetObject: function (key) {
            return JSON.parse($window.localStorage[key] || '{}');
        }
    }
}]);