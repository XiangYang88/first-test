using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucHeaderCN : System.Web.UI.UserControl
{
    public string code;
    protected string kindname01, kindname02, kindname03, kindname04, kindname06, kindname07;
    public DataTable dtkind01, dtkind02, dtkind03, dtkind04,dtkind07, dtkindPro;
    protected DataRow drKind05, drKind06;
    protected void Page_Load(object sender, EventArgs e)
    {
        drKind05 = BLL.Article.Kind.getKind("05", 1).Rows[0];
        drKind06 = BLL.Article.Kind.getKind("06", 1).Rows[0];
        kindname01 = BLL.Article.Kind.getKindName("01");
        kindname02 = BLL.Article.Kind.getKindName("02");
        kindname03 = BLL.Article.Kind.getKindName("03");
        kindname04 = BLL.Article.Kind.getKindName("04");
        kindname06 = BLL.Article.Kind.getKindName("06");
        kindname07 = BLL.Article.Kind.getKindName("07");

        dtkind01 = BLL.Article.Kind.getKind("01", 2);
        dtkindPro = BLL.Product.Kind.getList("", 2);
        dtkind02 = BLL.Article.Kind.getKind("02", 2);
        dtkind03 = BLL.Article.Kind.getKind("03", 2);
        dtkind04 = BLL.Article.Kind.getKind("04", 2);
        dtkind07 = BLL.Article.Kind.getKind("07", 2);
    }
}