<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyRoleMenu.aspx.cs" Inherits="sys_SyRoleMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1">
    <title>角色管理</title>
     <link href="../style/Style.css" rel="stylesheet" />
    <script type="text/javascript" src="../script/jquery-1.7.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../ext/resources/css/ext-all.css"/>    
    <script type="text/javascript" src="../ext/adapter/ext/ext-base.js"></script>
    <script type="text/javascript" src="../ext/ext-all.js"></script>
    <script type="text/javascript" src="../ext/ream/TreeCheckNodeUI.js"></script>
    
    <script type="text/javascript">
    var urlSave = "../Rx/SyRole.aspx";
    Ext.onReady(function(){
    
       Ext.BLANK_IMAGE_URL = '../img/s.gif';
       initTree();
    });
/*
 *   var tree = new Ext.tree.TreePanel({
 *		el:'tree-ct',
 *		width:568,
 *		height:300,
 *		checkModel: 'cascade',   //对树的级联多选
 *		onlyLeafCheckable: false,//对树所有结点都可选
 *		animate: false,
 *		rootVisible: false,
 *		autoScroll:true,
 *		loader: new Ext.tree.DWRTreeLoader({
 *			dwrCall:Tmplt.getTmpltTree,
 *			baseAttrs: { uiProvider: Ext.tree.TreeCheckNodeUI } //添加 uiProvider 属性
 *		}),
 *		root: new Ext.tree.AsyncTreeNode({ id:'0' })
 *	});
 *	tree.on("check",function(node,checked){alert(node.text+" = "+checked)}); //注册"check"事件
 *	tree.render();
 * 
 */
    function initTree(){
        
        //设置树的点击事件
     
	    //设置树形面板
	    var Tree = Ext.tree;
        var tree = new Tree.TreePanel({
		    el:'tree-div',
		    autoScroll:true,
		    //iconCls: "tbls",
		    //root:root,
		    animate:false,
		    //allowDrag:true,
		    useArrows:false,
		    enableDD:false,
		    border:true,
		    rootVisible:true,
		    containerScroll: true,
		    checkModel: 'cascade',   //对树的级联多选   single parentCascade ：级联选中所有父结点 childCascade  ：级联选中所有子结
            onlyLeafCheckable: false,//对树所有结点都可选 
		    frame: true,
		    loader: new Ext.tree.TreeLoader({
		        dataUrl: urlSave + '?role=' +Ext.getDom("PKID").value+ '&chk=1',    //加载数据
 			    baseAttrs: { uiProvider: Ext.tree.TreeCheckNodeUI } //添加 uiProvider 属性      使用扩展多选
 		    }),
		    root:{
		        id:'root',
		        text: '菜单列表',
		        draggable:false,
		        nodeType: 'async'
		    },
		    
    		//注册事件
		    listeners: {
                'checkchange': function(node, checked){
                    if(checked){
                        node.getUI().addClass('complete');
//                         alert(node.text+" = "+checked);
                    }else{
                        node.getUI().removeClass('complete');
                    }
                },
                'click': function(node,e){
                   
                }
            },
            
		    buttons: [{
                text: '保存设置',
                handler: function(){
                    var msg = '', selNodes = tree.getChecked();
                    Ext.each(selNodes, function(node){
                        if(msg.length > 0){
                            msg += ', ';
                        }
                        msg += node.id;
                    });
                    
                    if(msg !=""){
                        /*****************************************************/
                        ////使用加载进度条
                        Ext.MessageBox.show({
						   title: '请稍等',
						   msg: '正在提交数据...',
						   progressText: '',
						   width:300,
						   progress:true,
						   closable:false,
						   animEl: 'waiting'
					   });
					   //控制进度速度
					   var f = function(v){
						 return function(){
									var i = v/11;
									Ext.MessageBox.updateProgress(i, '');
					            };
					   };
					   for(var i = 1; i < 13; i++){
							setTimeout(f(i), i*150);
					   }
					   /*****************************************************/
								   
                        Ext.Ajax.request({
	                        url : urlSave, 
	                        params : { action : "save",pkid: Ext.getDom("PKID").value, para1:msg},
	                        method: 'POST',
	                        success: function ( result, request ) { 
		                        Ext.MessageBox.alert('系统提示！','服务器返回信息：'+result.responseText); //result.responseText
		                        //if(result.responseText != "")
		                            //window.location="SyRoleMenu.aspx?id=" + Ext.getDom("PKID").value;
		                            
	                        },
	                        failure: function ( result, request) { 
		                        Ext.MessageBox.alert('设置失败！', '服务器处理发生错误。'); 
	                        } 
                        });

                    }else{
                        Ext.Msg.show({
                            title: '操作提示！', 
                            msg: msg.length > 0 ? msg : '请选择菜单项。',
                            icon: Ext.Msg.INFO,
                            minWidth: 150,
                            buttons: Ext.Msg.OK
                        });
                    }
                }
            }]
    		
	    }); 
	    tree.render();
	    tree.getRootNode().expand(true,false);
	
    }

    </script>
    
</head>
<body  style="text-align:center; padding:0px; margin:0px;color:#000000">
   <div id="divHead" class="divTitle"><span>角色名称：<label id="lblRoleName" runat="server"></label></span><input type="text" id="PKID" runat="server" style="display:none"/></div>
   <div id="tree-div" class="divContent" style="padding:0px; margin:0px;height:400px;  width:97%;text-align:left"></div>

</body>
</html>
