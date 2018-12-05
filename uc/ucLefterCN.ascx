<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLefterCN.ascx.cs" Inherits="uc_ucLefterCN" %>
<!-- 左侧栏目 -->
<div class="listLeft">
    <%if (!string.IsNullOrEmpty(pcode))
      { %>
    <div class="sidebar">
        <h2 class="side_title"><%=kindname %></h2>
        <ul class="sidebarMenu">
            <%if (dtKind != null && dtKind.Rows.Count > 0)
              {
                  foreach (DataRow dr in dtKind.Rows)
                  { %>

            <%if (dr["code"].ToString().Substring(0, 2) == "01")
              { %>
            <%if (dr["code"].ToString() == "0106")
              { %>
            <li><a href="honor-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name"]%></a></li>
            <%}
              else
              { %>
            <li><a href="about-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name"]%></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "07")
              { %>
            <%if (dr["code"].ToString() == "0701")
              { %>
            <li><a href="support-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name"]%></a></li>
            <%}
              else if (dr["code"].ToString() == "0702")
              { %>
            <li><a href="download-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name"]%></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "02")
              { %>
            <li><a href="news-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name"]%></a></li>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "03")
              { %>
            <%if (dr["code"].ToString() == "0301")
              { %>
            <li><a href="hr-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["name"]%></a></li>
            <%}
              else if (dr["code"].ToString() == "0302")
              { %>
            <li><a href="job-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["name"]%></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "04")
              { %>
            <%if (dr["code"].ToString() == "0401")
              { %>
            <li><a href="contact-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["name"]%></a></li>
            <%}
              else if (dr["code"].ToString() == "0402")
              { %>
            <li><a href="message-<%=dr["code"]%>.html" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["name"]%></a></li>
            <%} %>
            <%} %>
            <%}
              } %>
        </ul>
    </div>
    <%
      }
      else
      { %>
    <div class="sidebar">
        <h2 class="side_title">新品上市</h2>
        <ul class="sidebarMenu">
            <%if (dtProkind.Rows.Count > 0)
              {
                  foreach (DataRow drP in dtProkind.Rows)
                  { %>
            <li><a class="<%=code.Substring(0,2)== drP["code"].ToString()? "current" : ""%>" href="products-<%=drP["code"]%>.html"><%=drP["name"]%></a>
            </li>
            <%}
              } %>
        </ul>
    </div>
    <%} %>
    <!-- 推荐产品 -->
    <div class="recommend_pro">
        <h3 class="left_title">推荐产品</h3>
        <div class="recommend_slide">
            <ul>
                <%if (dtRecommendPro.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtRecommendPro.Rows)
                      { %>
                <li><a href="productDetail-<%=dr["bs_prokindcode"]%>-<%=dr["id"]%>.html">
                    <img src="<%=dr["pic"].ToString()==""?"/":dr["pic"]%>" alt="<%=dr["name"]%>" onerror="errorImg(this)" /><span><%=dr["name"]%></span></a></li>
                <%}
                  } %>
            </ul>
        </div>
        <script type="text/javascript">
            /* 推荐产品无缝滚动 */
            jQuery(".recommend_pro").slide({ mainCell: ".recommend_slide ul", autoPlay: true, effect: "topMarquee", vis: 2, interTime: 50 });
        </script>
    </div>
    <!-- 联系我们 -->
    <div class="sidebarThree">
        <h2 class="left_title">联系我们</h2>
        <div class="twoCont">
            <%if (dtContact.Rows.Count > 0)
              { %>
            <%=dtContact.Rows[0]["map"] %>
            <%} %>
        </div>
    </div>
</div>
