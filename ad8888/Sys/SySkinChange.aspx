<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SySkinChange.aspx.cs" Inherits="ad8888_Sys_SySkinChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
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
                        <li class="current"><a href="javascript:">客服皮肤基本设置</a></li>
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
                                <th>客服皮肤选择：
                                </th>
                                <td colspan="3">
                                    <div class="skin">
                                        <div class="selects">
                                            <input type="radio" name="selects" value="1" onclick="check_radio()" />
                                        </div>
                                        <div>
                                            <img src="../images/skin1.png" />
                                        </div>
                                    </div>
                                    <div class="skin">
                                        <div class="selects">
                                            <input type="radio" name="selects" value="2" onclick="check_radio()" />
                                        </div>
                                        <div>
                                            <img src="../images/skin2.png" />
                                        </div>
                                    </div>
                                    <div class="skin">
                                        <div class="selects">
                                            <input type="radio" name="selects" value="3" onclick="check_radio()" />
                                        </div>
                                        <div>
                                            <img src="../images/skin3.png" />
                                        </div>
                                    </div>
                                    <div class="skin">
                                        <div class="selects">
                                            <input type="radio" name="selects" value="4" onclick="check_radio()" />
                                        </div>
                                        <div>
                                            <img src="../images/skin4.png" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <input type="text" id="KK_skinNum" runat="server" style="display: none" />
                    </div>
                    <script>
                        function check_radio() {
                            var str = "";
                            var items = document.getElementsByName("selects");  //获取name为check的一组元素(checkbox)
                            for (i = 0; i < items.length; i++) {  //循环这组数据
                                if (items[i].checked) {      //判断是否选中
                                    str= items[i].value;
                                }
                            }
                            $("#KK_skinNum").val(str);
                        }
                        $(function () {
                            if ($("#KK_skinNum").val() != "") {
                                var box = document.getElementsByName("selects");
                                strs = $("#KK_skinNum").val(); //字符分割
                                for (var n = 0; n < box.length; n++) {
                                    if (box[n].value == strs) {
                                        box[n].checked = true;
                                    }
                                }
                            }
                        });
                    </script>
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
