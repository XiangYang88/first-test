<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFooterMCN.ascx.cs" Inherits="uc_ucFooterMCN" %>
<footer>
    <div class="scroll_top"><a href="#container"><span><i class="icon-angle-up"></i>TOP</span></a></div>
    <nav class="b-nav">
        <ul>
            <li><a href="default.aspx">首页</a></li>
            <li><a href="about.aspx"><%=kindname01 %></a></li>
            <li><a href="products.aspx">产品中心</a></li>
            <li><a href="cases.aspx"><%=kindname06 %></a></li>
            <li><a href="support.aspx"><%=kindname07%></a></li>
            <li><a href="news.aspx"><%=kindname02 %></a></li>
            <li><a href="hr.aspx"><%=kindname03 %></a></li>
            <li><a href="contact.aspx"><%=kindname04%></a></li>
        </ul>
    </nav>
    <p class="copy">
        <%=BLL.Sys.Config.getConfigVal("copyright") %>&nbsp;<br />
        <a href="http://www.hunuo.com/" target="_blank">网站建设</a>：<a href="http://www.hunuo.com" target="_blank">互诺科技</a>&nbsp;<br />
        <%--<%=BLL.Sys.Config.getConfigVal("cnzz") %>--%>
    </p>
    <%if (BLL.Sys.Config.getConfigVal("wechatImg") != "")
                  {%>
    <p class="code">
        <img src="/upload/<%=BLL.Sys.Config.getConfigVal("wechatImg")%>" alt="微信二维码"><br />
        扫一扫，访问网站
    </p>
    <%} %>
</footer>

<script type="text/javascript" src="js/mobile.js"></script>
<script type="text/javascript" src="js/tab_news.js"></script>
