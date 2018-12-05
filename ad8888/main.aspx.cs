using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ad8888_main : System.Web.UI.Page
{
    public string MenuString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!BLL.User.AdminUser.isLogin())
        {
            Response.Redirect("login.aspx");
        }

        MenuString = Sys.syMenu.RenderParentMenu(BLL.User.AdminUser.UserID);
        if(MenuString.Length<=0)
            CSA.HC.Common.AlertAndRedirect( "对不起，你没有任何系统权限或登录超时，请重新登录或联系系统管理员开通权限!", "login.aspx");
    }
    
   
}