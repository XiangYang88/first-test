<%@ WebHandler Language="C#" Class="UserMobileList" %>

using System;
using System.Web;
using CSA.DAL;
using System.Data;
using System.Text;
public class UserMobileList : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string sql = "select mobile as id,name from bs_user where status='0' and isnull(mobile,'')<>''";
        DataTable dt = DBAccess.getRS(sql);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("[");
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("{id:\"" + dr["id"].ToString() + "\",name:\"" + dr["name"].ToString() + "\"}");
            if (i != dt.Rows.Count - 1)
            {
                sb.AppendLine(",");
            }
            i++;
        }
        sb.AppendLine("]");
        context.Response.Write(sb.ToString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}