using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using CSA.HC;
using CSA.DAL;
using CSA.IO;
using CSA.Control;
public partial class admin_sys_DbMaintenanceSqlserver : AdminPage
{
    private string backupDir = "";  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            checkUserGoLogin();
          
            if (!IsPostBack)
            {   
                bindData();
            }
            
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
          
        if (CSA.DAL.DBAccess.BackUp(true,null))
        {
            bindData();
            JscriptMsg("生成备份成功！", "", "Success");
        }
        else
        {
            JscriptMsg("生成备份失败,可能数据库正在被占用！", "", "Error");
        }
    }
    
    protected void RollBack(string file)
    {
        CSA.DAL.ConnEnum dbtype=CSA.DAL.DBAccess.getDbType();
        if (dbtype == CSA.DAL.ConnEnum.MSSQL)
        {
            try
            {
             
                string backupfile = DateTime.Now.ToString("yyyyMMddHHmm") + ".bak";
                CSA.DAL.DBAccess.BackUp(true, backupfile);//还原前先将当前的备份
                if (DBBakManager.RestoreDB(file))
                    JscriptMsg("成功还原,原数据库已经备份为" + backupfile, "", "Success");
                else
                    JscriptMsg("还原失败,可能数据库正在被占用！", "", "Error");
                bindData();
            }
            catch (Exception ex)
            {
                JscriptMsg(ex.Message, "", "Error");
            }
        }
        else
        {
            JscriptMsg("不能还原数据库,因为当前的数据库为:" + dbtype.ToString().Replace("db", "") + ",请联系开发商", "", "Error");
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string path=System.Web.HttpContext.Current.Server.MapPath("~/App_Data/backup/");
        string files = ControlHelper.getGridViewSelected(this.gvList, "int");
        string[] list = files.Split(new string[] {","},StringSplitOptions.RemoveEmptyEntries);
        foreach (string l in list)
        {
            try
            {
                File.Delete(path+l);
            }
            catch(Exception ex)
            {
                JscriptMsg("删除失败,可能文件正在被占用！", "", "Error");
            }
        }
        bindData();
    }
    
    private void bindData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("filename");
        string[] filelist = Directory.GetFiles(Server.MapPath("~/App_Data/backup"));
        foreach(string fl in filelist)
        {
            
            dt.Rows.Add(new object[] {fl.Substring(fl.LastIndexOf("\\")+1)});
        }
        gvList.DataSource = dt;
        gvList.DataBind();
    }
    /// <summary>
    /// gvList的选中事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RollBack(this.gvList.SelectedDataKey.Value.ToString());
    }
    /// <summary>
    /// gvList绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gvList = (GridView)sender;
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标经过时，行背景色变 66ccff
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor; this.style.backgroundColor='#FFFFCC'");
            //鼠标移出时，行背景色变 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
            //增加删除时的提示
            // int count = e.Row.Cells.Count;
            //ImageButton img = (ImageButton)e.Row.Cells[4].Controls[0];
           // img.Attributes.Add("onclick", "return confirm('您确定要删除此记录吗？');");
        }
        if (e.Row.RowIndex != -1)
        {
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[1].Text = id.ToString();
        }
    }
    /// <summary>
    /// 页面呈现前事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_PreRender(object sender, EventArgs e)
    {
        if (gvList.Rows.Count > 0)
        {
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
   

}
