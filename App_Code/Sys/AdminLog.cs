using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CSA.DAL;
using CSA.Control;
namespace BLL.Sys
{
    /// <summary>
    /// 后台管理员的日志记录
    /// </summary>
    public class AdminLog
    {
        public AdminLog()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="menuid">菜单ID</param>
        /// <param name="tblName">操作表名</param>
        /// <param name="pkid">主键</param>
        /// <param name="type">操作类型,如添加,删除,修改,批量操作</param>
        /// <param name="notes">备注</param>
        public static void AddLog(string menuid,string menuName,string tblName,string pkid,string type,string notes)
        {   
            SQLBuilder builder = new SQLBuilder("Sy_Log");
            builder.AddData("uid", BLL.User.AdminUser.UserID);
            builder.AddData("username", BLL.User.AdminUser.getLoginName());
            ////通过 menuid　获取当前操作页内容
            //if (!string.IsNullOrEmpty(pkid))
            //{
            //    builder.AddData("menuID", menuid);
            //    object menu = CSA.DAL.DBAccess.ExecuteScalar(
            //        "select name from sy_menu where pkid='" + menuid + "'");
            //    if(menu!=null)
            //        builder.AddData("menu",menu.ToString());
            //}
            builder.AddData("menu", menuName);
            builder.AddData("tblName", tblName);
            if(!string.IsNullOrEmpty(pkid))
                builder.AddData("pkid", pkid);
            builder.AddData("ip", CSA.HC.Common.getIP());
            builder.AddData("type", type);
            builder.AddData("time", DateTime.Now.ToString());
            builder.AddData("notes", notes);
            builder.AutoInsert();
        }
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="menuid">菜单ID</param>
        /// <param name="tblName">操作表名</param>
        /// <param name="pkid">主键</param>
        /// <param name="type">操作类型,如添加,删除,修改,批量操作</param>
        public static void AddLog(string menuid, string menuName, string tblName, string pkid, string type)
        {
            SQLBuilder builder = new SQLBuilder("Sy_Log");
            builder.AddData("uid", BLL.User.AdminUser.UserID);
            builder.AddData("username", BLL.User.AdminUser.getLoginName());
            ////通过 menuid　获取当前操作页内容
            //if (!string.IsNullOrEmpty(pkid))
            //{
            //    builder.AddData("menuID", menuid);
            //    object menu = CSA.DAL.DBAccess.ExecuteScalar(
            //        "select name from sy_menu where pkid='" + menuid + "'");
            //    if(menu!=null)
            //        builder.AddData("menu",menu.ToString());
            //}
            builder.AddData("menu", menuName);
            builder.AddData("tblName", tblName);
            if (!string.IsNullOrEmpty(pkid))
                builder.AddData("pkid", pkid);
            builder.AddData("ip", CSA.HC.Common.getIP());
            builder.AddData("type", type);
            builder.AddData("time", DateTime.Now.ToString());
            builder.AutoInsert();
        }
        /// <summary>
        /// 添加登陆日志
        /// </summary>
        public static void AddLogin()
        {
            string sql = "insert into sy_login(uid,username,ip,type) values('{0}','{1}','{2}','登录')";
            sql = string.Format(sql, BLL.User.AdminUser.UserID, BLL.User.AdminUser.getLoginName(),
                HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            CSA.DAL.DBAccess.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 添加退出日志
        /// </summary>
        public static void AddLogout()
        {
            string sql = "insert into sy_login(uid,username,ip,type) values('{0}','{1}','{2}','退出')";
            sql = string.Format(sql, BLL.User.AdminUser.UserID, BLL.User.AdminUser.getLoginName(),
                HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            CSA.DAL.DBAccess.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 最后登陆日志
        /// </summary>
        /// <param name="upkid"></param>
        /// <returns></returns>
        public static DataRow LastLog(string upkid)
        {
            string sql = "select top 1 from sy_login where uid='{0}' order by time desc";
            sql = string.Format(sql, upkid);
            DataTable dtTem = CSA.DAL.DBAccess.getRS(sql);
            if (null != dtTem && dtTem.Rows.Count > 0)
                return dtTem.Rows[0];
            else
                return null;
        }
    }
}