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
//using CSA;

public partial class sys_SyRoleMenu : AdminPage
{
    string TblName = "Sy_Role";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string id = Request["id"];  //Role id
                this.PKID.Value = id;
                lblRoleName.InnerHtml = Convert.ToString(getColumnItemByPKID(TblName, id, "Code,Name"));
            }
        }
        catch { }
    }
    /// <summary>
    /// 取表的列数据，可按PKID， if pkid is null then get top one in datasource
    /// </summary>
    /// <param name="tblName"></param>
    /// <param name="pkid"></param>
    /// <param name="colName"></param>
    /// <returns></returns>
    public static object getColumnItemByPKID(string tblName, object pkid, string colNames)
    {
        try
        {
            if (string.IsNullOrEmpty(colNames)) return "";

            string[] _cols = colNames.Split(',');
            if (_cols.Length > 1)
            {
                colNames = "";
                foreach (string _s in _cols)
                {
                    colNames += string.Format("+','+ISNULL({0},'')", _s);
                }
                if (colNames.Length > 0) colNames = colNames.Substring(5);
            }

            if (object.Equals(null, pkid) || "".Equals(pkid))
            {
                colNames = " TOP 1 " + colNames;
            }
            else
            {
                pkid = " WHERE PKID='" + pkid.ToString() + "'";
            }
            string sql = string.Format("SELECT {2} FROM {0} {1}", tblName, pkid, colNames);
            return CSA.DAL.DBAccess.ExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            throw new Exception("还没有进行网站配置设置\r\n" + ex.Message);
        }
    }
}
