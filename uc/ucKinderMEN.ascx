<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucKinderMEN.ascx.cs" Inherits="uc_ucKinderMEN" %>
<div class="hd" id="wrapper">
    <div id="scroller">
        <ul>
            <%if (dtKind != null && dtKind.Rows.Count > 0)
              {
                  foreach (DataRow dr in dtKind.Rows)
                  { %>
            <%if (dr["code"].ToString().Substring(0, 2) == "01")
              { %>
            <li class="<%=code==dr["Code"].ToString()?"on":""%>"><a href="about.aspx?code=<%=dr["code"]%>"><%=dr["name_en"] %></a></li>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "02")
              { %>
            <li class="<%=code==dr["Code"].ToString()?"on":""%>"><a href="news.aspx?code=<%=dr["code"]%>"><%=dr["name_en"] %></a></li>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "03")
              {
                  if (dr["code"].ToString() == "0301")
                  { %>
            <li class="<%=code==dr["Code"].ToString()?"on":""%>"><a href="hr.aspx?code=0301"><%=dr["name_en"] %></a></li>
            <%}
              else if (dr["code"].ToString() == "0302")
              { %>
            <li class="<%=code==dr["Code"].ToString()?"on":""%>"><a href="job.aspx?code=0302"><%=dr["name_en"] %></a></li>
            <%} %>
            <%}
              else if (dr["code"].ToString().Substring(0, 2) == "04")
              {
                  if (dr["code"].ToString() == "0401")
                  {%>
            <li class="<%=code==dr["Code"].ToString()?"on":""%>"><a href="contact.aspx?code=0401"><%=dr["name_en"] %></a></li>
            <%}
              else if (dr["code"].ToString() == "0402")
              { %>
            <li class="<%=code==dr["Code"].ToString()?"on":""%>"><a href="msg.aspx?code=0402"><%=dr["name_en"] %></a></li>
            <%} %>
            <%} %>

            <%}
              } %>
        </ul>
    </div>
</div>
