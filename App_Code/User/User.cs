using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using CSA.DAL;

namespace BLL.BsUser
{
    /// <summary>
    ///User 的摘要说明
    /// </summary>
    public class User
    {
        public User()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 增加一个用户
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool AddUser(string sql)
        {
            return CSA.DAL.DBAccess.ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 登陆并保持当前用户信息到session key:currentUser
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static bool login(string loginName, string pwd)
        {
            pwd = CSA.Security.Encrypt.getMD5(pwd);
            string sql = string.Format("select * from bs_user where Name = '{0}' and password='{1}' and status=0 ", loginName, pwd);
            DataTable dt = CSA.DAL.DBAccess.getRS(sql);
            if (dt != null && dt.Rows.Count != 0)
            {
                LoginLog(dt.Rows[0]["id"].ToString());
                CSA.HC.SessionHelper.set("currentUser", dt.Rows[0]);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 按名称查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataRow GetUserByName(string name)
        {
            string sql = string.Format("select * from bs_User where Name = '{0}'", name);
            DataTable dt = CSA.DAL.DBAccess.getRS(sql);
            if (dt != null && dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;
        }

        /// <summary>
        /// 按用户的ＩＤ来获得用户当前可用积分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetScoreByUserId(int userId)
        {
            string sql = string.Format("select UsableScore from bs_user where id = {0}", userId);
            return Convert.ToInt32(CSA.DAL.DBAccess.ExecuteScalar(sql));
        }
        public static void LoginLog(string id)
        {
            string sql = string.Format("update bs_user set lcount=isnull(lcount,0)+1,ltime='{2}',lip='{0}' where id={1}", CSA.HC.Common.getIP(), id, System.DateTime.Now.ToString("s"));
            CSA.DAL.DBAccess.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="pwd">新密码</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static bool ModPwd(string pwd, string userid)
        {
            string sql = "update bs_user set password ='{0}' where id={1}";
            sql = string.Format(sql, CSA.Security.Encrypt.getMD5(pwd), userid);
            if (CSA.DAL.DBAccess.ExecuteNonQuery(sql) > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 验证旧密码是否正确
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static bool cheOldPwd(string oldPwd, string userid)
        {
            string sql = "select count(*) from bs_user where id={0} and password='{1}'";
            sql = string.Format(sql, userid, CSA.Security.Encrypt.getMD5(oldPwd));
            if (Convert.ToInt32(CSA.DAL.DBAccess.ExecuteScalar(sql)) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取用户类型
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static string GetUserGrade(string gradeCode)
        {
            string sql = "select name from bs_usergrade where code='{0}'";
            sql = string.Format(sql, gradeCode);
            return DBAccess.getRS(sql).Rows[0][0].ToString();
        }
        public static decimal GetDisCount()
        {

            DataRow dr = (DataRow)HttpContext.Current.Session["currentUser"];
            string sql = "select discount from bs_usergrade where code = '" + dr["bs_usergradeCode"].ToString() + "'";
            DataRow drUserInfo = CSA.DAL.DBAccess.getRS(sql).Rows[0];
            return Convert.ToDecimal(drUserInfo["discount"]);

        }
        /// <summary>
        /// 忘密码
        /// </summary>
        /// <param name="uid">用户名</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public static bool fotgotPwd(string uid, string email)
        {
            string sql = "select name from bs_user where name='{0}' and email='{1}'";
            sql = string.Format(sql, uid, email);
            DataTable re = CSA.DAL.DBAccess.getRS(sql);
            if (re.Rows.Count > 0)
            {

                Random rnd = new Random();
                string newPwd = rnd.Next(100000, 999999).ToString();
                string sql1 = "update bs_user set password='{0}' where name='{1}'";
                sql1 = string.Format(sql1, CSA.Security.Encrypt.getMD5(newPwd), uid);
                if (CSA.DAL.DBAccess.ExecuteNonQuery(sql1) > 0)
                {
                    CSA.Net.Email emai = new CSA.Net.Email();
                    emai.Title=BLL.Sys.Config.getConfigVal("SiteTitle")+" 找回密码！";
                    emai.MailTo=new string[]{email};
                    emai.Content="您好，系统已经为您重置密码，新密码为:" + newPwd;
                    return emai.Send();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
       

    }
}
