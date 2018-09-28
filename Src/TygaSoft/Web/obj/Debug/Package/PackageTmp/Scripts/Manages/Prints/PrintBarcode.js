var PrintBarcode = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var isPrint = Common.GetQueryString('Print') == "1";
        if (isPrint) {
            $('#printBtns').remove();
        }
    },
    OnPrint: function () {
        var $t = $('.sw-barcode>table:first');
        var width = parseInt($t.css("width"));
        var height = parseInt($t.css("height"));
        var sizeFWidth = width;
        var sizeFHeight = height;
        var marginTop = 3;
        var marginRight = 3;
        var marginBottom = 3;
        var marginLeft = 3;
        window.open(window.location.href + "&Print=1&w=" + width + "&h=" + height + "&sfw=" + sizeFWidth + "&sfh=" + sizeFHeight + "&mt=" + marginTop + "&mr=" + marginRight + "&mb=" + marginBottom + "&ml=" + marginLeft + "");
    }
}