angular.module('ngTygaSoft.services.PandianAsset', [])

.factory('$tygasoftPandianAsset', function ($stateParams, $ionicModal, $ionicPopover, $ionicBackdrop, $ionicHistory, $state, $timeout, $ionicPopup, $ionicLoading, $cordovaToast, $ionicActionSheet, $cordovaBarcodeScanner, ngDialog, $tygasoftCommon, $tygasoftMC, $tygasoftLogin) {

    var ts = {};

    ts.InitEvent = function ($scope) {
        $scope.WinH = window.innerHeight;
        ts.DlgAddPandianAsset($scope);

        $scope.slideIndex = 0;
        $scope.activeSlide = function (index) {
            $scope.slideIndex = index;
        };

        $scope.onBack = function () {
            $scope.AddPandianAssetModel.hide();
        }

        $scope.onPandianAsset = function (item) {
            ts.OnPandianAsset($scope, item);
        }

        $scope.onDlgModal = function (name) {
            ts.OnDlgModal($scope, name);
        }

        $scope.onDlgSelect = function (item) {
            ts.OnDlgSelect($scope, item);
        }

        $scope.onToggleMenu = function () {
            ts.OnToggleMenu($scope);
        }

        $scope.onSave = function () {
            ts.OnSave($scope);
        }

        $scope.onCommit = function () {
            ts.OnCommit($scope);
        }
    };

    ts.InitData = function ($scope) {
        ts.GetPandianAssetList($scope, $scope.PandianInfo.Id);
        ts.GetOrgDepmtTree($scope);
        ts.GetCategoryTree($scope);
        ts.GetCbbStoragePlace($scope);
    };

    ts.GetPandianAssetList = function ($scope, pandianId) {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        $scope.ListNotPandian = [];
        $scope.ListPandian = [];
        $scope.ListPanying = [];
        ts.GetPandianAssets($scope, 1, 50, loginInfo, pandianId);
    };

    ts.GetPandianAssets = function ($scope, pageIndex, pageSize, loginInfo, pandianId) {
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/GetPandianAssetList";
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '","PandianId":"' + pandianId + '","PageIndex":' + pageIndex + ',"PageSize":' + pageSize + '}}';
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            //console.log('GetPandianAssetList--result--' + JSON.stringify(result));
            if (typeof result.Data == 'string') {
                result.Data = JSON.parse(result.Data);
            }
            var jData = result.Data.rows;
            if(!jData || jData.length < 1) return;
            for (var i = 0; i < jData.length; i++) {
                var jRow = jData[i];
                //console.log('i--'+ JSON.stringify(jRow));
                switch (jRow.StatusName) {
                    case "未盘点":
                        $scope.ListNotPandian.push(jRow);
                        break;
                    case "已盘点":
                        $scope.ListPandian.push(jRow);
                        break;
                    case "盘盈":
                        $scope.ListPanying.push(jRow);
                        break;
                    default:
                        break;
                }
            }
            pageIndex++;
            ts.GetPandianAssets($scope, pageIndex, pageSize, loginInfo, pandianId);
            ts.SetTotalQty($scope);
        });
    };

    ts.GetListByStatus = function ($scope, pandianId, status) {
        var list = [];
        var content = '"PandianAssetStatus":"' + status + '"';
        var sqlWhere = "and KeyName like '%" + pandianId + "%' and ContentValue like '%" + content + "%' ";
        $tygasoftKeyValueDAL.CallSearch('PandianAsset', sqlWhere, function (res) {
            if (res.rows.length > 0) {
                for (var i = 0; i < res.rows.length; i++) {
                    list.push(JSON.parse(res.rows.item(i).ContentValue));
                }
                switch (status) {
                    case "未盘点":
                        $scope.ListNotPandian = list;
                        break;
                    case "已盘点":
                        $scope.ListPandian = list;
                        break;
                    case "盘盈":
                        $scope.ListPanying = list;
                        break;
                    default:
                        break;
                }
            }
        })
    };

    ts.GetOrgDepmtTree = function ($scope) {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '"}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/GetOrgDepmtTree";
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            //console.log('GetOrgDepmtTree--result--' + JSON.stringify(result));
            if (typeof result.Data == 'string') {
                result.Data = JSON.parse(result.Data);
            }
            $scope.OrgDepmts = result.Data;
        });
    };

    ts.GetCategoryTree = function ($scope) {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '"}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/GetCategoryTree";
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            //console.log('GetCategoryTree--result--' + JSON.stringify(result));
            if (typeof result.Data == 'string') {
                result.Data = JSON.parse(result.Data);
            }
            $scope.Categories = result.Data;
        });
    };

    ts.GetCbbStoragePlace = function ($scope) {
        var loginInfo = $tygasoftLogin.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '"}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/GetCbbStoragePlace";
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            //console.log('GetCbbStoragePlace--result--' + JSON.stringify(result));
            if (typeof result.Data == 'string') {
                result.Data = JSON.parse(result.Data);
            }
            $scope.StoragePlaces = result.Data;
        });
    };

    ts.SetTotalQty = function ($scope) {
        $scope.PandianInfo.TotalQty = $scope.ListNotPandian.length + $scope.ListPandian.length + $scope.ListPanying.length;
    };

    ts.DlgCategory = function ($scope) {
        $scope.DlgCategoryModel = {};
        $ionicModal.fromTemplateUrl('templates/DlgCategory.html', {
            scope: $scope
        }).then(function (modal) {
            $scope.DlgCategoryModal = modal;
        });
    };

    ts.DlgOrgDepmt = function ($scope) {
        $scope.DlgOrgDepmtModel = {};
        $ionicModal.fromTemplateUrl('templates/DlgOrgDepmt.html', {
            scope: $scope
        }).then(function (modal) {
            $scope.DlgOrgDepmtModal = modal;
        });
    };

    ts.DlgAddPandianAsset = function ($scope) {
        $scope.AddPandianAssetModel = {};
        $ionicModal.fromTemplateUrl('templates/Pandian/AddPandianAsset.html', {
            scope: $scope
        }).then(function (modal) {
            $scope.AddPandianAssetModel = modal;
        });
    };

    ts.DlgBarcodeModal = function ($scope, barcode) {
        try {
            if (!barcode || barcode.IsNullOrEmpty()) {
                $scope.IsExistRow = false;
                ts.DlgEmptyModal($scope);
            }
            else {
                var oldRow = ts.FindRowByBarcode($scope,barcode,true,true,true);
                if (oldRow) {
                    ts.OnPandianAsset($scope, oldRow);
                }
                else {
                    $scope.IsExistRow = false;
                    ts.DlgEmptyModal($scope);
                }
            }
        }
        catch (e) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: e.message, okText: $tygasoftMC.MC.Btn_OkText });
        }
    };

    ts.DlgEmptyModal = function ($scope) {
        var jRow = { PandianId: "" + $scope.PandianInfo.Id + "", TotalQty: "0", Status: 2, StatusName: "盘盈", Barcode: $tygasoftCommon.GetBarcode() };
        ts.OnPandianAsset($scope, jRow);
    }

    ts.FindRowByBarcode = function ($scope, barcode, isNotPandian, isPandian, isPanying) {
        if (isNotPandian) {
            for (var i = 0; i < $scope.ListNotPandian.length; i++) {
                var jItem = $scope.ListNotPandian[i];
                if (jItem.Barcode == barcode) return jItem;
            }
        }
        if (isPandian) {
            for (var i = 0; i < $scope.ListPandian.length; i++) {
                var jItem = $scope.ListPandian[i];
                if (jItem.Barcode == barcode) return jItem;
            }
        }
        if (isPanying) {
            for (var i = 0; i < $scope.ListPanying.length; i++) {
                var jItem = $scope.ListPanying[i];
                if (jItem.Barcode == barcode) return jItem;
            }
        }
        
        return null;
    }

    ts.SetEmptyData = function (jData) {
        if (!jData.LastCategoryName || jData.LastCategoryName == '') {
            jData.LastCategoryName = '请选择';
            jData.LastCategoryId = null;
        }
        if (!jData.LastUseDepmtName || jData.LastUseDepmtName == '') {
            jData.LastUseDepmtName = '请选择';
            jData.LastUseDepmtId = null;
        }
        if (!jData.LastMgrDepmtName || jData.LastMgrDepmtName == '') {
            jData.LastMgrDepmtName = '请选择';
            jData.LastMgrDepmtId = null;
        }
        if (!jData.LastStoragePlaceName || jData.LastStoragePlaceName == '') {
            jData.LastStoragePlaceName = '请选择';
            jData.LastStoragePlaceId = null;
        }
    };

    ts.OnToggleMenu = function ($scope) {
        var btnMenus = [
            { text: '<i class="icon ion-social-hackernews-outline positive"></i> 盘盈' },
            { text: '<i class="icon ion-android-contract positive"></i> 扫描' },
            { text: '<i class="icon ion-ios-cloud-upload-outline positive"></i> 上传到服务器' },
            { text: '<i class="icon ion-ios-minus-outline assertive"></i> 取消' }
        ];
        $ionicActionSheet.show({
            buttons: btnMenus,
            cancelText: '取消',
            buttonClicked: function (index) {
                switch (index) {
                    case 0:
                        $scope.IsExistRow = false;
                        ts.DlgEmptyModal($scope);
                        return true;
                    case 1:
                        ts.OnScan($scope);
                        return true;
                    case 2:
                        ts.OnCommit($scope);
                        return true;
                    default:
                        return true;
                }
                return true;
            }
        });
    };

    ts.OnPandianAsset = function ($scope, jRow) {
        ts.SetEmptyData(jRow);
        $scope.PandianAssetInfo = jRow;
        $scope.IsPanying = jRow.StatusName == '盘盈';
        if (!$scope.IsPanying) $scope.IsExistRow = true;

        $scope.AddPandianAssetModel.show();
    };

    ts.OnScan = function ($scope) {
        $cordovaBarcodeScanner.scan().then(function (result) {
            var barcode = result.text;
            if (!barcode || barcode == "") return false;
            ts.DlgBarcodeModal($scope, barcode);
        }, function (err) {
            if (err != 1) {
                $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: err, okText: $tygasoftMC.MC.Btn_OkText });
            }
        });
    };

    ts.OnToggleCamera = function ($scope, $cordovaCamera) {
        var btnMenus = [
            { text: '<i class="icon ion-ios-camera-outline positive"></i> 拍照' },
            { text: '<i class="icon ion-image positive"></i> 相册' },
            { text: '<i class="icon ion-ios-minus-outline assertive"></i> 取消' }
        ];
        $ionicActionSheet.show({
            buttons: btnMenus,
            cancelText: '取消',
            buttonClicked: function (index) {
                switch (index) {
                    case 0:
                        ts.OnTakePicture($scope, $cordovaCamera, index);
                        return true;
                    case 1:
                        ts.OnTakePicture($scope, $cordovaCamera, index);
                        return true;
                    default:
                        return true;
                }
                return true;
            }
        });
    };

    ts.OnTakePicture = function ($scope, $cordovaCamera, pictureSourceType) {

        var options = {
            quality: 50,
            destinationType: Camera.DestinationType.FILE_URI
        };
        if (pictureSourceType == 1) {
            options.sourceType = Camera.PictureSourceType.PHOTOLIBRARY;
        }
        else {
            options.sourceType = Camera.PictureSourceType.CAMERA;
        }
        $cordovaCamera.getPicture(options).then(function (imageURI) {
            //$scope.CameraImage = "data:image/jpeg;base64," + imageData;
            $scope.dataModel.PictureUrl = imageURI;
        });
    };
    
    ts.OnShowCameraPicture = function ($scope) {
        if ($scope.dataModel.PictureUrl && !$scope.dataModel.PictureUrl.IsNullOrEmpty()) {
            $scope.cameraModel.show();
        }
        else {
            $cordovaToast.showShortCenter($tygasoftMC.MC.M_Camera_Error);
        }
    };

    ts.OnDlgModal = function ($scope, name) {
        $scope.DlgModalName = name;
        var url = '';
        switch (name) {
            case "Category":
                url = 'templates/DlgCategory.html';
                break;
            case "UseDepmt":
                url = 'templates/DlgOrgDepmt.html';
                break;
            case "MgrDepmt":
                url = 'templates/DlgOrgDepmt.html';
                break;
            case "StoragePlace":
                url = 'templates/DlgStoragePlace.html';
                break;
            default:
                break;
        }
        ngDialog.open({
            scope: $scope,
            template: url,
            className: 'ngdialog-theme-default',
            width: '100%',
            showClose: false
        });
    };

    ts.OnDlgSelect = function ($scope, item) {
        switch ($scope.DlgModalName) {
            case 'Category':
                $scope.PandianAssetInfo.LastCategoryId = item.id;
                $scope.PandianAssetInfo.LastCategoryName = item.text;
                break;
            case 'UseDepmt':
                $scope.PandianAssetInfo.LastUseDepmtId = item.id;
                $scope.PandianAssetInfo.LastUseDepmtName = item.text;
                break;
            case 'MgrDepmt':
                $scope.PandianAssetInfo.LastMgrDepmtId = item.id;
                $scope.PandianAssetInfo.LastMgrDepmtName = item.text;
                break;
            case 'StoragePlace':
                $scope.PandianAssetInfo.LastStoragePlaceId = item.Id;
                $scope.PandianAssetInfo.LastStoragePlaceName = item.Named;
                break;
            default:
                break;
        }
        ngDialog.closeAll();
    };

    ts.OnSave = function ($scope) {
        try {
            var jRow = $scope.PandianAssetInfo;
            //console.log('jRow-' + JSON.stringify(jRow));
            var isInsert = false;
            if ($scope.IsPanying) {
                $scope.slideIndex = 2;
                if (!jRow.AssetName || jRow.AssetName.replace('请选择', '').IsNullOrEmpty()) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                if (!jRow.LastCategoryName || jRow.LastCategoryName.replace('请选择', '').IsNullOrEmpty()) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                else {
                    jRow.CategoryId = jRow.LastCategoryId;
                    jRow.CategoryName = jRow.LastCategoryName;
                }
                if (!jRow.LastUseDepmtName || jRow.LastUseDepmtName.replace('请选择', '').IsNullOrEmpty()) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                else {
                    jRow.UseDepmtId = jRow.LastUseDepmtId;
                    jRow.UseDepmtName = jRow.LastUseDepmtName;
                }
                if (!jRow.LastMgrDepmtName || jRow.LastMgrDepmtName.replace('请选择', '').IsNullOrEmpty()) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                else {
                    jRow.MgrDepmtId = jRow.LastMgrDepmtId;
                    jRow.MgrDepmtName = jRow.LastMgrDepmtName;
                }
                if (!jRow.LastStoragePlaceName || jRow.LastStoragePlaceName.replace('请选择', '').IsNullOrEmpty()) {
                    $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.M_Required_Error, okText: $tygasoftMC.MC.Btn_OkText });
                    return false;
                }
                else {
                    jRow.StoragePlaceId = jRow.LastStoragePlaceId;
                    jRow.StoragePlaceName = jRow.LastStoragePlaceName;
                }
                jRow.Status = 2;
                var oldRow = ts.FindRowByBarcode($scope, jRow.Barcode, false, false, true);
                if (!oldRow) $scope.ListPanying.push(jRow);
                else {
                    for (var i = 0; i < $scope.ListPanying.length; i++) {
                        var jItem = $scope.ListPanying[i];
                        if (jItem.Barcode == jRow.Barcode) {
                            jItem = jRow;
                        }
                    }
                }
            }
            else {
                if (jRow.LastCategoryName && !jRow.LastCategoryName.replace('请选择', '').IsNullOrEmpty()) {
                    jRow.CategoryId = jRow.LastCategoryId;
                    jRow.CategoryName = jRow.LastCategoryName;
                }
                if (jRow.LastUseDepmtName && !jRow.LastUseDepmtName.replace('请选择', '').IsNullOrEmpty()) {
                    jRow.UseDepmtId = jRow.LastUseDepmtId;
                    jRow.UseDepmtName = jRow.LastUseDepmtName;
                }
                if (jRow.LastMgrDepmtName && !jRow.LastMgrDepmtName.replace('请选择', '').IsNullOrEmpty()) {
                    jRow.MgrDepmtId = jRow.LastMgrDepmtId;
                    jRow.MgrDepmtName = jRow.LastMgrDepmtName;
                }
                if (jRow.LastStoragePlaceName && !jRow.LastStoragePlaceName.replace('请选择', '').IsNullOrEmpty()) {
                    jRow.StoragePlaceId = jRow.LastStoragePlaceId;
                    jRow.StoragePlaceName = jRow.LastStoragePlaceName;
                }
                if (jRow.LastUsePersonName && !jRow.LastUsePersonName.replace('请输入使用人', '').IsNullOrEmpty()) {
                    jRow.UsePersonName = jRow.LastUsePersonName;
                }

                var oldRow = ts.FindRowByBarcode($scope, jRow.Barcode, false, true, false);
                if (oldRow) {
                    for (var i = 0; i < $scope.ListPandian.length; i++) {
                        var jItem = $scope.ListPandian[i];
                        if (jItem.Barcode == jRow.Barcode) jItem = jRow;
                    }
                }
                else {
                    jRow.Status = 1;
                    jRow.StatusName = '已盘点';
                    $scope.ListPandian.push(jRow);
                    for (var i = 0; i < $scope.ListNotPandian.length; i++) {
                        var jItem = $scope.ListNotPandian[i];
                        if (jItem.Barcode == jRow.Barcode) {
                            $scope.ListNotPandian.splice(i, 1);
                            break;
                        }
                    }
                }
                $scope.slideIndex = 1;
            }
            $scope.AddPandianAssetModel.hide();
        }
        catch (e) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: e.message, okText: $tygasoftMC.MC.Btn_OkText });
        }
    };

    ts.OnCommit = function ($scope) {
        var list = [];
        for (var i = 0; i < $scope.ListPandian.length; i++) {
            var jItem = $scope.ListPandian[i];
            var jRow = { AssetId: jItem.AssetId, AssetName: jItem.AssetName, Barcode: jItem.Barcode, SpecModel: jItem.SpecModel, Unit: jItem.Unit, CategoryId: jItem.LastCategoryId, UseDepmtId: jItem.UseDepmtId, MgrDepmtId: jItem.MgrDepmtId, StoreLocationId: jItem.StoragePlaceId, UsePerson: jItem.UsePersonName, Remark: jItem.Remark, Status: jItem.Status };
            console.log('ListPandian--jRow--' + JSON.stringify(jRow));
            list.push(jRow);
        }
        for (var i = 0; i < $scope.ListPanying.length; i++) {
            var jItem = $scope.ListPanying[i];
            console.log('ListPanying--jItem--' + JSON.stringify(jItem));
            var jRow = { AssetId: jItem.AssetId, AssetName: jItem.AssetName, Barcode: jItem.Barcode, SpecModel: jItem.SpecModel, Unit: jItem.Unit, CategoryId: jItem.CategoryId, UseDepmtId: jItem.UseDepmtId, MgrDepmtId: jItem.MgrDepmtId, StoreLocationId: jItem.StoragePlaceId, UsePerson: jItem.UsePersonName, Remark: jItem.Remark, Status: jItem.Status };
            console.log('ListPanying--jRow--' + JSON.stringify(jRow));
            list.push(jRow);
        }
        if (list.length == 0) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.MC.Data_ToServer_EmptyError, okText: $tygasoftMC.MC.Btn_OkText });
            return false;
        }

        var loginInfo = $tygasoftLogin.GetLoginInfo();
        var postData = '{"model":{"Platform":"' + loginInfo.Platform + '","DeviceId":"' + loginInfo.DeviceId + '","PandianId":"' + $scope.PandianInfo.Id + '","ItemList":' + JSON.stringify(list) + '}}';
        var url = $tygasoftCommon.ServerUrl() + "/services/PdaService.svc/SavePandianAsset";
        //console.log('postData--' + postData);
        //return false;
        $tygasoftCommon.DoHttp(url, postData, 'POST', null, true, true, function (result) {
            $ionicPopup.alert({ title: $tygasoftMC.MC.Alert_Title, template: $tygasoftMC.GetString('Params_UploadCount', list.length), okText: $tygasoftMC.MC.Btn_OkText });
            $scope.ListPandian = [];
            $scope.ListPanying = [];
            ts.SetTotalQty($scope);
        });
    };

    return ts;
});