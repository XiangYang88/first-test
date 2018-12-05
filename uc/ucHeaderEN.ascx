<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeaderEN.ascx.cs" Inherits="uc_ucHeaderEN" %>
<%if (BLL.Sys.Config.getConfigVal("IsMobile") == "1")
  { %>
<script src="js/uaredirect.js" type="text/javascript"></script>
<script type="text/javascript">uaredirect("/m/default.aspx");</script>
<%} %>

<!-- 头部 -->
<div class="header grWidth">
    <div class="topBar">
        <a class="t1" href="sitemap.html" target="_blank">SiteMap</a>|
            <a class="t2" href="message.html">FeedBack</a>|
            <a class="t3" href="/default.html" style="font-family: Arial;">中文</a>
    </div>
    <h1 class="logo fl"><a href="default.html">
        <img src="/upload/<%=BLL.Sys.Config.getConfigVal("logo") %>" alt="<%=BLL.Sys.Config.getConfigVal("webname")%>" title="<%=BLL.Sys.Config.getConfigVal("webname")%>"></a></h1>
    <div class="head_phone fr">Service Hotline<strong><%=BLL.Sys.Config.getConfigVal("phone1") %></strong><strong><%=BLL.Sys.Config.getConfigVal("phone2") %></strong></div>
</div>
<!-- 主导航 -->
<div class="nav">
    <ul class="navMain grWidth">
        <li class="m"><a href="default.html">Home</a></li>
        <li class="m ">
            <a href="about.html"><%=kindname01 %></a>
            <ul class="nav_sub">
                <%if (dtkind01 != null && dtkind01.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtkind01.Rows)
                      { %>
                <%if (dr["code"].ToString() == "0106")
                  { %>
                <li><a href="honor-<%=dr["code"]%>.html"><%=dr["name_en"]%></a></li>
                <%}
                  else
                  { %>
                <li><a href="about-<%=dr["code"]%>.html"><%=dr["name_en"]%></a></li>
                <%} %>
                <%}
                  } %>
            </ul>
        </li>
        <li class="m ">
            <a href="products.html">Product Center</a>
            <ul class="nav_sub">
                <%if (dtkindPro != null && dtkindPro.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtkindPro.Rows)
                      { %>
                <li><a href="products-<%=dr["code"]%>.html"><%=dr["name_en"]%></a></li>
                <%}
                  } %>
            </ul>
        </li>
        <li class="m ">
            <a href="cases.html"><%=kindname06 %></a>
        </li>
        <li class="m ">
            <a href="support.html"><%=kindname07 %></a>
            <ul class="nav_sub">
                <%if (dtkind07 != null && dtkind07.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtkind07.Rows)
                      { %>
                <%if (dr["code"].ToString() == "0701")
                  { %>
                <li><a href="support-<%=dr["code"]%>.html"><%=dr["name_en"] %></a></li>
                <%}
                  else if (dr["code"].ToString() == "0702")
                  { %>
                <li><a href="download-<%=dr["code"]%>.html"><%=dr["name_en"] %></a></li>
                <%} %>
                <%}
                  } %>
            </ul>
        </li>
        <li class="m ">
            <a href="news.aspx"><%=kindname02 %></a>
            <ul class="nav_sub">
                <%if (dtkind02 != null && dtkind02.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtkind02.Rows)
                      { %>
                <li><a href="news-<%=dr["code"]%>.html"><%=dr["name_en"]%></a></li>
                <%}
                  } %>
            </ul>
        </li>
        <li class="m ">
            <a href="hr.html"><%=kindname03 %></a>
            <ul class="nav_sub">
                <%if (dtkind03 != null && dtkind03.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtkind03.Rows)
                      { %>
                <%if (dr["code"].ToString() == "0301")
                  { %>
                <li><a href="hr-<%=dr["code"]%>.html"><%=dr["name_en"] %></a></li>
                <%}
                  else if (dr["code"].ToString() == "0302")
                  { %>
                <li><a href="job-<%=dr["code"]%>.html"><%=dr["name_en"] %></a></li>
                <%} %>
                <%}
                  } %>
            </ul>
        </li>
        <li class="m last">
            <a href="contact.html"><%=kindname04 %></a>
            <ul class="nav_sub">
                <%if (dtkind04 != null && dtkind04.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtkind04.Rows)
                      { %>
                <%if (dr["code"].ToString() == "0401")
                  { %>
                <li><a href="contact-<%=dr["code"]%>.html"><%=dr["name_en"] %></a></li>
                <%}
                  else if (dr["code"].ToString() == "0402")
                  { %>
                <li><a href="message-<%=dr["code"]%>.html"><%=dr["name_en"] %></a></li>
                <%} %>
                <%}
                  } %>
            </ul>
        </li>
    </ul>
</div>
<script type="text/javascript">
    jQuery(".nav").slide({
        type: "menu", //效果类型
        titCell: ".m", // 鼠标触发对象
        targetCell: ".nav_sub", // 效果对象，必须被titCell包含
        effect: "slideDown",//下拉效果
        delayTime: 300, // 效果时间
        triggerTime: 0, //鼠标延迟触发时间
        returnDefault: true,  //返回默认状态

    });
</script>
