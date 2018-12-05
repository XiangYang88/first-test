<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGeneralEN.ascx.cs" Inherits="uc_ucGeneralEN" %>
<%if (BLL.Sys.Config.getConfigVal("pctheme") == "")
  { %>
<link href="css/Blue.css" type="text/css" rel="stylesheet" />
<%}
  else
  { %>
<link href="css/<%=BLL.Sys.Config.getConfigVal("pctheme")%>.css" type="text/css" rel="stylesheet" />
<%} %>
<script src="js/jquery.min.js"></script>
<div id="web" class="g_web "></div>
<div id="containerPlaceholder" class="containerPlaceholder"></div>
<div class="floatLeftTop">
    <div id="floatLeftTopForms" class="forms sideForms floatForms"></div>
</div>
<div class="floatRightTop">
    <div id="floatRightTopForms" class="forms sideForms floatForms"></div>
</div>
<div class="floatLeftBottom">
    <div id="floatLeftBottomForms" class="forms sideForms floatForms"></div>
</div>
<div class="floatRightBottom">
    <div id="floatRightBottomForms" class="forms sideForms floatForms"></div>
</div>
<link href="/showwap/css/wapcss.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="/showwap/js/jquery-core.min.js"></script>
<script type="text/javascript" src="/showwap/js/fai.min.js"></script>
<script type="text/javascript" src="/showwap/js/jquery-ui-core.min.js"></script>
<script type="text/javascript" src="/showwap/js/site.min.js"></script>
<script type="text/javascript">
    var _colOtherStyleData = {
        "independentList": [],
        "y": 0,
        "h": 0,
        "layout4Width": 0,
        "layout5Width": 0
    };
    $(function () {
        Site.initPage();
    });
    var _mobiSiteDomain = '/m/en/';
    var _siteDemo = true;
</script>

<script src="js/jquery.SuperSlide.2.1.1.js"></script>
<script src="js/layer/layer.js"></script>
<script src="js/js.js"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script>
        DD_belatedPNG.fix('*');
    </script>
    <![endif]-->
<%if (BLL.Sys.Config.getConfigVal("ico") != "")
  { %>
<link rel="shortcut icon" href="/upload/<%=BLL.Sys.Config.getConfigVal("ico")%>" />
<%} %>