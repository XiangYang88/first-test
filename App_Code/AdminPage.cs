using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using CSA.DAL;

/// <summary>
/// BasePage 的摘要说明
/// </summary>
public class AdminPage : Page
{
    public string Def_Page = "default.aspx";
    /// <summary>
    /// 页面初始化，简单检查权限
    /// </summary>
    public AdminPage()
    {
        if (!Page.IsPostBack)
        {
            //return;//开发中先通过检查
            string uid = BLL.User.AdminUser.UserID;
            string mid = HttpContext.Current.Request.QueryString["mid"];
            string page = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToLower();
            if (!page.EndsWith("server.aspx") && !page.EndsWith("syuseritem.aspx") && !page.EndsWith("syrolemenu.aspx"))
            {
                if (!BLL.User.AdminUser.checkUserRole(mid, uid))
                {
                    HttpContext.Current.Response.Write("参数错误,或者你没有此页面的访问权限");
                    HttpContext.Current.Response.End();
                }
                else
                {

                    if (!BLL.Sys.security.checkPageMD5())
                    {
                        HttpContext.Current.Response.Write("参数错误,或者你没有此页面的访问权限");
                        HttpContext.Current.Response.End();
                    }
                }
            }
        }
    }
       
    #region 页面操作

    public void Alert(string str)
    {
        Response.Write("<script language='javascript'>alert('" + str.Replace("\"", "") + "')</script>");
    }

    public void AlertAndGoBack(string str)
    {
        Response.Write("<script language='javascript'>alert('" + str.Replace("\"", "") + "');history.go(-1);</script>");
        Response.End();
    }

    public void AlertAndRedirect(string str, string url)
    {
        Response.Write("<script language='javascript'>alert('" + str.Replace("\"", "") + "');window.location='" + url + "'</script>");
        Response.End();
    }
   
    public void goUrl(string url)
    {
        Response.Write("<script language='javascript'>window.parent.location='" + url + "'</script>");
        Response.End();
        //Response.Redirect(url);
        //Response.End();
    }

    public void WriteFunc(string func)
    {
        Response.Write("<script language='javascript'>" + func + "</script>");
        Response.End();
    }

    public void WriteEnd(string msg)
    {
        Response.Write(msg);
        Response.End();
    }
    public void Write(string msg)
    {
        Response.Write(msg);
    }

    #region JS提示============================================

    /// <summary>
    /// 添加编辑删除提示
    /// </summary>
    /// <param name="msgtitle">提示文字</param>
    /// <param name="url">返回地址</param>
    /// <param name="msgcss">CSS样式</param>
    protected void JscriptMsg(string msgtitle, string url, string msgcss)
    {
        string msbox = "parent.jsprint(\"" + msgtitle.Replace("\r\n","") + "\", \"" + url + "\", \"" + msgcss + "\")";
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
    }

    /// <summary>
    /// 带回传函数的添加编辑删除提示
    /// </summary>
    /// <param name="msgtitle">提示文字</param>
    /// <param name="url">返回地址</param>
    /// <param name="msgcss">CSS样式</param>
    /// <param name="callback">JS回调函数</param>
    protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
    {
        string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
    }
    #endregion

    #endregion

    /// <summary>
    /// 获取链接，并加入当将的 mid,type参数
    /// </summary>
    /// <param name="linkpage">链接的页面</param>
    /// <param name="paras">参数，如 C_Field=Value</param>
    /// <returns></returns>
    public string getLinkWidthBaseParas(string linkpage,string[] paras)
    {
        if (string.IsNullOrEmpty(linkpage))
        {
            linkpage = CSA.HC.Common.getPageName();
        }
        HttpContext context = HttpContext.Current;
        string mid = context.Request.QueryString["mid"];
        string type = context.Request.QueryString["type"];
        linkpage += string.Format("?mid={0}",mid);
        bool existsType = false;
        foreach (string str in paras)
        {
            if (str.StartsWith("type=", StringComparison.OrdinalIgnoreCase)&&!existsType)
            {
                existsType = true;
                type = str.Replace("type=", "").Replace("Type=", "").Replace("TYPE=", "");
                continue;
            }
            linkpage += "&" + str;
        }
        string md5 = BLL.Sys.security.getPageMD5(type, mid, linkpage);
        linkpage += "&type=" + type + "&md5=" + md5;
        return linkpage;
    }
    /// <summary>
    /// 通过 用户角色/控件权限 来显示关键控件
    /// </summary>
    public void setControlRole()
    {
        string uid = BLL.User.AdminUser.UserID; 
        string sql = "select a.controlID from sy_control a,sy_rolectrl b,sy_role c,sy_userrole d " +
            "where a.pkid=b.sy_controlPKID and b.sy_rolePKID=c.pkid and c.pkid=d.Sy_RoleFK and d.Sy_UserFK='"+uid+"'";
        DataTable dt = CSA.DAL.DBAccess.getRS(sql);
        foreach (DataRow dr in dt.Rows)
        {
            Control ctrl = Page.FindControl(dr["controlID"].ToString());
            if (ctrl != null)
            {
                ctrl.Visible = true;
            }
        }
    }
    /// <summary>
    /// 获取链接，并加入当将的 mid,type参数
    /// </summary>
    /// <param name="linkpage">链接的页面</param>
    /// <param name="paras">参数，如 C_Field=Value</param>
    /// <returns></returns>
    public string getLinkWidthBaseParas(string linkpage, string field,string value)
    {
        return getLinkWidthBaseParas(linkpage, new string[] { field+"="+value});
    }
    /// <summary>
    /// 从URL里面获取查询条件, 条件参数必须以"C_"开头，如 C_Field=Value,
    /// </summary>
    /// <returns></returns>
    public string getConditionsByQueryString()
    {
        HttpContext context=HttpContext.Current;
        string where = " 1=1 ";
        foreach (string key in context.Request.QueryString.AllKeys)
        {
            if (key.StartsWith("C_"))
            {
                string field = key.Substring(2);
                string value = context.Request.QueryString[key];
                where += string.Format(" and {0}={1}",field,value);
            }
            else if (key.StartsWith("L_"))
            {
                string field = key.Substring(2);
                string value = context.Request.QueryString[key];
                where += string.Format(" and {0} like '{1}%'", field, value);
            }
            else if (key.StartsWith("S_"))
            {
                string field = key.Substring(2);
                string value = context.Request.QueryString[key];
                where += string.Format(" and {0}='{1}'", field, value);
            }
        }
        return where;
    }
    /// <summary>
    /// 验证用户是否登陆
    /// </summary>
    /// <returns></returns>
    public bool checkUserGoLogin()
    {
        if (!BLL.User.AdminUser.isLogin())
        {
           
            goUrl("../login.aspx");
            return false;
        }
        return true;
    }
    /// <summary>
    /// 是否为开发管理员
    /// </summary>
    /// <returns></returns>
    public bool IsRoot()
    {
        return BLL.User.AdminUser.IsRoot();
    }
   
    /// <summary>
    /// 获取ViewState
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string getViewState(string key)
    {
        if (ViewState[key] != null)
        {
            return ViewState[key].ToString();
        }
        return "";
    }
    /// <summary>
    /// 设置ViewState
    /// </summary>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public void setViewState(string key, string val)
    {
        ViewState[key] = val;
    }
    /// <summary>
    /// 获取删除的图片
    /// </summary>
    /// <param name="tblName">表明</param>
    /// <param name="where">查询条件</param>
    /// <param name="picRowName">存放图片或媒体列名</param>
    /// <returns></returns>
    protected string GetDelImg(string tblName, string where, string picRowName)
    {

        StringBuilder builder = new StringBuilder();
        try
        {
            if (where.Length > 0)
            {
                DataTable dt = DBAccess.getRS(Util.buildListSQL(tblName, where, "id"));
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        builder.Append(dr[picRowName] + ",");
                    }
                }
            }
        }
        catch (Exception)
        {
            return null;
        }
        return builder.ToString();
    }
    /// <summary>
    /// 删除图片
    /// </summary>
    /// <param name="filePath">要删除的文件路径</param>
    /// <param name="uploadFilePath">上传文件保存的路径，可能有大小图文件夹 </param>
    protected void DeleteImg(string filePath, string[] uploadFilePath)
    {
        try
        {
            string[] saveFlod = uploadFilePath;//上传文件的路径
            if (filePath.Length > 0)
            {
                string[] img = filePath.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (img.Length > 0)
                {
                    foreach (string item in img)
                    {
                        foreach (string flod in saveFlod)
                        {
                            string path = Server.MapPath("~" + flod + item);
                            File.Delete(path);
                        }


                    }

                }


            }
        }
        catch (Exception)
        {

        }

    }

    protected bool multiLanguageEN = Common.Utils.MultiLanguageEN();
    protected bool multiLanguageRU = Common.Utils.MultiLanguageRU();
}
