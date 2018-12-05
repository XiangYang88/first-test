<%@ Page Language="C#" AutoEventWireup="true" CodeFile="center.aspx.cs" Inherits="ad8888_main_center" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>网站后台管理系统</title>
<link href="style/style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="script/JavaScript.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            hideColumn();
        });
        //隐藏左侧栏
        var hideColumn = function () {
            //var txt = $(window.parent.document).find("div"); 获取iframe上级文档的div
            var Container_Left = jQuery(window.parent.document).find(".Container_Left");
            var ContainerMain = jQuery(window.parent.document).find(".ContainerMain");
            Container_Left.hide();
            ContainerMain.css({ marginLeft: 10 });
        };
    </script>
</head>
<body class="mainbody" >
     <div class="column_Box mainAutoHeight">
<form id="form1" runat="server">
<div class="navigation nav_icon">您好，<i><%=admin_info["name"].ToString() %></i>，欢迎进入后台管理平台</div>
<div class="line10"></div>


<div class="line10"></div>
    <div class="nlist2 clearfix">
    <h2>版本信息</h2>
    <ul>
    	<li>目前版本：V<%=Common.Utils.GetVersion()%></li>
      
        <li>程序开发：<a href="http://www.hunuo.com" target="_blank">广州互诺科技</a></li>
        <li>联系方式：http://www.hunuo.com</li>
        <li style="width:90%">版权声明：<br />1、本软件为商业软件；<br/>
            2、您可以对本系统进行修改和美化，但必须保留完整的版权信息，不得将修改后的版本用于任何商业目的；<br/>
            3、本软件受中华人民共和国《著作权法》《计算机软件保护条例》等相关法律、法规保护，作者保留一切权利。<br/>
            4、如有可能，请在您的网站上添加本站链接,</li>
    </ul>
    <div class="line10"></div>
</div>
    <div class="line10"></div>
<div class="nlist2 clearfix">
    <h2>站点信息</h2>
    <ul>
    	<li>站点名称：<%=drConfig["webname"] %></li>
        <li>网站域名：<%=drConfig["website"]  %></li>
        <li>服务器名称：<%=Server.MachineName%> </li>
        <li>服务器IP：<%=Request.ServerVariables["LOCAL_ADDR"] %></li>
        <li>NET框架版本：<%=Environment.Version.ToString()%></li>
        <li>操作系统：<%=Environment.OSVersion.ToString()%></li>
        <li>IIS环境：<%=Request.ServerVariables["SERVER_SOFTWARE"]%></li>
        <li>服务器端口：<%=Request.ServerVariables["SERVER_PORT"]%></li>
        <li>目录物理路径：<%=Request.ServerVariables["APPL_PHYSICAL_PATH"]%></li>
        <li>服务器现在时间：<%=DateTime.Now.ToString() %></li>
        <li>Session总数：<%=Session.Contents.Count.ToString() %></li>
        <li>Application总数：<%=Application.Contents.Count.ToString() %></li>
      
    </ul>
    <div class="line10"></div>
</div>
    <div class="line10"></div>
    <div class="nlist2 clearfix">
    <h2>浏览者信息</h2>
    <ul>
    	<li>浏览者ip地址：<%=Request.ServerVariables["REMOTE_ADDR"] %></li>
        <li>浏览者操作系统：<%=bc.Platform.ToString()  %></li>
        <li>浏览器：<%=bc.Browser.ToString()%> </li>
        <li>浏览器版本：<%=bc.Version.ToString() %></li>
        <li>JavaScript：<%= bc.JavaScript.ToString()%></li>
        <li>JavaApplets：<%=bc.JavaApplets.ToString()%></li>
        <li>Cookies：<%=bc.Cookies.ToString()%></li>
        <li>语言：<%=Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]%></li>
        <li>Frames（分栏）：<%=bc.Frames.ToString() %></li>
    </ul>
    <div class="line10"></div>
</div>
    <div class="line10"></div>

<div class="clear" style="height:20px;"></div>
</form>
         </div>
</body>
</html>
