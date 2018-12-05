<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BsOrdersItem.aspx.cs" Inherits="admin_BsPg_BsOrdersItem" %>
<!--数据操作的页面模板-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>订单明细</title>
     <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../script/jQueryPlugin.js"></script>
    <script type="text/javascript" src="../script/JavaScript.js"></script>
</head>
<body  >
    <div class="column_Box mainAutoHeight">
    <form id="frmList" runat="server">
         <div id="divHead" class="divHead">
          
            <div style="clear:both;font-size:1px; height:1px;"></div>
        </div>
        
        <!--Foot-->
        <div id="divDtls" class="divFoot body" runat="server">
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
                <td><input class="textBg" title="订单编号" Lock=1 type="text" runat="server" id="KK_Code" style="width:150px" readonly="readOnly" /></td>
                <td class="tdLeft">状态：</td>
                <td>
                    <asp:DropDownList ID="KK_status" title="状态" runat=server></asp:DropDownList>
                </td>
            </tr>
            <tr><td class="tdLeft">产品数量：</td>
                <td><input class="textBg" title="产品数量" Lock=1 type="text" runat="server" id="KK_quantity" style="width:150px" readonly="readOnly"/></td>
                <td class="tdLeft">订单总金额：</td>
                <td><input class="textBg" title="产品金额" Lock=1 type="text" runat="server" id="KK_Amount" style="width:150px" readonly="readOnly"/></td>
            </tr>
            
            <tr><td class="tdLeft">运费金额：</td>
                <td colspan="3"><input class="textBg" title="运费金额" Lock=1 type="text" runat="server" id="KK_deliverFee" style="width:150px" readonly="readOnly"/></td>
               <%-- <td class="tdLeft">产品折扣：</td>
                <td><input class="textBg" title="产品折扣" type="text" runat="server" id="KK_discount" style="width:150px" readonly="readOnly"/></td>--%>
            </tr>
            
           
           
            <tr><td class="tdLeft">支付方式：</td>
                <td>
                    
                    <asp:DropDownList ID="KK_Sy_PayCode" title="支付方式" runat=server></asp:DropDownList>
                
                </td>
                <td class="tdLeft">支付流水号：</td>
                <td><input class="textBg" type="text" title="支付流水号" runat="server" id="KK_payNo" style="width:150px" /></td>
            </tr> 
            
            <tr><td class="tdLeft">配送方式：</td>
                <td>
                
                    <asp:DropDownList ID="KK_Sy_DeliverCode" title="配送方式" runat=server></asp:DropDownList>
                
                </td>
                <td class="tdLeft">物流号：</td>
                <td><input class="textBg" type="text" title="物流号" runat="server" id="KK_DeliverNo" style="width:150px" /></td>
            </tr> 
          
             <tr><td class="tdLeft">收货人：</td>
                <td><input class="textBg" title="收货人" Lock=1 type="text" runat="server" id="KK_csgName" style="width:150px" /></td>
                <td class="tdLeft">电话：</td>
                <td><input class="textBg" title="电话" Lock=1 type="text" runat="server" id="KK_csgPhone" style="width:150px"/></td> 
            </tr>
            <tr>
                 <td class="tdLeft">邮箱：</td>
                <td><input class="textBg" title="邮箱" type="text"  Lock=1 runat="server" id="KK_csgEmail" style="width:150px"/></td>
                     <td class="tdLeft">手机：</td>
                <td><input class="textBg" title="手机" type="text"  Lock=1 runat="server" id="KK_csgMobile" style="width:150px"/></td> 

            </tr>
            
            
           
            <tr><td class="tdLeft">地址：</td>
                <td colspan=3>
                
                <input class="textBg" title="地址" Lock=1 type="text" runat="server" id="KK_csgAddress" style="width:584px" >
                <input class="textBg" title="邮编" Lock=1 type="text" runat="server" id="KK_csgPostCode" style="width:67px; display:none" />
                </td>
                
            </tr>
            
            <tr><td class="tdLeft">备注：</td>
                <td colspan=3>
               <textarea id="KK_Notes" runat=server Lock=1 style="width: 468px; height: 47px" readonly="readOnly"></textarea> 
                </td>
                
            </tr>
            
            <tr><td class="tdLeft">处理备注：</td>
                <td colspan=3>
                <asp:GridView id="gvMsgList" runat="server"  CssClass="defaultGridV" Width="100%"
                   DataKeyNames="id" CellPadding="3" PageSize="10" AutoGenerateColumns="False" AllowSorting="true"
                    OnRowDeleting="gvMsgList_RowDeleting"  OnPreRender="gvMsgList_PreRender">
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                <FooterStyle HorizontalAlign="center" VerticalAlign="Middle"></FooterStyle>
                <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                <AlternatingRowStyle BackColor="#E6F5FA"></AlternatingRowStyle>
                <Columns>
                <asp:CommandField HeaderText="删除" DeleteText="删除" ShowDeleteButton="True">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                </asp:CommandField>
                <asp:BoundField DataField="adduserName" DataFormatString="{0}" HeaderText="添加者">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="notes" DataFormatString="{0}" HeaderText="内容">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="addtime" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="添加日期">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>   
               
                </Columns>
                </asp:GridView>
                    <textarea id="KK3_Notes" runat=server style="width: 466px; height: 47px"></textarea> 
                    <br />
                    <asp:Button ID="btnReply" runat="server" CssClass="SrvButton" Text="提交处理备注" OnClick="btnReply_Click" />
                </td>
            </tr>
            
            <tr><th colspan="4" style="text-align:left">购买的商品列表</th></tr>
            <tr><td colspan="4" style="height: 20px">
               
                <div id="divProAdd" runat=server>
                    <table cellpadding="2" cellspacing="0" border="0" >
                        <tr>
                            <td>
                                商品编号：</td>
                            <td colspan=3><input class="textBg" type="text" runat="server" id="KK2_Bs_ProductsCode"/>
                                单价：
                                <input class="textBg" type="text" runat="server" id="KK2_Price" style="width:50px" value="0"/>
                                数量：
                                <input class="textBg" type="text" runat="server" id="KK2_Quantity" style="width:50px" value=""/>
                                颜色：<input class="textBg" type="text" runat="server" id="KK2_Color" style="width:50px" value=""/>
                                尺寸：<input class="textBg" type="text" runat="server" id="KK2_Dimension" style="width:50px" value=""/>
                                </td>
                            </tr>
                            <tr>
                            <td>
                                备注：</td>
                            <td >
                            <textarea class="textCSS" runat="server" id="KK2_Notes"></textarea></td>
                            <td style="width: 142px" ><asp:Button ID="btnAddPro" runat="server" CssClass="SrvButton" Text="添加到商品列表" Visible="true"
                             OnClick="btnAddPro_Click"/></td>
                             
                        </tr>
                    </table>
                </div>    
                <asp:GridView id="gvProList" runat="server"  CssClass="defaultGridV" Width="100%"
                   DataKeyNames="id" CellPadding="3" PageSize="100" AutoGenerateColumns="False" AllowSorting="true"
                   OnRowDataBound="gvProList_RowDataBound" OnRowDeleting="gvProList_RowDeleting" OnPreRender="gvProList_PreRender">
                
                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                <HeaderStyle HorizontalAlign="center" VerticalAlign="Middle"></HeaderStyle>
                <FooterStyle HorizontalAlign="center" VerticalAlign="Middle"></FooterStyle>
                <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                <AlternatingRowStyle BackColor="#E6F5FA"></AlternatingRowStyle>
            
            
                <Columns>
                
                <asp:CommandField HeaderText="删除"  DeleteText="删除" ShowDeleteButton="True" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                </asp:CommandField>
                
                <asp:BoundField HeaderText="序号" >
                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="36px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>                
             
                <asp:TemplateField HeaderText="图片">
                <ItemTemplate>
                    <img src='<%# CSA.HC.Common.getAppPath()+"/upload/"+Eval("pic").ToString().Split(',')[0] %>' width="50" height="50" />
                </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:TemplateField>
                
                
                <asp:BoundField DataField="proCode" DataFormatString="{0}" HeaderText="产品型号">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="产品名称">
                    <ItemTemplate><%# Eval("proName") %></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:TemplateField>
                
                     <asp:TemplateField HeaderText="单计">
                    <ItemTemplate><%# (Convert.ToInt32(Eval("quantity"))*Convert.ToDecimal(Eval("price"))).ToString("f2") %></ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:TemplateField>

              
                <asp:BoundField DataField="quantity" DataFormatString="{0:N0}"  HeaderText="购买数量" >
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
              
                
                
            
                
               
                </Columns>
                </asp:GridView>
              
                <div style="height:20px; line-height:20px"></div>
                   </td>
            </tr>
            <tr>
                <td  style="text-align:right" colspan="4" >&nbsp;&nbsp;订单总额：<asp:Label runat="server" ID="KK_Amount1" ></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数量总计:<asp:Label runat="server" ID="quantity1"></asp:Label>
            </td>
            </tr>
            <tr><td class="tdLeft" style="height: 10px">操　　作：</td>
                <td colspan="3" style="height: 10px">
                    <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="保存新增"
                     Visible="false" OnClick="btnSvAdd_Click"/>
                    <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改"
                     Visible="false" OnClick="btnSvEdit_Click"/>
                    <asp:Button ID="btnSvPrintOrder" runat="server" CssClass="SrvButton"  Text="打印订单" Visible="false" OnClientClick="window.print()" />
                    <asp:Button ID="btnSvPrintPro" runat="server" CssClass="SrvButton" Text="打印产品"  Visible="false" />
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
    </form> </div>
</body>
</html>