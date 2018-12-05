using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
using System.Text;
public partial class admin_BsPg_BsUser : AdminPage
{
    private string TblName = "BS_User";
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
    private void SendEmailToUser(string uname,string upwd,string uemail) {
        if (!uemail.Contains("@"))
            return;
        CSA.Net.Email email = new CSA.Net.Email();
        string webName=BLL.Sys.Config.getConfigVal("WebName");
        email.Title = "经销商账号信息 " + webName+"  "+DateTime.Now.ToShortTimeString();
        StringBuilder strB = new StringBuilder();
        strB.AppendFormat("尊敬的用户：{0}，你好！",uname);
        strB.AppendFormat("<br/>恭喜您成为 {0} 的经销商！", webName);
        strB.AppendFormat("<br/>您的用户名：{0}", uname);
        strB.AppendFormat("<br/>您的密码：{0}", upwd);
        strB.AppendFormat("<br/>温馨提示：请登录后修改密码！以防账号被盗用！<a href='{0}' target=_blank>现在登录</a>", BLL.Sys.Config.getConfigVal("WebSite"));
        email.Content = strB.ToString();
        email.MailTo = new string[] { uemail };
        email.Send();
    }

    #endregion

    #region 数据绑定
    /// <summary>
    /// 初始化控件
    /// </summary>
    private void initCtrl()
    {
        setControlRole();//显示关键控件

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
    /// 获取列表的数据源,用于gridview的绑定,和导出的数据源
    /// </summary>
    /// <param name="where">额外加入的条件,可为空,主要是导出时需要加条件</param>
    /// <returns></returns>
    private DataTable getListDataTable(string where)
    {
        string paras = CSA.Control.ControlHelper.getSearchCondition(divHead);
        paras += " and " + getConditionsByQueryString();
        string p = getViewState("pcode");
        if (p != "")
        {
            paras += " and Bs_UserGradeCode like '" + p + "%'";
        }
        paras += " and " + getConditionsByQueryString();

        string sql = "select *,(select count(*) from Bs_Msg where Bs_UserID={0}.id) as msgcount," +
            "(select count(*) from Bs_Orders where Bs_UserID={0}.id) as ordercount from {0} where {1} order by {2}";

        sql = string.Format(sql, TblName, paras, string.Format("{0}.status desc,{0}.id desc", TblName));
        return DBAccess.getRS(sql);
    }
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private void bindData()
    {
        setPageSize(); //设置分页
        try
        {
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
    /// 设置选中项的属性
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetItemValue_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string setfield = btn.Attributes["SetField"];
        string setvalue = btn.Attributes["SetValue"];
        if (!string.IsNullOrEmpty(setfield) && !string.IsNullOrEmpty(setvalue))
        {
            string pkids = ControlHelper.getGridViewSelected(this.gvList, PKType);

            if (pkids != "")
            {
                string sql = "update {0} set {1}={2} where {3} in({4})";
                sql = string.Format(sql, TblName, setfield, setvalue, PKField, pkids);
                DBAccess.ExecuteNonQuery(sql);
                bindData();
                JscriptMsg("设置成功", "", "Success");
            }
        }
    }
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
           // builder.AddData(PKField, this.PKID.Value);
            builder.setAddUserInfo(BLL.User.AdminUser.UserID);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);
            string K_password = this.K_password.Value;
            if(K_password.Length>0)
                builder.AddData("password", CSA.Security.Encrypt.getMD5(K_password));
            if (builder.AutoInsert())
            {
                //SendEmailToUser(this.KK_Name.Value, this.K_password.Value,this.KK_Email.Value);
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
            string K_password = this.K_password.Value;
            if (K_password.Length > 0)
                builder.AddData("password", CSA.Security.Encrypt.getMD5(K_password));
            if (builder.AutoUpdate())
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, TblName, PKID.Value,
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
        string excelfile = CSA.IO.UploadHelper.UploadTempFile(fuImportFile);
        excelfile = Server.MapPath("~/upload/temp/") + excelfile;
        if (excelfile != null)
        {
            try
            {
                string sheetName = "Sheet1";
                int recordCount= CSA.IO.ImportExcel.TransferData(excelfile, sheetName);
                string insertSql = @"insert into {0}(code,name,RealName,EMail,Mobile,Phone,Sex,address,Password) 
                                select  [会员编号],[用户名],[真实姓名],[邮箱],[手机],[电话号码],[性别],[地址],'{2}'  from {1}";
                insertSql = string.Format(insertSql, TblName, sheetName, CSA.Security.Encrypt.getMD5("123456"));
                if (CSA.DAL.DBAccess.ExecuteNonQuery(insertSql) > 0)
                {
                    JscriptMsg("成功导入 " + recordCount + "条数据。", "", "Success");
                }
            }
            catch (Exception ex)
            {
                JscriptMsg(ex.Message, "", "Error");
            }
            finally
            {
                CSA.DAL.DBAccess.ExecuteNonQuery("drop table sheet1");
            }

         
           bindData();
        }
        InitField(ControlHelper.CEnum.CANCEL);
    }
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
            //<asp:ListItem Value=0>导出选中行</asp:ListItem>
            //        <asp:ListItem Value=1 Selected=true>导出当前页</asp:ListItem>
            //        <asp:ListItem Value=2>导出所有页</asp:ListItem>
            DataTable dt = new DataTable();
            string where = "";
            switch (this.rbtnlExportType.SelectedValue)
            {
                case "0":
                    where += "and " + PKField + " in ("
                    + ControlHelper.getGridViewSelected(this.gvList, PKType) + ")";
                    dt = getListDataTable(where);
                    break;
                case "1":
                    where += "and " + PKField + " in ("
                        + ControlHelper.getGridViewAll(this.gvList, PKType) + ")";
                    dt = getListDataTable(where);
                    break;
                case "2":
                    where += "and " + ControlHelper.getSearchCondition(this.divHead);
                    dt = getListDataTable(where);
                    break;
            }

            string tempFile = Server.MapPath("../template/导出会员列表模板.xls");//导出模板
            string file = "/upload/temp/会员列表-" + DateTime.Now.ToString("yyMMddhhmmss") + (new Random()).Next(100, 999).ToString() + ".xls";//导出文件存放路径
            string savePath = Server.MapPath(file);
            try
            {
                if (System.IO.File.Exists(savePath))
                    System.IO.File.Delete(savePath);
                System.IO.File.Copy(tempFile, savePath);
            }
            catch (Exception ex)
            {
                JscriptMsg("导出失败\n" + ex.Message, "", "Error");
            }

            CSA.IO.ExcelHelper.DataTableToExcel(dt, savePath, "Sheet1$", cblExportField);

            BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value, "导出", file);

            this.lblExportMsg.Text = "<a href='" + CSA.HC.Common.getAppPath() + file
                + "'><font color=green>导出成功,点击下载</font></a>";
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
                    this.divContent.Visible = false;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;

                    ControlHelper.setControlsEmpty(divDtls);
                    this.KK_Code.Value = DateTime.Now.ToString("yyMMddss") + CSA.Text.Util.getRnd(1000, 9999);
                    ControlHelper.setControlsLocked(divDtls, false);

                    break;
                case ControlHelper.CEnum.MOD:
                    //修改
                    this.btnSvEdit.Visible = true;
                    this.divDtls.Visible = true;
                    this.btnSvAdd.Visible = false;
                    this.divContent.Visible = false;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    ControlHelper.setControlsLocked(divDtls, true);
                    break;
                case ControlHelper.CEnum.VIEW:
                    //查看
                    this.btnSvAdd.Visible = false;
                    this.divDtls.Visible = true;
                    this.btnSvEdit.Visible = false;
                    this.divContent.Visible = false;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
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
