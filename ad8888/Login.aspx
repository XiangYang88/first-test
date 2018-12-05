<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="ad8888_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>网站后台管理系统登录</title>
<link href="script/ui/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
<link href="style/login.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="script/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="script/jquery/jquery.validate.min.js"></script> 
<script type="text/javascript" src="script/jquery/messages_cn.js"></script>
<script type="text/javascript" src="script/ui/js/ligerBuild.min.js"></script>
<script type="text/javascript">
    //表单验证
    $(function () {
        //检测IE
        if ($.browser.msie && $.browser.version == "6.0") {
            window.location.href = 'ie6update.html';
        }
        $('#txtUserName').focus();
        $("#form1").validate({
            errorPlacement: function (lable, element) {
                element.ligerTip({ content: lable.html(), appendIdTo: lable });
            },
            success: function (lable) {
                lable.ligerHideTip();
            }
        });
    });
    function ToggleCode(obj, codeurl) {
        $(obj).attr("src", codeurl + "?time=" + Math.random());
    }
</script>
</head>
<body class="loginbody">
<form id="form1" runat="server">
<div class="login_div">
	<div class="loginlogo">
            <img src="images/logo.gif" alt=""/>            
    </div>
        <div class="login_content">
            <table border="0" cellspacing="0" cellpadding="3">
                <tr>
                    <td height="180">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        用户名：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="login_input required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        密<span style="padding-left: 12px;">码</span>：
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserPwd" runat="server" CssClass="login_input required" TextMode="Password"  />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="baseline">
                        验证码：
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="login_input required" MaxLength="6" style="width:55px;" />
                <img src="../tools/verify_code.ashx" width="93" height="22" alt="点击切换验证码" title="点击切换验证码" style=" margin-top:2px; vertical-align:top;cursor:pointer;" onclick="ToggleCode(this, '../tools/verify_code.ashx');return false;" />
                    </td>
                </tr>
                <tr>
                    <td height="55">
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text=" " CssClass="login_btn" onclick="btnSubmit_Click" /><asp:CheckBox ID="cbRememberId" runat="server" Text="记住用户名" Checked="True" />
                    </td>
                </tr>
            </table>
              <div class="login_tip">
            <asp:Label ID="lblTip" runat="server" Text="请输入用户名及密码" Visible="False" />
        </div>
        </div>
      <div class="clear">
        </div>
	<div class="login_copyright">Copyright  &copy; <a href="http://www.hunuo.com" target="_blank">互诺科技</a></div>
    </div>
</form>
</body>
</html>
