using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucFooterMCN : System.Web.UI.UserControl
{
    protected string kindname01, kindname02, kindname03, kindname04, kindname06, kindname07;
    protected void Page_Load(object sender, EventArgs e)
    {
        kindname01 = BLL.Article.Kind.getKindName("01");
        kindname02 = BLL.Article.Kind.getKindName("02");
        kindname03 = BLL.Article.Kind.getKindName("03");
        kindname04 = BLL.Article.Kind.getKindName("04");
        kindname06 = BLL.Article.Kind.getKindName("06");
        kindname07 = BLL.Article.Kind.getKindName("07");
    }
}