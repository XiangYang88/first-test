<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGeneralMCN.ascx.cs" Inherits="uc_ucGeneralMCN" %>
<meta content="width=device-width,minimum-scale=1,user-scalable=no,maximum-scale=1,initial-scale=1" name="viewport" />
<meta name="format-detection" content="telephone=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="white" />
<meta name="keywords" content="" />
<meta name="description" content="" />
<%if (BLL.Sys.Config.getConfigVal("mobiletheme") == "")
  { %>
<link href="css/Blue.css" type="text/css" rel="stylesheet" />
<%}
  else
  { %>
<link href="css/<%=BLL.Sys.Config.getConfigVal("mobiletheme")%>.css" type="text/css" rel="stylesheet" />
<%} %>
<link href="css/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>
<script src="js/layer/layer.js"></script>
<script type="text/javascript">
    /*图片垂直居中显示*/
    function reset_pic(obj, size) {
        size = size.split(',');
        var dW = size[0];
        var dH = size[1];
        var img = new Image();
        img.src = obj.src;
        if (img.width / img.height >= dW / dH) {
            if (img.width > dW) {
                obj.width = dW;
                obj.height = img.height * dW / img.width;
            } else {
                obj.width = img.width;
                obj.height = img.height > dH ? img.height * img.width / dW : img.height;
            }
        } else {
            if (img.height > dH) {
                obj.height = dH;
                obj.width = img.width * dH / img.height;
            } else {
                obj.height = img.height;
                obj.width = img.width > dW ? img.height * img.width / dH : img.width;
            }
        }
        obj.style.marginTop = (dH - obj.height) / 2 + 'px';
    }
</script>
