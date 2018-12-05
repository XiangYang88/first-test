<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uploadIframe.aspx.cs" Inherits="admin_BsPg_uploadIframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>无标题页</title>
    
    <script>
    var filelist="<%=this.upFile.FileList %>";
    var id="<%=id %>";
        window.onload=function()
        {   
      //  debugger;
            if(id!="")
            {
                window.parent.document.getElementById(id).value=filelist;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <uc:upload ID="upFile" runat="server" />
    </div>
    </form>
</body>
</html>
