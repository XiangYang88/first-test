using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using CSA.DAL;
using CSA.Control;
using CSA.IO;
public partial class Rx_SyRole : Page
{
    StringBuilder sbs = null;
    string ChkBoxParas = "", type = "", msg="", pkid="";
    string TblName = "Sy_RoleMnu";
    List<string> sqlList = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                switch (Request["action"])
                {
                    case "save":        //设置权限
                        try
                        {
                            string pkids = Request["para1"];        //role ids
                            pkid = Request["pkid"];
                            
                            //delete old RoleMnu set 

                            sqlList.Add(string.Format("DELETE FROM Sy_RoleMnu WHERE Sy_RoleFK = '{0}'", pkid));
                            //insert new RoleMnu 
                            SQLBuilder builder = new SQLBuilder(TblName);
                            pkids = pkids.Replace("root", "");
                            foreach (string _pkid in pkids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                builder.Clear();
                                builder.TblName = TblName;
                                builder.AddData("ModUser", BLL.User.AdminUser.UserID);
                                builder.AddData("ModTime", DateTime.Now.ToString());
                                builder.AddData("Sy_RoleFK", pkid);
                                builder.AddData("Sy_MenuFK",_pkid.Trim());
                                sqlList.Add(builder.InsertSql);
                            }

                            DBAccess.ExecuteSqlTran(sqlList);

                            msg = "权限已成功更新到服务器！";
                        }
                        catch (Exception ex)
                        {
                            msg = "保存到服务器时出现错误！" + ex.Message;
                        }
                        finally {
                            sqlList = null;
                        }
                        break;

                    default:            //读取菜单
                        string node = Request["node"];  //id
                        pkid = Request["pkid"];
                        type = Request["type"];  //  分类，root
                        string chk = Request["chk"];
                        string role = Request["role"];

                        //过滤node不是PKID时
                        if (node.Equals(type)) { node = ""; }
                        if ("1".Equals(chk)) { ChkBoxParas = "cls: 'folder',checked:false,"; }
                        else { ChkBoxParas = ""; }

                        msg = getSyMenu(node.Replace("root",""), role);
                        break;
                }

                Response.Write(msg);//Comm.getMsgForJson(pkids));
                Response.End();
            }
        }
        catch (Exception ex) {
          //  WriteEnd("初始数据出错！" + ex.Message);
        }
    } 
    //[{id: 1,text: \'子节点1\',leaf: true},{id: 2,text: \'儿子节点2\',children: [{id: 3,text: \'孙子节点\',leaf: true}]}]
    private string getSyMenu(string pkid, string roleId)
    {
        sbs = new StringBuilder("[");

        string sql="";
        if (!string.IsNullOrEmpty(pkid))
        {
            sql = "SELECT DISTINCT Mnu.Sy_roleFK, Sy_Menu.*" +
         "  FROM Sy_Menu " +
        "	LEFT JOIN Sy_RoleMnu [Mnu] ON Sy_Menu.PKID = [Mnu].Sy_MenuFK AND mnu.Sy_Rolefk ='{0}' " +
        "	LEFT JOIN Sy_User [MUsr] ON Sy_Menu.ModUser = MUsr.PKID" +
        "	WHERE Sy_Menu.ParentID ='{1}'" +
        "	ORDER BY Sy_Menu.SortNo,Sy_Menu.Code";
        }
        else
        {
            sql = "SELECT DISTINCT Mnu.Sy_roleFK, Sy_Menu.*" +
           "	FROM Sy_Menu " +
           "	LEFT JOIN Sy_RoleMnu [Mnu] ON Sy_Menu.PKID = [Mnu].Sy_MenuFK AND mnu.Sy_Rolefk ='{0}' " +
           "	LEFT JOIN Sy_User [MUsr] ON Sy_Menu.ModUser = MUsr.PKID" +
           "	WHERE Sy_Menu.ParentID IS NULL" +
           "	ORDER BY Sy_Menu.SortNo,Sy_Menu.Code";
        }


        using (DataTable dt_menu = DBAccess.getRS(string.Format(sql,roleId,pkid)))
        {

            foreach (DataRow _dr in dt_menu.Rows)
            {
                if (!"".Equals(ChkBoxParas)) {
                    ChkBoxParas = string.Format("cls:'{0}',checked:{1},", "1".Equals(_dr["EnMode"].ToString().Trim()) ? "file" : "folder", 
                            !object.Equals(null, _dr["Sy_RoleFK"]) && !"".Equals(_dr["Sy_RoleFK"].ToString().Trim()) ? "true" : "false");
                }

                sbs.Append(string.Format("{0}{6}id:'{2}',text:'{3}',url:'{4}',leaf:{5}{1},", "{", "}",
                        _dr["PKID"].ToString(), _dr["Name"].ToString(), _dr["func"].ToString(), "1".Equals(_dr["EnMode"].ToString().Trim()) ? "true" : "false",
                        ChkBoxParas)
                );
            }
            sbs.Append("]");
            return sbs.ToString();
        }
    }
 
}
