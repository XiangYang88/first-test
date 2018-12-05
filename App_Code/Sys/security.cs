using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BLL.Sys
{
    /// <summary>
    ///security 的摘要说明
    /// </summary>
    public class security
    {
        public security()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static string getPageMD5(string type, string mid, string page)
        {
           
            if (mid.Equals("46f23c83-bfe4-4d63-9287-02f7a4151e6c", StringComparison.CurrentCultureIgnoreCase))
            {
                int ii = 0;
            }
            string p = page.ToLower();
            int start = 0;
            if (p.Contains("/"))
            {
                start = p.LastIndexOf("/")+1;
            }
            int end = p.IndexOf(".aspx");
            if (end >= 0)
            {
                p = p.Substring(start, end - start);
                string code = type + mid + p + BLL.User.AdminUser.UserID;
                code = code.ToLower();

                string md5 = CSA.Security.Encrypt.getMD5(code + "gssystem");
                return md5;
            }
            else
            {
                return "";
            }
        }
        public static bool checkPageMD5()
        {
            string type = HttpContext.Current.Request.QueryString["type"];
            string mid = HttpContext.Current.Request.QueryString["mid"];
            string page = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];
            string md5 = getPageMD5(type, mid, page);
            string cur_md5 = HttpContext.Current.Request.QueryString["md5"];
            return md5 == cur_md5;
        }
        
    }
}