$(function () {
    $("#btnSumbit").click(function () {
        var currentVal = $("#KK_name").val();
        if (currentVal == "") {
            layer.msg("请输入您的姓名！", 1);
            $("#KK_name").focus();
            return false;
        }
        if (currentVal.length > 28) {
            layer.msg("请输入正确的姓名！", 1);
            $("#KK_name").focus();
            return false;
        }
        var currentVal = $("#KK_Email").val();
        if (currentVal == "") {
            layer.msg("请输入您的邮箱！", 1);
            $("#KK_Email").focus();
            return false;
        }
        var reg = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;
        if (!reg.test(currentVal)) {
            layer.msg("请输入正确的邮箱！", 1);
            $("#KK_Email").focus();
            return false;
        }
        var currentVal = $("#KK_phone").val();
        if (currentVal == "") {
            layer.msg("请输入您的手机！", 1);
            $("#KK_phone").focus();
            return false;
        }
        if (!(/^(0|86|17951)?(13[0-9]|15[012356789]|17[678]|18[0-9]|14[57])[0-9]{8}$/.test(currentVal))) {
            layer.msg("请输入正确手机号码！", 1);
            $("#KK_phone").focus();
            return false;
        }
        currentVal = $("#KK_content").val();
        if (currentVal == "") {
            layer.msg("请输入您的留言！", 1);
            $("#KK_content").focus();
            return false;
        }
        var flage1 = false;
        currentVal = $("#txtCode").val();
        if (currentVal == "") {
            layer.msg("请输入验证码！", 1);
            $("#txtCode").focus();
            return false;
        }
        jQuery.ajax({
            type: 'POST',
            url: '/ajax/CheckREG.aspx?action=checkform&type=code',
            data: 'values=' + $("#txtCode").val(),
            async: false,
            success: function (msg) {
                if (msg == "0") {
                    flage1 = false;
                    layer.msg("请输入正确的验证码！", 1);
                }
                else {
                    flage1 = true;
                }
            },
            error: function () {
                alert("error");
                flage1 = false;
            }
        });
        return flage1;
    });

    $(".select").each(function () {
        var s = $(this);
        var z = parseInt(s.css("z-index"));
        var dt = $(this).children("dt");
        var dd = $(this).children("dd");
        var _show = function () { dd.slideDown(200); dt.addClass("cur"); s.css("z-index", z + 1); };
        var _hide = function () { dd.slideUp(200); dt.removeClass("cur"); s.css("z-index", z); };
        dt.click(function () { dd.is(":hidden") ? _show() : _hide(); });
        dd.find("a").click(function () { dt.html($(this).html()); _hide(); });
        $("body").click(function (i) { !$(i.target).parents(".select").first().is(s) ? _hide() : ""; });
    });

    $("#aFloatTools_Show").click(function () {
        $('#divFloatToolsView').animate({ width: 'show', opacity: 'show' }, 100, function () { $('#divFloatToolsView').show(); });
        $('#aFloatTools_Show').hide();
        $('#aFloatTools_Hide').show();
    });
    $("#aFloatTools_Hide").click(function () {
        $('#divFloatToolsView').animate({ width: 'hide', opacity: 'hide' }, 100, function () { $('#divFloatToolsView').hide(); });
        $('#aFloatTools_Show').show();
        $('#aFloatTools_Hide').hide();
    });

    $(".db").click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 300);
        return false;
    });


    var flag = 0;
    $('#rightArrow').on("click", function () {
        if (flag == 1) {
            $("#floatDivBoxs").animate({ right: '-175px' }, 300);
            $(this).animate({ right: '-5px' }, 300);
            $(this).css('background-position', '-50px 0');
            flag = 0;
        } else {
            $("#floatDivBoxs").animate({ right: '0' }, 300);
            $(this).animate({ right: '170px' }, 300);
            $(this).css('background-position', '0px 0');
            flag = 1;
        }
    });

    //初始隐藏
    $("#side_content").animate({ width: '0px' }, "fast");
    $("#show_btn").animate({ width: '25px' }, "fast");

    $("#show_btn").click(
    function () {
        $(this).animate({ width: '0px' }, "fast");
        $("#side_content").stop(true, true).delay(200).animate({ width: '154px' }, "fast");
    });

    $("#close_btn").click(
    function () {
        $("#side_content").animate({ width: '0px' }, "fast");
        $("#show_btn").stop(true, true).delay(300).animate({ width: '25px' }, "fast");
    });

});

function setNav(i) {
    $(".navMain .m ").eq(i).children("a").addClass("current");
}
function errorImg(img) {
    img.src = "images/onerror.jpg";
    img.onerror = null;
}