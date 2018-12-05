<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DbMaintenanceSqlserver.aspx.cs" Inherits="admin_sys_DbMaintenanceSqlserver" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>数据库备份管理</title>
 <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
</head>
<body  >
      <div class="column_Box mainAutoHeight">
    <form id="frmList" runat="server">

               <div id="divHead" class="divHead" runat=server>
     
          <div style="float: left; width: auto; border: 0px red solid;">
               <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_xingjian"></span><i>生成备份文件</i></asp:LinkButton>
                    <asp:LinkButton ID="btnDel" OnClick="btnDel_Click" OnClientClick="return confirm('你确定删除所有选中的记录吗？')" CssClass="dot_Item" runat="server"><span class="Icon_item icon_shanchu"></span><i>删除选中项</i></asp:LinkButton>
                <span style="text-align:center"><asp:ImageButton ID="imgbtnhelp" ImageUrl="../img/help.gif" runat="server" ToolTip="显示隐藏帮助信息"  OnClientClick="return showHelp()" /></span>
                
            </div>
            
             <div style="clear:both;font-size:1px; height:1px;"></div>
        </div>     
      
                <!--Content-->
        <div id="divContent" class="body" runat=server>
            <div id="divEmpty" runat="server" visible="false" class="divNotDataInfo gray_12_normal"><label>暂无相关记录显示</label></div>
            <div id="divList" runat="server">
                <asp:GridView id="gvList" runat="server"  CssClass="defaultGridV" Width="100%"
                    CellPadding="3"   AutoGenerateColumns="False" DataKeyNames="filename"
                   OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnPreRender="gvList_PreRender">
                   
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
                      
                <asp:CommandField HeaderText="还原" SelectText="还原" ShowSelectButton="True" >
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                </asp:CommandField>
                
                  <asp:TemplateField HeaderText="备份文件">
                    <HeaderStyle HorizontalAlign="left" BorderWidth="1px"  BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                    <ItemTemplate>
                            <%# Eval("filename")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 
            </Columns>
            </asp:GridView> 
            <!--分页-->
           
           </div>
        </div>
        
        <!--Foot-->        
        <div id="divHelp" class="divFoot" style="display:none;">
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
