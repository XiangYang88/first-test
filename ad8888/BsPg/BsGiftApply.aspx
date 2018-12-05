<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BsGiftApply.aspx.cs" Inherits="admin_BsPg_BsGiftApply" %>

<!--数据操作的页面模板   最新版-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>礼品申请</title>
   <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
</head>
<body  >
       <div class="column_Box mainAutoHeight">
    <form id="frmList" runat="server">

          <div id="divHead" class="divHead" runat=server>
            <div style="float:left; width:auto; padding-right:5px">
                <span><label id="lblTitle" 
                 style="color:#15428B;padding-right:0px;padding-left:10px">查询条件：</label></span>
                <span>                
                    <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Field0">
                        <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                        <asp:ListItem Text="单号" Value="bs_GiftApply.code^String"></asp:ListItem>
                        <asp:ListItem Text="会员名" Value="bs_user.name^String"></asp:ListItem>
                        <asp:ListItem Text="收货人" Value="csgName^String"></asp:ListItem>
                        <asp:ListItem Text="Email" Value="csgEmail^String"></asp:ListItem>
                        <asp:ListItem Text="手机" Value="csgMobile^String"></asp:ListItem>
                        <asp:ListItem Text="电话" Value="csgPhone^String"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Compare0">
                        <asp:ListItem Text="包含" Value="like"></asp:ListItem>
                        <asp:ListItem Text="不包含" Value="not like"></asp:ListItem>
                        <asp:ListItem Text="等于" Value="="></asp:ListItem>
                        <asp:ListItem Text="不等于" Value="!="></asp:ListItem>
                    </asp:DropDownList>
                    
                    <input Search_FieldID="Search_Field0" Search_CompareID="Search_Compare0"
                     class="textSearch" type="text" runat="server" id="Search_Keyword0" />
                    
                    每页显示<input type="text" value="20" runat="server" id="txtPageSize" style="width:30px" />条 
                     
                     </span>
                    <span>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SrvButton"
                     Text="查询" OnClick="btnSearch_Click" />
                    </span>
                     
            </div>
            
          <div style="float:left; width:auto; border:0px red solid;">
                 <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_xingjian"></span><i>新建</i></asp:LinkButton>
                    <asp:LinkButton ID="btnDel" OnClick="btnDel_Click" OnClientClick="return confirm('你确定删除所有选中的记录吗？')" CssClass="dot_Item" runat="server"><span class="Icon_item icon_shanchu"></span><i>删除选中项</i></asp:LinkButton>
                 <asp:LinkButton ID="btnExport" OnClick="btnExport_Click"  CssClass="dot_Item" runat="server"><span class="Icon_item icon_export"></span><i>导出Excel</i></asp:LinkButton>
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
                        <asp:ListItem Value="Code" Selected=true>单号</asp:ListItem>
                        <asp:ListItem Value="statusName" Selected=true>状态</asp:ListItem>
                        <asp:ListItem Value="userName" Selected=true>会员名</asp:ListItem>
                       <asp:ListItem Value="quantity" Selected=true>礼品名称</asp:ListItem>
                       <asp:ListItem Value="score" Selected=true>所需积分</asp:ListItem>
                       <asp:ListItem Value="usablescore" Selected=true>会员当前积分</asp:ListItem>
                       <asp:ListItem Value="csgName" Selected=true>收货人</asp:ListItem>
                       <asp:ListItem Value="csgprovince" Selected=true>省份</asp:ListItem>
                       <asp:ListItem Value="csgcity" Selected=true>城市</asp:ListItem>
                       <asp:ListItem Value="csgcounty" Selected=true>县/区</asp:ListItem>
                       <asp:ListItem Value="csgaddress" Selected=true>地址</asp:ListItem>
                       <asp:ListItem Value="csgpostcode" Selected=true>邮编</asp:ListItem>
                       <asp:ListItem Value="csgmobile" Selected=true>手机</asp:ListItem>
                       <asp:ListItem Value="csgphone" Selected=true>电话</asp:ListItem>
                       <asp:ListItem Value="message" Selected=true>客户留言</asp:ListItem>                       
                       <asp:ListItem Value="addtime" Selected=true>申请时间</asp:ListItem>
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
                <asp:GridView id="gvList" runat="server"  CssClass="defaultGridV" Width="1200px"
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
                
                <asp:CommandField HeaderText="修改"  EditText="修改" ShowEditButton="true">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                </asp:CommandField>      
               <asp:TemplateField ShowHeader="False" HeaderText="删除">
                <ItemTemplate>
                     <asp:ImageButton ID="ImageButtonDel" runat="server" CausesValidation="False"
                         OnClientClick="javascript:return confirm('你确认要删除吗?')" CommandName="Delete" ImageUrl="../img/delete.gif"   />
                </ItemTemplate>
            </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="状态" SortExpression="status">
                <ItemTemplate>
                    <%# Eval("statusName") %>
                </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:TemplateField>  
                
                <asp:BoundField DataField="Code" DataFormatString="{0}" HeaderText="单号" SortExpression="Code">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="userName" DataFormatString="{0}" HeaderText="会员名" SortExpression="userName">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="UsableScore" DataFormatString="{0}" HeaderText="会员当前积分" SortExpression="UsableScore">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="giftName" DataFormatString="{0}" HeaderText="礼品名称" SortExpression="giftName">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="Score" DataFormatString="{0}" HeaderText="所需积分" SortExpression="Score">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                
                <asp:BoundField DataField="csgName" DataFormatString="{0}" HeaderText="收货人" SortExpression="csgName">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="csgMobile" DataFormatString="{0}" HeaderText="手机" SortExpression="csgMobile">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="csgPhone" DataFormatString="{0}" HeaderText="电话" SortExpression="csgPhone">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="csgEmail" DataFormatString="{0}" HeaderText="Email" SortExpression="csgEmail">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="AddTime" DataFormatString="{0}" HeaderText="申请时间" SortExpression="ReTime">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" ></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" ></ItemStyle>
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
            
            <tr><td class="tdLeft">订单编号：</td>
                <td><input class="textBg" title="订单编号" Lock=1 type="text" runat="server" id="KK_Code" style="width:150px" /></td>
                <td class="tdLeft">状态：</td>
                <td>
                    <asp:DropDownList ID="KK_status" title="状态" runat=server></asp:DropDownList>
                </td>
            </tr>
            
            <tr><td class="tdLeft">会员帐号：</td>
                <td><input class="textBg" title="会员帐号" Lock=1 type="text" runat="server" id="KK2_userName" style="width:150px"/></td>
                <td class="tdLeft">会员当前积分：</td>
                <td><input class="textBg" title="会员当前积分" Lock=1 type="text" runat="server" id="KK2_usableScore" style="width:150px"/></td>
            </tr>
            
            <tr><td class="tdLeft">礼品名称：</td>
                <td><input class="textBg" title="礼品名称" Lock=1 type="text" runat="server" id="KK2_giftName" style="width:150px"/></td>
                <td class="tdLeft">所需积分：</td>
                <td><input class="textBg" title="所需积分" Lock=1 type="text" runat="server" id="KK_Score" style="width:150px"/></td>
            </tr>
            
             <tr><td class="tdLeft">收货人：</td>
                <td><input class="textBg" title="收货人" Lock=1 type="text" runat="server" id="KK_csgName" style="width:150px"/></td>
                <td class="tdLeft">Email：</td>
                <td><input class="textBg" title="Email" Lock=1 type="text" runat="server" id="KK_csgEmail" style="width:150px"/></td>
            </tr>
            
            <tr><td class="tdLeft">手机：</td>
                <td><input class="textBg" title="手机" Lock=1 type="text" runat="server" id="KK_csgMobile" style="width:150px"/></td>
                <td class="tdLeft">电话：</td>
                <td><input class="textBg" title="电话" Lock=1 type="text" runat="server" id="KK_csgPhone" style="width:150px"/></td>
            </tr>
            
            <tr><td class="tdLeft">地区：</td>
                <td colspan=3>
                
                    <input class="textBg" title="省份" Lock=1 type="text" runat="server" id="KK_csgProvince" style="width:150px"/>
                -<input class="textBg" title="城市" Lock=1 type="text" runat="server" id="KK_csgCity" style="width:150px"/>
                -<input class="textBg" title="县/区" Lock=1 type="text" runat="server" id="KK_csgCounty" style="width:150px"/>
                </td>
                
            </tr>
            <tr><td class="tdLeft">地址：</td>
                <td colspan=3>
                
                <input class="textBg" title="地址" Lock=1 type="text" runat="server" id="KK_csgAddress" style="width:384px"/>
                -<input class="textBg" title="邮编" Lock=1 type="text" runat="server" id="KK_csgPostCode" style="width:67px"/>
                </td>
                
            </tr>
            
            <tr><td class="tdLeft">客户留言：</td>
                <td colspan=3>
               <textarea id="KK_Message" Lock=1 runat=server style="width: 468px; height: 131px"></textarea> 
                </td>
                
            </tr>            
            
            <tr><td class="tdLeft" style="height: 10px">操　　作：</td>
                <td colspan="3" style="height: 10px">
                    <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="保存新增"
                     Visible="false" OnClick="btnSvAdd_Click"/>
                    <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改"
                     Visible="false" OnClick="btnSvEdit_Click"/>
                    <asp:Button ID="btnSvCancel" runat="server" OnClientClick="window.close();" CssClass="SrvButton" Text="关闭"  />
                    </td>
            </tr>                    
            </table>
        </div>
        
        <div id="divHelp" class="divFoot" style="margin-top:40px; display:none">
            <table class="defaultTable" cellpadding="1px" cellspacing="1px">
            <tr><th colspan="4">功能操作说明</th></tr>
                        
            <tr><td class="lefttd" style="width:150px">［新增记录］</td>
                <td colspan="3">点击［新增记录］打开详细内容界面，输入完相应信息后，点击下方的［保存新增］。</td>
            </tr>
            <tr><td class="lefttd" style="width:150px">［删除选中项］</td>
                <td colspan="3">首先选中列表中的行记录(可多选)，点击［删除选中项］，删除选中的行记录，删除后不可恢复。</td>
            </tr>
            </table>
        </div>
    </form>
            </div>
</body>
</html>