<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uploadFile.ascx.cs" Inherits="usercontrl_uploadFile" %>


<asp:FileUpload ID="fuProPic1" runat="server" Height="22px" Width="304px" />
<asp:Button ID="btnProPic" runat="server" Text="上传" OnClick="btnProPic_Click" />



<asp:Label ID="lblMsg" ForeColor="red" runat="server"></asp:Label>
<br />


<asp:DataList ID="dlProPic" runat="server" RepeatDirection="Horizontal" RepeatColumns="4">
    <ItemTemplate>
        <%# getFile(Container.DataItem.ToString()) %>
        <br />
        <asp:Button ID="delProPic" runat="server" Text="删除" CommandArgument="<%# Container.DataItem.ToString() %>" OnClick="deleteProPic" />

    </ItemTemplate>
</asp:DataList>




<div style="display: none">
    <asp:Label ID="lblProPic" runat="server"></asp:Label></div>
