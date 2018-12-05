<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeaderMEN.ascx.cs" Inherits="uc_ucHeaderMEN" %>
<header>
    <div class="header-top">
        <a href="default.aspx" class="logo">
            <img src="/upload/<%=BLL.Sys.Config.getConfigVal("logo") %>" alt="<%=BLL.Sys.Config.getConfigVal("webname_en")%>">
        </a>
        <a class="lang" href="/m/default.aspx"><i class="icon-globe"></i></a>
        <a class="tel" href="javascript:void(0)">
            <i class="icon-phone"></i>
            <span style="font-size: 0.8em;"><%=BLL.Sys.Config.getConfigVal("phone1") %></span>
        </a>
    </div>
</header>
<p class="clear"></p>
<div class="g-nava">
    <li class="goback">
        <a href="javascript:window.history.back(-1)">
            <i class="icon-angle-left"></i>Back
        </a>
    </li>
    <li class="goindex">
        <a href="default.aspx">
            <i class="icon-home"></i>Home
        </a>
    </li>
    <li class="goclass">
        <a href="javascript:;" id="godaohang">
            <i class="icon-th-large"></i>Navigation
        </a>
    </li>
    <p class="clear"></p>
</div>
<p class="clear"></p>
<div class="daohang">
    <ul class="clearfix">
        <li><a href="default.aspx">Home</a></li>
        <li><a href="about.aspx"><%=kindname01 %></a></li>
        <li><a href="products.aspx">Products</a></li>
        <li><a href="cases.aspx"><%=kindname06 %></a></li>
        <li><a href="support.aspx"><%=kindname07%></a></li>
        <li><a href="news.aspx"><%=kindname02 %></a></li>
        <li><a href="hr.aspx"><%=kindname03 %></a></li>
        <li><a href="contact.aspx"><%=kindname04%></a></li>
    </ul>
</div>
<p class="clear"></p>
<div class="position">
    <label class="fenlei"><a href="javascript:;" id="catlist"><%=kindnameP %></a></label>
    <a href="default.aspx" target="_blank">Home</a>
</div>
<p class="clear"></p>
<div class="catalog">
    <ul>
        <%if (!string.IsNullOrEmpty(pcode))
          { %>
        <li><a href="javascript:;">√<%=kindnameP %> </a></li>
        <%if (dtKind != null && dtKind.Rows.Count > 0)
          {
              foreach (DataRow dr in dtKind.Rows)
              { %>

        <%if (dr["code"].ToString().Substring(0, 2) == "01")
          { %>
        <%if (dr["code"].ToString() == "0106")
          { %>
        <li><a href="honor.aspx?code=<%=dr["code"]%>"><%=dr["Name_en"]%></a></li>
        <%}
          else
          { %>
        <li><a href="about.aspx?code=<%=dr["code"]%>"><%=dr["Name_en"]%></a></li>
        <%} %>
        <%}
          else if (dr["code"].ToString().Substring(0, 2) == "07")
          { %>
        <%if (dr["code"].ToString() == "0701")
          { %>
        <li><a href="support.aspx?code=<%=dr["code"]%>"><%=dr["Name_en"]%></a></li>
        <%}
          else if (dr["code"].ToString() == "0702")
          { %>
        <li><a href="download.aspx?code=<%=dr["code"]%>"><%=dr["Name_en"]%></a></li>
        <%} %>
        <%}
          else if (dr["code"].ToString().Substring(0, 2) == "02")
          { %>
        <li><a href="news.aspx?code=<%=dr["code"]%>"><%=dr["Name_en"]%></a></li>
        <%}
          else if (dr["code"].ToString().Substring(0, 2) == "03")
          { %>
        <%if (dr["code"].ToString() == "0301")
          { %>
        <li><a href="hr.aspx?code=0301"><%=dr["Name_en"]%></a></li>
        <%}
          else if (dr["code"].ToString() == "0302")
          { %>
        <li><a href="job.aspx?code=0302"><%=dr["name_en"]%></a></li>
        <%} %>
        <%}
          else if (dr["code"].ToString().Substring(0, 2) == "04")
          { %>
        <%if (dr["code"].ToString() == "0401")
          { %>
        <li><a href="contact.aspx?code=0401"><%=dr["Name_en"]%></a></li>
        <%}
          else if (dr["code"].ToString() == "0402")
          { %>
        <li><a href="message.aspx?code=0402"><%=dr["Name_en"]%></a></li>
        <%} %>
        <%} %>
        <%}
          } %>
        <%}
          else
          { %>
        <li><a href="javascript:;">√Product Center </a></li>
        <%if (dtProkind.Rows.Count > 0)
          {
              foreach (DataRow drP in dtProkind.Rows)
              { %>
        <li><a href="products.aspx?code=<%=drP["code"]%>"><%=drP["Name_en"]%></a>
        </li>
        <%}
              } %>
        <%} %>
    </ul>
    <span class="catbtn">
        <img src="images/icon_slid.png" width="30" alt="右" /></span>
</div>
<p class="positionb">
    <img src="images/arcb.jpg" alt="下" />
</p>
