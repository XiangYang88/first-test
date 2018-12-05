<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHeaderCN.ascx.cs" Inherits="uc_ucHeaderCN" %>
<%if (BLL.Sys.Config.getConfigVal("IsMobile") == "1")
  { %>
<script src="js/uaredirect.js" type="text/javascript"></script>
<script type="text/javascript">uaredirect("/m/default.aspx");</script>
<%} %>
<div id="header" class="">

  
   <div class="content"><a href="/default.aspx" id="logo"><img src="/upload/<%=BLL.Sys.Config.getConfigVal("logo") %>" height="40" /></a>
    <ul id="nav">
            <li class="navitem"><a class="nav-a <%=string.IsNullOrEmpty(code)?"active":"" %>" href="default.aspx" target="_self"><span  data-title="首页">首页</span></a></li>
            <li class="navitem"><a class="nav-a <%=code=="04"?"active":"" %>" href="project.aspx" target="_self"><span data-title="<%=drKind05["name"] %>"><%=drKind05["name"] %></span></a></li>
            <li class="navitem"><a class="nav-a " href="project.aspx?code=01" target="_blank"><span data-title="新品上市">新品上市</span></a></li>
            <li class="navitem"><a class="nav-a <%=code=="01"?"active":"" %>" href="javascript:;" target=""><span data-title="<%=BLL.Article.Kind.getKindName("01") %>"><%=BLL.Article.Kind.getKindName("01") %></span><i class="fa fa-angle-down"></i></a>        
                <ul class="subnav">
                    <li><a href="about.aspx" target="_self"><span data-title="<%=BLL.Article.Kind.getKindName("0101") %>"><%=BLL.Article.Kind.getKindName("0101") %></span><i class="fa fa-angle-right"></i></a></li>
                    <li><a href="service.aspx" target="_self"><span data-title="<%=BLL.Article.Kind.getKindName("0102") %>"><%=BLL.Article.Kind.getKindName("0102") %></span><i class="fa fa-angle-right"></i></a></li>
                  </ul>
        </li>
            <li class="navitem"><a class="nav-a <%=code=="02"?"active":"" %>" href="team.aspx" target="_self"><span data-title="<%=BLL.Article.Kind.getKindName("02") %>"><%=BLL.Article.Kind.getKindName("02") %></span></a></li>
            <li class="navitem"><a class="nav-a  <%=code=="03"?"active":"" %>" href="news.aspx" target="_self"><span data-title="<%=BLL.Article.Kind.getKindName("03") %>"><%=BLL.Article.Kind.getKindName("03") %></span></a></li>
            <li class="navitem"><a class="nav-a " href="<%=drKind06["url"] %>" target="_blank"><span data-title="<%=drKind06["name"] %>"><%=drKind06["name"] %></span></a></li>
          </ul>
    <div class="clear"></div>
  </div>
    <a id="headSHBtn" href="javascript:;"><i class="fa fa-bars"></i></a>
  </div>
