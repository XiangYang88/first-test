<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFooterCN.ascx.cs" Inherits="uc_ucFooterCN" %>

<div id="footer">
        <p><%=BLL.Sys.Config.getConfigVal("CopyRight") %> <a class="beian" href="http://www.miitbeian.gov.cn/" style="display: inline; width: auto; color: #8e8e8e" target="_blank"></a></p>
    </div>
    <div id="shares">
        <a id="sshare"><i class="fa fa-share-alt"></i></a>
        <a href="http://service.weibo.com/share/share.php?appkey=3206975293&" target="_blank" id="sweibo">
            <i class="fa fa-weibo"></i></a>
        <a href="javascript:;" id="sweixin"><i class="fa fa-weixin"></i></a>
        <a href="javascript:;" id="gotop"><i class="fa fa-angle-up"></i></a></div>
    <div class="fixed" id="fixed_weixin">
        <div class="fixed-container">
            <div id="qrcode"></div>
            <p>扫描二维码分享到微信</p>
        </div>
    </div>
    <div class="hide">
        <script src="static/js/stat.js" type="text/javascript"></script>
        <script src="static/js/copyright04.js" type="text/javascript"></script>
    </div>