<%@ WebHandler Language="C#" Class="UserEmailList" %>

using System;
using System.Web;
using System.Data;
using CSA.DAL;
using System.Text;
public class UserEmailList : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        //string sql = "select Email as id,name from bs_user where status='0'  and isnull(Email,'')<>''";
        string sql = "select id ,Bs_NewsKindCode name from bs_news where status='0'";
        DataTable dt=DBAccess.getRS(sql);
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
        //[
        //        {id: "10",name: "请选择中国苹果"},
        //        {id: "20",name: "请选择中国香蕉"},
        //        {id: "30",name: "请选择中国西瓜"},
        //        {id: "40",name: "请选择中国桃子"},
        //        {id: "50",name: "请选择中国葡萄"}
        //    ]
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}