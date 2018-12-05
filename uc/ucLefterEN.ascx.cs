using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucLefterEN : System.Web.UI.UserControl
{
    public string pcode, code;
    public DataTable dtKind;
    public DataTable dtProkind = new DataTable();
    public DataTable dtRecommendPro = new DataTable();
    public DataTable dtContact = new DataTable();
    public string kindname;
    protected void Page_Load(object sender, EventArgs e)
    {
        code = Com.Util.getStringByObject(Request["code"]);
        if (!string.IsNullOrEmpty(pcode))
        {
            dtKind = CSA.DAL.DBAccess.getRS("select * from Bs_NewsKind where [status]=0 and code like '" + pcode + "%' and len(code)=4 order by sortno asc");
            kindname = BLL.Article.Kind.getKindName(code.Substring(0, 2), "_en");
        }
        else
        {
            dtProkind = BLL.Product.Kind.getList("", 0);
        }
        dtRecommendPro = CSA.DAL.DBAccess.getRS("select * from [Bs_Products] where [Status]=0  and isindex=1 order by SortNo desc, AddTime desc");
        dtContact = BLL.Article.Article.getArticle("0401", 1);
    }
}