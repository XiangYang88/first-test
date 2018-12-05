<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyOu.aspx.cs" Inherits="sys_SyOu" %>
<!--���ݲ�����ҳ��ģ��-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>��λ����</title>
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
                 style="color:#15428B;padding-right:0px;padding-left:10px">��ѯ������</label></span>
                <span>
                
                    <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Field0">
                        <asp:ListItem Text="��ѡ��" Value=""></asp:ListItem>
                        <asp:ListItem Text="���" Value="Code^String"></asp:ListItem>
                        <asp:ListItem Text="����" Value="Name^String"></asp:ListItem>
                    </asp:DropDownList>
                    
                    <asp:DropDownList CssClass="dropList" runat="server" ID="Search_Compare0">
                        <asp:ListItem Text="����" Value="like"></asp:ListItem>
                        <asp:ListItem Text="������" Value="not like"></asp:ListItem>                       
                        <asp:ListItem Text="����" Value="="></asp:ListItem>
                        <asp:ListItem Text="������" Value="!="></asp:ListItem>
                    </asp:DropDownList>
                    
                    <input Search_FieldID="Search_Field0" Search_CompareID="Search_Compare0"
                     class="textSearch" type="text" runat="server" id="Search_Keyword0" />
                    
                    ÿҳ��ʾ<input type="text" value="10" runat="server" id="txtPageSize" style="width:30px" />�� 
                     
                     </span>
                    <span>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SrvButton"
                     Text="��ѯ" OnClick="btnSearch_Click" />
                    </span>
                     
            </div>
            
           <div style="float:left; width:auto; border:0px red solid;">
                 <asp:LinkButton ID="btnAdd" OnClick="btnAdd_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_xingjian"></span><i>�½�</i></asp:LinkButton>
                    <asp:LinkButton ID="btnDel" OnClick="btnDel_Click" OnClientClick="return confirm('��ȷ��ɾ������ѡ�еļ�¼��')" CssClass="dot_Item" runat="server"><span class="Icon_item icon_shanchu"></span><i>ɾ��ѡ����</i></asp:LinkButton>
                 <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" CssClass="dot_Item" runat="server"><span class="Icon_item icon_export"></span><i>����Excel</i></asp:LinkButton>
                   <asp:LinkButton ID="btnImport" OnClick="btnImport_Click" Visible="false" CssClass="dot_Item" runat="server"><span class="Icon_item icon_import"></span><i>����Excel</i></asp:LinkButton>
               <a onclick="window.location=window.location.href" href="javascript:;" class="dot_Item"><span class="Icon_item icon_zhuanyi" ></span><i>ˢ��</i></a>
                <a onclick="history.go(-1)" href="javascript:;" class="dot_Item"><span class="Icon_item icon_fuzhi" ></span><i>����</i></a>
                <span style="text-align:center"><asp:ImageButton ID="imgbtnhelp" ImageUrl="../img/help.gif" runat="server" ToolTip="��ʾ���ذ�����Ϣ"  OnClientClick="return showHelp()" /></span>
                
            </div>
            
             <div style="clear:both;font-size:1px; height:1px;"></div>
        </div>
        
        <div id="divImport" runat="server" visible="false">
             <div class="tab">
                <ul>
                    <li class="current"><a href="javascript:">����Excel</a></li>
                </ul>
            </div>
            <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
            <colgroup>
                    <col width="200px" />
                    <col />
                </colgroup>
            <tr><td class="tdLeft">�ϴ�Excel�ļ���</td>
                <td><asp:FileUpload ID="fuImportFile" runat=server /></td>
            </tr>
            <tr><td class="tdLeft">������ʾ��</td>
                <td><asp:Label ID="lblImportMsg" ForeColor=red runat=server></asp:Label></td>
            </tr>
            <tr><td class="tdLeft">�١�������</td>
                <td >
                    <asp:Button ID="btnSvImportSave" runat="server" CssClass="SrvButton" Text="ִ�е���" Visible="true" OnClick="btnSvImportSave_Click"  />&nbsp;
                    <asp:Button ID="btnSvImportCancel" runat="server" CssClass="SrvButton" Text="ȡ������" OnClick="btnSvImportCancel_Click" /></td>
            </tr>   
            </table>
        </div>
        
        <div id="divExport" runat="server" visible="false">
              <div class="tab">
                <ul>
                    <li class="current"><a href="javascript:">����Excel</a></li>
                </ul>
            </div>
            <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
             <colgroup>
                    <col width="200px" />
                    <col />
                </colgroup>
            <tr><td class="tdLeft">ѡ�񵼳����ݣ�</td>
                <td>
                    <asp:CheckBoxList runat=server RepeatColumns=8 ID="cblExportField">
                        <asp:ListItem Value="Code" Selected=true>���</asp:ListItem>
                        <asp:ListItem Value="Name^{0}" Selected=true>����</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr><td class="tdLeft">�������ͣ�</td>
                <td><asp:RadioButtonList runat=server RepeatDirection=horizontal ID="rbtnlExportType">
                    <asp:ListItem Value=0>����ѡ����</asp:ListItem>
                    <asp:ListItem Value=1 Selected=true>������ǰҳ</asp:ListItem>
                    <asp:ListItem Value=2>��������ҳ</asp:ListItem>
                </asp:RadioButtonList></td>
            </tr>
            <tr><td class="tdLeft">������ʾ��</td>
                <td><asp:Label ID="lblExportMsg" ForeColor=red runat=server></asp:Label></td>
            </tr>
            <tr><td class="tdLeft">�١�������</td>
                <td >
                    <asp:Button ID="btnSvExportSave" runat="server" CssClass="SrvButton" Text="ִ�е���" Visible="true" OnClick="btnSvExportSave_Click" />&nbsp;
                    <asp:Button ID="btnSvExportCancel" runat="server" CssClass="SrvButton" Text="ȡ������" OnClick="btnSvExportCancel_Click" /></td>
            </tr>
                    
            </table>
        </div>
        <div id="divDtls" class="divDtls"  runat="server" visible="false" >
               <div class="tab">
                <ul>
                    <li class="current"><a href="javascript:">��ϸ����</a></li>
                </ul>
            </div>
            <input type="hidden" runat="server" id="PKID" />               
                <table class="defaultTbl">
              <colgroup>
                    <col width="200px" />
                    <col />
                     <col width="200px" />
                    <col />
                </colgroup>
                <tr><td class="tdLeft">��λ��ţ�</td>
                    <td><input class="textBg" type="text" title="��λ���" runat="server" id="KK_code" style="width:150px"/></td>
                </tr>
                <tr><td class="tdLeft">��λ���ƣ�</td>
                    <td><input class="textBg" type="text" title="��λ����" runat="server" id="KK_name" style="width:150px"/></td>
                </tr>  
                
                <tr><td class="tdLeft">��ϵ�ˣ�</td>
                    <td><input class="textBg" type="text" title="��ϵ��" runat="server" id="KK_contact" style="width:150px"/></td>
                </tr>
                
                 <tr><td class="tdLeft">��ϵ�˵绰��</td>
                    <td><input class="textBg" type="text" title="��ϵ�˵绰" runat="server" id="KK_cellphone" style="width:150px"/></td>
                </tr>
                <tr><td class="tdLeft">Email��</td>
                    <td><input class="textBg" type="text" title="Email" runat="server" id="KK_mail" style="width:150px"/></td>
                </tr>
                 <tr><td class="tdLeft">��ע��</td>
                    <td><input class="textBg" type="text" title="��ע" runat="server" id="KK_notes" style="width:150px"/></td>
                </tr>
                <tr><td class="tdLeft">�١�������</td>
                    <td>
                        <asp:Button ID="btnSvAdd" runat="server" CssClass="SrvButton" Text="��������" OnClick="btnSvAdd_Click"/>&nbsp;
                        <asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="�����޸�"  OnClick="btnSvEdit_Click"/>&nbsp;
                        <asp:Button ID="btnSvCancel" runat="server" CssClass="SrvButton" Text="�ء�����"  OnClick="btnSvCancel_Click"/></td>
                </tr>
                </table>            
     </div>
                <!--Content-->
        <div id="divContent" class="body" runat=server>
            <div id="divEmpty" runat="server" visible="false" class="divNotDataInfo gray_12_normal"><label>������ؼ�¼��ʾ</label></div>
            <div id="divList" runat="server">
                <asp:GridView id="gvList" runat="server"  CssClass="defaultGridV" Width="100%"
                    CellPadding="3" AllowPaging="True"
                    AutoGenerateColumns="False" AllowSorting="true"
                    OnSorting="gvList_Sorting"
                   OnRowDataBound="gvList_RowDataBound"
                     OnSelectedIndexChanged="gvList_SelectedIndexChanged"
                      OnRowEditing="gvList_RowEditing"
                   OnRowDeleting="gvList_RowDeleting" OnPreRender="gvList_PreRender">
                   
                <PagerSettings FirstPageText="��ҳ" LastPageText="ĩҳ" Mode="NumericFirstLast" NextPageText="��һҳ" PreviousPageText="��һҳ" Visible="False"></PagerSettings>
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

                <asp:BoundField HeaderText="���" ReadOnly="True">
                <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="36px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                <asp:CommandField HeaderText="�鿴" SelectText="�鿴" ShowSelectButton="True">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                </asp:CommandField>
                
                <asp:CommandField HeaderText="�޸�" ButtonType="Image" EditImageUrl="../img/Edit.gif" EditText="�޸�" ShowEditButton="true">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" Width="35px"></ItemStyle>
                </asp:CommandField>      
                <asp:TemplateField ShowHeader="False" HeaderText="ɾ��">
                <ItemTemplate>
                     <asp:ImageButton ID="ImageButtonDel" runat="server" CausesValidation="False"
                         OnClientClick="javascript:return confirm('��ȷ��Ҫɾ����?')" CommandName="Delete" ImageUrl="../img/delete.gif"   />
                </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="Code" SortExpression="Code" DataFormatString="{0}" HeaderText="���">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="Name" SortExpression="Name" DataFormatString="{0}" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>               
                
                <asp:BoundField DataField="contact" SortExpression="contact" DataFormatString="{0}" HeaderText="��ϵ��">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="cellphone" SortExpression="cellphone" DataFormatString="{0}" HeaderText="��ϵ�绰">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="mail" SortExpression="mail" DataFormatString="{0}" HeaderText="Email">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="notes" SortExpression="notes" DataFormatString="{0}" HeaderText="��ע">
                    <HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                </asp:BoundField>
            </Columns>
            </asp:GridView> 
            <!--��ҳ-->
            <div style="padding-top: 10px; width: 100%">
                <webdiyer:AspNetPager ID="ANPage1" runat="server" HorizontalAlign="Center" ShowCustomInfoSection="Left"
                    NumericButtonTextFormatString="[{0}]" OnPageChanged="ANPage1_PageChanged" FirstPageText="��ҳ"
                    LastPageText="βҳ" PrevPageText="��һҳ" NextPageText="��һҳ" Height="18px" ShowPageIndexBox="always"
                     ShowPageIndex="true"  CustomInfoStyle="padding-top:5px" 
                    Width="100%" NumericButtonCount="10">
                </webdiyer:AspNetPager>
            </div>
           </div>
        </div>
        
        <!--Foot-->        
        <div id="divHelp" class="divFoot" style="display:none;">
            <table class="defaultTable" cellpadding="1px" cellspacing="1px">
            <tr><th colspan="4">���ܲ���˵��</th></tr>
                        
            <tr><td class="lefttd" style="width:150px">��������¼��</td>
                <td colspan="3">�����������¼�ݴ���ϸ���ݽ��棬��������Ӧ��Ϣ�󣬵���·��ģ۱��������ݡ�</td>
            </tr>
            <tr><td class="lefttd" style="width:150px">��ɾ��ѡ�����</td>
                <td colspan="3">����ѡ���б��е��м�¼(�ɶ�ѡ)�������ɾ��ѡ����ݣ�ɾ��ѡ�е��м�¼��ɾ���󲻿ɻָ���</td>
            </tr>
            </table>
        </div>
    </form>
        </div>
</body>
</html>
