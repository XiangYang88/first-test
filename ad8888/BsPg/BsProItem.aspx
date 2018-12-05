<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BsProItem.aspx.cs" Inherits="admin_BsProItem" %>

<!--数据操作的页面模板-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>产品明细</title>
    <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
    <script charset="utf-8" src="../hn_editor/kindeditor.js"></script>
    <script charset="utf-8" src="../hn_editor/lang/zh_CN.js"></script>
    <script type="text/javascript">
        KindEditor.ready(function (K) {
            window.editor = K.create('#KK_Content', { width: '98%', height: '400px' });
            window.editor = K.create('#KK_Content_en', { width: '98%', height: '400px' });
        });
    </script>
</head>
<body>
    <div class="column_Box mainAutoHeight">
        <form id="frmList" runat="server">
            <div id="divHead" class="divHead">

                <div style="clear: both; font-size: 1px; height: 1px;">
                </div>
            </div>
            <!--Foot-->
            <div id="divDtls" class="divFoot body" runat="server">
                <div class="tab">
                    <ul>
                        <li class="current"><a href="javascript:">基本信息</a></li>
                        <%if (multiLanguageEN)
                          { %>
                        <li><a href="javascript:">详细内容_英文</a></li>
                        <%} %>
                    </ul>
                </div>
                <div class="wrapBox ">
                    <input type="text" runat="server" id="PKID" visible="false" />
                    <div class="body">
                        <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                            <colgroup>
                                <col width="120px" />
                                <col />
                                <col width="120px" />
                                <col />
                            </colgroup>
                            <tr style="display: none;">
                                <td class="tdLeft">产品编号
                                </td>
                                <td>
                                    <input class="textBg" title="产品编号" type="text" runat="server" id="KK_Code" style="width: 250px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">名称：
                                </td>
                                <td>
                                    <input class="textBg" title="名称" type="text" runat="server" id="KK_Name" style="width: 250px" />
                                </td>
                            </tr>

                            <tr>
                                <td class="tdLeft">分类：
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" title="分类" CssClass="dropList" ID="KK_Bs_ProKindCode">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td class="tdLeft">图片：
                                </td>
                                <td>
                                    <input type="text" title="图片" id="KK_Pic" runat="server" style="display: none" />
                                    <iframe scrolling="no" width="800" height="350px" scrolling="auto" frameborder="0"
                                        src="../uploadIframe.aspx?id=KK_Pic&filelist=<%=this.KK_Pic.Value %>&multi=false&isWatermark=true&pathlist=upload/product|0|0,upload/product/small|270|270"></iframe>
                                    <br />
                                    图片最佳尺寸：<span class="tips" id="tips" runat="server">487px*333px</span>
                                </td>
                            </tr>
                            <script type="text/javascript">
                                $("#KK_Bs_ProKindCode").change(function () {
                                    if ($("#KK_Bs_ProKindCode").val() == '02') {
                                        $(".tips").html("193px*193px");
                                        $("#trpro").removeAttr("style");
                                    } else {
                                        $(".tips").html("487px*333px");
                                        $("#trpro").attr("style", "display:none;");
                                    }
                                });
                            </script>
                            <tr style="<%=hascontent%>" id="trpro">
                                <td class="tdLeft">产品介绍：
                                </td>
                                <td class="tdContent">
                                    <textarea title="内容" runat="server" id="KK_Content"></textarea>
                                </td>
                            </tr>
                            <tr>

                                <td class="tdLeft">排 序：
                                </td>
                                <td>
                                    <input class="textBg" value="0" title="排序" type="text" runat="server" id="KK_sortNo"
                                        style="width: 250px" />
                                </td>
                            </tr>
                            <%if (Common.Utils.OpenSeo())
                              { %><tr>
                                  <td class="tdLeft">Seo：
                                  </td>
                                  <td colspan="3">如不填写则用网站设置中Seo信息
                                  </td>
                              </tr>

                            <tr>
                                <td class="tdLeft">SeoTitle：
                                </td>
                                <td colspan="3">
                                    <textarea class="textCSS" title="SeoTitle" type="text" runat="server" id="KK_SeoTitle"
                                        style="width: 450px; height: 65px"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">KeyWord：
                                </td>
                                <td colspan="3">
                                    <textarea class="textCSS" title="KeyWord" type="text" runat="server" id="KK_KeyWord"
                                        style="width: 450px; height: 65px" cols="20" name="S1" rows="1"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Description：
                                </td>
                                <td colspan="3">
                                    <textarea class="textCSS" title="Description" type="text" runat="server" id="KK_Description"
                                        style="width: 450px; height: 65px"></textarea>

                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td class="tdLeft">属性：
                                </td>
                                <td>
                                    <asp:CheckBox ID="KK_Status" Text="隐藏" runat="server" />&nbsp;&nbsp;<asp:CheckBox ID="KK_isindex" Text="是否首页显示" runat="server" />
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
                                <td class="tdLeft">名称：
                                </td>
                                <td>
                                    <input class="textBg" title="名称" type="text" runat="server" id="KK_Name_en" style="width: 250px" />
                                </td>
                            </tr>

                            <tr>
                                <td class="tdLeft">产品介绍：
                                </td>
                                <td class="tdContent">
                                    <textarea title="内容" runat="server" id="KK_Content_en"></textarea>
                                </td>
                            </tr>

                            <%if (Common.Utils.OpenSeo())
                              { %><tr>
                                  <td class="tdLeft">Seo：
                                  </td>
                                  <td colspan="3">如不填写则用网站设置中Seo信息
                                  </td>
                              </tr>

                            <tr>
                                <td class="tdLeft">SeoTitle：
                                </td>
                                <td colspan="3">
                                    <textarea class="textCSS" title="SeoTitle" type="text" runat="server" id="KK_SeoTitle_en"
                                        style="width: 450px; height: 65px"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">KeyWord：
                                </td>
                                <td colspan="3">
                                    <textarea class="textCSS" title="KeyWord" type="text" runat="server" id="KK_KeyWord_en"
                                        style="width: 450px; height: 65px" cols="20" name="S1" rows="1"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Description：
                                </td>
                                <td colspan="3">
                                    <textarea class="textCSS" title="Description" type="text" runat="server" id="KK_Description_en"
                                        style="width: 450px; height: 65px"></textarea>

                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                </div>
                <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                    <colgroup>
                        <col width="120px" />
                        <col />
                        <col width="120px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLeft">操 作：
                        </td>
                        <td>
                            <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="保存新增" Visible="false"
                                OnClick="btnSvAdd_Click" />&nbsp;
                    <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改" Visible="false"
                        OnClick="btnSvEdit_Click" />&nbsp;
                    <input type="button" id="btnReturn" runat="server" class="SrvButton" value="关　　闭"
                        onclick="window.close();" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divHelp" class="divFoot" style="margin-top: 40px; display: none">
                <table class="defaultTable" cellpadding="1px" cellspacing="1px">
                    <tr>
                        <th colspan="4">功能操作说明
                        </th>
                    </tr>
                    <tr>
                        <td class="lefttd" style="width: 150px">［新增记录］
                        </td>
                        <td colspan="3">点击［新增记录］打开详细内容界面，输入完相应信息后，点击下方的［保存新增］。
                        </td>
                    </tr>
                    <tr>
                        <td class="lefttd" style="width: 150px">［删除选中项］
                        </td>
                        <td colspan="3">首先选中列表中的行记录(可多选)，点击［删除选中项］，删除选中的行记录，删除后不可恢复。
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
</body>
</html>
