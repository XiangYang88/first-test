using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class admin_Rx_Menu : Page
{
    StringBuilder sbs = null;
    string ChkBoxParas = "", type="",menutype="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!BLL.User.AdminUser.isLogin()) return;

        string node = Request["node"];  //id
        type = Request["type"];  //  分类，root
        menutype = Request["menutype"];//菜单类型
        string chk = Request["chk"];

        //过滤node不是PKID时
        if (node.Equals(type)) { node = ""; }
        if ("1".Equals(chk)) { ChkBoxParas = "cls: 'folder',checked: false,"; }
        else{ ChkBoxParas = "";}
        if (string.IsNullOrEmpty(menutype)) menutype = "left";//默认左边菜单
        if (menutype == "left")
        {
            Response.Write(getSyLeftMenu(type, node));
        }
        else
        {
            Response.Write(getSyMenu(type, node));
        }
        Response.End();
    }
    public DataTable getSyMenuListByUser(string code, string pkid, object userID)
    {
        string sql = "";
        if (!string.IsNullOrEmpty(pkid))
        {
            sql = "SELECT DISTINCT Sy_Menu.*" +
        "  FROM Sy_Menu " +
        " 	INNER JOIN Sy_RoleMnu [Mnu] ON Sy_Menu.PKID = [Mnu].Sy_MenuFK " +
        " 	INNER JOIN Sy_UserRole [URole] ON [Mnu].Sy_RoleFK = [URole].Sy_RoleFK" +
        " 	WHERE [URole].Sy_UserFK = '{2}' AND Sy_Menu.ParentId = '{1}'" +
        "  ORDER BY Sy_Menu.SortNo,Sy_Menu.Code";
        }
        else
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (code == "root")
                {
                    sql = "SELECT * FROM Sy_Menu Where Sy_Menu.ParentID IS NULL ORDER BY SortNo,Code";
                }
                else
                {
                    sql = "SELECT DISTINCT Sy_Menu.*" +
            "  FROM Sy_Menu " +
            "  INNER JOIN Sy_RoleMnu [Mnu] ON Sy_Menu.PKID = [Mnu].Sy_MenuFK " +
            "  INNER JOIN Sy_UserRole [URole] ON [Mnu].Sy_RoleFK = [URole].Sy_RoleFK" +
            "  WHERE [URole].Sy_UserFK = '{2}' AND" +
            "  Sy_Menu.ParentID = (SELECT PKID FROM Sy_Menu Where Code = '{0}')" +
            "  ORDER BY Sy_Menu.SortNo,Sy_Menu.Code";
                }
            }
            else
            {
                sql = "SELECT * FROM Sy_Menu WHERE Sy_Menu.ParentId IS NULL ORDER BY Sy_Menu.Code";
            }
        }
        sql = string.Format(sql, code, pkid, userID);

        return CSA.DAL.DBAccess.getRS(sql);
    }
    #region 左菜单
    //{treeNode: '菜单名1', pkid: '91cf8895-cce5-43f7-a07f-bb37dcba1d92', url: '',code:'161101',ParentId:''}|{treeNode: '菜单2', pkid: '', url: '',code:'161102',ParentId:''}
    private string getSyLeftMenu(string type, string pkid)
    {
        sbs = new StringBuilder();

        using (DataTable dt_menu = getSyMenuListByUser(type, pkid, BLL.User.AdminUser.UserID))
        {

            if ("root".Equals(type.ToLower().Trim()))
            {
                getLeftTree(dt_menu, ref sbs, pkid);
            }
            else
            {
                DataTable dt_root = getSyMenuListByUser("", "", "");//dt_menu;
                foreach (DataRow dr in dt_root.Rows)
                {
                    if (type.Equals(dr["Code"]))
                    {
                        getLeftTree(dt_menu, ref sbs, "".Equals(pkid) ? dr["PKID"].ToString() : pkid);
                    }
                }
                dt_root = null;
            }
            if (sbs.Length > 0)
                return sbs.ToString().Substring(1);
            else
                return "null";
        }
    }

    private void getLeftTree(DataTable dtSrc, ref StringBuilder sbs, string parentId)
    {
        DataView dv = new DataView();

        dv.Table = dtSrc;
        if (!"root".Equals(type))
            dv.RowFilter = parentId.Length < 10 ? "Code='" + parentId + "'" : "ParentId='" + parentId + "'";
        else
        {
            dv.RowFilter = "".Equals(parentId) ? "ParentId IS NULL" : "ParentId='" + parentId + "'";
        }

        foreach (DataRowView drv in dv)
        {

            sbs.Append("|{");
            sbs.AppendFormat("treeNode: '{0}', pkid: '{1}', url: '{2}',code:'{3}',ParentId:'{4}'", drv["Name"].ToString(), drv["PKID"].ToString(), Sys.syMenu.setParas(drv["func"], drv["PKID"]), drv["code"].ToString(), drv["ParentId"].ToString());
            sbs.Append("}");
            getLeftTree(dtSrc, ref sbs, drv["PKID"].ToString());
        }
        dv = null;
    }

    #endregion
    #region 三级菜单
    //[{id: 1,text: \'子节点1\',leaf: true},{id: 2,text: \'儿子节点2\',children: [{id: 3,text: \'孙子节点\',leaf: true}]}]
    private string getSyMenu(string type, string pkid)
    {
        sbs = new StringBuilder("");

        using (DataTable dt_menu = getSyMenuListByUser(type, pkid, BLL.User.AdminUser.UserID))
        {
            if ("root".Equals(type.ToLower().Trim()))
            {
                getTree(dt_menu, ref sbs, pkid);
            }
            else
            {
                DataTable dt_root = getSyMenuListByUser("", "", "");
                foreach (DataRow dr in dt_root.Rows)
                {
                    if (type.Equals(dr["Code"]))
                    {
                        getTree(dt_menu, ref sbs, "".Equals(pkid) ? dr["PKID"].ToString() : pkid);
                    }
                }
                dt_root = null;
            }
            if (sbs.Length > 0)
            {
                return "[" + sbs.ToString().Substring(1, sbs.Length - 1) + "]";
            }
            sbs.Append("[]");
            return sbs.ToString();
        }
    }
    private void getTree(DataTable dtSrc, ref StringBuilder sbs, string parentId)
    {
        DataView dv = new DataView();

        dv.Table = dtSrc;
        if (!"root".Equals(type))
            dv.RowFilter = parentId.Length < 10 ? "Code='" + parentId + "'" : "ParentId='" + parentId + "'";
        else
        {
            dv.RowFilter = "".Equals(parentId) ? "ParentId IS NULL" : "ParentId='" + parentId + "'";
        }
        foreach (DataRowView drv in dv)
        {
            string ss = string.Format(",{0}{5}id:'{3}',text:'{2}',url:'{3}',leaf:{4},icon:'{6}'{7}{1}", "{", "}",
                     drv["Name"].ToString(), Sys.syMenu.setParas(drv["func"], drv["PKID"]), "1".Equals(drv["EnMode"].ToString().Trim()) ? "true" : "false",
                    ChkBoxParas, drv["icon"], GetChildren(drv["EnMode"].ToString(), drv["PKID"].ToString()));
            sbs.Append(ss);
        }
        dv = null;
    }

    private string GetChildren(string enMode,string pkid)
    {
        string schildren = "";
        if (enMode.Trim() != "1")
        {
            schildren += ",children: [";
            DataTable dtSrc2 = getSyMenuListByUser(type, pkid, BLL.User.AdminUser.UserID);
            StringBuilder sbsc = new StringBuilder();
            getTree2(dtSrc2,ref sbsc, pkid);
            if (sbsc.Length > 0)
            {
                schildren += sbsc.ToString().Substring(1, sbsc.Length - 1);
            }
            schildren += "]";
        }
        return schildren;
    }

    private void getTree2(DataTable dtSrc, ref StringBuilder sbsc, string parentId)
    {
        DataView dv = new DataView();
        dv.Table = dtSrc;
        if (!"root".Equals(type))
            dv.RowFilter = parentId.Length < 10 ? "Code='" + parentId + "'" : "ParentId='" + parentId + "'";
        else
        {
            dv.RowFilter = "".Equals(parentId) ? "ParentId IS NULL" : "ParentId='" + parentId + "'";
        }
        foreach (DataRowView drv in dv)
        {
            if (drv["EnMode"].ToString().Trim() != "1")
            {
                string ss = string.Format(",{0}{5}id:'{3}',text:'{2}',url:'{3}',leaf:{4},icon:'{6}'{7}{1}", "{", "}",
                     drv["Name"].ToString(), Sys.syMenu.setParas(drv["func"], drv["PKID"]), "1".Equals(drv["EnMode"].ToString().Trim()) ? "true" : "false",
                    ChkBoxParas, drv["icon"], GetChildren(drv["EnMode"].ToString(), drv["PKID"].ToString()));
                sbsc.Append(ss);
            }
            else {
                string ss = string.Format(",{0}{5}id:'{3}',text:'{2}',url:'{3}',leaf:{4},icon:'{6}'{1}", "{", "}",
                     drv["Name"].ToString(), Sys.syMenu.setParas(drv["func"], drv["PKID"]), "1".Equals(drv["EnMode"].ToString().Trim()) ? "true" : "false",
                    ChkBoxParas, drv["icon"]);
                sbsc.Append(ss);
            }
        }
        dv = null;
    }
    #endregion
   

}
