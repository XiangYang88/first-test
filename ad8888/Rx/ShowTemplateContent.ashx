<%@ WebHandler Language="C#" Class="ShowTemplateContent" %>

using System;
using System.Web;

public class ShowTemplateContent : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string pkid = context.Request["pkid"];
        if (!string.IsNullOrEmpty(pkid))
        {
            string sql = "select content from bs_template where pkid='"+pkid+"'";
            string cnt = CSA.DAL.DBAccess.ExecuteScalar(sql).ToString();

            if (context.Request["type"]=="SMS")
            {
                cnt=CSA.Text.Util.removeHTML(cnt);
            }
            context.Response.Write(cnt);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}