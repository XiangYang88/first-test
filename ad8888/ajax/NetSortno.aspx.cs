
using CSA.DAL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ad8888_ajax_NetSortno : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string tblName=Request.Form["tblName"];
        string FKField=Request.Form["FKField"];
        string pc=Request.Form["pc"];
       Response.Write(Util.getNextSortNo(tblName, "SortNo",pc != "" ? string.Format(FKField + "='{0}'", pc) : pc).ToString());
       Response.End();
    }
}