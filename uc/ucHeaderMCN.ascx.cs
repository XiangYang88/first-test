using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucHeaderMCN : System.Web.UI.UserControl
{
    public string code, kindnameP,pcode;
    protected string kindname01, kindname02, kindname03, kindname04, kindname06, kindname07;
    protected DataTable dtKind, dtProkind;

    protected void Page_Load(object sender, EventArgs e)
    {
        code = Com.Util.getStringByObject(Request["code"]);
        if (!string.IsNullOrEmpty(pcode))
        {
            dtKind = CSA.DAL.DBAccess.getRS("select * from Bs_NewsKind where [status]=0 and code like '" + pcode + "%' and len(code)=4 order by sortno asc");
            kindnameP = BLL.Article.Kind.getKindName(code.Substring(0, 2));
        }
        else
        {
            dtProkind = BLL.Product.Kind.getList("", 0);
            kindnameP = "产品中心";
        }

        kindname01 = BLL.Article.Kind.getKindName("01");
        kindname02 = BLL.Article.Kind.getKindName("02");
        kindname03 = BLL.Article.Kind.getKindName("03");
        kindname04 = BLL.Article.Kind.getKindName("04");
        kindname06 = BLL.Article.Kind.getKindName("06");
        kindname07 = BLL.Article.Kind.getKindName("07");

    }
}