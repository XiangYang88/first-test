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

namespace BLL.User
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class AdminUser
    {
        public AdminUser()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 验证用户角色的权限
        /// </summary>
        /// <param name="mnuid"></param>
        public static bool checkUserRole(string mnuid, string uid)
        {
            if (string.IsNullOrEmpty(mnuid))
            {   
                return false;
            }
            try
            {
                string sql = "SELECT count(*)" +
	                        " FROM Sy_RoleMnu INNER JOIN"+
	                        " Sy_Role ON Sy_RoleMnu.Sy_RoleFK = Sy_Role.PKID INNER JOIN"+
	                        " Sy_menu on Sy_RoleMnu.Sy_menufk = sy_menu.pkid INNER JOIN"+
	                        " Sy_UserRole on Sy_Role.PKID = sy_userrole.sy_rolefk inner join"+
	                        " sy_user on sy_userrole.sy_userfk = sy_user.pkid"+
	                        " where Sy_User.PKID = '{0}' and"+
                            " Sy_RoleMnu.Sy_menufk = '{1}'";

                sql = string.Format(sql,uid, mnuid);
                string iR = DBAccess.ExecuteScalar(sql).ToString();
                if (iR != "0") return true;
                else return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public const string KEY = "admin_id";
        public static bool verifyUser(string uid, string pwd)
        {
            //if (CSA.DAL.DBAccess.getDbType() == CSA.DAL.ConnEnum.MSSQL)
            //    CSA.DAL.DBAccess.BackUp(false, null);
            string sql = "select * from sy_user where code='" + CSA.Text.Util.getSqlStr(uid) + "'";
            DataTable dt = CSA.DAL.DBAccess.getRS(sql);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (CSA.Security.Encrypt.verifyMD5(pwd,dr["password"].ToString()))
                {
                    CSA.HC.CookiesHelper.set(KEY, uid, 1);
                    CSA.HC.CookiesHelper.set(KEY+"_pkid", dr["pkid"].ToString(), 1);
                    CSA.HC.CookiesHelper.set("admininfo", CSA.Security.Encrypt.getMD5(uid + dr["password"].ToString()), 1);
                    BLL.Sys.AdminLog.AddLogin();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取登录用户的信息
        /// </summary>
        /// <returns></returns>
        public static DataRow getLoginInfo()
        {
            DataTable dt=getAdminDetail(getLoginName());
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
        }
        /// <summary>
        /// 登陆的User PKID
        /// </summary>
        public static string UserID
        {
            get
            {
                return CSA.HC.CookiesHelper.get(KEY + "_pkid");
            }
        }
        /// <summary>
        /// 登陆的User Name
        /// </summary>
        public static string UserName
        {
            get
            {
                return CSA.HC.CookiesHelper.get(KEY);
            }
        }
        /// <summary>
        /// 获取管理员的具体信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable getAdminDetail(string name)
        {
            return CSA.DAL.DBAccess.getRS("select * from sy_user where code='" +name+ "'");
        }
        public static string getLoginName()
        {
            return CSA.HC.CookiesHelper.get(KEY);
        }
        public static bool logout()
        {
            Sys.AdminLog.AddLogout();
            CSA.HC.CookiesHelper.clear(KEY);
            CSA.HC.CookiesHelper.clear(KEY+"_pkid");
            CSA.HC.CookiesHelper.clear("admininfo");
            return true;
        }
        public static bool isLogin()
        {
            

            string uid = CSA.HC.CookiesHelper.get(KEY);
            if (uid != "")
            {
                DataTable dt= CSA.DAL.DBAccess.getRS("select * from sy_user where code='" + uid + "'");
                if (dt.Rows.Count>0)
                {
                    string admininfo = CSA.HC.CookiesHelper.get("admininfo");
                    if (admininfo == CSA.Security.Encrypt.getMD5(uid + dt.Rows[0]["password"].ToString()))
                    {
                        CSA.HC.CookiesHelper.set(KEY, uid, 1);
                        CSA.HC.CookiesHelper.set(KEY+"_pkid", dt.Rows[0]["pkid"].ToString(), 1);
                        CSA.HC.CookiesHelper.set("admininfo", admininfo, 1);
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 是否为开发管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsRoot()
        {
            DataTable dt=CSA.DAL.DBAccess.getRS(string.Format("select Sy_Role.code from sy_user left join Sy_UserRole on sy_user.pkid=Sy_UserRole.sy_userFk left join Sy_Role on Sy_Role.pkid=Sy_UserRole.sy_roleFK where sy_user.pkid='{0}'",UserID));
            if (dt.Rows.Count > 0)
            {
              return dt.Rows[0][0].ToString() == "99";
            }
            return false;
        }
    }

}