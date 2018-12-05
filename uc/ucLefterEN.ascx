<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLefterEN.ascx.cs" Inherits="uc_ucLefterEN" %>
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
            <li><a href="honor.aspx?code=<%=dr["code"]%>" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%}
              else
              { %>
            <li><a href="about.aspx?code=<%=dr["code"]%>" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "07")
              { %>
            <%if (dr["code"].ToString() == "0701")
              { %>
            <li><a href="support.aspx?code=<%=dr["code"]%>" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%}
              else if (dr["code"].ToString() == "0702")
              { %>
            <li><a href="download.aspx?code=<%=dr["code"]%>" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "02")
              { %>
            <li><a href="news.aspx?code=<%=dr["code"]%>" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "03")
              { %>
            <%if (dr["code"].ToString() == "0301")
              { %>
            <li><a href="hr.aspx?code=0301" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%}
              else if (dr["code"].ToString() == "0302")
              { %>
            <li><a href="job.aspx?code=0302" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "04")
              { %>
            <%if (dr["code"].ToString() == "0401")
              { %>
            <li><a href="contact.aspx?code=0401" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
            <%}
              else if (dr["code"].ToString() == "0402")
              { %>
            <li><a href="message.aspx?code=0402" class="<%=code == dr["code"].ToString() ? "current" : ""%>"><%=dr["Name_en"]%></a></li>
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
        <h2 class="side_title">Product Center</h2>
        <ul class="sidebarMenu">
            <%if (dtProkind.Rows.Count > 0)
              {
                  foreach (DataRow drP in dtProkind.Rows)
                  { %>
            <li><a class="<%=code.Substring(0,2)== drP["code"].ToString()? "current" : ""%>" href="products.aspx?code=<%=drP["code"]%>"><%=drP["name_en"]%></a>
            </li>
            <%}
              } %>
        </ul>
    </div>
    <%} %>
    <!-- 推荐产品 -->
    <div class="recommend_pro">
        <h3 class="left_title">Recommend Product</h3>
        <div class="recommend_slide">
            <ul>
                <%if (dtRecommendPro.Rows.Count > 0)
                  {
                      foreach (DataRow dr in dtRecommendPro.Rows)
                      { %>
                <li><a href="productDetail.aspx?code=<%=dr["bs_prokindcode"]%>&id=<%=dr["id"]%>">
                    <img src="<%=dr["pic"].ToString()==""?"/":dr["pic"]%>" alt="<%=dr["Name_en"]%>" onerror="errorImg(this)" /><span><%=dr["Name_en"]%></span></a></li>
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
        <h2 class="left_title">Contact Us</h2>
        <div class="twoCont">
            <%if (dtContact.Rows.Count > 0)
              { %>
            <%=dtContact.Rows[0]["map_en"] %>
            <%} %>
        </div>
    </div>
</div>
