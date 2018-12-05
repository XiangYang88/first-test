<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyUserItem.aspx.cs" Inherits="admin_sys_BsUserItem" %>

<!--数据操作的页面模板-->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>管理员设置</title>
     <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
     <script type="text/javascript">
         jQuery(document).ready(function () {
             hideColumn();
         });
         //隐藏左侧栏
         var hideColumn = function () {
             //var txt = $(window.parent.document).find("div"); 获取iframe上级文档的div
             var Container_Left = jQuery(window.parent.document).find(".Container_Left");
             var ContainerMain = jQuery(window.parent.document).find(".ContainerMain");
             Container_Left.hide();
             ContainerMain.css({ marginLeft: 10 });
         };
    </script>
</head>
<body>
     <div class="column_Box mainAutoHeight">
    <form id="frmList" runat="server">  
  
    <div id="divDtls" class="divFoot" runat="server" >
        <div class="tab">
                <ul>
                    <li class="current"><a href="javascript:">详细内容</a></li>
                </ul>
            </div>
            <input type="text" runat="server" id="PKID" visible="false"/>

            <table class="defaultTbl" cellpadding="1px" cellspacing="1px">
         
            <tr><th>用　　户：</th>
                <td><input class="textBg" title="用　　户" type="text" runat="server" id="KK_code" style="width:150px"/></td>
               <th style="color:Red">设置密码：</th>
                <td ><input class="textBg" title="设置密码" type="text" runat="server" id="Password" style="width:150px"/><font color="gray">填写则修改密码</font></td>         
            </tr>
            <tr>
            <th>名　　称：</th>
                <td ><input class="textBg" title="名　　称" type="text" runat="server" id="KK_name" style="width:150px"/></td>
                  <th>电子邮箱：</th>
                <td><input class="textBg" title="电子邮箱" type="text" runat="server" id="KK_Email" style="width:150px"/></td>
            </tr>
            
            <tr><th>联系电话：</th>
                <td colspan="3"><input class="textBg" title="联系电话" type="text" runat="server" id="KK_Phone" style="width:150px"/></td>
               
            </tr>
            <tr><th>备注说明：</th>
                <td colspan="3"><textarea title="备注说明" class="textCSS" runat="server" id="KK_Notes" style="height:50px;width:250px"></textarea></td>
            </tr>
                   
            <tr><th>操　　作：</th>
                <td colspan="3"><asp:Button ID="btnSvEdit" runat="server" CssClass="SrvButton" Text="保存修改" Visible="true" OnClick="btnSvEdit_Click"/></td>
            </tr>
                    
            </table>
        </div>
    </form>
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
        </div>
   
</body>
</html>
