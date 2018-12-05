using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Com;
/// <summary>
/// Common 的摘要说明
/// </summary>
public class CurInfo
{
    //当前登录用户
    private static Bs_User _curUser;

    public static Bs_User CurUser
    {
        get
        {
            Bs_User user = HttpContext.Current.Session[Const.sessionNames[0]] as Bs_User;
            if (user == null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[Const.sessionNames[0]];
                int userid = 0;
                if (cookie != null)
                {
                    userid = Com.Util.getIntByObject(cookie.Value);
                }
                //int userid = Com.Util.getIntByObject(cookie.Value);// = Com.Util.getIntByObject(HttpContext.Current.Request.Cookies[Const.sessionNames[0]].Value);
                if (userid == 0)
                {
                    return null;
                }
                else {
                    user = new Bs_User();
                    Com.Util.ConvertToEntity(user,Com.Factory.getUserBllInstance().getDrUserByID(userid));
                    _curUser = user;
                    return user;
                }
            }
            
            return HttpContext.Current.Session[Const.sessionNames[0]] as Bs_User;
        }
        set
        {

            HttpContext.Current.Session[Const.sessionNames[0]] = value;

            Bs_User user = value as Bs_User;
            if (user != null)
            {
                //保存Cookie
                HttpCookie cookie = new HttpCookie(Const.sessionNames[0]);
                cookie.Value = user.ID.ToString();
                cookie.Expires = DateTime.Now.AddMinutes(60);  
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }


    public static string CurUrlPath
    {
        get
        {
            string strPath = HttpContext.Current.Request.Url.PathAndQuery;
            strPath = strPath.Substring(strPath.IndexOf("/", 1) + 1);
            return strPath;
        }
    }


    public static string CurUrlLocal
    {
        get
        {
            string strPath = HttpContext.Current.Request.Url.OriginalString;
            strPath = strPath.Substring(0,strPath.LastIndexOf("/") + 1 );
            return strPath;
        }
    }
    public static string CurUrlLocalNo
    {
        get
        {
            string strPath = HttpContext.Current.Request.Url.OriginalString;
            strPath = strPath.Substring(0, strPath.LastIndexOf("/"));
            return strPath;
        }
    }

}
