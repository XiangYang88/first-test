<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyConfig.aspx.cs" Inherits="sys_SyConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>网站设置</title>
    <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
</head>
<body>
    <div class="column_Box mainAutoHeight">
        <form id="frmList" runat="server">
            <div id="divHead" class="divHead2">
            </div>
            <div id="divDtls" class="divDtls body" runat="server">
                <div class="tab">
                    <ul>
                        <li class="current"><a href="javascript:">网站基本设置</a></li>
                        <%if (multiLanguageEN)
                          { %>
                        <li><a href="javascript:">网站基本设置(英文)</a></li>
                        <%} %>
                    </ul>
                </div>
                <div class="wrapBox ">
                    <input type="hidden" runat="server" id="PKID" />
                    <div class="body">
                        <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                            <colgroup>
                                <col width="200px" />
                                <col />
                                <col width="200px" />
                                <col />
                            </colgroup>
                            <tr>
                                <th>网站地址：
                                </th>
                                <td colspan="3">
                                    <input title="网站名称" class="textBg" type="text" runat="server" id="KK_WebSite" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th>网站名称：
                                </th>
                                <td colspan="3">
                                    <input title="网站名称" class="textBg" type="text" runat="server" id="KK_WebName" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th>标 题：
                                </th>
                                <td colspan="3">
                                    <input title="标　　题" class="textBg" type="text" runat="server" id="KK_SiteTitle" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>关 键 字：
                                </th>
                                <td colspan="3">
                                    <input title="关 键 字" class="textBg" type="text" runat="server" id="KK_KeyWords" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>网站描述：
                                </th>
                                <td colspan="3">
                                    <textarea class="textCSS" title="网站描述" type="text" runat="server" id="KK_Descp" style="width: 450px; height: 65px" />
                                </td>
                            </tr>
                            <tr>
                                <th>网站logo：
                                </th>
                                <td colspan="3">
                                    <input type="text" title="图片" id="KK_logo" runat="server" style="display: none" />
                                    <iframe scrolling="no" width="400" height="190" frameborder="0" src="../uploadIframe.aspx?id=KK_logo&multi=false&filelist=<%=this.KK_logo.Value %>"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <th>网站底部logo：
                                </th>
                                <td colspan="3">
                                    <input type="text" title="图片" id="KK_logo_b" runat="server" style="display: none" />
                                    <iframe scrolling="no" width="400" height="190" frameborder="0" src="../uploadIframe.aspx?id=KK_logo_b&multi=false&filelist=<%=this.KK_logo_b.Value %>"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <th>网站ico：
                                </th>
                                <td colspan="3">
                                    <input type="text" title="图片" id="KK_ico" runat="server" style="display: none" />
                                    <iframe scrolling="no" width="400" height="190" frameborder="0" src="../uploadIframe.aspx?id=KK_ico&multi=false&filelist=<%=this.KK_ico.Value %>"></iframe>
                                    <br />
                                    <font>必须上传格式为ico的透明图片16px*16px
                                    </font>
                                </td>
                            </tr>
                            <tr>
                                <th>网站底部信息：
                                </th>
                                <td colspan="3">
                                    <textarea title="网站底部信息" class="textCSS" type="text" runat="server" id="KK_CopyRight"
                                        style="width: 500px; height: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th>统计代码：
                                </th>
                                <td colspan="3">
                                    <textarea class="textCSS" type="text" runat="server" id="KK_cnzz"
                                        style="width: 500px; height: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th>是否开启手机版:
                                </th>
                                <td colspan="3">
                                    <asp:CheckBox ID="KK_IsMobile" Text="是否开启手机版" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>PC版主题:
                                </th>
                                <td colspan="3">
                                    <div class="box">
                                        <input type="radio" class="rdbPC" name="rdbPCColor" value="Blue" /><samp style="background-color: #005ec7;"></samp>
                                    </div>
                                    <div class="box">
                                        <input type="radio" class="rdbPC" name="rdbPCColor" value="Red" /><samp style="background-color: #c30101;"></samp>
                                    </div>
                                    <div class="box">
                                        <input type="radio" class="rdbPC" name="rdbPCColor" value="DeepBlue" /><samp style="background-color: #373b85;"></samp>
                                    </div>
                                    <div class="box">
                                        <input type="radio" class="rdbPC" name="rdbPCColor" value="Black" /><samp style="background-color: #333;"></samp>
                                    </div>
                                    <input type="text" name="KK_PCTheme" id="KK_PCTheme" runat="server" style="display: none" />
                                </td>
                            </tr>
                            <tr>
                                <th>手机版主题:
                                </th>
                                <td colspan="3">
                                    <div class="box">
                                        <input type="radio" class="rdb" name="rdbColor" value="Blue" /><samp style="background-color: #0195ff;"></samp>
                                    </div> 
                                    <div class="box">
                                        <input type="radio" class="rdb" name="rdbColor" value="Red" /><samp style="background-color: #c30101;"></samp>
                                    </div>                           
                                    <div class="box">
                                        <input type="radio" class="rdb" name="rdbColor" value="DeepBlue" /><samp style="background-color: #373b85;"></samp>
                                    </div>      
                                    <div class="box">
                                        <input type="radio" class="rdb" name="rdbColor" value="Black" /><samp style="background-color: #333;"></samp>
                                    </div>     
                                    <input type="text" name="KK_MobileTheme" id="KK_MobileTheme" runat="server" style="display: none" />
                                </td>
                            </tr>
                            <script>
                                function set_radio() {
                                    if ($("#KK_MobileTheme").val() != "") {
                                        var strs1 = $("#KK_MobileTheme").val();
                                        $("input[name=rdbColor][value='" + strs1 + "']").attr("checked", true);
                                    }
                                    if ($("#KK_PCTheme").val() != "") {
                                        var strs2 = $("#KK_PCTheme").val();
                                        $("input[name=rdbPCColor][value='" + strs2 + "']").attr("checked", true);
                                    }
                                }
                                $(function () {
                                    set_radio();
                                    $(".rdb").click(function () {
                                        $("#KK_MobileTheme").val($(this).val());
                                    });
                                    $(".rdbPC").click(function () {
                                        $("#KK_PCTheme").val($(this).val());
                                    });
                                });
                            </script>
                            <tr>
                                <th>微信二维码：
                                </th>
                                <td colspan="3">
                                    <input type="text" title="图片" id="KK_WechatImg" runat="server" style="display: none" />
                                    <iframe scrolling="no" width="400" height="190" frameborder="0" src="../uploadIframe.aspx?id=KK_WechatImg&multi=false&filelist=<%=this.KK_WechatImg.Value %>"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <th>QQ：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_QQ" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>微博：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_SinaWeibo" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>地址：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_Address" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>电话：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_Phone1" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>传真：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_Fax" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>邮编：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_ZipCode" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>手机：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_Phone2" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>邮箱：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_Email" style="width: 450px" />
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td></td>
                                <td colspan="3">水印图片参数设置
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th>参数：
                                </th>
                                <td colspan="3">
                                    <asp:CheckBox ID="KK_PicSY" title="是否启用水印" runat="server" Text="是否启用水印" />
                                    &nbsp;&nbsp;&nbsp;<br />
                                    <asp:RadioButtonList ID="KK_UseSYType" title="水印类型" RepeatColumns="2" runat="server" RepeatDirection="horizontal">
                                        <asp:ListItem Value="0">文字水印</asp:ListItem>
                                        <asp:ListItem Value="1">图片水印</asp:ListItem>
                                    </asp:RadioButtonList>
                                    水印文字：<input class="textBg" type="text" title="水印文字" runat="server" id="KK_PicSYText" style="width: 150px;" /><br />
                                    水印图片：<input class="textBg" type="text" title="水印图片" runat="server" id="KK_ImgSYUrl" style="width: 200px" />(图片文件名)
                    <br />
                                    <asp:FileUpload ID="upWaterPic" runat="server" /><font color="red">格式必须为png</font>
                                    <asp:Button ID="Button1" title="水印图片" runat="server" Text="上传水印图片" OnClick="Button1_Click" CssClass="SrvButton" /><br />
                                    水印位置：<asp:DropDownList ID="KK_SYPosition" runat="server">
                                        <asp:ListItem Value="1" Text="左上"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="中上"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="右上"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="左中"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="正中"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="右中"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="左下"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="中下"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="右下"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td></td>
                                <td colspan="3">邮件参数设置 <font color="red">填写的邮箱必须支持smtp服务并开通该服务</font>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th>SMTP服务器地址：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" title="SMTP服务器地址" type="text" runat="server" id="KK_EmailSMTP"
                                        style="width: 268px" />发送邮件的服务器地址
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th>邮箱账号：
                                </th>
                                <td>
                                    <input class="textBg" type="text" title="邮箱账号" runat="server" id="KK_EmailUID" style="width: 150px" />用于发送邮件时的验证
                                </td>
                                <th>邮箱密码：
                                </th>
                                <td>
                                    <input class="textBg" type="text" title="邮箱密码" runat="server" id="KK_EmailPWD" style="width: 150px" />用于发送邮件时的验证
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <th style="height: 20px">显示名：
                                </th>
                                <td style="height: 20px">
                                    <input title="显示名" class="textBg" type="text" runat="server" id="KK_EmailDisplayName"
                                        style="width: 150px" />邮件发送人的名称
                                </td>
                                <th style="height: 20px">显示邮箱：
                                </th>
                                <td style="height: 20px">
                                    <input title="显示邮箱" class="textBg" type="text" runat="server" id="KK_EmailFrom" style="width: 150px" />邮件发送人
                                </td>
                            </tr>


                        </table>
                    </div>
                    <div class="body">
                        <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                            <colgroup>
                                <col width="200px" />
                                <col />
                                <col width="200px" />
                                <col />
                            </colgroup>

                            <tr>
                                <th>网站名称：
                                </th>
                                <td colspan="3">
                                    <input title="网站名称" class="textBg" type="text" runat="server" id="KK_WebName_en" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th>标 题：
                                </th>
                                <td colspan="3">
                                    <input title="标　　题" class="textBg" type="text" runat="server" id="KK_SiteTitle_en" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>关 键 字：
                                </th>
                                <td colspan="3">
                                    <input title="关 键 字" class="textBg" type="text" runat="server" id="KK_KeyWords_en" style="width: 450px" />
                                </td>
                            </tr>
                            <tr>
                                <th>网站描述：
                                </th>
                                <td colspan="3">
                                    <textarea class="textCSS" title="网站描述" type="text" runat="server" id="KK_Descp_en" style="width: 450px; height: 65px" />
                                </td>
                            </tr>
                            <tr>
                                <th>网站底部信息：
                                </th>
                                <td colspan="3">
                                    <textarea title="网站底部信息" class="textCSS" type="text" runat="server" id="KK_CopyRight_en"
                                        style="width: 500px; height: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <th>企业愿景：
                                </th>
                                <td colspan="3">
                                    <input class="textBg" type="text" runat="server" id="KK_vision_en" style="width: 450px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                    <colgroup>
                        <col width="200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>操 作：
                        </th>
                        <td>
                            <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改" Visible="true"
                                OnClick="btnSvEdit_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <!--Content-->
            <!--Foot-->
            <div id="divHelp" class="divFoot" style="display: none;">
                <table class="defaultTable" cellpadding="1px" cellspacing="1px">
                    <tr>
                        <th colspan="4">功能操作说明
                        </th>
                    </tr>
                </table>
            </div>
        </form>
    </div>
</body>
</html>
