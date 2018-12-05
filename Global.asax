<%@ Application Language="C#" %>
<script RunAt="server">

        void Application_Start(object sender, EventArgs e)
        {
            //在应用程序启动时运行的代码

        }

        void Application_End(object sender, EventArgs e)
        {
            //在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            //在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            //在新会话启动时运行的代码
        }

        void Session_End(object sender, EventArgs e)
        {
            //在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式 
            //设置为 StateServer 或 SQLServer，则不会引发该事件。

        }
        ///  <summary>          
        /// 在 Application_BeginRequest中加入函数StartProcessRequest()        
        ///  </summary>          
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            string curpage = Request.ServerVariables["SCRIPT_NAME"].ToLower();
            if (!curpage.Contains("ad8888"))
            {
                if (Request.Cookies != null)
                {
                    if (safe_360.CookieData())
                    {
                        Response.Redirect("/");
                        Response.End();

                    }

                }

                if (Request.UrlReferrer != null)
                {
                    if (safe_360.referer())
                    {
                        Response.Redirect("/");
                        Response.End();
                    }
                }

                if (Request.RequestType.ToUpper() == "POST")
                {
                    if (safe_360.PostData())
                    {

                        Response.Redirect("/");
                        Response.End();
                    }
                }
                if (Request.RequestType.ToUpper() == "GET")
                {
                    if (safe_360.GetData())
                    {
                        Response.Redirect("/");
                        Response.End();
                    }
                }
                // StartProcessRequest();
            }

        } 
</script>
