// 设置主导航菜单
function setMainMenu() {
    var objArray = MenuArray;
    for (var i = 0; i < objArray.length; i++){
        var row = objArray[i];
        var id = "_Menu_" + row["value"];
        var liStr = "<li id=\"" + id + "\" onclick=\"setChildMenu(this," + row["value"] + ",'" + row["url"] + "');\" ><a href=\"#\"><b>" + row["treeNode"] + "</b></a></li>";
        $("#_Navigation").append(liStr);
    }
}

// 设置子导航菜单.
function setChildMenu(ele, id, defaultUrl) {
    var objArray = MenuArray;
    for (var i = 0; i < objArray.length; i++) {
        var row = objArray[i];
        if (row["value"] == id) {
            $("#_VMenutable").html("<div id=\"_ChildMenu\"></div>");
            var objArraySub = row["childNode"];
            for (var j = 0; j < objArraySub.length; j++) {
                var rowSub = objArraySub[j];
                var id = "_ChildMenuItem_" + rowSub["value"];
                var liStr = "<div id=\"" + id + "\" class=\"divtab\" onclick=\"onChildMenuClick(this);\" onmouseover=\"onChildMenuHover(this);\" onmouseout=\"onChildMenuHover(this);\" url=\"" + rowSub["url"] + "\"><b><span>" + rowSub["treeNode"] + "</span></b></div>";
                $("#_VMenutable").append(liStr);

                if (j == 0)
                    defaultUrl = rowSub["url"];
            }
        }
    }

    // 主导航菜单默认的子菜单页设置.
    if (defaultUrl != undefined && defaultUrl != null && defaultUrl.length != 0) {
        $("#_VMenutable").show();
        $("#_MainArea").attr("src", defaultUrl);
        $(".divtab").each(function() {
            var childUrl = $(this).attr("url");
            if (childUrl == defaultUrl) {
                $(this).addClass("divtabCurrent");
            }
        });
    } else {
        $("#_MainArea").attr("src", "");
    }
}

// 主导航菜单单击事件.
function onMainMenuClick(defaultUrl) {
    if (defaultUrl.length == 0) return;

    $("#_MainArea").attr("src", defaultUrl);
    
    $(".divtab").each(function() {
        var childUrl = $(this).attr("url"); 
        if (childUrl == defaultUrl) {
            $(this).addClass("divtabCurrent");
        }
    });
}

// 子导航菜单单击事件.
function onChildMenuClick(ele) {    
    var url = $(ele).attr("url");
    $("#_MainArea").attr("src", url);
    
    $(".divtab").each(function() {
        $(this).removeClass("divtabCurrent");
        $(this).removeClass("divtabHover");
    });
    $(ele).addClass("divtabCurrent");
}

// 子导航菜单鼠标移入/移出事件
function onChildMenuHover(elem) {
    var classNames = $(elem).attr("class");
    if (classNames.indexOf("divtabCurrent") != -1) {
        $(elem).removeClass("divtabHover");
        return;
    }
    $(elem).toggleClass("divtabHover");
}

$(document).ready(function() {
    // 设置主导航菜单.
    setMainMenu();
    
    // 设置菜单导航初始值.
    if (MenuArray.length > 0) {
        var defaultMenu = MenuArray[0];
        var defaultMenuId = "_Menu_" + defaultMenu["value"];
//        setChildMenu(defaultMenuId, defaultMenu["value"], defaultMenu.childNode[0].url);
        $("#_MainArea").attr("src", "main.aspx");
        $("#_VMenutable").hide();
        $("#" + defaultMenuId).addClass("onNav");
    }

    // 主菜单标签样式切换.
    $(".Navigation li").click(function() {
        $(".Navigation li").each(function() {
            $(this).removeClass("onNav");
        });
        $(this).toggleClass("onNav");
    });

    //设置iframe高度
    iframeSet.layoutAdjust();
});
