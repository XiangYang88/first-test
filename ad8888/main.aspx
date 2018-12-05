<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="ad8888_main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网站后台管理系统</title>
    <link href="style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="script/JavaScript.js"></script>

    <script type="text/javascript">
        // 呈现系统菜单.
        
        var menu = "<%=MenuString %>";
        jQuery(document).ready(function () {
            $(".Container_Left").hide();
            var MenuArray = EvalJson(menu);
            for (var i = 0; i < MenuArray.length; i++) {
                var row = MenuArray[i];
                var liStr = "<li><a href=\"" + row["url"] + "\" target=\"main\" class=\"show_" + row["pkid"] + "\">" + row["treeNode"] + "</a></li>"
                $("#_Navigation").append(liStr);
            }

            //加载二级菜单
            for (var i = 0; i < MenuArray.length; i++) {
                var row = MenuArray[i];
                $.ajax({
                    type: "post",
                    url: "rx/menu.aspx",
                    data: "node=" + row["pkid"] + "&type=" + row["code"],
                    async: false,
                    success: function (menuList) {
                        if (menuList != "null") {
                            var MenuArray = EvalJson(menuList);
                            for (var i = 0; i < MenuArray.length; i++) {
                                var row = MenuArray[i];
                                var liStr = "<li class=\"show_" + row["ParentId"] + "\"><a href=\"" + row["url"] + "\" target=\"main\" >" + row["treeNode"] + "</a></li>"
                                $(".Container_Left ul.c").append(liStr);
                            }
                        }
                    }
                })
                
            }
            MenuClick();

            
        });
        function EvalJson(obj) {
            obj = obj.split("|");
            var jsonArray = new Array();
            for (var i = 0; i < obj.length; i++) {
                if (obj[i].replace(/(^\s*)|(\s*$)/g, "") == "") continue;
                jsonArray[i] = eval("(" + obj[i] + ")");
            }
            return jsonArray;
        }
        function MenuClick()
        {

            jQuery(".current_click").each(function () {
                var list = jQuery(this).find("ul.c li");
                var liList = jQuery(".Container_Left ul");
                list.each(function () {
                    var o = jQuery(this);
                    o.click(function () {
                        list.removeClass('current');
                        o.addClass('current');
                    });
                    //显示相对应的左侧栏目
                    o.children("a").click(function () {
                        var a = jQuery(this);
                        var className = a.attr("class");
                        if (className == undefined) return;
                        var newlist = liList.find("." + className);
                        liList.children("li").hide();
                        newlist.show().eq(0).click();
                    });
                });
            });
        }
        function getHeight() {
            return $(window).height();
        }
    </script>
</head>
<body>
    <div class="Header">
        <a class="logo" href="javascript:" onclick="document.location.reload();"></a>
        <div class="MenuDiv current_click">
            <ul class="c" id="_Navigation"></ul>
        </div>
        <div class="itemBar">
            <ul>
                <li><a href="javascript:;" class="name"><%=BLL.User.AdminUser.getLoginInfo()["name"] %></a>，您好！</li>
                <li><a href="/default.aspx" target="_blank" class="c">网站首页</a></li>
                <li><a href="center.aspx" target="main" class="c">管理平台</a></li>
                <li><a href="sys/syUserItem.aspx?mid=e12c530a-40b3-43a3-86cb-168881d65d7c" target="main" class="pas">修改密码</a></li>
                <li><a href="http://hunuo.com" target="_blank" class="about">关于互诺科技</a></li>
                <li><a href="logout.aspx" class="out">退出</a></li>
            </ul>
        </div>
    </div>
    <div class="minlineheight"></div>
    <div class="Container">
        <div class="Container_Left autoHeight current_click" >
            <ul class="c" >
            </ul>
        </div>
        <div class="ContainerMain autoHeight">
            <iframe src="center.aspx" width="100%" height="100%" id="main" frameborder="0" scrolling="no" name="main"></iframe>
        </div>
    </div>
    <div class="Footer">
       Copyright  &copy; <a href="http://www.hunuo.com" target="_blank">互诺科技</a>
    </div>
</body>
</html>
