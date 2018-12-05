<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProvince.ascx.cs" Inherits="ucProvince" %>
<%
    if (isRoot)
    {
     %>
<script charset="utf-8" type="text/javascript" src="js/PCASClass.js"></script>
<% }
   else
   {
        %>
  <script charset="utf-8" type="text/javascript" src="../js/PCASClass.js"></script>      
        <%
   }   %>
   <select id="hn_province" name="hn_province">
   </select>
   <select name="hn_city" id="hn_city"></select>
   <select name="hn_county" id="hn_county"></select>
   <script type="text/javascript">
new PCAS("hn_province","hn_city",'hn_county',"<%=province %>","<%=city %>","<%=county %>");    	
   </script>