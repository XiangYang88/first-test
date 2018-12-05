var isIE = navigator.userAgent.toLowerCase().indexOf("msie") != -1;
var isIE8 = !!window.XDomainRequest && !!document.documentMode;
var isIE7 = navigator.userAgent.toLowerCase().indexOf("msie 7.0") != -1 && !isIE8;
var isIE6 = navigator.userAgent.toLowerCase().indexOf("msie 6.0") != -1;
var isGecko = navigator.userAgent.toLowerCase().indexOf("gecko") != -1;
var isQuirks = document.compatMode == "BackCompat";
var isBorderBox = isIE && isQuirks;

var iframeSet = {};

iframeSet.initFrameHeight = function(id) {
try{
    var jqobj = $("#" + id)
    var f = jqobj.get(0); //document.getElementById(id);
    var b = document.body;
    var ch = document.compatMode == "BackCompat" ? document.body.clientHeight : document.documentElement.clientHeight;
    //var pos = f.getPosition();
    f.scrolling = "no";
    var height1 = ch - iframeSet.getPosition(f).y;
    f.height = height1;
    var p = f.offsetParent;
    while (p.offsetParent != document.body) {
        p = p.offsetParent;
    }
    var fDim = iframeSet.getDimensions(f);
    var fPos = iframeSet.getPosition(f);
    var pDim = iframeSet.getDimensions(p);
    var pPos = iframeSet.getPosition(p);


    var d = pDim.height + pPos.y - fDim.height - fPos.y;
    var currHeight = (f.height - d - (isIE ? 1 : 0)); //IE8需要-1
    if (currHeight < 300) {
        currHeight = 300;
    }
    
    f.style.height = currHeight + "px";
    }
    catch(ex)
    {
    
    }
}

//获取尺寸规格
iframeSet.getDimensions = function(ele) {
    ele = ele || this;
    //ele = $(ele);
    var dim;
    if (ele.tagName.toLowerCase() == "script") {
        dim = { width: 0, height: 0 };
    } else if (iframeSet.visible(ele)) {
        if (isIE && ele.offsetWidth == 0 && ele.offsetHeight == 0) {
            if (isBorderBox) {
                dim = { width: ele.currentStyle.pixelWidth, height: ele.currentStyle.pixelHeight };
            } else {
                dim = { width: +ele.currentStyle.pixelWidth + ele.currentStyle.borderLeftWidth.replace(/\D/g, '') + ele.currentStyle.borderRightWidth.replace(/\D/g, '') + ele.currentStyle.paddingLeft.replace(/\D/g, '') + ele.currentStyle.paddingRight.replace(/\D/g, ''),
                    height: +ele.currentStyle.pixelHeight + ele.currentStyle.borderTopWidth.replace(/\D/g, '') + ele.currentStyle.borderBottomWidth.replace(/\D/g, '') + ele.currentStyle.paddingTop.replace(/\D/g, '') + ele.currentStyle.paddingBottom.replace(/\D/g, '')
                };
            }
        } else {
            dim = { width: ele.offsetWidth, height: ele.offsetHeight };
        }
    } else {
        var style = ele.style;
        var vis = style.visibility;
        var pos = style.position;
        var dis = style.display;
        style.visibility = 'hidden';
        style.position = 'absolute';
        style.display = 'block';
        var w = ele.offsetWidth;
        var h = ele.offsetHeight;
        style.display = dis;
        style.position = pos;
        style.visibility = vis;
        dim = { width: w, height: h };
    }
    return dim;
}

//获取坐标
iframeSet.getPosition = function(ele) {
    ele = ele || this;
    //ele = $(ele);
    var doc = ele.ownerDocument;
    if (ele.parentNode === null || ele.style.display == 'none') {
        return false;
    }
    var parent = null;
    var pos = [];
    var box;
    if (ele.getBoundingClientRect) {//IE,FF3,己很精确，但还没有非常确定无误的定位
        box = ele.getBoundingClientRect();
        var scrollTop = Math.max(doc.documentElement.scrollTop, doc.body.scrollTop);
        var scrollLeft = Math.max(doc.documentElement.scrollLeft, doc.body.scrollLeft);
        var X = box.left + scrollLeft - doc.documentElement.clientLeft;
        var Y = box.top + scrollTop - doc.documentElement.clientTop;
        if (isIE) {
            X--;
            Y--;
        }
        return { x: X, y: Y };
    } else if (doc.getBoxObjectFor) { // FF2
        box = doc.getBoxObjectFor(ele);
        var borderLeft = (ele.style.borderLeftWidth) ? parseInt(ele.style.borderLeftWidth) : 0;
        var borderTop = (ele.style.borderTopWidth) ? parseInt(ele.style.borderTopWidth) : 0;
        pos = [box.x - borderLeft, box.y - borderTop];
    }
    if (ele.parentNode) {
        parent = ele.parentNode;
    } else {
        parent = null;
    }
    while (parent && parent.tagName != 'BODY' && parent.tagName != 'HTML') {
        pos[0] -= parent.scrollLeft;
        pos[1] -= parent.scrollTop;
        if (parent.parentNode) {
            parent = parent.parentNode;
        } else {
            parent = null;
        }
    }
    return { x: pos[0], y: pos[1] }
}

//设置iframe的高度和导航的宽度
iframeSet.layoutAdjust = function() {
    //        if (document.body.clientWidth < 900) {
    //            if ($("_VMenutable").innerHTML.length > 40) {
    //                $("_HMenutable").innerHTML = $("_VMenutable").innerHTML;
    //                $("_VMenutable").innerHTML = "<div></div>";
    //            }
    //            iframeSet.setDivtabWidth($("_ChildMenu")); //方法定义在Tabpage.js 用于调整标签按钮宽度
    //        } else {
    //            if ($("_HMenutable").innerHTML.length > 0) {
    //                $("_VMenutable").innerHTML = $("_HMenutable").innerHTML;
    //                $("_HMenutable").innerHTML = "";
    //            }

    //        }
    //alert(document.body.clientWidth);
    if (document.body.clientWidth < 900) {
        $("#setDiv").width(900);
    }
    else {
        $("#setDiv").width(document.body.clientWidth);
    }
    iframeSet.initFrameHeight("_MainArea");
}

//设置iframe的高度和导航的宽度
iframeSet.layoutAdjust2 = function() {
    iframeSet.initFrameHeight("_SubArea");
}


//是否隐藏
iframeSet.visible = function(ele) {
    ele = ele || this;
    //ele = $(ele);
    if (ele.style.display == "none") {
        return false;
    }
    return true;
}


//当窗口大小改变时重置iframe
if (document.attachEvent) {
    window.attachEvent('onresize', iframeSet.layoutAdjust);
} else {
    window.addEventListener('resize', iframeSet.layoutAdjust, false);
}
