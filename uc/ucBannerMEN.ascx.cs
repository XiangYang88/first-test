﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucBannerMEN : System.Web.UI.UserControl
{
    public string pcode, banner;
    public string code, kindnameP_en;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtBanner = BLL.Ad.Ad.GetAdByAdKindCode(pcode);
        if (dtBanner != null && dtBanner.Rows.Count > 0)
        {
            banner = dtBanner.Rows[0]["pic_en_m"].ToString();
        }
        code = Com.Util.getStringByObject(Request["code"]);
        kindnameP_en = BLL.Article.Kind.getKindName(code.Substring(0, 2), "_en");
    }
}