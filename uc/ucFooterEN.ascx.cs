using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_ucFooterEN : System.Web.UI.UserControl
{
    public DataTable dtQQ = new DataTable();
    public DataTable dtLinks = new DataTable();
    protected string kindname01, kindname02;
    public DataTable dtkind01, dtkind02, dtkindPro, dtContact;
    public string skinnum;
    public string pcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        dtQQ = BLL.Article.Article.getArticle("05", 0);
        skinnum = BLL.Sys.Config.getConfigVal("skinNum");
        dtLinks = CSA.DAL.DBAccess.getRS("select top 6 * from bs_links where Bs_LinksKindCode='01' and [status]=0 order by sortno desc");

        kindname01 = BLL.Article.Kind.getKindName("01","_en");
        kindname02 = BLL.Article.Kind.getKindName("02", "_en");

        dtkind01 = BLL.Article.Kind.getKind("01", 2);
        dtkindPro = BLL.Product.Kind.getList("", 2);
        dtkind02 = BLL.Article.Kind.getKind("02", 2);

        dtContact = BLL.Article.Article.getArticle("0401", 1);
    }
}