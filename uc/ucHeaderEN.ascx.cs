using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucHeaderEN : System.Web.UI.UserControl
{
    protected string kindname01, kindname02, kindname03, kindname04, kindname06, kindname07;
    public DataTable dtkind01, dtkind02, dtkind03, dtkind04, dtkind07, dtkindPro;
    protected void Page_Load(object sender, EventArgs e)
    {
        kindname01 = BLL.Article.Kind.getKindName("01","_en");
        kindname02 = BLL.Article.Kind.getKindName("02", "_en");
        kindname03 = BLL.Article.Kind.getKindName("03", "_en");
        kindname04 = BLL.Article.Kind.getKindName("04", "_en");
        kindname06 = BLL.Article.Kind.getKindName("06", "_en");
        kindname07 = BLL.Article.Kind.getKindName("07", "_en");

        dtkind01 = BLL.Article.Kind.getKind("01", 2);
        dtkindPro = BLL.Product.Kind.getList("", 2);
        dtkind02 = BLL.Article.Kind.getKind("02", 2);
        dtkind03 = BLL.Article.Kind.getKind("03", 2);
        dtkind04 = BLL.Article.Kind.getKind("04", 2);
        dtkind07 = BLL.Article.Kind.getKind("07", 2);
    }
}