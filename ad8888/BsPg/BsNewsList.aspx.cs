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
using System.Collections.Generic;

public partial class admin_BsPg_BsNewsList : AdminPage
{

    protected string TblName = "BS_News";
    protected string PKField = "pkid";
    protected string PKType = "string";
    protected string FKField = "BS_NewsKindCode";
    protected string hascontent = "";
    protected string hasimg = "";
    protected string hassortno = "";
    protected string hasurl = "display:none";
    protected string hasnotes = "display:none";
    protected string hasindexnotes = "display:none;";
    protected string hasmap = "display:none;";
    protected string hasmedia = "display:none";
    protected string kindCode = "";
    protected string imagesize = "";
    protected string istop = "display:none;";
    protected string title_sub = "display:none";
    protected string addtime = "display:none";


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            checkUserGoLogin();
            if (!IsPostBack)
            {
                setViewState("adtype", Request.QueryString["adtype"]); //广告类型
                //如果只是修改单篇文章则隐藏头部控件
                string pkid = Request.QueryString["pkid"];
                if (!string.IsNullOrEmpty(pkid))
                {
                    setViewState("pkid", pkid);
                    this.divHead.Attributes.Add("style", "display:none");
                    this.btnSvCancel.Visible = false;
                    InitField(ControlHelper.CEnum.MOD);
                    showDtls(ControlHelper.CEnum.MOD, pkid);
                }
                else if (Request.QueryString["type"].Equals("dtl",
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    string where = getConditionsByQueryString();
                    where += " and bs_newskindCode='" + Request.QueryString["pcode"] + "'";
                    DataTable dt = DBAccess.getRS(Util.buildItemSQL(TblName, where));

                    if (dt.Rows.Count > 0)
                    {
                        setViewState("pkid", dt.Rows[0]["pkid"].ToString());
                        this.divHead.Attributes.Add("style", "display:none");
                        this.btnSvCancel.Visible = false;
                        InitField(ControlHelper.CEnum.MOD);
                        showDtls(ControlHelper.CEnum.MOD, dt);
                    }
                    else
                    {
                        setViewState("pcode", Request.QueryString["pcode"]);
                        setViewState("pkid", CSA.Text.Util.getNewGuid());
                        InitField(ControlHelper.CEnum.ADD);
                    }
                    initCtrl();
                }
                else
                {
                    initCtrl();
                    bindData();
                }
            }
            string kCode = getViewState("pcode");
            if (kCode != "")
            {
                try
                {
                    imagesize = CSA.DAL.DBAccess.ExecuteScalar("select top 1 ImageSize from Bs_NewsKind where code='" + kCode + "' order by sortno desc").ToString();
                }
                catch (Exception)
                {
                    imagesize = "";
                }
            }
            if (Request.QueryString["noimg"] == "1") hasimg = "display:none";
            if (Request.QueryString["nocontent"] == "1") hascontent = "display:none";
            if (getViewState("pkid") != "") { hassortno = "display:none"; }
            if (Request.QueryString["hasurl"] == "1") hasurl = "";
            if (Request.QueryString["hasnotes"] == "1") hasnotes = "";
            if (Request.QueryString["hasindexnotes"] == "1") hasindexnotes = "";
            if (Request.QueryString["hasmap"] == "1") hasmap = "";
            if (Request.QueryString["nomedia"] == "1") hasmedia = "";
            if (Request.QueryString["istop"] == "1") istop = "";
            if (Request.QueryString["title_sub"] == "1") title_sub = "";

        }
        catch (Exception ex)
        {
           JscriptMsg(ex.Message, "", "Error");
        }
    }

    #region 个性化功能
    protected bool isMedia()
    {
        return getViewState("adtype").ToLower() == "media";
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
        string kCode = getViewState("pcode");
        string kindSqlW="";
        if (!string.IsNullOrEmpty(kCode))
        {
            kindSqlW = string.Format(" where code like '{0}%'", kCode);
        }
        
        DataTable kind = DBAccess.getRS(string.Format("select name,code from bs_newskind {0} order by code", kindSqlW));
        foreach (DataRow dr in kind.Rows)
        {
            dr["name"] = "".PadLeft(dr["code"].ToString().Length - 2, '－') + dr["name"].ToString();
        }

        ControlHelper.setCtrlDataSource(this.KK_BS_NewsKindCode, kind, new string[] {
            "name","code","--请选择分类--"
        });
        ControlHelper.setCtrlDataSource(this.Search_Keyword1, kind, new string[] {
            "name","code"
        });


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
            string p = getViewState("pcode");
            if (p != "")
            {
                paras += " and " + FKField + " like '" + p + "%'";
            }
            string sql = @"select {0}.*,AddUser.Name as AddUserName,ModUser.Name as ModUserName,NewsKind.name as KindName from {0}
             left join Sy_user AddUser on AddUser.Pkid={0}.AddUser
             left join Sy_user ModUser on ModUser.pkid={0}.ModUser
             left join bs_NewsKind NewsKind on NewsKind.code={0}.bs_newskindCode where {1} order by {0}.SortNo desc,{0}.addTime desc";
            sql = string.Format(sql, TblName, paras);
          
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
            using (DataTable dt = DBAccess.getRS(Util.buildItemSQL(TblName, PKField + "='" + spkid + "'"))) //dao.getRowItemByPKID(TblName, this.PKID.Value))
            {
                showDtls(etype, dt);
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
    private void showDtls(ControlHelper.CEnum etype, DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                this.PKID.Value = dt.Rows[0][PKField].ToString();
                ControlHelper.bindControlByDataRow(this.divDtls, dt.Rows[0], null);
                ControlHelper.setControlsLocked(this.divDtls, true);
                this.kindCode = dt.Rows[0]["bs_newsKindCode"].ToString();

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
            builder.AddData(PKField, this.PKID.Value);
            builder.setAddUserInfo(BLL.User.AdminUser.UserID);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);
            builder.AddData(FKField, getViewState("pcode"));
            builder.AddData("AddTime",DateTime.Now);
            if (builder.AutoInsert())
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "添加", ControlHelper.getControlContent(divDtls, null));
                bindData();
                JscriptMsg("添加操作成功", "", "Success");
                //Response.Write(builder.InsertSql);
                //如果是单篇文章的修改,则修改完不改变当前控件状态
                string pkid = getViewState("pkid");
                if (string.IsNullOrEmpty(pkid))
                {
                    InitField(ControlHelper.CEnum.CANCEL);
                }
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

            if (builder.AutoUpdate())
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "修改", ControlHelper.getControlContent(divDtls, null));
                bindData();
                JscriptMsg("修改操作成功!", "", "Success");

                //如果是单篇文章的修改,则修改完不改变当前控件状态
                string pkid = getViewState("pkid");
                if (string.IsNullOrEmpty(pkid))
                {
                    InitField(ControlHelper.CEnum.CANCEL);
                }
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
            //执行删除数据前获取到要删除文件的路径
            string images = GetDelImg(TblName, PKField + " in(" + pkids + ")", "pic");
            if (CSA.DAL.Util.deleteRecord(TblName, PKField + " in (" + pkids + ")") > 0)
            {
                string[] ids = pkids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string id in ids)
                {
                    BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, id.Replace("'", ""), "删除", "");
                }
                bindData();
                //删除成功，执行清除文件，节省空间
                DeleteImg(images, new string[] { "/upload/" });
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

                    string pc = getViewState("pcode");
                    this.KK_SortNo.Value = Util.getNextSortNo(TblName, "SortNo",
                        pc != "" ? string.Format(FKField + "='{0}'", pc) : pc).ToString();
                    break;
                case ControlHelper.CEnum.MOD:
                    //修改
                    this.btnSvEdit.Visible = true;
                    this.divDtls.Visible = true;
                    this.btnSvAdd.Visible = false;
                    this.divContent.Visible = false;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    break;
                case ControlHelper.CEnum.VIEW:
                    //查看
                    this.btnSvAdd.Visible = false;
                    this.divDtls.Visible = true;
                    this.btnSvEdit.Visible = false;
                    this.divContent.Visible = false;
                    this.divImport.Visible = false;
                    this.divExport.Visible = false;
                    ControlHelper.setControlsLocked(divDtls, true);
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
