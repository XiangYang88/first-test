<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BsMsg1.aspx.cs" Inherits="ad8888_BsPg_BsMsg1" %>
<!--数据操作的页面模板-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>留言管理</title>
    <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
</head>
<body>
    <div class="column_Box mainAutoHeight">
        <form id="frmList" runat="server">

            <div id="divHead" class="divHead" runat="server">
                <div style="float: left; width: auto; padding-right: 5px">
                    <span>
                        <label id="lblTitle"
                            style="color: #15428B; padding-right: 0px; padding-left: 10px">
                            查询条件：</label></span>
                    <span>
                        <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Field0">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                            <asp:ListItem Text="应聘者姓名" Value="name^String"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Compare0">
                            <asp:ListItem Text="包含" Value="like"></asp:ListItem>
                            <asp:ListItem Text="不包含" Value="not like"></asp:ListItem>
                            <asp:ListItem Text="等于" Value="="></asp:ListItem>
                            <asp:ListItem Text="不等于" Value="!="></asp:ListItem>
                        </asp:DropDownList>

                        <input search_fieldid="Search_Field0" search_compareid="Search_Compare0"
                            class="textSearch" type="text" runat="server" id="Search_Keyword0" />



                        <asp:DropDownList CssClass="dropList" Search_Field="a.isnew"
                            runat="server" ID="Search_Keyword1">
                            <asp:ListItem Text="未/已读" Value=""></asp:ListItem>
                            <asp:ListItem Text="未读" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已读" Value="0"></asp:ListItem>
                        </asp:DropDownList>



                        每页显示<input type="text" value="10" runat="server" id="txtPageSize" style="width: 30px" />条 
                     
                    </span>
                    <span>
                        <asp:Button ID="btnSearch" runat="server" CssClass="SrvButton"
                            Text="查询" OnClick="btnSearch_Click" />
                    </span>

                </div>

                <div style="float: left; width: auto; border: 0px red solid;">
                    <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_xingjian"></span><i>新建</i></asp:LinkButton>
                    <asp:LinkButton ID="btnDel" OnClick="btnDel_Click" OnClientClick="return confirm('你确定删除所有选中的记录吗？')" CssClass="dot_Item" runat="server"><span class="Icon_item icon_shanchu"></span><i>删除选中项</i></asp:LinkButton>
                    <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_export"></span><i>导出Excel</i></asp:LinkButton>
                    <asp:LinkButton ID="btnImport" OnClick="btnImport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_import"></span><i>导入Excel</i></asp:LinkButton>
                    <asp:LinkButton ID="btnShow" SetField="status" SetValue="0" OnClick="btnSetItemValue_Click" CssClass="dot_Item" runat="server" Visible="false"><span class="Icon_item icon_listshow"></span><i>设为显示</i></asp:LinkButton>
                    <asp:LinkButton ID="btnNoHide" SetField="status" SetValue="1" OnClick="btnSetItemValue_Click" CssClass="dot_Item" runat="server" Visible="false"><span class="Icon_item icon_listhide"></span><i>设为隐藏</i></asp:LinkButton>

                    <a onclick="window.location=window.location.href" href="javascript:;" class="dot_Item"><span class="Icon_item icon_zhuanyi"></span><i>刷新</i></a>
                    <a onclick="history.go(-1)" href="javascript:;" class="dot_Item"><span class="Icon_item icon_fuzhi"></span><i>返回</i></a>
                    <span style="text-align: center">
                        <asp:ImageButton ID="imgbtnhelp" ImageUrl="../img/help.gif" runat="server" ToolTip="显示隐藏帮助信息" OnClientClick="return showHelp()" /></span>

                </div>


                <div style="clear: both; font-size: 1px; height: 1px;"></div>
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
                        <td class="tdLeft">上传Excel文件：</td>
                        <td>
                            <asp:FileUpload ID="fuImportFile" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLeft">导入提示：</td>
                        <td>
                            <asp:Label ID="lblImportMsg" ForeColor="red" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="tdLeft">操　　作：</td>
                        <td>
                            <asp:Button ID="btnSvImportSave" runat="server" CssClass="SrvButton" Text="执行导入" Visible="true" OnClick="btnSvImportSave_Click" />&nbsp;
                    <asp:Button ID="btnSvImportCancel" runat="server" CssClass="SrvButton" Text="取　　消" OnClick="btnSvImportCancel_Click" /></td>
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
                        <td class="tdLeft">选择导出内容：</td>



                        <td>
                            <asp:CheckBoxList runat="server" RepeatColumns="8" ID="cblExportField">
                                <asp:ListItem Value="Code" Selected="true">编号</asp:ListItem>
                                <asp:ListItem Value="Name" Selected="true">名称</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">导出类型：</td>
                        <td>
                            <asp:RadioButtonList runat="server" RepeatDirection="horizontal" ID="rbtnlExportType">
                                <asp:ListItem Value="0">导出选中行</asp:ListItem>
                                <asp:ListItem Value="1" Selected="true">导出当前页</asp:ListItem>
                                <asp:ListItem Value="2">导出所有页</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td class="tdLeft">导出提示：</td>
                        <td>
                            <asp:Label ID="lblExportMsg" ForeColor="red" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="tdLeft">操　　作：</td>
                        <td>
                            <asp:Button ID="btnSvExportSave" runat="server" CssClass="SrvButton" Text="执行导出" Visible="true" OnClick="btnSvExportSave_Click" />&nbsp;
                    <asp:Button ID="btnSvExportCancel" runat="server" CssClass="SrvButton" Text="取　　消" OnClick="btnSvExportCancel_Click" /></td>
                    </tr>

                </table>
            </div>
            <!--Content-->
            <div id="divContent" class="body" runat="server">
                <div id="divEmpty" runat="server" visible="false" class="divNotDataInfo gray_12_normal">
                    <label>暂无相关记录显示</label>
                </div>
                <div id="divList" runat="server">
                    <asp:GridView ID="gvList" runat="server" CssClass="defaultGridV" Width="100%"
                        CellPadding="3" AllowPaging="True"
                        AutoGenerateColumns="False" AllowSorting="true"
                        OnSorting="gvList_Sorting"
                        OnRowDataBound="gvList_RowDataBound"
                        OnSelectedIndexChanged="gvList_SelectedIndexChanged"
                        OnRowEditing="gvList_RowEditing"
                        OnRowDeleting="gvList_RowDeleting" OnPreRender="gvList_PreRender">
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

                            <asp:CommandField HeaderText="修改" ButtonType="Image" EditImageUrl="../img/Edit.gif" EditText="修改" ShowEditButton="true">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                            </asp:CommandField>
                            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButtonDel" runat="server" CausesValidation="False"
                                        OnClientClick="javascript:return confirm('你确认要删除吗?')" CommandName="Delete" ImageUrl="../img/delete.gif" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="未/已读" SortExpression="isNew">
                                <ItemTemplate>
                                    <%# Eval("isNew").ToString()=="0"?"已读":"<font color=red>未读</font>" %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:TemplateField>

                            <asp:BoundField DataField="name" DataFormatString="{0}" HeaderText="应聘者姓名" SortExpression="name">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>

                            <asp:BoundField DataField="mobile" DataFormatString="{0}" HeaderText="电话" SortExpression="mobile">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AddTime" DataFormatString="{0}" HeaderText="应聘时间" SortExpression="AddTime">
                                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            </asp:BoundField>

                        </Columns>
                    </asp:GridView>
                    <!--分页-->
                    <div style="padding-top: 10px; width: 100%">
                        <webdiyer:AspNetPager ID="ANPage1" runat="server" HorizontalAlign="Center" ShowCustomInfoSection="Left"
                            NumericButtonTextFormatString="[{0}]" OnPageChanged="ANPage1_PageChanged" FirstPageText="首页"
                            LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" Height="18px" ShowPageIndexBox="always" ShowPageIndex="true"
                            CustomInfoStyle="padding-top:5px"
                            Width="100%" NumericButtonCount="10">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>

            <!--Foot-->
            <div id="divDtls" class="divFoot" runat="server" visible="false">
                <div class="tab">
                    <ul>
                        <li class="current"><a href="javascript:">详细内容</a></li>
                    </ul>
                </div>
                <input type="text" runat="server" id="PKID" visible="false" />
                <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
                    <colgroup>
                        <col width="200px" />
                        <col />
                        <col width="200px" />
                        <col />
                    </colgroup>


                    <tr>
                        <td class="tdLeft">应聘者姓名：</td>
                        <td>
                            <input title="联系人" class="textBg" type="text" runat="server" id="KK_name" lock="1" style="width: 200px" /></td>                        
                    </tr>
                    <tr>
                        <td class="tdLeft" >电话：</td>
                        <td>
                            <input title="电话" class="textBg" type="text" runat="server" id="KK_mobile" lock="1" style="width: 200px" /></td>
                        <td class="tdLeft">IP：</td>
                        <td>
                            <input title="IP" class="textBg" type="text" runat="server" id="KK_IP" lock="1" style="width: 293px" /></td>
                    </tr>
                    <tr>
                        <td class="tdLeft">应聘者简述：</td>
                        <td colspan="3">
                            <textarea title="内容" runat="server" id="KK_content" lock="1" style="width: 500px; height: 143px;"></textarea>
                        </td>

                    </tr>
                    <tr style="display: none;">
                        <td class="tdLeft">回复：</td>
                        <td colspan="3">
                            <textarea id="KK_Reply" runat="server" style="width: 500px; height: 143px"
                                title="回复"></textarea></td>

                    </tr>
                    <tr style="display: none;">
                        <td class="tdLeft">属性：</td>
                        <td colspan="3">
                            <asp:CheckBox ID="KK_status" Text="隐藏" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLeft">操作：</td>
                        <td colspan="3">
                            <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="保存新增" Visible="true" OnClick="btnSvAdd_Click" />&nbsp;
                    <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改" Visible="true" OnClick="btnSvEdit_Click" />&nbsp;
                    <asp:Button ID="btnSvCancel" runat="server" CssClass="SrvButton" Text="取　　消" OnClick="btnSvCancel_Click" /></td>
                    </tr>

                </table>
            </div>

            <div id="divHelp" class="divFoot" style="margin-top: 40px; display: none">
                <table class="defaultTable" cellpadding="1px" cellspacing="1px">
                    <tr>
                        <th colspan="4">功能操作说明</th>
                    </tr>

                    <tr>
                        <td class="lefttd" style="width: 150px">［新增记录］</td>
                        <td colspan="3">点击［新增记录］打开详细内容界面，输入完相应信息后，点击下方的［保存新增］。</td>
                    </tr>
                    <tr>
                        <td class="lefttd" style="width: 150px">［删除选中项］</td>
                        <td colspan="3">首先选中列表中的行记录(可多选)，点击［删除选中项］，删除选中的行记录，删除后不可恢复。</td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
</body>
</html>
