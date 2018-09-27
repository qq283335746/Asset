
angular.module('starter', ['ionic', 'starter.controllers', 'ionic-datepicker', 'ionic-timepicker', 'ngCordova', 'ngDialog', 'ngTygaSoft'])
.run(function ($ionicPlatform, $ionicHistory, $rootScope, $state, $cordovaToast, $cordovaDevice, $tygasoftLS, $tygasoftMC, $tygasoftLogin) {
    $ionicPlatform.ready(function () {
        if (cordova.platformId === 'ios' && window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);

        }
        if (window.StatusBar) {
            StatusBar.styleDefault();
        }

        $ionicPlatform.registerBackButtonAction(function (e) {
            if ($ionicHistory.backView()) {
                $ionicHistory.goBack();
            }
            else {
                if ($rootScope.backButtonPressedOnceToExit) {
                    $tygasoftLS.Set('LoginId', '');
                    ionic.Platform.exitApp();
                } else {
                    $rootScope.backButtonPressedOnceToExit = true;
                    $cordovaToast.showShortCenter($tygasoftMC.MC.M_ExitApp);
                    setTimeout(function () {
                        $rootScope.backButtonPressedOnceToExit = false;
                    }, 2000);
                }
            }
            e.preventDefault();
            return false;
        }, 101);

        var jDeviceInfo = { "Platform": "" + $cordovaDevice.getPlatform() + "", "UUID": "" + $cordovaDevice.getUUID() + "", "Version": "" + $cordovaDevice.getVersion() + "" };
        $tygasoftLS.SetObject("DeviceInfo", jDeviceInfo);

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams, options) {
            var loginInfo = $tygasoftLogin.GetLoginInfo();
            var hasLogin = loginInfo.LoginId != '';
            if (!hasLogin) {
                if (toState.name == 'app.Login') {
                    $ionicHistory.nextViewOptions({ disableAnimate: false, disableBack: true, historyRoot: false });
                }
                else {
                    //$state.go('app.Login');
                    //event.preventDefault();
                    $ionicHistory.nextViewOptions({ disableAnimate: false, disableBack: false, historyRoot: false });
                }
            }
        });

        //$tygasoftLS.Set("ServiceUrl", "http://localhost/asset");
        //$tygasoftLS.Set("ServiceUrl", "http://115.28.5.84/asset");
    });
})

.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, ionicDatePickerProvider, ionicTimePickerProvider) {
    $ionicConfigProvider.navBar.alignTitle('center');
    $ionicConfigProvider.scrolling.jsScrolling(true);
    ionicDatePickerProvider.configDatePicker({
        inputDate: new Date(),
        setLabel: '确定',
        todayLabel: '今天',
        closeLabel: '关闭',
        mondayFirst: false,
        weeksList: ["日", "一", "二", "三", "四", "五", "六"],
        monthsList: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        templateType: 'popup',
        showTodayButton: true,
        dateFormat: 'yyyy年MM月dd日',
        closeOnSelect: false,
        disableWeekdays: [6],
    });
    ionicTimePickerProvider.configTimePicker({
        inputTime: (((new Date()).getHours() * 60 * 60) + ((new Date()).getMinutes() * 60)),
        format: 24,
        step: 1,
        setLabel: '确定',
        closeLabel: '关闭'
    });
    $stateProvider

    .state('app', {
        url: '/app',
        abstract: true,
        templateUrl: 'templates/Menu.html',
        controller: 'MenuCtrl'
    })
    .state('app.Home', {
        url: '/Home',
        views: {
            'menuContent': {
                templateUrl: 'templates/Home.html',
                controller: 'AppCtrl'
            }
        }
    })
    .state('app.SysSet', {
        url: '/SysSet',
        views: {
            'menuContent': {
                templateUrl: 'templates/SysSet.html',
                controller: 'SysCtrl'
            }
        }
    })
    .state('app.Login', {
        url: '/Login',
        views: {
            'menuContent': {
                templateUrl: 'templates/Login.html',
                controller: 'LoginCtrl'
            }
        }
    })
    .state('app.Product', {
        url: '/Product',
         views: {
             'menuContent': {
                 templateUrl: 'templates/Product.html',
                 controller: 'ProductCtrl'
             }
         }
     })
    .state('app.Pandian', {
        url: '/Pandian',
        views: {
            'menuContent': {
                templateUrl: 'templates/Pandian/ListPandian.html',
                controller: 'PandianCtrl'
            }
        }
    })
    .state('app.PandianAsset', {
        url: '/PandianAsset/:item',
        views: {
            'menuContent': {
                templateUrl: 'templates/Pandian/ListPandianAsset.html',
                controller: 'PandianAssetCtrl'
            }
        }
    })

    $urlRouterProvider.otherwise('/app/Home');
});
