using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ad8888_BsPg_getTreeData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////Response.Write("[{id:26,text:'关于科耐',singleClickExpand:false,children:[{id:27,text:'关于科耐',singleClickExpand:false,leaf:true}]},{id:28,text:'产品世界',singleClickExpand:false,children:[{id:29,text:'产品世界',singleClickExpand:false,leaf:true}]},{id:30,text:'新闻中心',singleClickExpand:false,children:[{id:31,text:'新闻中心',singleClickExpand:false,leaf:true}]},{id:32,text:'技术支持',singleClickExpand:false,children:[{id:33,text:'技术支持',singleClickExpand:false,leaf:true}]},{id:34,text:'合作加盟',singleClickExpand:false,children:[{id:35,text:'合作加盟',singleClickExpand:false,leaf:true}]},{id:36,text:'联系我们',singleClickExpand:false,children:[{id:37,text:'联系我们',singleClickExpand:false,leaf:true}]},{id:44,text:'人才招聘',singleClickExpand:false,children:[{id:45,text:'人才招聘',singleClickExpand:false,leaf:true}]},{id:46,text:'客户留言',singleClickExpand:false,children:[{id:47,text:'客户留言',singleClickExpand:false,leaf:true}]},{id:49,text:'友情链接',singleClickExpand:false,leaf:true},{id:51,text:'合作与共赢',singleClickExpand:false,leaf:true}]");
        //Response.Write("[{id:27,text:'关于科耐',singleClickExpand:false,leaf:true},{id:28,text:'品牌',singleClickExpand:false,leaf:true}]");
        Response.Write("[{id:'BsPg/BsNewsList.aspx?type=del&pcode=01&mid=91cf8895-cce5-43f7-a07f-bb37dcba1d92',text:'南方黑芝麻集团',url:'BsPg/BsNewsList.aspx?type=del&pcode=01&mid=91cf8895-cce5-43f7-a07f-bb37dcba1d92',leaf:true,icon:''},{id:'BsPg/BsNewsList.aspx?type=del&pcode=02&mid=473f0f41-f0d2-4c46-841a-68c855d84867',text:'品牌文化',url:'BsPg/BsNewsList.aspx?type=del&pcode=02&mid=473f0f41-f0d2-4c46-841a-68c855d84867',leaf:true,icon:''}]");
    }
}