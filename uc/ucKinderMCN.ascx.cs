using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucKinderMCN : System.Web.UI.UserControl
{
    public string code,pcode;
    public DataTable dtKind;
    protected void Page_Load(object sender, EventArgs e)
    {
        code = Com.Util.getStringByObject(Request["code"]);
        dtKind = CSA.DAL.DBAccess.getRS("select * from Bs_NewsKind where [status]=0 and code like '" + pcode + "%' and len(code)=4 order by sortno asc");
    }
}