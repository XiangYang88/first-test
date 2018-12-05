<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFooterEN.ascx.cs" Inherits="uc_ucFooterEN" %>

<!-- 底部 -->
<div class="footer">
    <div class="grWidth">
        <div class="footLogo fl">
            <a href="default.aspx">
                <img src="/upload/<%=BLL.Sys.Config.getConfigVal("logo_b") %>" class="logo"></a>
            <div class="footWx">
                <%if (BLL.Sys.Config.getConfigVal("wechatImg") != "")
                  {%>
                <img src="/upload/<%=BLL.Sys.Config.getConfigVal("wechatImg")%>" alt="" width="100" height="100" /><p>
                    Scan QRCode<br />
                    Follow <%=BLL.Sys.Config.getConfigVal("webname_en") %> News
                </p>
                <%} %>
            </div>
        </div>
        <div class="footLeft fl">
            <div class="footMenu">
                <h3><%=kindname01 %></h3>
                <ul>
                    <%if (dtkind01 != null && dtkind01.Rows.Count > 0)
                      {
                          foreach (DataRow dr in dtkind01.Rows)
                          { %>
                    <%if (dr["code"].ToString() == "0106")
                      { %>
                    <li><a href="honor-<%=dr["code"]%>.html"><%=dr["Name_en"]%></a></li>
                    <%}
                      else
                      { %>
                    <li><a href="about-<%=dr["code"]%>.html"><%=dr["Name_en"]%></a></li>
                    <%} %>
                    <%}
                      } %>
                </ul>
            </div>
            <div class="footMenu footMenu2">
                <h3>Products Center</h3>
                <ul>
                    <%if (dtkindPro != null && dtkindPro.Rows.Count > 0)
                      {
                          foreach (DataRow dr in dtkindPro.Rows)
                          { %>
                    <li><a href="products-<%=dr["code"]%>.html"><%=dr["Name_en"]%></a></li>
                    <%}
                      } %>
                </ul>
            </div>
            <div class="footMenu">
                <h3><%=kindname02 %></h3>
                <ul>
                    <%if (dtkind02 != null && dtkind02.Rows.Count > 0)
                      {
                          foreach (DataRow dr in dtkind02.Rows)
                          { %>
                    <li><a href="news-<%=dr["code"]%>.html"><%=dr["Name_en"]%></a></li>
                    <%}
                      } %>
                </ul>
            </div>
        </div>
        <div class="footConatct fr">
            <%if (dtContact.Rows.Count > 0)
              { %>
            <%=dtContact.Rows[0]["ContentIndex_en"] %>
            <%} %>
        </div>
    </div>
</div>
<div class="firendLink">
    <div class="grWidth">
        <strong>Links:</strong>
        <%if (dtLinks.Rows.Count > 0)
          {
              foreach (DataRow dr in dtLinks.Rows)
              { %>
        <a href="<%=dr["url"] %>" target="_blank"><%=dr["name_en"] %></a>&nbsp;|&nbsp;
        <%}
          } %>
        <%=BLL.Sys.Config.getConfigVal("copyright_en") %>&nbsp;&nbsp;
        <a href="http://www.hunuo.com" target="_blank">Powered by hunuo.com</a> <%=BLL.Sys.Config.getConfigVal("cnzz") %>
    </div>
</div>


<%if (skinnum == "1")
  { %>
<!-- 客服1 -->
<div id="rightArrow"><a href="javascript:;" title="Online Service"></a></div>
<div id="floatDivBoxs">
    <div class="floatDtt">Online Service</div>
    <div class="floatShadow">
        <ul class="floatDqq">
            <%if (dtQQ.Rows.Count > 0)
              {
                  foreach (DataRow dr in dtQQ.Rows)
                  { %>
            <li><a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=dr["Notes"]%>&site=qq&menu=yes">
                <img src="/qqonline.aspx?qq=<%=dr["Notes"]%>:52" align="absmiddle" /><%=dr["Title_en"]%></a></li>
            <%}
              } %>
        </ul>
        <%if (BLL.Sys.Config.getConfigVal("wechatImg") != "")
          {%>
        <div class="wechatimg">
            <img src="/upload/<%=BLL.Sys.Config.getConfigVal("wechatImg")%>" />
        </div>
        <%} %>
    </div>
    <div class="floatDbg"></div>
</div>
<!-- end -->
<%}
  else if (skinnum == "2")
  { %>
<!-- 客服2 -->
<div id="floatTools" class="rides-cs">
    <div class="floatL">
        <a id="aFloatTools_Show" class="btnOpen" style="top: 20px; display: block" href="javascript:void(0);">Show</a>
        <a id="aFloatTools_Hide" class="btnCtn" style="top: 20px; display: none" href="javascript:void(0);">Hide</a>
    </div>
    <div id="divFloatToolsView" class="floatR" style="display: none; width: 140px;">
        <div class="cn">
            <h3 class="titZx">Online Service</h3>
            <ul>
                <%if (dtQQ.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtQQ.Rows)
                      { %>
                <li><span><%=dr["Title_en"]%></span> <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=dr["Notes"]%>&site=qq&menu=yes">
                    <img border="0" src="/qqonline.aspx?qq=<%=dr["Notes"]%>:52" /></a> </li>
                <%}
                  } %>
            </ul>
            <%if (BLL.Sys.Config.getConfigVal("wechatImg") != "")
              {%>
            <div class="wechatimg">
                <img src="/upload/<%=BLL.Sys.Config.getConfigVal("wechatImg")%>" />
            </div>
            <%} %>
        </div>
    </div>
</div>
<!--end-->
<%}
  else if (skinnum == "3")
  { %>
<!--客服3-->
<div id="scrollsidebar" class="side_blue">
    <div id="side_content">
        <div class="side_list">
            <div class="side_titles">
                <a id="close_btn" title="隐藏"><span>Close</span></a>
            </div>
            <div class="side_center">
                <div class="qqserver">
                    <%if (dtQQ.Rows.Count > 0)
                      {
                          foreach (DataRow dr in dtQQ.Rows)
                          { %>
                    <p>
                        <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=dr["Notes"]%>&site=qq&menu=yes">
                            <img border="0" src="/qqonline.aspx?qq=<%=dr["Notes"]%>:52" />&nbsp;<%=dr["Title_en"]%></a>
                    </p>
                    <%}
                      } %>
                </div>
                <hr />
                <%if (BLL.Sys.Config.getConfigVal("wechatImg") != "")
                  {%>
                <div class="wechatimg">
                    <img src="/upload/<%=BLL.Sys.Config.getConfigVal("wechatImg")%>" />
                </div>
                <%} %>
            </div>
            <div class="side_bottom">
            </div>
        </div>
    </div>
    <div id="show_btn">
        <span>Online Service</span>
    </div>
</div>
<script>
    var menuYloc = $("#scrollsidebar").offset().top; //#scrollsidebar 当前窗口的相对偏移
    $(window).scroll(function () {
        var offsetTop = menuYloc + $(window).scrollTop() + "px";
        $("#scrollsidebar").animate({ top: offsetTop }, { duration: 300, queue: false });
    });
</script>
<!--end-->
<%}
  else if (skinnum == "4")
  { %>
<div class="cbl" id="test2">
    <div class="kf">
        <span class="title_">Services</span>
        <div class="kftc">
            <div class="kftou">
                <span class="right">Online Service</span>
            </div>
            <div class="qqkf right">
                <%if (dtQQ.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtQQ.Rows)
                      { %>
                <div class="kfqq">
                    <a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=dr["notes"] %>&site=qq&menu=yes">
                        <img border="0" src="/qqonline.aspx?qq=<%=dr["notes"] %>:52" />&nbsp;</a>
                    <span><a target="_blank" href="http://wpa.qq.com/msgrd?v=3&uin=<%=dr["notes"] %>&site=qq&menu=yes"><%=dr["title_en"] %></a></span>
                </div>
                <%}
                  } %>
            </div>
        </div>
    </div>
    <a href="javascript:void(0)" class="db"><span class="title_">Top</span></a>
    <a href="javascript:history.go(-1)" class="fh"><span class="yl title_">Back</span></a>
</div>
<%} %>
