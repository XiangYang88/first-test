<%@ WebHandler Language="C#" Class="Http_ImgLoad" %>

using System;
using System.Web;

using System.IO;

public class Http_ImgLoad : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
  int strW;
            int strH;
            if (int.TryParse(context.Request.Params["w"], out strW) && int.TryParse(context.Request.Params["h"], out strH))
            {
                context.Response.Clear();
                string mode = context.Request.Params["m"];
                string gurl = context.Request.Params["gurl"];
                if (!string.IsNullOrEmpty(gurl))
                {
                    if (File.Exists(context.Server.MapPath(gurl)))
                    {
                       // LoadImage.GenThumbnail(context.Request.Params["gurl"], strW, strH);
                        LoadImage.GenThumbnail(context.Request.Params["gurl"], strW, strH, mode);
                    }
                    else
                    {
                     //   LoadImage.GenThumbnail("/images/nopic.gif", strW, strH);
                        LoadImage.GenThumbnail("/images/onerror.jpg", strW, strH, mode);
                    }
                }
                else
                {
                    LoadImage.GenThumbnail("/images/onerror.jpg", strW, strH, mode);
                   // LoadImage.GenThumbnail("/images/nopic.gif", strW, strH);
                }
            }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
