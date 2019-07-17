var Orders = {
    Init: function () {

    },
    InitData: function () {

    },
    GetOrderProcessList: function (pageIndex, pageSize) {
        var keyword = $.trim($("#txtKeyword").textbox('getValue'));
        var postData = '{"model":{"PageIndex": "' + pageIndex + '", "PageSize": "' + pageSize + '", "Keyword": "' + keyword + '"}}';
        var url = Common.AppName + '/Services/Service.svc/GetOrderProcessList';
        Common.Ajax(url, postData, 'POST', '', true, true, function (result) {
            console.log('GetOrderProcessList--result--' + JSON.stringify(result));
            $("#dg").datagrid('loadData', JSON.parse(result.Data));
        });
    },
    OnSearch: function () {
        Orders.GetOrderProcessList(1, 100);
    }
}