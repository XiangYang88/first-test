﻿using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CSA.Control;
using CSA.DAL;
using CSA.IO;
using CSA.HC;
public partial class sys_SyMenu : AdminPage
{
    private string TblName = "Sy_Menu";
    private string PKField = "pkid";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            checkUserGoLogin();
            if (!IsPostBack)
            {
                initCtrl();
                setViewState("pcode", Request.QueryString["pcode"]);
                bindData();
            }
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }

    #region 个性化功能
    /// <summary>
    /// 获取上级或下级页面
    /// </summary>
    /// <param name="enmode"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getViewUrl(object enmode, object code)
    {
        string e = enmode.ToString(), c = code.ToString();
        string url = "";

        if (c.Length > 2)
        {
            url += "<a href='" + getLinkWidthBaseParas("", "pcode", c.Substring(0, c.Length - 4)) + "'>返回上级</a>";
        }
        if (e != "1")
        {
            url += "　<a href='" + getLinkWidthBaseParas("", "pcode", Eval("code").ToString()) + "'>查看下级</a>";
        }
        return url;
    }

    #endregion

    #region 数据绑定
    /// <summary>
    /// 初始化控件
    /// </summary>
    private void initCtrl()
    {
        setControlRole();
        setViewState("pcode", Request.QueryString["pcode"]);
        setViewState("type", Request.QueryString["type"]); //设置操作权限        
        this.gvList.DataKeyNames = new string[] { PKField };

        this.btnAdd.Visible = false;
        this.btnDel.Visible = false;
        switch (getViewState("type").ToLower())
        {
            case "view":
                ControlHelper.setGridViewRight(this.gvList, ControlHelper.CEnum.VIEW);
                break;
            case "add":
                ControlHelper.setGridViewRight(this.gvList, ControlHelper.CEnum.VIEW);
                this.btnAdd.Visible = true;
                InitField(ControlHelper.CEnum.ADD);
                break;
            case "mod":
                ControlHelper.setGridViewRight(this.gvList, ControlHelper.CEnum.MOD);
                this.btnAdd.Visible = true;
                break;
            case "del":
                ControlHelper.setGridViewRight(this.gvList, ControlHelper.CEnum.DEL);
                this.btnAdd.Visible = true;
                this.btnDel.Visible = true;
                break;
            default:
                ControlHelper.setGridViewRight(this.gvList, ControlHelper.CEnum.VIEW);
                break;
        }
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
    /// 绑定列表数据
    /// </summary>
    private void bindData()
    {
        setPageSize();
        try
        {
            string paras = CSA.Control.ControlHelper.getSearchCondition(divHead);
            paras += " and " + getConditionsByQueryString();
            string p = getViewState("pcode");
            if (p != "")
            {
                paras += " and code like '" + p + "%' and len(code)=" + (p.Length + 2);
            }
            else
            {
                paras += " and len(code)=2";
            }

            using (DataTable dt = DBAccess.getRS(Util.buildListSQL(TblName, paras, "SortNo,code")))
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

                txtCode.Value = dt.Rows[0]["code"].ToString();
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
    #endregion

    #region 对数据的增删改等操作
    /// <summary>
    /// 保存新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSvAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string pid = "";
            string pcode = getViewState("pcode");
            if (pcode != "")
            {
                pid = DBAccess.ExecuteScalar("select pkid from " + TblName + " where code='"
                    + pcode + "'").ToString();
            }

            this.PKID.Value = CSA.Text.Util.getNewGuid();
            CSA.DAL.SQLBuilder builder = new SQLBuilder();
            builder.TblName = TblName;
            builder.AutoSetInfo(divDtls);

            builder.AddData("pkid", this.PKID.Value);
            builder.setAddUserInfo(BLL.User.AdminUser.UserID);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);

            if (!string.IsNullOrEmpty(pid))
                builder.AddData("parentID", pid);

            if (builder.AutoInsert())
            {
                bindData();
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "添加", ControlHelper.getControlContent(divDtls, null));

                InitField(ControlHelper.CEnum.CANCEL);
                JscriptMsg("添加操作成功", "", "Success");
            }
            else
            {
               JscriptMsg("添加失败！", "", "Error");
            }
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSvEdit_Click(object sender, EventArgs e)
    {
        try
        {
            CSA.DAL.SQLBuilder builder = new SQLBuilder();
            builder.TblName = TblName;
            builder.AutoSetInfo(divDtls);
            builder.Where = " and pkid='" + PKID.Value + "'";

            builder.setModUserInfo(BLL.User.AdminUser.UserID);

            List<string> lst = new List<string>();
            lst.Add(builder.UpdateSql);

            string oldcode = txtCode.Value.Trim();
            string newcode = KK_Code.Value.Trim();
            if (oldcode != newcode)
            {
                //修改下级的Code
                //sqlserver 下的语法
                //string sql = "update sy_menu set code='{0}'+right(code,len(code)-{3})" +
                //        " where code like '{2}%' and len(code)>{3}";

                //sqlite下的语法
                string sql = "update sy_menu set code='{0}'+substr(code,{3}+1)" +
                         " where code like '{2}%' and len(code)>{3}";

                sql = string.Format(sql, newcode, newcode.Length, oldcode, oldcode.Length);
                lst.Add(sql);

                if (newcode.Length == 2)
                {
                    //将本身的ParentID置为null
                    sql = "update sy_menu set parentID=null where pkid='" + PKID.Value + "'";
                    lst.Add(sql);
                }
                else if (newcode.Length > 2)
                {
                    //将本身的ParentID置为新的父ID
                    sql = "update sy_menu set parentID=(select pkid from sy_menu where code='{0}') " +
                        "where pkid='{1}'";
                    sql = string.Format(sql, newcode.Substring(0, newcode.Length - 2), PKID.Value);
                    lst.Add(sql);
                }
                else
                {
                    return;
                }
            }
            if (DBAccess.ExecuteSqlTran(lst) > 0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "修改", ControlHelper.getControlContent(divDtls, null));
                bindData();
                InitField(ControlHelper.CEnum.CANCEL);
                JscriptMsg("修改操作成功!", "", "Success");
            }
            else
            {
                JscriptMsg("修改操作失败！", "", "Error");
            }
        }
        catch (Exception ex)
        {
            JscriptMsg("修改失败！" + ex.Message.Replace("\r\n",""), "", "Error");
        }
    }

    /// <summary>
    /// 执行导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSvImportSave_Click(object sender, EventArgs e)
    {
       

        #region 执行导入

        #endregion

        JscriptMsg("导入成功","", "Success");
        InitField(ControlHelper.CEnum.CANCEL);
    }
    /// <summary>
    /// 执行导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSvExportSave_Click(object sender, EventArgs e)
    {
       
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
            string pkids = ControlHelper.getGridViewSelected(this.gvList, "string");
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
            string sql = "select count(a.{1}) from {0} a,{0} b where a.{1} in ({2}) and b.code like a.code+'%' and len(b.code)=len(a.code)+2";
            sql = string.Format(sql, TblName, PKField, pkids);
            string cnt = DBAccess.ExecuteScalar(sql).ToString();
            if (cnt == "0")
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
                JscriptMsg("删除失败，下级菜单数不为0！", "", "Error");
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
        this.lblImportMsg.Text = "";
        
        try
        {
            switch (etype)
            {
                case ControlHelper.CEnum.ADD:
                    //新增
                    this.btnSvAdd.Visible = true;
                    this.divDtls.Visible = true;

                    this.btnSvEdit.Visible = false;
                    this.divContent.Visible = true;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    ControlHelper.setControlsEmpty(divDtls);
                    ControlHelper.setControlsLocked(divDtls, false);

                    string pc = getViewState("pcode");
                    this.KK_Code.Value = Util.getNextCode(TblName, "Code", pc, 2);
                    this.KK_SortNo.Value = Util.getNextSortNo(TblName, "SortNo",
                        pc != "" ? string.Format("code like '{0}%' and len(code)={1}", pc,
                        pc.Length + 2) : pc).ToString();
                    break;
                case ControlHelper.CEnum.MOD:
                    //修改
                    this.btnSvEdit.Visible = true;
                    this.divDtls.Visible = true;
                    this.btnSvAdd.Visible = false;
                    this.divContent.Visible = true;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    ControlHelper.setControlsLocked(divDtls, true);
                    break;
                case ControlHelper.CEnum.VIEW:
                    //查看
                    this.btnSvAdd.Visible = false;
                    this.divDtls.Visible = true;
                    this.btnSvEdit.Visible = false;
                    this.divContent.Visible = true;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    ControlHelper.setControlsLocked(divDtls, false);
                    break;
                case ControlHelper.CEnum.CANCEL:
                    //取消操作
                    this.divContent.Visible = true;
                    this.divDtls.Visible = false;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    break;
                case ControlHelper.CEnum.Import:
                    //导入操作
                    this.divContent.Visible = false;
                    this.divDtls.Visible = false;
                    this.divImport.Visible = true;
                    this.divExport.Visible = false;
                    break;
                case ControlHelper.CEnum.Export:
                    //导出操作
                    this.divContent.Visible = false;
                    this.divDtls.Visible = false;
                    this.divImport.Visible = false;
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
