/*

 @Name: layui WebIM 1.0.0
 @Author：贤心
 @Date: 2014-04-25
 @Blog: http://sentsin.com
 
 */

; !function (win, undefined) {
    if (checkIE()) {
        RongIMClient.init("qf3d5gbj3avch");
        var token = "";
        var Log_name = "";
        var Log_pic = "";
        $.ajax({
            type: "post",
            url: "/ajax/AjaxChat.aspx?action=checkLogin&t=" + Date.now(),
            dataType: "json",
            success: function (msg) {
                var re = msg.result;
                if (msg.code == 200 && re.token != "") {
                    token = re.token;
                    Log_name = re.name;
                    Log_pic = re.pic;

                    //=================================================================//
                    var config = {
                        msgurl: '私信地址',
                        chatlogurl: '聊天记录url前缀',
                        aniTime: 200,
                        right: -232,
                        api: {
                            friend: "/ajax/AjaxChat.aspx?action=getShop2&t=" + Date.now(), //好友列表接口
                            group: '', //群组列表接口 
                            chatlog: '', //聊天记录接口
                            groups: '', //群组成员接口
                            sendurl: '' //发送消息接口
                        },
                        user: { //当前用户信息
                            name: Log_name,
                            face: Log_pic
                        },

                        //自动回复内置文案，也可动态读取数据库配置
                        autoReplay: [
                            '您好，我现在有事不在，一会再和您联系。',
                            '你没发错吧？',
                            '洗澡中，请勿打扰，偷窥请购票，个体四十，团体八折，订票电话：一般人我不告诉他！',
                            '你好，我是主人的美女秘书，有什么事就跟我说吧，等他回来我会转告他的。',
                            '我正在拉磨，没法招呼您，因为我们家毛驴去动物保护协会把我告了，说我剥夺它休产假的权利。',
                            '<（@￣︶￣@）>',
                            '你要和我说话？你真的要和我说话？你确定自己想说吗？你一定非说不可吗？那你说吧，这是自动回复。',
                            '主人正在开机自检，键盘鼠标看好机会出去凉快去了，我是他的电冰箱，我打字比较慢，你慢慢说，别急……',
                            '(*^__^*) 嘻嘻，是贤心吗？'
                        ],
                        chating: {},
                        hosts: (function () {
                            var dk = location.href.match(/\:\d+/);
                            dk = dk ? dk[0] : '';
                            return 'http://' + document.domain + dk + '/';
                        })(),
                        json: function (url, data, callback, error) {
                            return jq_191.ajax({
                                type: 'POST',
                                url: url,
                                data: data,
                                dataType: 'json',
                                success: callback,
                                error: error
                            });
                        },
                        stopMP: function (e) {
                            e ? e.stopPropagation() : e.cancelBubble = true;
                        }
                    },
                    dom = [jq_191(window), jq_191(document), jq_191('html'), jq_191('body')],
                    xxim = {};

                    //============================================================================================//
                    RongIMClient.connect(token, {
                        onSuccess: function (userId) {
                            console.log("Login successfully." + userId);
                        },
                        onError: function (errorCode) {
                            console.log("Login failed." + errorCode.getValue(), "error message: " + errorCode.getMessage());
                        }
                    });
                    // 设置连接监听状态 （ status 标识当前连接状态）
                    // 连接状态
                    RongIMClient.setConnectionStatusListener({
                        onChanged: function (status) {
                            // status:RongIMClient.ConnectionStatusListener.ConnectionStatus 对象
                            console.log(status.getValue(), status.getMessage());
                        }
                    });

                    var isOpenNews = true;//默认点击用户弹出对话框
                    var audio = document.getElementsByTagName("audio")[0];
                    // 消息监听器
                    RongIMClient.getInstance().setOnReceiveMessageListener({
                        // 接收到的消息
                        onReceived: function (message) {
                            // message:RongIMClient.RongIMMessage 子类
                            // 接收到消息处理逻辑

                            console.log("收到了一条信息");
                            //audio.play();

                            var dataID = message.getTargetId();
                            var dataType = message.getMessageType();
                            var dataContent = message.getContent();

                            var dataImgUrl = "";
                            if (dataType == RongIMClient.MessageType.ImageMessage)
                                dataImgUrl = message.getsetImageUri();

                            if ($(".mp_id_" + message.getTargetId()).length <= 0)
                            {
                                $.ajax({
                                    type: "POST",
                                    async:false,
                                    url: "/ajax/AjaxChat.aspx?action=getFriendOne&t=" + Date.now(),
                                    data: "uid=" + message.getTargetId(),
                                    dataType: 'json',
                                    success: function (result) {
                                        if (result) {
                                            if (result.status == "1") {
                                                $(".xxim_chatlist").append("<li type=\"one\" style='display:none;' class=\"xxim_childnode mp_id_" + result.data[0]["id"] + "\" data-id=\"" + result.data[0]["id"] + "\"><img class=\"xxim_oneface\" src=\"" + result.data[0]["face"] + "\"><span class=\"xxim_onename\">" + result.data[0]["name"] + "</span></li>");
                                            }
                                        }
                                    }
                                });
                            }

                            if ($(".xubox_layer").length > 0) {
                                var userList = $(".layim_chatlist li");
                                var checked = true;
                                userList.each(function () {
                                    var sid = $(this).attr("data-id");
                                    var csd = $(this).attr("class");
                                    if (sid == dataID) {
                                        if (userList.length > 1 && csd != "layim_chatnow") {
                                            $(this).attr("class", "hastips");
                                            checked = false;
                                        }
                                    }
                                })
                                if (checked) {
                                    isOpenNews = false;
                                    //如果聊天窗口没有该用户，则添加该用户的选项卡，高亮显示
                                    $(".mp_id_" + message.getTargetId()).click();
                                }
                            }
                            else {
                                //如果没有聊天窗口，则直接弹出窗口
                                $(".mp_id_" + message.getTargetId()).click();
                            }

                            var obj_user = $(".mp_id_" + dataID);
                            var to_name = obj_user.find(".xxim_onename").html();
                            var to_pic = obj_user.find(".xxim_oneface").attr("src");

                            var param = {
                                id: dataID, //用户ID
                                type: 'one',
                                dataType: dataType,
                                name: to_name,  //用户名
                                face: to_pic,  //用户头像
                                href: '/Shop.aspx?id=' + dataID, //用户主页
                                time: new Date().toLocaleString(),
                                imgUrl: dataImgUrl,
                                content: dataContent  //用户消息
                            }
                            xxim.getMsg(param);
                        }
                    });

                    //============================================================================================//
                    xxim.drawExpressionWrap = function () {
                        var RongIMexpressionObj = $(".RongIMexpressionWrap");
                        if (win.RongIMClient) {
                            var arrImgList = RongIMClient.Expression.getAllExpression(60, 0);
                            if (arrImgList && arrImgList.length > 0) {
                                for (var objArr in arrImgList) {
                                    var imgObj = arrImgList[objArr].img;
                                    imgObj.setAttribute("alt", arrImgList[objArr].chineseName);
                                    imgObj.setAttribute("title", arrImgList[objArr].chineseName);
                                    //                    imgObj.alt = arrImgList[objArr].chineseName;
                                    var newSpan = $('<span class="RongIMexpression_' + arrImgList[objArr].englishName + '"></span>');
                                    newSpan.append(imgObj);
                                    RongIMexpressionObj.append(newSpan);
                                }
                            }
                            $(".RongIMexpressionWrap>span").bind('click', function (event) {
                                $(".layim_write")[0].value += "[" + $(this).children(".RC_Expression").attr("alt") + "]";
                                //$(".textarea").append($(this).clone());
                            });
                            $(".RongIMexpressionWrap").bind('mouseleave', function () {
                                $('.RongIMexpressionWrap').stop(true, true).animate({ left: -484 }, 500);
                            })
                            $(".layim_addface").bind('click', function () {
                                $('.RongIMexpressionWrap').stop(true, true).animate({ left: 0 }, 500);
                            })
                        }
                        ;
                    };
                    //读取信息
                    xxim.getMsg = function (param) {
                        var node = xxim.node, log = {}, keys = param.type + param.id;

                        log.imarea = jq_191('#layim_chatarea').find('#layim_area' + keys);
                        log.imarea.append(xxim.html(param, ''));
                        log.imarea.scrollTop(log.imarea[0].scrollHeight);
                    }

                    //聊天模版
                    xxim.html = function (param, type) {

                        var chat_conn = param.content;

                        //=========消息类型处理=============//
                        //表情
                        function initEmotion(str) {
                            var a = document.createElement("span");
                            return RongIMClient.Expression.retrievalEmoji(str, function (img) {
                                a.appendChild(img.img);
                                var str = '<span class="rc_RongIMexpression rc_RongIMexpression_' + img.englishName + '">' + a.innerHTML + '</span>';
                                a.innerHTML = "";
                                return str;
                            });
                        }
                        //符号
                        String.prototype.replaceAll = function (s1, s2) {
                            return this.replace(new RegExp(s1, "gm"), s2);
                        };
                        function symbolreplace(str) {
                            if (!str) return '';
                            str = str.replace(/&/g, '&amp;');
                            str = str.replace(/</g, '&lt;');
                            str = str.replace(/>/g, '&gt;');
                            str = str.replace(/"/g, '&quot;');
                            str = str.replace(/'/g, '&#039;');
                            str = str.replaceAll("\n", "<br>");
                            str = str.replace(/(https?:\/\/[^ \r\n<]+)\ ?/gi, function (Str) {
                                Str = '<a href="' + Str + '" target="_blank" style="color: #000;">' + Str + '</a>';
                                return Str;
                            });
                            return str;
                        }

                        if (param.dataType == RongIMClient.MessageType.TextMessage) {
                            chat_conn = initEmotion(symbolreplace(chat_conn));
                        } else if (param.dataType == RongIMClient.MessageType.ImageMessage) {
                            chat_conn = "<img class='imgThumbnail' src='data:image/jpg;base64," + chat_conn + "' bigUrl='" + param.imgUrl + "'/>";
                        } else if (param.dataType == RongIMClient.MessageType.VoiceMessage) {
                            chat_conn = "<div style='color:#bbb;'>收到一条语音信息，PC版暂不支持，请使用手机客户端</div>";
                        }

                        //=========消息类型处理 end=============//


                        return '<li class="' + (type === 'me' ? 'layim_chateme' : '') + '">'
                            + '<div class="layim_chatuser">'
                                + '<img src="' + param.face + '" >'
                                + '<span class="layim_chatname">' + param.name + '</span>'
                                + '<span class="layim_chattime">' + param.time + '</span>'
                            + '</div>'
                            + '<div class="layim_chatsay">' + chat_conn + '<em class="layim_zero"></em></div>'
                        + '</li>';
                    }

                    //主界面tab
                    xxim.tabs = function (index) {
                        var node = xxim.node;
                        node.tabs.eq(index).addClass('xxim_tabnow').siblings().removeClass('xxim_tabnow');
                        node.list.eq(index).show().siblings('.xxim_list').hide();
                        if (node.list.eq(index).find('li').length === 0) {
                            xxim.getDates(index);
                        }
                    };

                    //节点
                    xxim.renode = function () {
                        var node = xxim.node = {
                            tabs: jq_191('#xxim_tabs>span'),
                            list: jq_191('.xxim_list'),
                            online: jq_191('.xxim_online'),
                            setonline: jq_191('.xxim_setonline'),
                            onlinetex: jq_191('#xxim_onlinetex'),
                            xximon: jq_191('#xxim_on'),
                            layimFooter: jq_191('#xxim_bottom'),
                            xximHide: jq_191('#xxim_hide'),
                            xximSearch: jq_191('#xxim_searchkey'),
                            searchMian: jq_191('#xxim_searchmain'),
                            closeSearch: jq_191('#xxim_closesearch'),
                            layimMin: jq_191('#layim_min')
                        };
                    };

                    //主界面缩放
                    xxim.expend = function () {
                        var node = xxim.node;
                        if (xxim.layimNode.attr('state') !== '1') {
                            xxim.layimNode.stop().animate({ right: config.right }, config.aniTime, function () {
                                node.xximon.addClass('xxim_off');
                                try {
                                    localStorage.layimState = 1;
                                } catch (e) { }
                                xxim.layimNode.attr({ state: 1 });
                                node.layimFooter.addClass('xxim_expend').stop().animate({ marginLeft: config.right }, config.aniTime / 2);
                                node.xximHide.addClass('xxim_show');
                            });
                        } else {
                            xxim.layimNode.stop().animate({ right: 1 }, config.aniTime, function () {
                                node.xximon.removeClass('xxim_off');
                                try {
                                    localStorage.layimState = 2;
                                } catch (e) { }
                                xxim.layimNode.removeAttr('state');
                                node.layimFooter.removeClass('xxim_expend');
                                node.xximHide.removeClass('xxim_show');
                            });
                            node.layimFooter.stop().animate({ marginLeft: 0 }, config.aniTime);
                        }
                    };

                    //初始化窗口格局
                    xxim.layinit = function () {
                        var node = xxim.node;

                        //主界面
                        try {
                            /*
                            if(!localStorage.layimState){       
                                config.aniTime = 0;
                                localStorage.layimState = 1;
                            }
                            */
                            if (localStorage.layimState === '1') {
                                xxim.layimNode.attr({ state: 1 }).css({ right: config.right });
                                node.xximon.addClass('xxim_off');
                                node.layimFooter.addClass('xxim_expend').css({ marginLeft: config.right });
                                node.xximHide.addClass('xxim_show');
                            }
                        } catch (e) {
                            //layer.msg(e.message, 5, -1);
                        }
                    };

                    //聊天窗口
                    xxim.popchat = function (param) {
                        var node = xxim.node, log = {};

                        log.success = function (layero) {
                            layer.setMove();

                            xxim.chatbox = layero.find('#layim_chatbox');
                            log.chatlist = xxim.chatbox.find('.layim_chatmore>ul');

                            log.chatlist.html('<li data-id="' + param.id + '" type="' + param.type + '"  id="layim_user' + param.type + param.id + '"><span>' + param.name + '</span><em>×</em></li>')
                            xxim.tabchat(param, xxim.chatbox);
                            xxim.drawExpressionWrap();
                            //最小化聊天窗
                            xxim.chatbox.find('.layer_setmin').on('click', function () {
                                var indexs = layero.attr('times');
                                layero.hide();
                                node.layimMin.text(xxim.nowchat.name).show();
                            });

                            //关闭窗口
                            xxim.chatbox.find('.layim_close').on('click', function () {
                                var indexs = layero.attr('times');
                                layer.close(indexs);
                                xxim.chatbox = null;
                                config.chating = {};
                                config.chatings = 0;
                            });

                            //关闭某个聊天
                            log.chatlist.on('mouseenter', 'li', function () {
                                jq_191(this).find('em').show();
                            }).on('mouseleave', 'li', function () {
                                jq_191(this).find('em').hide();
                            });
                            log.chatlist.on('click', 'li em', function (e) {
                                var parents = jq_191(this).parent(), dataType = parents.attr('type');
                                var dataId = parents.attr('data-id'), index = parents.index();
                                var chatlist = log.chatlist.find('li'), indexs;

                                config.stopMP(e);

                                delete config.chating[dataType + dataId];
                                config.chatings--;

                                parents.remove();
                                jq_191('#layim_area' + dataType + dataId).remove();
                                if (dataType === 'group') {
                                    jq_191('#layim_group' + dataType + dataId).remove();
                                }

                                if (parents.hasClass('layim_chatnow')) {
                                    if (index === config.chatings) {
                                        indexs = index - 1;
                                    } else {
                                        indexs = index + 1;
                                    }
                                    xxim.tabchat(config.chating[chatlist.eq(indexs).attr('type') + chatlist.eq(indexs).attr('data-id')]);
                                }

                                if (log.chatlist.find('li').length === 1) {
                                    log.chatlist.parent().hide();
                                }
                            });

                            //聊天选项卡
                            log.chatlist.on('click', 'li', function () {
                                var othis = jq_191(this), dataType = othis.attr('type'), dataId = othis.attr('data-id');
                                xxim.tabchat(config.chating[dataType + dataId]);
                            });

                            //发送热键切换
                            log.sendType = jq_191('#layim_sendtype'), log.sendTypes = log.sendType.find('span');
                            jq_191('#layim_enter').on('click', function (e) {
                                config.stopMP(e);
                                log.sendType.show();
                            });
                            //log.sendTypes.on('click', function () {
                            //    log.sendTypes.find('i').text('')
                            //    jq_191(this).find('i').text('√');
                            //});

                            xxim.transmit();
                        };

                        log.html = '<div class="layim_chatbox" id="layim_chatbox">'
                                + '<h6>'
                                + '<span class="layim_move"></span>'
                                + '    <a href="' + param.url + '" class="layim_face" target="_blank"><img src="' + param.face + '" ></a>'
                                + '    <a href="' + param.url + '" class="layim_names" target="_blank">' + param.name + '</a>'
                                + '    <span class="layim_rightbtn">'
                                + '        <i class="layer_setmin"></i>'
                                + '        <i class="layim_close"></i>'
                                + '    </span>'
                                + '</h6>'
                                + '<div class="layim_chatmore" id="layim_chatmore">'
                                + '    <ul class="layim_chatlist"></ul>'
                                + '</div>'
                                //+ '<div class="layim_groups" id="layim_groups"></div>'
                                + '<div class="layim_chat">'
                                + '    <div class="layim_chatarea" id="layim_chatarea">'
                                + '        <ul class="layim_chatview layim_chatthis"  id="layim_area' + param.type + param.id + '"></ul>'
                                + '    </div>'
                                + '    <div class="layim_tool">'
                                + '        <i class="layim_addface" title="发送表情"></i>'
                                //+ '        <a href="javascript:;"><i class="layim_addimage" title="上传图片"></i></a>'
                                //+ '        <a href="javascript:;"><i class="layim_addfile" title="上传附件"></i></a>'
                                //+ '        <a href="" target="_blank" class="layim_seechatlog"><i></i>聊天记录</a>'
                                + '<div class="RongIMexpressionWrap"></div>'
                                + '    </div>'
                                + '    <textarea class="layim_write" id="layim_write"></textarea>'
                                + '    <div class="layim_send">'
                                + '        <div class="layim_sendbtn" id="layim_sendbtn">发送<span class="layim_enter" id="layim_enter"><em class="layim_zero"></em></span></div>'
                                + '        <div class="layim_sendtype" id="layim_sendtype">'
                                + '            <span><i>√</i>按Enter键发送</span>'
                                + '            <span><i></i>按Ctrl+Enter键发送</span>'
                                + '        </div>'
                                + '    </div>'
                                + '</div>'
                                + '</div>';

                        if (config.chatings < 1) {
                            jq_191.layer({
                                type: 1,
                                border: [0],
                                title: false,
                                shade: [0],
                                area: ['620px', '493px'],
                                move: '.layim_chatbox .layim_move',
                                moveType: 1,
                                closeBtn: false,
                                offset: [((jq_191(window).height() - 493) / 2) + 'px', ''],
                                page: {
                                    html: log.html
                                }, success: function (layero) {
                                    log.success(layero);
                                }
                            })
                        } else {

                            log.chatmore = xxim.chatbox.find('#layim_chatmore');
                            log.chatarea = xxim.chatbox.find('#layim_chatarea');

                            log.chatmore.show();

                            log.chatmore.find('ul>li').removeClass('layim_chatnow');
                            log.chatmore.find('ul').append('<li data-id="' + param.id + '" type="' + param.type + '" id="layim_user' + param.type + param.id + '" class="layim_chatnow"><span>' + param.name + '</span><em>×</em></li>');

                            log.chatarea.find('.layim_chatview').removeClass('layim_chatthis');
                            log.chatarea.append('<ul class="layim_chatview layim_chatthis" id="layim_area' + param.type + param.id + '"></ul>');

                            xxim.tabchat(param);
                        }

                        //群组
                        //log.chatgroup = xxim.chatbox.find('#layim_groups');
                        //if (param.type === 'group') {
                        //    log.chatgroup.find('ul').removeClass('layim_groupthis');
                        //    log.chatgroup.append('<ul class="layim_groupthis" id="layim_group' + param.type + param.id + '"></ul>');
                        //    xxim.getGroups(param);
                        //}
                        ////点击群员切换聊天窗
                        //log.chatgroup.on('click', 'ul>li', function () {
                        //    xxim.popchatbox(jq_191(this));
                        //});
                    };

                    //定位到某个聊天队列
                    xxim.tabchat = function (param) {
                        var node = xxim.node, log = {}, keys = param.type + param.id;
                        xxim.nowchat = param;

                        xxim.chatbox.find('#layim_user' + keys).stop(true, true).addClass('layim_chatnow').removeClass('hastips').siblings().removeClass('layim_chatnow');
                        xxim.chatbox.find('#layim_area' + keys).addClass('layim_chatthis').siblings().removeClass('layim_chatthis');
                        xxim.chatbox.find('#layim_group' + keys).addClass('layim_groupthis').siblings().removeClass('layim_groupthis');

                        xxim.chatbox.find('.layim_face>img').attr('src', param.face);
                        xxim.chatbox.find('.layim_face, .layim_names').attr('href', param.href);
                        xxim.chatbox.find('.layim_names').text(param.name);

                        //xxim.chatbox.find('.layim_seechatlog').attr('href', config.chatlogurl + param.id);

                        //$('.xubox_layer').attr("now_chat_id", param.id);

                        log.groups = xxim.chatbox.find('.layim_groups');
                        if (param.type === 'group') {
                            log.groups.show();
                        } else {
                            log.groups.hide();
                        }

                        jq_191('#layim_write').focus();
                        log.imarea = jq_191('#layim_chatarea').find('#layim_area' + keys);
                        log.imarea.scrollTop(log.imarea[0].scrollHeight);
                    };

                    //弹出聊天窗
                    xxim.popchatbox = function (othis) {
                        var node = xxim.node,
                            dataId = othis.attr('data-id'),
                            param = {
                                id: dataId, //用户ID
                                type: othis.attr('type'),
                                name: othis.find('.xxim_onename').text(),  //用户名
                                face: othis.find('.xxim_oneface').attr('src'),  //用户头像
                                href: config.hosts + '/Shop.aspx?id=' + dataId //用户主页
                            }, key = param.type + dataId;
                        if (!config.chating[key]) {
                            xxim.popchat(param);
                            config.chatings++;
                        } else {
                            xxim.tabchat(param);
                        }
                        config.chating[key] = param;

                        var chatbox = jq_191('#layim_chatbox');
                        if (chatbox[0]) {
                            node.layimMin.hide();
                            chatbox.parents('.xubox_layer').attr("now_chat_id", dataId).show();
                        }
                    };

                    //请求群员
                    xxim.getGroups = function (param) {
                        var keys = param.type + param.id, str = '',
                        groupss = xxim.chatbox.find('#layim_group' + keys);
                        groupss.addClass('loading');
                        config.json(config.api.groups, {}, function (datas) {
                            if (datas.status === 1) {
                                var ii = 0, lens = datas.data.length;
                                if (lens > 0) {
                                    for (; ii < lens; ii++) {
                                        str += '<li data-id="' + datas.data[ii].id + '" type="one"><img src="' + datas.data[ii].face + '" class="xxim_oneface"><span class="xxim_onename">' + datas.data[ii].name + '</span></li>';
                                    }
                                } else {
                                    str = '<li class="layim_errors">没有群员</li>';
                                }

                            } else {
                                str = '<li class="layim_errors">' + datas.msg + '</li>';
                            }
                            groupss.removeClass('loading');
                            groupss.html(str);
                        }, function () {
                            groupss.removeClass('loading');
                            groupss.html('<li class="layim_errors">请求异常</li>');
                        });
                    };

                    //消息传输
                    xxim.transmit = function () {
                        var node = xxim.node, log = {};
                        node.sendbtn = jq_191('#layim_sendbtn');
                        node.imwrite = jq_191('#layim_write');
                        //发送
                        log.send = function () {
                            //替换表情
                            //=========消息类型处理=============//
                            //表情
                            function strreplace(str) {
                                if (!str) return '';
                                str = str.replace(/&amp;/g, '&');
                                str = str.replace(/&lt;/g, '<');
                                str = str.replace(/&gt;/g, '>');
                                str = str.replace(/&quot;/g, '"');
                                str = str.replace(/&#039;/g, "'");
                                str = str.replace(/&nbsp;/g, " ");
                                return str;
                            }
                            //=========消息类型处理 end=============//

                            var con = node.imwrite.val().replace(/\[.+?\]/g, function (x) {
                                return RongIMClient.Expression.getEmojiObjByEnglishNameOrChineseName(x.slice(1, x.length - 1)).tag || x;
                            });
                            con = strreplace(con);

                            var data = {
                                content: con,
                                id: xxim.nowchat.id,
                                sign_key: '', //密匙
                                _: +new Date
                            };
                            if (data.content.replace(/\s/g, '') === '') {
                                layer.tips('说点啥呗！', '#layim_write', 2);
                                node.imwrite.focus();
                            } else {
                                //发送操作
                                var msg = new RongIMClient.TextMessage();
                                msg.setContent(data.content);
                                var msg = RongIMClient.TextMessage.obtain(data.content);
                                var content = new RongIMClient.MessageContent(msg);
                                var conversationtype = RongIMClient.ConversationType.PRIVATE; // 私聊
                                var targetId = $('.xubox_layer').attr("now_chat_id"); // 目标 Id
                                RongIMClient.getInstance().sendMessage(conversationtype, targetId, content, null, {
                                    // 发送消息成功
                                    onSuccess: function () {
                                        console.log("Send successfully");
                                    },
                                    onError: function (errorCode) {
                                        console.log(errorCode.getValue(), errorCode.getMessage());
                                    }
                                });

                                //此处皆为模拟
                                var keys = xxim.nowchat.type + xxim.nowchat.id;

                                //聊天模版
                                log.html = function (param, type) {

                                    var chat_conn = param.content;

                                    //=========消息类型处理=============//
                                    //表情
                                    function initEmotion(str) {
                                        var a = document.createElement("span");
                                        return RongIMClient.Expression.retrievalEmoji(str, function (img) {
                                            a.appendChild(img.img);
                                            var str = '<span class="rc_RongIMexpression rc_RongIMexpression_' + img.englishName + '">' + a.innerHTML + '</span>';
                                            a.innerHTML = "";
                                            return str;
                                        });
                                    }
                                    function symbolreplace(str) {
                                        if (!str) return '';
                                        str = str.replace(/&/g, '&amp;');
                                        str = str.replace(/</g, '&lt;');
                                        str = str.replace(/>/g, '&gt;');
                                        str = str.replace(/"/g, '&quot;');
                                        str = str.replace(/'/g, '&#039;');
                                        str = str.replace(new RegExp("\n", "gm"), "<br>");
                                        str = str.replace(/(https?:\/\/[^ \r\n<]+)\ ?/gi, function (Str) {
                                            Str = '<a href="' + Str + '" target="_blank" style="color: #000;">' + Str + '</a>';
                                            return Str;
                                        });
                                        return str;
                                    }
                                    //=========消息类型处理 end=============//

                                    chat_conn = initEmotion(symbolreplace(chat_conn));

                                    return '<li class="' + (type === 'me' ? 'layim_chateme' : '') + '">'
                                        + '<div class="layim_chatuser">'
                                            + function () {
                                                if (type === 'me') {
                                                    return '<span class="layim_chattime">' + param.time + '</span>'
                                                           + '<span class="layim_chatname">' + param.name + '</span>'
                                                           + '<img src="' + param.face + '" >';
                                                } else {
                                                    return '<img src="' + param.face + '" >'
                                                           + '<span class="layim_chatname">' + param.name + '</span>'
                                                           + '<span class="layim_chattime">' + param.time + '</span>';
                                                }
                                            }()
                                        + '</div>'
                                        + '<div class="layim_chatsay">' + chat_conn + '<em class="layim_zero"></em></div>'
                                    + '</li>';
                                };

                                log.imarea = xxim.chatbox.find('#layim_area' + keys);

                                log.imarea.append(log.html({
                                    time: new Date().toLocaleString(),
                                    name: config.user.name,
                                    face: config.user.face,
                                    content: data.content
                                }, 'me'));
                                node.imwrite.val('').focus();
                                log.imarea.scrollTop(log.imarea[0].scrollHeight);
                            }
                        };
                        node.sendbtn.on('click', log.send);

                        node.imwrite.keyup(function (e) {
                            if (e.keyCode === 13) {
                                log.send();
                            }
                        });
                    };

                    //事件
                    xxim.event = function () {
                        var node = xxim.node;

                        //主界面tab
                        node.tabs.eq(0).addClass('xxim_tabnow');
                        node.tabs.on('click', function () {
                            var othis = jq_191(this), index = othis.index();
                            xxim.tabs(index);
                        });

                        //列表展收
                        //node.list.on('click', 'h5', function () {
                        //    var othis = jq_191(this), chat = othis.siblings('.xxim_chatlist'), parentss = othis.parent();
                        //    if (parentss.hasClass('xxim_liston')) {
                        //        chat.hide();
                        //        parentss.removeClass('xxim_liston');
                        //    } else {
                        //        chat.show();
                        //        parentss.addClass('xxim_liston');
                        //    }
                        //});

                        //设置在线隐身
                        node.online.on('click', function (e) {
                            config.stopMP(e);
                            node.setonline.show();
                        });
                        node.setonline.find('span').on('click', function (e) {
                            var index = jq_191(this).index();
                            config.stopMP(e);
                            if (index === 0) {
                                node.onlinetex.html('在线');
                                node.online.removeClass('xxim_offline');
                            } else if (index === 1) {
                                node.onlinetex.html('隐身');
                                node.online.addClass('xxim_offline');
                            }
                            node.setonline.hide();
                        });

                        node.xximon.on('click', xxim.expend);
                        node.xximHide.on('click', xxim.expend);

                        //搜索
                        node.xximSearch.keyup(function () {
                            var val = jq_191(this).val().replace(/\s/g, '');
                            if (val !== '') {
                                node.searchMian.show();
                                node.closeSearch.show();
                                //此处的搜索ajax参考xxim.getDates
                                //node.list.eq(3).html('<li class="xxim_errormsg">没有符合条件的结果</li>');

                                myf = node.list.eq(3);
                                myf.addClass('loading');

                                config.json("/ajax/AjaxChat.aspx?action=Search&key=" + val, {}, function (datas) {
                                    if (datas.status === 1) {
                                        var i = 0,
                                            myflen = datas.data.length,
                                            str = '',
                                            item;
                                        if (myflen > 0) {
                                            if (index !== 2) {
                                                for (; i < myflen; i++) {
                                                    str += '<li data-id="' + datas.data[i].id + '" class="xxim_parentnode xxim_liston">'
                                                        + '<h5><i></i><span class="xxim_parentname">' + datas.data[i].name + '</span><em class="xxim_nums">（' + datas.data[i].nums + '）</em></h5>'
                                                        + '<ul class="xxim_chatlist" style="display: block;">';
                                                    item = datas.data[i].item;
                                                    for (var j = 0; j < item.length; j++) {
                                                        //str += '<li data-id="' + item[j].id + '" class="xxim_childnode mp_id_' + item[j].id + '" type="' + (index === 0 ? 'one' : 'group') + '"><img src="' + item[j].face + '" class="xxim_oneface"><span class="xxim_onename">' + item[j].name + '</span></li>';
                                                        str += '<li data-id="' + item[j].id + '" class="xxim_childnode mp_id_' + item[j].id + '" type="one"><img src="' + item[j].face + '" class="xxim_oneface"><span class="xxim_onename">' + item[j].name + '</span></li>';
                                                    }
                                                    str += '</ul></li>';
                                                }
                                            } else {
                                                str += '<li class="xxim_liston">'
                                                    + '<ul class="xxim_chatlist">';
                                                for (; i < myflen; i++) {
                                                    str += '<li data-id="' + datas.data[i].id + '" class="xxim_childnode" type="one"><img src="' + datas.data[i].face + '"  class="xxim_oneface"><span  class="xxim_onename">' + datas.data[i].name + '</span><em class="xxim_time">' + datas.data[i].time + '</em></li>';
                                                }
                                                str += '</ul></li>';
                                            }
                                            myf.html(str);
                                        } else {
                                            myf.html('<li class="xxim_errormsg">没有任何数据</li>');
                                        }
                                        myf.removeClass('loading');
                                    } else {
                                        myf.html('<li class="xxim_errormsg">' + datas.msg + '</li>');
                                    }
                                }, function () {
                                    myf.html('<li class="xxim_errormsg">请求失败</li>');
                                    myf.removeClass('loading');
                                });


                            } else {
                                node.searchMian.hide();
                                node.closeSearch.hide();
                            }
                        });
                        node.closeSearch.on('click', function () {
                            jq_191(this).hide();
                            node.searchMian.hide();
                            node.xximSearch.val('').focus();
                        });

                        //弹出聊天窗
                        config.chatings = 0;
                        node.list.on('click', '.xxim_childnode', function () {
                            var othis = jq_191(this);
                            xxim.popchatbox(othis);
                        });

                        //点击最小化栏
                        node.layimMin.on('click', function () {
                            jq_191(this).hide();
                            jq_191('#layim_chatbox').parents('.xubox_layer').show();
                        });


                        //document事件
                        dom[1].on('click', function () {
                            node.setonline.hide();
                            jq_191('#layim_sendtype').hide();
                        });
                    };

                    //请求列表数据
                    xxim.getDates = function (index) {
                        var api = [config.api.friend, config.api.group, config.api.chatlog],
                            node = xxim.node,
                            myf = node.list.eq(index);
                        myf.addClass('loading');

                        config.json(api[index], {}, function (datas) {
                            if (datas.status === 1) {
                                var i = 0,
                                    myflen = datas.data.length,
                                    str = '',
                                    item;
                                if (myflen > 0) {
                                    if (index !== 2) {
                                        for (; i < myflen; i++) {
                                            str += '<li data-id="' + datas.data[i].id + '" class="xxim_parentnode xxim_liston">'
                                                + '<h5><i></i><span class="xxim_parentname">' + datas.data[i].name + '</span><em class="xxim_nums">（' + datas.data[i].nums + '）</em></h5>'
                                                + '<ul class="xxim_chatlist" style="display: block;">';
                                            item = datas.data[i].item;
                                            for (var j = 0; j < item.length; j++) {
                                                str += '<li data-id="' + item[j].id + '" class="xxim_childnode mp_id_' + item[j].id + '" type="' + (index === 0 ? 'one' : 'group') + '"><img src="' + item[j].face + '" class="xxim_oneface"><span class="xxim_onename">' + item[j].name + '</span></li>';
                                            }
                                            str += '</ul></li>';
                                        }
                                    } else {
                                        str += '<li class="xxim_liston">'
                                            + '<ul class="xxim_chatlist">';
                                        for (; i < myflen; i++) {
                                            str += '<li data-id="' + datas.data[i].id + '" class="xxim_childnode" type="one"><img src="' + datas.data[i].face + '"  class="xxim_oneface"><span  class="xxim_onename">' + datas.data[i].name + '</span><em class="xxim_time">' + datas.data[i].time + '</em></li>';
                                        }
                                        str += '</ul></li>';
                                    }
                                    myf.html(str);
                                } else {
                                    myf.html('<li class="xxim_errormsg">没有任何数据</li>');
                                }
                                myf.removeClass('loading');
                            } else {
                                myf.html('<li class="xxim_errormsg">' + datas.msg + '</li>');
                            }
                        }, function () {
                            myf.html('<li class="xxim_errormsg">请求失败</li>');
                            myf.removeClass('loading');
                        });
                    };

                    //渲染骨架
                    xxim.view = (function () {
                        var xximNode = xxim.layimNode = jq_191('<div id="xximmm" class="xxim_main">'
                                + '<div class="xxim_top" id="xxim_top">'
                                + '  <div class="xxim_search"><i></i><input id="xxim_searchkey" /><span id="xxim_closesearch">×</span></div>'
                                + '  <div class="xxim_tabs" id="xxim_tabs"><!--<span class="xxim_tabfriend" title="好友"><i></i></span>--><span class="xxim_tabgroup" title="商家列表"><i></i></span><!--<span class="xxim_latechat"  title="最近聊天"><i></i></span>--></div>'
                                + '  <ul class="xxim_list" style="display:block"></ul>'
                                + '  <ul class="xxim_list"></ul>'
                                + '  <ul class="xxim_list"></ul>'
                                + '  <ul class="xxim_list xxim_searchmain" id="xxim_searchmain"></ul>'
                                + '</div>'
                                + '<ul class="xxim_bottom" id="xxim_bottom">'
                                //+ '<li class="xxim_online" id="xxim_online">'
                                //    + '<i class="xxim_nowstate"></i><span id="xxim_onlinetex">在线</span>'
                                //    + '<div class="xxim_setonline">'
                                //        + '<span><i></i>在线</span>'
                                //        + '<span class="xxim_setoffline"><i></i>隐身</span>'
                                //    + '</div>'
                                //+ '</li>'
                                + '<li class="xxim_mymsg" title="' + Log_name + '"><img src="' + Log_pic + '" width="50" height="50"></li>'
                                //+ '<li class="xxim_mymsg" id="xxim_mymsg" title="我的私信"><i></i><a href="' + config.msgurl + '" target="_blank"></a></li>'
                                //+ '<li class="xxim_seter" id="xxim_seter" title="设置">'
                                //    + '<i></i>'
                                //    + '<div class="">'

                                //    + '</div>'
                                //+ '</li>'
                                //+ '<li class="xxim_hide" id="xxim_hide"><i></i></li>'
                                + '<li class="xxim_name" title="' + Log_name + '" style="width:150px;line-height:50px;border:none;font-size:15px;">' + Log_name + '</li>'
                                + '<li id="xxim_on" class="xxim_icon xxim_on"></li>'
                                + '<div class="layim_min" id="layim_min"></div>'
                            + '</ul>'
                        + '</div>');
                        dom[3].append(xximNode);

                        xxim.renode();
                        xxim.getDates(0);
                        xxim.event();
                        xxim.layinit();
                    }());
                    //=================================================================//
                } else {
                    alert("读取token失败！");
                    return;
                }
            }
        });
    }
}(window);

