using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sys
{
    
    public class syMenu
    {

        private static string JSON_MENUSTRING_FORMAT = "treeNode: '{0}', pkid: '{1}', url: '{2}',code:'{3}'";

        /// <summary>
        /// 获取顶级栏目
        /// </summary>
        /// <returns></returns>
        public static string RenderParentMenu(string userID)
        {
            StringBuilder sbMenu = new StringBuilder();
            DataTable dtParentList = Parent(userID);
            foreach (DataRow  dr in dtParentList.Rows)
            {
                sbMenu.Append("|{");
                sbMenu.AppendFormat(JSON_MENUSTRING_FORMAT, new object[] { dr["name"], dr["pkid"], setParas(dr["func"], dr["PKID"]), dr["code"] });
                sbMenu.Append("}");
            }
            if (sbMenu.Length > 0)
            {
                return sbMenu.ToString().Substring(1);
            }
            return string.Empty;
        }
        private static DataTable Parent(string userID)
        {
            string sql = "SELECT DISTINCT Sy_Menu.*" +
            "  FROM Sy_Menu " +
            "  INNER JOIN Sy_RoleMnu [Mnu] ON Sy_Menu.PKID = [Mnu].Sy_MenuFK " +
            "  INNER JOIN Sy_UserRole [URole] ON [Mnu].Sy_RoleFK = [URole].Sy_RoleFK" +
            "  WHERE [URole].Sy_UserFK = '{0}' AND" +
            "  parentid is null" +
            "  ORDER BY Sy_Menu.SortNo,Sy_Menu.Code";
            sql = string.Format(sql, userID);

            return CSA.DAL.DBAccess.getRS(sql);
        }
        /// <summary>
        /// 设置菜单参数
        /// </summary>
        /// <param name="func"></param>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public static string setParas(object func, object pkid)
        {
            string[] paras = func.ToString().Split(new string[] { "&", "?" },
           StringSplitOptions.RemoveEmptyEntries);
            string type = "";
            foreach (string p in paras)
            {
                if (p.StartsWith("type=", StringComparison.CurrentCultureIgnoreCase))
                {
                    type = p.ToLower().Replace("type=", "");
                    break;
                }
            }
            if (!object.Equals(null, func) && !"".Equals(func))
            {
                if (func.ToString().LastIndexOf("?") > 0)
                {
                    func = string.Format("{0}&mid={1}&md5=" + BLL.Sys.security.getPageMD5(type, pkid.ToString(), func.ToString()), func, pkid);
                }
                else
                {
                    func = string.Format("{0}?mid={1}&md5=" + BLL.Sys.security.getPageMD5(type, pkid.ToString(), func.ToString()), func, pkid);
                }
            }
            return func.ToString();
        }
        
        public static string GetPkid(string code)
        {
            object obj= CSA.DAL.DBAccess.ExecuteScalar(string.Format("select pkid from sy_menu where code='{0}'", code));
            if (obj != null)
                return obj.ToString();
            else
                return null;
        }
    }
}
