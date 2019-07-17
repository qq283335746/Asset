var UCMenu = {
    Init: function () {
        //this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var href = '';
        if (/^(.*)\/s\/(.*)/.test(window.location.href)) UCMenu.GetMenuByParentName('匿名访问');
        else {
            UCMenu.GetMenuByParentName($.trim($('[id$=lbAppId]').text()));
        }
    },
    InitForm:function(){
    
    },
    GetMenuByParentName: function (parentName) {
        var acc = $("#accUcMenu");
        var data = {"parentName":"" + parentName + ""};
        var url = Common.AppName + '/Services/SecurityService.svc/GetMenusChildrenByParentName';
        Common.Ajax(url, data, 'GET', '', true, true, function (result) {
            UCMenu.AccInit($('#accUcMenu'), JSON.parse(result.Data));
        });
    },
    AccInit: function (acc, data) {
        var smpMenu = $('#SitePaths>span');
        var hasSelected = false;
        for (var i in data) {
            var isSelect = smpMenu.filter(':contains("' + data[i].Title + '")').length > 0;
            if (isSelect) hasSelected = true;

            var treeCtId = 'treeCt' + i + '';
            acc.accordion('add', {
                selected:isSelect,
                title: data[i].Title,
                content: '<input type="hidden" value="' + data[i].Id + '" /><ul id="' + treeCtId + '" class="menus" style="margin-top:8px;"></ul>'
            });
        }
        if (!hasSelected) acc.accordion('select', 0);
    },
    OnAccAdd: function (title, index) {
        var t = $('#treeCt' + index + '');
        t.tree({
            onLoadSuccess: function (node, data) {
                UCMenu.OnTreeLoadSuccess(t, data);
            },
            onClick: function (node) {
                var url = node.attributes.Url;
                if (url && url != '') {
                    window.location = url;
                }
            }
        })
    },
    OnAccSelect: function (title, index) {
        var t = $('#treeCt' + index + '');
        var roots = t.tree('getRoots');
        if (roots && roots.length > 0) return;

        var currPanel = $('#accUcMenu').accordion('getPanel', index);
        var parentId = currPanel.panel('body').find('[type=hidden]:first').val();

        var data = {"parentId":"" + parentId + ""};
        var url = Common.AppName + '/Services/SecurityService.svc/GetMenusTreeChildrenByParentId';
        Common.Ajax(url, data, 'GET', 'application/x-www-form-urlencoded', true, true, function (result) {
            var jData = JSON.parse(result.Data);
            t.tree('loadData', jData);
        });
    },
    OnTreeLoadSuccess: function (t, data) {
        var smpMenus = $('#SitePaths>span');
        var len = smpMenus.length - 1;
        for (var i = len; i >= 0; i--) {
            for (var j in data) {
                if (data[j].text == smpMenus.eq(i).text()) {
                    var node = t.tree('find', data[j].id);
                    t.tree('select', node.target);
                    return false;
                }
            }
        }
    },
    AppendTreeChildren: function (t, node) {
        if (!node.attributes.HasChild) return;
        var childNode = t.tree('getChildren', node.target);
        if (childNode && childNode.length > 0) return;

        var sData = { "parentName": "" + node.text + "" };
        $.ajax({
            url: Common.AppName + "/Services/SecurityService.svc/GetMenusTreeChildrenByParentName",
            type: "GET",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (result) {
                console.log('GetMenusTreeChildrenByParentName--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                var jData = JSON.parse(result.Data);
                t.tree('append', {
                    parent: node.target,
                    data: jData
                });
            }
        });
    }
}