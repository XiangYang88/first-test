<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BsNewsList1.aspx.cs" Inherits="admin_BsPg_BsNewsList1" %>

<!--数据操作的页面模板-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章列表</title>
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
            window.editor = K.create('#KK_ContentIndex', { width: '98%', height: '400px' });
            window.editor = K.create('#KK_ContentIndex_en', { width: '98%', height: '400px' });
            window.editor = K.create('#KK_Map', { width: '98%', height: '400px' });
            window.editor = K.create('#KK_Map_en', { width: '98%', height: '400px' });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            //排序号
            $('#KK_BS_NewsKindCode').bind('change', function () {
                var code = $('select#KK_BS_NewsKindCode option:selected').attr("value");
                if (code != "") {
                    $.ajax({
                        type: "POST",
                        url: "../ajax/NetSortno.aspx",
                        data: "tblName=<%=TblName%>&FKField=<%=FKField%>&pc=" + code,
                        success: function (sortno) {

                            $("#KK_SortNo").val(sortno)
                        }
                    });
                }
            });

        })

        function checkForm() {
            var newType = $("#KK_BS_NewsKindCode").attr("value");
            if (newType == "") {
                setMsg("请选择分类");
                $("#KK_BS_NewsKindCode").focus();
                return false;
            }
            var title = $("#KK_Title");
            if (title.val() == "") {
                setMsg("标题不能为空！");
                title.focus();
                return false;
            }

            return true;

        }
        function setMsg(v) {
            parent.jsprint(v, "", "Error");
        }
    </script>
</head>
<body>

    <div class="column_Box mainAutoHeight">
        <form id="frmList" runat="server">
            <div id="divHead" class="divHead" runat="server">
                <div style="float: left; width: auto; padding-right: 5px">
                    <span>
                        <label id="lblTitle" style="color: #15428B; padding-right: 0px; padding-left: 10px">
                            查询条件：</label></span> <span>
                                <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Field0">
                                    <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                                    <asp:ListItem Text="标题" Value="title^String"></asp:ListItem>
                                    <asp:ListItem Text="内容" Value="Bs_News.content^String"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Compare0">
                                    <asp:ListItem Text="包含" Value="like"></asp:ListItem>
                                    <asp:ListItem Text="不包含" Value="not like"></asp:ListItem>
                                    <asp:ListItem Text="等于" Value="="></asp:ListItem>
                                    <asp:ListItem Text="不等于" Value="!="></asp:ListItem>
                                </asp:DropDownList>
                                <input search_fieldid="Search_Field0" search_compareid="Search_Compare0" class="textSearch"
                                    type="text" runat="server" id="Search_Keyword0" />
                                <asp:DropDownList CssClass="dropList" Search_Compare="like_code" Search_Field="Bs_NewsKindCode"
                                    runat="server" ID="Search_Keyword1">
                                </asp:DropDownList>
                                每页显示<input type="text" value="10" runat="server" id="txtPageSize" style="width: 30px" />条
                            </span><span>
                                <asp:Button ID="btnSearch" runat="server" CssClass="SrvButton" Text="查询" OnClick="btnSearch_Click" />
                            </span>
                </div>
                <div style="float: left; width: auto; border: 0px red solid;">
                    <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_xingjian"></span><i>新建</i></asp:LinkButton>
                    <asp:LinkButton ID="btnDel" OnClick="btnDel_Click" OnClientClick="return confirm('你确定删除所有选中的记录吗？')" CssClass="dot_Item" runat="server"><span class="Icon_item icon_shanchu"></span><i>删除选中项</i></asp:LinkButton>
                    <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_export"></span><i>导出Excel</i></asp:LinkButton>
                    <asp:LinkButton ID="btnImport" OnClick="btnImport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_import"></span><i>导入Excel</i></asp:LinkButton>
                    <a onclick="window.location=window.location.href" href="javascript:;" class="dot_Item"><span class="Icon_item icon_zhuanyi"></span><i>刷新</i></a>
                    <a onclick="history.go(-1)" href="javascript:;" class="dot_Item"><span class="Icon_item icon_fuzhi"></span><i>返回</i></a>
                    <span style="text-align: center">
                        <asp:ImageButton ID="imgbtnhelp" ImageUrl="../img/help.gif" runat="server" ToolTip="显示隐藏帮助信息" OnClientClick="return showHelp()" /></span>
                </div>
                <div style="clear: both; font-size: 1px; height: 1px;">
                </div>
            </div>
            <div id="divImport" runat="server" visible="false">
                <div class="tab">
                    <ul>
                        <li class="current"><a href="javascript:">导入Excel</a></li>
                    </ul>
                </div>
                <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                    <colgroup>
                        <col width="200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLeft">上传Excel文件：
                        </td>
                        <td>
                            <asp:FileUpload ID="fuImportFile" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">导入提示：
                        </td>
                        <td>
                            <asp:Label ID="lblImportMsg" ForeColor="red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">操 作：
                        </td>
                        <td>
                            <asp:Button ID="btnSvImportSave" runat="server" CssClass="SrvButton" Text="执行导入"
                                Visible="true" OnClick="btnSvImportSave_Click" />&nbsp;
                    <asp:Button ID="btnSvImportCancel" runat="server" CssClass="SrvButton" Text="取　　消"
                        OnClick="btnSvImportCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divExport" runat="server" visible="false">
                <div class="tab">
                    <ul>
                        <li class="current"><a href="javascript:">导出Excel</a></li>
                    </ul>
                </div>
                <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                    <colgroup>
                        <col width="200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLeft">选择导出内容：
                        </td>
                        <td>
                            <asp:CheckBoxList runat="server" RepeatColumns="8" ID="cblExportField">
                                <asp:ListItem Value="Code" Selected="true">编号</asp:ListItem>
                                <asp:ListItem Value="Name^{0}" Selected="true">名称</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">导出类型：
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" RepeatDirection="horizontal" ID="rbtnlExportType">
                                <asp:ListItem Value="0">导出选中行</asp:ListItem>
                                <asp:ListItem Value="1" Selected="true">导出当前页</asp:ListItem>
                                <asp:ListItem Value="2">导出所有页</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">导出提示：
                        </td>
                        <td>
                            <asp:Label ID="lblExportMsg" ForeColor="red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">操 作：
                        </td>
                        <td>
                            <asp:Button ID="btnSvExportSave" runat="server" CssClass="SrvButton" Text="执行导出"
                                Visible="true" OnClick="btnSvExportSave_Click" />&nbsp;
                    <asp:Button ID="btnSvExportCancel" runat="server" CssClass="SrvButton" Text="取　　消"
                        OnClick="btnSvExportCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <!--Content-->
            <div id="divContent" class="body" runat="server">
                <div id="divEmpty" runat="server" visible="false" class="divNotDataInfo gray_12_normal">
                    <label>
                        暂无相关记录显示</label>
                </div>
                <div id="divList" runat="server">
                    <asp:GridView ID="gvList" runat="server" CssClass="defaultGridV" Width="100%" CellPadding="3"
                        AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" OnSorting="gvList_Sorting"
                        OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged"
                        OnRowEditing="gvList_RowEditing" OnRowDeleting="gvList_RowDeleting" OnPreRender="gvList_PreRender">
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                        <FooterStyle HorizontalAlign="center" VerticalAlign="Middle"></FooterStyle>
                        <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                        <AlternatingRowStyle BackColor="#E6F5FA"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Left" BorderWidth="1px" BorderStyle="Solid" Width="20px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="setGridViewSelected(this.checked, 'chkItem')" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" class="chkItem" id="chkItem" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="序号" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="36px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                            <asp:CommandField HeaderText="查看" SelectText="查看" ShowSelectButton="True">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                            </asp:CommandField>
                            <asp:CommandField HeaderText="修改" ButtonType="Image" EditImageUrl="../img/Edit.gif"
                                EditText="修改" ShowEditButton="true">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                            </asp:CommandField>
                            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonDel" runat="server" CausesValidation="False" OnClientClick="javascript:return confirm('你确认要删除吗?')"
                                        CommandName="Delete" ImageUrl="../img/delete.gif" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" DataFormatString="{0}" HeaderText="标题" SortExpression="Title">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="KindName" DataFormatString="{0}" HeaderText="所属分类" SortExpression="KindName">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SortNo" DataFormatString="{0}" HeaderText="排序号" SortExpression="SortNo">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="56px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="56px"></ItemStyle>
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="显示/隐藏">
                                <ItemTemplate>
                                    <%# Eval("status").ToString()=="0"?"显示":"<font color=red>隐藏</font>" %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="60px" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="60px" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AddUserName" DataFormatString="{0}" HeaderText="添加者" SortExpression="AddUserName">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" Width="50px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" Width="50px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AddTime" DataFormatString="{0}" HeaderText="添加时间" SortExpression="addTime">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" Width="120" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" Width="120" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ModUserName" DataFormatString="{0}" HeaderText="修改者" SortExpression="ModUserName">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" Width="50px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" Width="50px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <!--分页-->
                    <div style="padding-top: 10px; width: 100%">
                        <webdiyer:AspNetPager ID="ANPage1" runat="server" HorizontalAlign="Center" ShowCustomInfoSection="Left"
                            NumericButtonTextFormatString="[{0}]" OnPageChanged="ANPage1_PageChanged" FirstPageText="首页"
                            LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" Height="18px" ShowPageIndexBox="always"
                            ShowPageIndex="true" CustomInfoStyle="padding-top:5px" Width="100%" NumericButtonCount="10">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
            <!--Foot-->
            <div id="divDtls" class="divFoot body" runat="server" visible="false">
                <div class="tab">
                    <ul>
                        <li class="current"><a href="javascript:">详细内容</a></li>
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
                                <col width="200px" />
                                <col />
                            </colgroup>

                            <tr>
                                <td class="tdLeft">文章分类：
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" title="文章分类" CssClass="dropList" ID="KK_BS_NewsKindCode">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">标题：
                                </td>
                                <td>
                                    <input title="标题" class="textBg" type="text" runat="server" id="KK_Title" style="width: 800px" />
                                </td>

                            </tr>
                            <tr style="<%=title_sub%>">
                                <td class="tdLeft">小标题：
                                </td>
                                <td>
                                    <input title="小标题" class="textBg" type="text" runat="server" id="KK_Title_Sub" style="width: 800px" />
                                </td>

                            </tr>
                            <tr style="<%=hasurl%>">
                                <td class="tdLeft">链接：
                                </td>
                                <td>
                                    <input title="链接" class="textBg" type="text" runat="server" id="KK_url" style="width: 300px" /><br />
                                    <font color="gray"></font>
                                </td>

                            </tr>
                            <tr style="<%=hasnotes %>">
                                <td class="tdLeft">简介：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="简介" runat="server" style="width: 800px; height: 100px" id="KK_Notes"></textarea>
                                </td>
                            </tr>
                            <tr style="<%=hasmap%>">
                                <td class="tdLeft">左侧地址：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="首页简介" runat="server" style="width: 800px; height: 100px" id="KK_Map"></textarea>
                                </td>
                            </tr>
                            <tr style="<%=hasindexnotes %>">
                                <td class="tdLeft">底部地址：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="首页简介" runat="server" style="width: 800px; height: 100px" id="KK_ContentIndex"></textarea>
                                </td>
                            </tr>
                            <tr style="<%=hascontent%>">
                                <td class="tdLeft">内容：
                                </td>
                                <td>
                                    <textarea title="内容" runat="server" id="KK_Content"></textarea>
                                </td>
                            </tr>

                            <tr style="<%=hasmedia%>">
                                <td class="tdLeft">媒体：
                                </td>
                                <td>
                                    <input type="text" title="媒体" id="KK_Media" runat="server" style="display: none" />
                                    <iframe scrolling="no" width="800" height="190" frameborder="0" src="../uploadIframe.aspx?id=KK_Media&multi=false&filelist=<%=this.KK_Media.Value %>"></iframe>
                                </td>
                            </tr>

                            <tr style="<%=hasimg %>">
                                <td class="tdLeft">列表图片：
                                </td>
                                <td>
                                    <input type="text" title="图片" id="KK_Pic" runat="server" style="display: none" />
                                    <iframe scrolling="auto" width="600" height="250" frameborder="0" src="../uploadIframe.aspx?id=KK_Pic&filelist=<%=this.KK_Pic.Value %>&multi=true&pathlist=upload|0|0,upload/article/small|270|270"></iframe>
                                    <br />
                                    <%if (imagesize != "")
                                      {%><font color="red">图片最佳尺寸：<%=imagesize%></font><br />
                                    <%}
                                      else
                                      { %>
                                    <font color="gray">最佳尺寸：180*160</font>
                                    <%} %>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">内页图片：
                                </td>
                                <td>
                                    <input type="text" title="小图" id="KK_Pic_small" runat="server" style="display: none" />
                                    <iframe scrolling="auto" width="600" height="250" frameborder="0" src="../uploadIframe.aspx?id=KK_Pic_small&multi=true&filelist=<%=this.KK_Pic_small.Value %>"></iframe>
                                    <br />
                                    <font color="gray">最佳尺寸：410*520</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">排序号：
                                </td>
                                <td>
                                    <input title="排序号" class="textBg" type="text" runat="server" id="KK_SortNo" style="width: 50px" />
                                    是否隐藏:<asp:CheckBox title="是否隐藏" ID="KK_Status" runat="server" />

                                </td>
                            </tr>
                            <tr style="<%=istop%>">
                                <td class="tdLeft">是否推荐:
                                </td>
                                <td>
                                    <asp:CheckBox ID="KK_IsTop" runat="server" />
                                </td>
                            </tr>
                            <%if (Common.Utils.OpenSeo())
                              { %><tr>
                                  <td class="tdLeft">Seo：
                                  </td>
                                  <td>如不填写则用网站设置中Seo信息
                                  </td>
                              </tr>

                            <tr>
                                <td class="tdLeft">SeoTitle：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="SeoTitle" type="text" runat="server" id="KK_SeoTitle"
                                        style="width: 450px; height: 65px"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">KeyWord：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="KeyWord" type="text" runat="server" id="KK_KeyWord"
                                        style="width: 450px; height: 65px" cols="20" name="S1" rows="1"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Description：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="Description" type="text" runat="server" id="KK_Description"
                                        style="width: 450px; height: 65px"></textarea>

                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                    <div class="body">
                        <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                            <colgroup>
                                <col width="200px" />
                                <col />
                            </colgroup>

                            <tr>
                                <td class="tdLeft">标题：
                                </td>
                                <td>
                                    <input title="标题" class="textBg" type="text" runat="server" id="KK_Title_en" style="width: 800px" />
                                </td>

                            </tr>
                            <tr style="<%=hasnotes %>">
                                <td class="tdLeft">简介：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="简介" runat="server" style="width: 800px; height: 100px" id="KK_Notes_en"></textarea>
                                </td>
                            </tr>    
                            <tr style="<%=hasmap%>">
                                <td class="tdLeft">左侧地址：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="首页简介" runat="server" style="width: 800px; height: 100px" id="KK_Map_en"></textarea>
                                </td>
                            </tr>
                            <tr style="<%=hasindexnotes %>">
                                <td class="tdLeft">底部地址：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="首页简介" runat="server" style="width: 800px; height: 100px" id="KK_ContentIndex_en"></textarea>
                                </td>
                            </tr>
                            <tr style="<%=hascontent%>">
                                <td class="tdLeft">内容：
                                </td>
                                <td>
                                    <textarea title="内容" runat="server" id="KK_Content_en"></textarea>
                                </td>
                            </tr>
                            <%if (Common.Utils.OpenSeo())
                              { %><tr>
                                  <td class="tdLeft">Seo：
                                  </td>
                                  <td>如不填写则用网站设置中Seo信息
                                  </td>
                              </tr>

                            <tr>
                                <td class="tdLeft">SeoTitle：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="SeoTitle" type="text" runat="server" id="KK_SeoTitle_en"
                                        style="width: 450px; height: 65px"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">KeyWord：
                                </td>
                                <td>
                                    <textarea class="textCSS" title="KeyWord" type="text" runat="server" id="KK_KeyWord_en"
                                        style="width: 450px; height: 65px" cols="20" name="S1" rows="1"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">Description：
                                </td>
                                <td>
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
                        <col width="200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <td class="tdLeft">操作：
                        </td>
                        <td>
                            <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="保存新增" Visible="true"
                                OnClick="btnSvAdd_Click" OnClientClick="return checkForm()" />&nbsp;
                    <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改" Visible="true"
                        OnClick="btnSvEdit_Click" />&nbsp;
                    <asp:Button ID="btnSvCancel" runat="server" CssClass="SrvButton" Text="取　　消" OnClick="btnSvCancel_Click" />
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
