using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ad8888_main_center : System.Web.UI.Page
{
    protected DataRow drConfig = null;
    protected DataRow admin_info = null;
    protected HttpBrowserCapabilities bc = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            drConfig = BLL.Sys.Config.getConfig().Rows[0];
            admin_info = BLL.User.AdminUser.getLoginInfo();
            bc = Request.Browser;
        }
        
    }
}