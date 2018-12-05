<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BsNewsModule.aspx.cs" Inherits="admin_BsPg_BsNewsModule" %>

<!--数据操作的页面模板-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章列表</title>
     <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
   
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
     <link rel="stylesheet" type="text/css" href="../extjs2/resources/css/ext-all.css" />
    <link rel="stylesheet" type="text/css" href="../extjs2/resources/css/xtheme-gray.css" />
    <script type="text/javascript" src="../extjs2/adapter/ext/ext-base.js"></script>
    <%--<script src="../script/iframeSet.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript" src="../extjs2/ext-all-debug.js"></script>
</head>
<body>
    
  
        <div class="column_Box">   
       
     <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" class="treebox">
                <div style="-moz-user-select: none;" class="treeItem">
                    <div id="tree-div" style="overflow: auto; height: 480px; width: 180px;">
                    </div>
                </div>
            </td>
            <td valign="top">
                <iframe id='_SubArea' name="_SubArea" frameborder="0" width="100%"  src='about:blank'
                    scrolling="auto"></iframe>
            </td>
        </tr>
    </table> </div>
    <script type="text/javascript" language="javascript">
        var tree = null;
        Ext.onReady(function () {
            Ext.BLANK_IMAGE_URL = "../extjs2/resources/images/default/s.gif"
            var Tree = Ext.tree;
            tree = new Tree.TreePanel({
                el: 'tree-div',
                autoScroll: true,
                animate: true,
                enableDD: false,
                containerScroll: true,
                onlyLeafCheckable: false,
                loader: new Tree.TreeLoader({
                    //dataUrl: '../common/getTreeData.aspx?nonoutside=1&iswap='
                    dataUrl: "../rx/menu.aspx?node=<%=pkid%>&type=<%=mcode%>&menutype=newlistmenu"
                }),
                margins: '15,15,15,15',
                width: 180,
                border: false
            });

            var root = new Tree.AsyncTreeNode({
                text: '内容管理',
                draggable: false,
                id: '0'
            });
            tree.setRootNode(root);

            //定义右键菜单
            var rightClick = new Ext.menu.Menu({
                id: 'rightClickCont',
                items: [{
                    id: 'open',
                    text: '打开',
                    handler: function () {
                        defaultUrl = selected.id;//url
                        setContentManageUrl();
                    }
                }]
            });

            //增加右键点击事件
            //tree.on('contextmenu', function (node, event) {//声明菜单类型
            //    event.preventDefault(); //关闭默认的菜单，以避免弹出两个菜单 
            //    node.select(); //选中点击的节点 
            //    selected = new Ext.tree.TreeNode({
            //        id: node.id,
            //        text: node.text,
            //        leaf: node.leaf
            //    });
            //    rightClick.showAt(event.getXY()); //取得鼠标点击坐标，展示菜单
            //});

            //绑定节点点击事件
            tree.on('click', function (node) {
                if (node.attributes.children != undefined && node.attributes.children.length > 0) {
                    if (node.expanded) node.collapse();
                    else node.expand();
                    return false;
                }

                if (node.id != '0') {
                    defaultUrl = node.id;//url
                    setContentManageUrl();
                }
            });

            tree.render();

            root.expand(false, false);
        });


        
        var defaultUrl = "<%=getLinkWidthBaseParas("BsNewsList.aspx",new string[]{"pcode=01","type=del"})%>";

        $(function () {
            $("#_SubArea").attr("src", defaultUrl);
        });

        //设置内容管理链接
        function setContentManageUrl() {
            var url = defaultUrl;
            if (url.length > 0)
                url = url.substring(5, url.length);
            $("#_SubArea").attr("src", url);
        }

        function initHMenus() {
            try {
               
                    $(".treeItem").css("height", window.parent.getHeight() - 110);
                    $("#tree-div").css("height", window.parent.getHeight() - 110);
               
            }
            catch (ex) { }

            try {
                var ch = document.compatMode == "BackCompat" ? document.body.clientHeight : document.documentElement.clientHeight;

                //内容管理或者栏目管理时
                if (window.location.href.indexOf('BsNewsModule') > 0)
                    $("#_SubArea").css("height", ch - 15).css('overflow', 'none');
              
                $(".column_Box").css('height', ch - 10);

                if ($("#box").height() != null) {
                    $(".column_Box").css('overflow-y', 'scroll').css('overflow-x', 'hidden');

                    if ($("#box").height() > ch)
                        $(".column_Box").css('overflow-y', 'scroll');
                    else
                        $(".column_Box").css('overflow-y', 'hidden');
                }
            }
            catch (ex) { }
        }
        $(document).ready(function () {
            initHMenus();
        });

        //当窗口大小改变时重置iframe
        if (document.attachEvent) {
            window.attachEvent('onresize', initHMenus);
        } else {
            window.addEventListener('resize', initHMenus, false);
        }
    </script>
</body>
</html>
