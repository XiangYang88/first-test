$(function () {
    //验证
    $("#btnSumbit").click(function () {
        var currentVal = $("#KK_name").val();
        if (currentVal == "") {
            layer.msg("请输入姓名！", 1);
            $("#KK_name").focus();
            return false;
        }
        if (currentVal.length >28) {
            layer.msg("请输入正确的姓名！", 1);
            $("#KK_name").focus();
            return false;
        }

        var currentVal = $("#KK_Mobile").val();
        if (currentVal == "") {
            layer.msg("请输入手机号码！", 1);
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
            layer.msg("请输入留言内容！", 1);
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
})