using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class admin_BsPg_uploadIframe : System.Web.UI.Page
{
    public string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
       if (!BLL.User.AdminUser.isLogin())
        {
            Response.End();
        }
        id=Request.QueryString["id"];
        if (!Page.IsPostBack)
        {
            string pathlist = Request.QueryString["pathlist"];
            string filelist = Request.QueryString["filelist"];
            string multi = Request.QueryString["multi"];
            string isWatermark = Request.QueryString["isWatermark"];
		
            if (!string.IsNullOrEmpty(pathlist))
            {
                this.upFile.PathList = pathlist.Split(new string[] {","},StringSplitOptions.RemoveEmptyEntries);
            }
            if (!string.IsNullOrEmpty(filelist))
            {
                this.upFile.FileList=filelist;
            }
            if (!string.IsNullOrEmpty(multi))
            {
                this.upFile.Multi = bool.Parse(multi);
            }
            if (!string.IsNullOrEmpty(isWatermark))
            {
                this.upFile.IsWatermark = bool.Parse(isWatermark);
            }
			

        }
    }
}
