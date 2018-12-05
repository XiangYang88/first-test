using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CSA.DAL;
public partial class admin_sys_DbExec : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUserGoLogin();
        if (!Page.IsPostBack)
        {
            string type = Request.QueryString["type"];
            string sql = "";
            switch (type)
            {
                case "log":
                    sql = "DUMP TRANSACTION " + SqlHelper.getDataBaseName() + " WITH NO_LOG";
                    break;
                case "compress":
                    sql="DBCC SHRINKDATABASE("+SqlHelper.getDataBaseName()+")";
                    break;
            }
            this.txtSQL.Text = sql;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        string sql = txtSQL.Text.Trim();
        bool select = false;
        if (sql.StartsWith("select"))
        {
            select = true;
        }
        if (sql.StartsWith("Select"))
        {
            select = true;
        }
        if (sql.StartsWith("SELECT"))
        {
            select= true;
        }
        BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, "", null, "执行SQL", sql);
        if (select)
        {
            DataTable dt = DBAccess.getRS(sql);
            this.gvList.DataSource = dt;
            this.gvList.DataBind();
        }
        else
        {
            JscriptMsg("执行成功,影响行数:" + DBAccess.ExecuteNonQuery(sql), "", "Success");
          
        }
    }
    /// <summary>
    /// 页面呈现前事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_PreRender(object sender, EventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}
