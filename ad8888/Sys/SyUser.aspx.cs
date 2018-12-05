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
using System.Collections.Generic;
using CSA.Control;
using CSA.DAL;
using CSA.IO;
using CSA.HC;

public partial class sys_SyUser : AdminPage
{
    private string TblName = "Sy_User";
    private string PKField = "pkid";
    private string PKType = "string";
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
    /// <summary>
    /// 保存权限设置
    /// </summary>
    /// <param name="lst"></param>
    private void saveUserRole(ref List<string> lst)
    {
        lst.Add("delete from SY_UserRole where Sy_UserFK='" + PKID.Value + "'");
        string moduser = BLL.User.AdminUser.UserID;
        SQLBuilder builder = new SQLBuilder();
        foreach (ListItem li in chkRoleList.Items)
        {
            if (li.Selected)
            {
                builder.Clear();
                builder.TblName = "SY_UserRole";
                builder.AddData("PKID", CSA.Text.Util.getNewGuid());
                builder.AddData("Sy_RoleFK", li.Value);
                builder.AddData("Sy_UserFK", PKID.Value);
                builder.AddData("ModUser", moduser);
                builder.AddData("ModTime", DateTime.Now.ToString());
                lst.Add(builder.InsertSql);
            }
        }
    }

    #endregion

    #region 数据绑定
    /// <summary>
    /// 初始化控件
    /// </summary>
    private void initCtrl()
    {
        setControlRole();
        setViewState("type", Request.QueryString["type"]); //设置操作权限   

        DataTable dtOU = DBAccess.getRS("select * from sy_ou order by code");
        ControlHelper.setCtrlDataSource(KK_OuID, dtOU, new string[] {
            "Name","PKID",""
        });

        DataTable dtRole = DBAccess.getRS("select * from sy_role order by code");
        chkRoleList.DataSource = dtRole;
        chkRoleList.DataTextField = "Name";
        chkRoleList.DataValueField = "PKID";
        chkRoleList.DataBind();

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
            case "setuser":
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
        setPageSize(); //设置分页
        try
        {
            string paras = CSA.Control.ControlHelper.getSearchCondition(divHead);
            paras += " and " + getConditionsByQueryString();
            string sql = "select sy_user.*,sy_ou.name ouName,Sy_Role.code roleCode from sy_user";
            sql += " left join sy_ou on sy_user.ouid=sy_ou.pkid";
            sql += " left join Sy_UserRole on sy_user.pkid=Sy_UserRole.sy_userFk";
            sql += " left join Sy_Role on Sy_Role.pkid=Sy_UserRole.sy_roleFK order by sy_ou.addtime";
            using (DataTable dt = DBAccess.getRS(sql))
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


                //绑定角色
                DataTable role = DBAccess.getRS("select * from sy_userrole where Sy_UserFK='"+PKID.Value+"'");
                foreach (ListItem li in chkRoleList.Items)
                {
                    li.Selected = false;
                }
                foreach (ListItem li in chkRoleList.Items)
                {
                    foreach (DataRow dr in role.Rows)
                    {
                        if(dr["Sy_RoleFK"].ToString()==li.Value)
                        {
                            li.Selected=true;
                            break;
                        }
                    }
                }
                
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
            //系统开发用户不能删除
            DataRowView drv = (DataRowView)e.Row.DataItem;
            string rolecode = drv["roleCode"].ToString();

            if (rolecode == "99")
            {
                ImageButton imgBtn = e.Row.Cells[3].FindControl("ImageButtonDel") as ImageButton;
                imgBtn.Visible = imgBtn.Enabled = false;


                HtmlInputCheckBox cbox = e.Row.Cells[0].FindControl("chkItem") as HtmlInputCheckBox;
                cbox.Visible = false;
                cbox.Disabled = true;

            }
            
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
    /// 保存新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSvAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.PKID.Value = CSA.Text.Util.getNewGuid();
            CSA.DAL.SQLBuilder builder = new SQLBuilder();
            builder.TblName = TblName;
            builder.AutoSetInfo(divDtls);
            builder.AddData(PKField, this.PKID.Value);
            builder.setAddUserInfo(BLL.User.AdminUser.UserID);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);
            string pwd = this.Password.Value;
            if (!string.IsNullOrEmpty(pwd))
            {
                builder.AddData("Password", CSA.Security.Encrypt.getMD5(pwd));
            }
            List<string> lst = new List<string>();
            lst.Add(builder.InsertSql);

            //设置权限
            saveUserRole(ref lst);

            if (DBAccess.ExecuteSqlTran(lst)>0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, TblName, PKID.Value,
                    "添加", ControlHelper.getControlContent(divDtls, null));
                bindData();
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
            builder.Where = string.Format(" and {0}='{1}'", PKField, PKID.Value);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);
            string pwd = this.Password.Value;
            if (!string.IsNullOrEmpty(pwd))
            {
                builder.AddData("Password", CSA.Security.Encrypt.getMD5(pwd));
            }
            List<string> lst = new List<string>();
            lst.Add(builder.UpdateSql);

            //设置权限
            saveUserRole(ref lst);

            if (DBAccess.ExecuteSqlTran(lst) > 0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, TblName, PKID.Value,
                    "修改", ControlHelper.getControlContent(divDtls, null));
                bindData();
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
            string pkids = ControlHelper.getGridViewSelected(this.gvList, PKType);
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
                    BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, TblName, id.Replace("'", ""), "删除", "");
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
