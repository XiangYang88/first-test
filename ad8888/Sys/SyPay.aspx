<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyPay.aspx.cs" Inherits="sys_SyPay" %>
<!--数据操作的页面模板-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>支付方式</title>
   <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
    <script type="text/javascript" src="../ueditor/ueditor.all.js"></script>
    <script type="text/javascript" src="../ueditor/ueditor.config.js"></script>
    <script type="text/javascript">
        $(function () {
            var editor = new UE.ui.Editor();
            editor.render("KK_content");
        })
    </script>
</head>
<body  >
      <div class="column_Box mainAutoHeight">
    <form id="frmList" runat="server">
       
        <!--head-->
                 <div id="divHead" class="divHead" runat=server>
            <div style="float:left; width:auto; padding-right:5px">
                <span><label id="lblTitle" 
                 style="color:#15428B;padding-right:0px;padding-left:10px">查询条件：</label></span>
                <span>
                
                    <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Field0">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        <asp:ListItem Text="编号" Value="Code^String"></asp:ListItem>
                        <asp:ListItem Text="名称" Value="Name^String"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Compare0">
                        <asp:ListItem Text="包含" Value="like"></asp:ListItem>
                        <asp:ListItem Text="不包含" Value="not like"></asp:ListItem>
                        <asp:ListItem Text="等于" Value="="></asp:ListItem>
                        <asp:ListItem Text="不等于" Value="!="></asp:ListItem>
                    </asp:DropDownList>
                    
                    <input Search_FieldID="Search_Field0" Search_CompareID="Search_Compare0"
                     class="textSearch" type="text" runat="server" id="Search_Keyword0" />
                    
                    每页显示<input type="text" value="10" runat="server" id="txtPageSize" style="width:30px" />条 
                     
                     </span>
                    <span>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SrvButton"
                     Text="查询" OnClick="btnSearch_Click" />
                    </span>
                     
            </div>
            
          <div style="float:left; width:auto; border:0px red solid;">
                 <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_xingjian"></span><i>新建</i></asp:LinkButton>
                    <asp:LinkButton ID="btnDel" OnClick="btnDel_Click" OnClientClick="return confirm('你确定删除所有选中的记录吗？')" CssClass="dot_Item" runat="server"><span class="Icon_item icon_shanchu"></span><i>删除选中项</i></asp:LinkButton>
                 <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_export"></span><i>导出Excel</i></asp:LinkButton>
                   <asp:LinkButton ID="btnImport" OnClick="btnImport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_import"></span><i>导入Excel</i></asp:LinkButton>
               <a onclick="window.location=window.location.href" href="javascript:;" class="dot_Item"><span class="Icon_item icon_zhuanyi" ></span><i>刷新</i></a>
                <a onclick="history.go(-1)" href="javascript:;" class="dot_Item"><span class="Icon_item icon_fuzhi" ></span><i>返回</i></a>
                <span style="text-align:center"><asp:ImageButton ID="imgbtnhelp" ImageUrl="../img/help.gif" runat="server" ToolTip="显示隐藏帮助信息"  OnClientClick="return showHelp()" /></span>
                
            </div>
             <div style="clear:both;font-size:1px; height:1px;"></div>
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
            <tr><td class="tdLeft">上传Excel文件：</td>
                <td><asp:FileUpload ID="fuImportFile" runat=server /></td>
            </tr>
            <tr><td class="tdLeft">导入提示：</td>
                <td><asp:Label ID="lblImportMsg" ForeColor=red runat=server></asp:Label></td>
            </tr>
            <tr><td class="tdLeft">操　　作：</td>
                <td >
                    <asp:Button ID="btnSvImportSave" runat="server" CssClass="SrvButton" Text="执行导入" Visible="true" OnClick="btnSvImportSave_Click"  />&nbsp;
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
            <tr><td class="tdLeft">选择导出内容：</td>
                <td>
                    <asp:CheckBoxList runat=server RepeatColumns=8 ID="cblExportField">
                        <asp:ListItem Value="Code" Selected=true>编号</asp:ListItem>
                        <asp:ListItem Value="Name^{0}" Selected=true>名称</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr><td class="tdLeft">导出类型：</td>
                <td><asp:RadioButtonList runat=server RepeatDirection=horizontal ID="rbtnlExportType">
                    <asp:ListItem Value=0>导出选中行</asp:ListItem>
                    <asp:ListItem Value=1 Selected=true>导出当前页</asp:ListItem>
                    <asp:ListItem Value=2>导出所有页</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr><td class="tdLeft">导出提示：</td>
                <td><asp:Label ID="lblExportMsg" ForeColor=red runat=server></asp:Label></td>
            </tr>
            <tr><td class="tdLeft">操　　作：</td>
                <td >
                    <asp:Button ID="btnSvExportSave" runat="server" CssClass="SrvButton" Text="执行导出" Visible="true" OnClick="btnSvExportSave_Click" />&nbsp;
                    <asp:Button ID="btnSvExportCancel" runat="server" CssClass="SrvButton" Text="取　　消" OnClick="btnSvExportCancel_Click" /></td>
            </tr>
                    
            </table>
        </div>
        <!--Content-->
        <div id="divContent" class="body" runat=server>
            <div id="divEmpty" runat="server" visible="false" class="divNotDataInfo gray_12_normal"><label>暂无相关记录显示</label></div>
            <div id="divList" runat="server">
                <asp:GridView id="gvList" runat="server"  CssClass="defaultGridV" Width="100%"
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
                    <input type="checkbox" onclick="setGridViewSelected(this.checked,'chkItem')" />                    
                    </HeaderTemplate>
                    <ItemTemplate><input type="checkbox" class="chkItem" id="chkItem" runat="server"/></ItemTemplate>
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
                         OnClientClick="javascript:return confirm('你确认要删除吗?')" CommandName="Delete" ImageUrl="../img/delete.gif"   />
                </ItemTemplate>
            </asp:TemplateField>  
                <asp:BoundField DataField="Code" DataFormatString="{0}" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="Name" DataFormatString="{0}" HeaderText="名称">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="payURL" DataFormatString="{0}" HeaderText="支付接口">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="SortNo" DataFormatString="{0}" SortExpression="SortNo" HeaderText="排序">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Notes" DataFormatString="{0}" HeaderText="说明">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="显示/隐藏">
                <ItemTemplate>
                    <%# Eval("status").ToString() == "0" ? "显示" : "<font color=red>隐藏</font>"%>
                </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:TemplateField>               
             
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
            
            <tr><td class="tdLeft">编　　号：</td>
                <td><input title="编　　号" class="textBg" type="text" runat="server" id="KK_Code" style="width:150px"/></td>
                <td class="tdLeft">名　　称：</td>
                <td><input title="名　　称" class="textBg" type="text" runat="server" id="KK_name" style="width:350px"/></td>
            </tr>
            
            <tr><td class="tdLeft" style="height: 20px">属性：</td>
                <td style="height: 20px">
                <asp:CheckBox ID="KK_status" title="是否隐藏" runat="server" Text="是否隐藏"/></td>
                    <td class="tdLeft">排序：</td>
                <td><input title="排序" class="textBg" type="text" runat="server" id="KK_SortNo" style="width:50px"/></td>
            
            </tr>
            
            <tr><td class="tdLeft">支付账号设置：
                <br /><font color=red>只限在线支付填写</font>
            </td>
                <td colspan="3">
                    
                    <div>支付接口<input title="支付接口" class="textBg" type="text" runat="server"
                     id="KK_payURL" style="width:350px"/></div>
                    <div>商家账号<input title="商家账号" class="textBg" type="text" runat="server" id="KK_payAccount" style="width:350px"/><label id="lblMsgAcct" runat="server"></label></div>
                    <div>商家ID<input title="商家ID" class="textBg" type="text" runat="server" id="KK_payID" style="width:350px"/><label id="lblMsgId" runat="server"></label></div>
                    <div>校验码<input title="校验码" class="textBg" type="text" runat="server" id="KK_paySafeCode" style="width:350px"/><label id="lblMsgKey" runat="server"></label></div>
                    <div>本站接口<input title="本站接口" class="textBg" type="text" runat="server"
                     id="KK_sendURL" style="width:350px"/></div>
                     <div>返回接口<input title="返回接口" class="textBg" type="text" runat="server"
                     id="KK_returnURL" style="width:350px"/></div>
                     <div>对帐接口<input title="对帐接口" class="textBg" type="text" runat="server"
                     id="KK_notifyURL" style="width:350px"/></div>
                </td>
            </tr>
            
            <tr><td class="tdLeft">内容说明：</td>
                <td class="tdContent2" colspan="3">
                      <textarea title="内容说明"  runat="server" id="KK_content"></textarea>
                  
                </td>
            </tr>
            
            <tr><td class="tdLeft">备注说明：</td>
                <td colspan="3">
                <textarea title="备注说明" class="textCSS" runat="server" id="KK_notes" style=" width:400px; height:50px">
                </textarea></td>
            </tr>
            
      
            
            <tr><td class="tdLeft">操　　作：</td>
                <td colspan="3">
                    <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="保存新增" Visible="true" OnClick="btnSvAdd_Click"/>&nbsp;
                    <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改" Visible="true" OnClick="btnSvEdit_Click"/>&nbsp;
                    <asp:Button ID="btnSvCancel" runat="server" CssClass="SrvButton" Text="取　　消" OnClick="btnSvCancel_Click"/></td>
            </tr>
            </table>
        </div>
        
        <div id="divHelp" class="divFoot" style="margin-top:40px; display:none">
            <table class="defaultTable" cellpadding="1px" cellspacing="1px">
            <tr><th colspan="4">功能操作说明</th></tr>
                        
            <tr><td class="lefttd" style="width:150px">［支付帐号设置］</td>
                <td colspan="3">如果为非在线支付方式,则支付账号设置不用填写. 而对于在线支付方式, 支付帐号等信息必须填写正确</td>
            </tr>
            <tr><td class="lefttd" style="width:150px">［删除选中项］</td>
                <td colspan="3">首先选中列表中的行记录(可多选)，点击［删除选中项］，删除选中的行记录，删除后不可恢复。</td>
            </tr>
            </table>
        </div>
    </form>
</body>
</html>
