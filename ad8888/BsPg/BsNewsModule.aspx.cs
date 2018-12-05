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

public partial class admin_BsPg_BsNewsModule : AdminPage
{

    protected string mcode = "";
    protected string pkid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
      checkUserGoLogin();
      mcode = Request.QueryString["mcode"];
      pkid = Sys.syMenu.GetPkid(mcode);
      mcode = mcode.Length >= 2 ? mcode.Substring(0, 2) : mcode;
    }
}
