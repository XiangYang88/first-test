<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DbExec.aspx.cs" Inherits="admin_sys_DbExec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>sql执行页面(危险,小心使用)</title>
   <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
</head>
<body style="margin:30px">
     <div class="column_Box mainAutoHeight">
    <form id="frmList" runat="server">
         <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
             <tr>
                 <td>
        <asp:TextBox TextMode=multiLine ID="txtSQL" CssClass="" runat="server" Height="133px" Width="815px"></asp:TextBox>
                     </td>
             </tr>
             </table>
        <br />
        <asp:Button ID="Button1" OnClientClick="return confirm('你确定执行此SQL吗？')"
         CssClass="SrvButton" runat="server" Text="确定执行" OnClick="Button1_Click" />

        <br />
         <div id="divContent" class="body" runat=server>
                 <asp:GridView id="gvList" runat="server"  CssClass="defaultGridV" Width="100%"
                    CellPadding="3" AllowPaging="false"
                    AutoGenerateColumns="true" OnPreRender="gvList_PreRender">
                   
              <RowStyle HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                <FooterStyle HorizontalAlign="center" VerticalAlign="Middle"></FooterStyle>
                <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                <AlternatingRowStyle BackColor="#E6F5FA" ></AlternatingRowStyle>
            
                <Columns>         
            </Columns>
            </asp:GridView> 
         </div>
        </form>
        </div>
</body>
</html>