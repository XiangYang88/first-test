﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CSA.HC;
using CSA.DAL;
using CSA.IO;
using CSA.Control;
public partial class admin_sys_SyLogin : AdminPage
{
    private string TblName = "Sy_Login";
    private string PKField = "id";
    private string PKType = "int";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            checkUserGoLogin();
            if (!IsPostBack)
            {
                initCtrl();
                bindData();
            }
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }

    #region 个性化功能


    #endregion

    #region 数据绑定
    /// <summary>
    /// 初始化控件
    /// </summary>
    private void initCtrl()
    {
        setViewState("type", Request.QueryString["type"]); //设置操作权限        
        this.gvList.DataKeyNames = new string[] { PKField };
    }
    private void setPageSize()
    {
        int pagesize = 0;
        int.TryParse(this.txtPageSize.Value.Trim(), out pagesize);
        if (pagesize == 0)
        {
            pagesize = 10;
        }
        this.gvList.PageSize = pagesize;
        this.ANPage1.PageSize = pagesize;
    }
    /// <summary>
    /// 获取列表的数据源,用于gridview的绑定,和导出的数据源
    /// </summary>
    /// <param name="where">额外加入的条件,可为空,主要是导出时需要加条件</param>
    /// <returns></returns>
    private DataTable getListDataTable(string where)
    {
        string paras = CSA.Control.ControlHelper.getSearchCondition(divHead);
        if (!string.IsNullOrEmpty(where))
        {
            paras += where;
        }
        return DBAccess.getRS(Util.buildListSQL(TblName, paras, "id desc"));
    }
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private void bindData()
    {
        setPageSize(); //设置分页
        try
        {
            string paras = CSA.Control.ControlHelper.getSearchCondition(divHead);

            using (DataTable dt = getListDataTable(null))
            {
                if (dt.Rows.Count <= 0)
                {
                    divEmpty.Visible = true;
                    divList.Visible = false;
                }
                else
                {
                    divEmpty.Visible = false;
                    divList.Visible = true;

                    ANPage1.RecordCount = dt.Rows.Count;
                    ControlHelper.setAspNetPager(ANPage1);
                    ControlHelper.setGridViewDataSource(gvList, dt,
                        ANPage1.PageSize, ANPage1.CurrentPageIndex);
                }
            }
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }
    /// <summary>
    /// 绑定明细记录
    /// </summary>
    /// <param name="etype"></param>
    /// <param name="spkid"></param>
    private void showDtls(ControlHelper.CEnum etype, string spkid)
    {
        try
        {
            this.PKID.Value = spkid.ToString();//this.gvList.SelectedDataKey.Value.ToString();
            using (DataTable dt = DBAccess.getRS(Util.buildItemSQL(TblName, PKField + "='" + PKID.Value + "'"))) //dao.getRowItemByPKID(TblName, this.PKID.Value))
            {
                if (dt.Rows.Count > 0)
                    ControlHelper.bindControlByDataRow(this.divDtls, dt.Rows[0], null);
            }

        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }
    #endregion

    #region 分页,搜索等重新加载数据
    protected void ANPage1_PageChanged(object sender, EventArgs e)
    {
        bindData();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.CANCEL);
        bindData();
    }
    #endregion

    #region GridView的事件
    /// <summary>
    /// gvList排序事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
    {
        // 从事件参数获取排序数据列
        string sortExpression = e.SortExpression.ToString();
        // 假定为排序方向为“顺序”
        string sortDirection = "ASC";
        // “ASC”与事件参数获取到的排序方向进行比较，进行GridView排序方向参数的修改
        if (sortExpression == this.gvList.Attributes["SortExpression"])
        {
            //获得下一次的排序状态
            sortDirection = (this.gvList.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }
        // 重新设定GridView排序数据列及排序方向
        this.gvList.Attributes["SortExpression"] = sortExpression;
        this.gvList.Attributes["SortDirection"] = sortDirection;

        bindData();
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
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor; this.style.backgroundColor='#FcF3F2'");
            //鼠标移出时，行背景色变 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
            //增加删除时的提示
            // int count = e.Row.Cells.Count;
            // ImageButton img = (ImageButton)e.Row.Cells[4].Controls[0];
            //            img.Attributes.Add("onclick", "return confirm('您确定要删除此记录吗？');");
        }
        if (e.Row.RowIndex != -1)
        {
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[1].Text = id.ToString();
        }
    }
    /// <summary>
    /// gvList的选中事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        showDtls(ControlHelper.CEnum.VIEW, this.gvList.SelectedDataKey.Value.ToString());
        InitField(ControlHelper.CEnum.VIEW);
    }
    /// <summary>
    /// gvList的编辑事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        showDtls(ControlHelper.CEnum.MOD, this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
        InitField(ControlHelper.CEnum.MOD);
    }
    /// <summary>
    /// gvList的删除事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string pkid = gvList.DataKeys[e.RowIndex].Value.ToString();
            DeleteRow("'" + pkid + "'");
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
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
    #endregion

    #region 对数据的增删改等操作

    /// <summary>
    /// 执行导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSvExportSave_Click(object sender, EventArgs e)
    {
        this.lblExportMsg.Text = "";
        bool flag = false;
        foreach (ListItem li in cblExportField.Items)
        {
            if (li.Selected)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            try
            {
                DataTable dt = new DataTable();
                string where = "";
                switch (this.rbtnlExportType.SelectedValue)
                {
                    case "0":
                        where += "and " + TblName + "." + PKField + " in ("
                        + ControlHelper.getGridViewSelected(this.gvList, PKType) + ")";
                        dt = getListDataTable(where);
                        break;
                    case "1":
                        where += "and " + TblName + "." + PKField + " in ("
                            + ControlHelper.getGridViewAll(this.gvList, PKType) + ")";
                        dt = getListDataTable(where);
                        break;
                    case "2":
                        dt = getListDataTable(null);
                        break;
                }

                string tempFile = Server.MapPath("../template/导出登录日志列表模板.xls");//导出模板
                string file = "/upload/temp/登录日志列表-" + DateTime.Now.ToString("yyMMddhhmmss") + (new Random()).Next(100, 999).ToString() + ".xls";//导出文件存放路径
                string savePath = Server.MapPath(file);

                if (System.IO.File.Exists(savePath))
                    System.IO.File.Delete(savePath);
                System.IO.File.Copy(tempFile, savePath);


                CSA.IO.ExcelHelper.DataTableToExcel(dt, savePath, "Sheet1$", cblExportField);

                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value, "导出", file);

                this.lblExportMsg.Text = "<a href='" + CSA.HC.Common.getAppPath() + file
                    + "'><font color=green>导出成功,点击下载</font></a>";
            }
            catch (Exception ex)
            {
                JscriptMsg("导出失败\n" + ex.Message, "", "Error");
            }
        }
        else
        {
            this.lblExportMsg.Text = "请选择导出项";
        }
    }
    /// <summary>
    /// 删除选中项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            string pkids = ControlHelper.getGridViewSelected(this.gvList, "int");
            DeleteRow(pkids);
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }
    /// <summary>
    /// 保存删除
    /// </summary>
    /// <param name="pkids"></param>
    private void DeleteRow(string pkids)
    {
        if (pkids.Length > 0)
        {
            if (CSA.DAL.Util.deleteRecord(TblName, PKField + " in (" + pkids + ")") > 0)
            {
                string[] ids = pkids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in ids)
                {
                    BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, id.Replace("'", ""), "删除", "");
                }
                bindData();
                JscriptMsg("删除成功！", "", "Success");
            }
            else
            {
                JscriptMsg("删除失败！", "", "Error");
            }
        }
        else
        {
            JscriptMsg("请选择要删除的记录行！", "", "Error");
        }
    }
    #endregion

    #region 设置页面状态
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.ADD);
    }
    protected void btnSvCancel_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.CANCEL);
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.Export);
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.Import);
    }
    protected void btnSvImportCancel_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.CANCEL);
    }
    protected void btnSvExportCancel_Click(object sender, EventArgs e)
    {
        InitField(ControlHelper.CEnum.CANCEL);
    }
    /// <summary>
    /// 设置页面状态
    /// </summary>
    /// <param name="otype">add,mod,view,cancel,import,export</param>
    private void InitField(ControlHelper.CEnum etype)
    {
        this.lblExportMsg.Text = "";

        try
        {
            switch (etype)
            {
                case ControlHelper.CEnum.CANCEL:
                    //取消操作
                    this.divContent.Visible = true;
                    this.divExport.Visible = false;
                    break;
                case ControlHelper.CEnum.Export:
                    //导出操作
                    this.divContent.Visible = false;
                    this.divExport.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }

    #endregion
}
