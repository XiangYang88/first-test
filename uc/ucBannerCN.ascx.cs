using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucBannerCN : System.Web.UI.UserControl
{
    public string pcode, banner,url;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtBanner = BLL.Ad.Ad.GetAdByAdKindCode(pcode);
        if (dtBanner != null && dtBanner.Rows.Count > 0)
        {
            banner = dtBanner.Rows[0]["pic"].ToString();
            url = dtBanner.Rows[0]["url"].ToString();
        }
    }
}