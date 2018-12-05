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
public partial class admin_BsPg_BsOrdersItem : AdminPage
{
    private string TblName = "BS_Orders";
    private string PKField = "code";
    private string PKType = "string";
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUserGoLogin();
        if (!Page.IsPostBack)
        {
            initCtrl();
            bindData();
        }
    }
    private void bindData()
    {
        string type = Request.QueryString["type"].ToLower();
        if (type == "mod"||type=="view")
        {
            this.PKID.Value = Request.QueryString["pkid"];
            if (this.PKID.Value != "")
            {
                string sql = "select *,Amount as Amount1 from {0} where {1}='{2}'";
                sql = string.Format(sql, TblName, PKField, this.PKID.Value);
                using (DataTable dt = DBAccess.getRS(sql))
                {
                    if (dt.Rows.Count > 0)
                    {
                        ControlHelper.bindControlByDataRow(divDtls, dt.Rows[0], null);
                        this.quantity1.Text = KK_quantity.Value;
                    }
                }
                bindOrderMsg();
                bindOrderPro();
            }
        }
    }
    private void bindOrderMsg()
    {
        string sql = Util.buildListSqlWidthAdmin("Bs_OrdersMsg", "Bs_OrdersCode='" + this.PKID.Value + "'", "Bs_OrdersMsg.id desc");
        DataTable dt = DBAccess.getRS(sql);
        this.gvMsgList.DataSource = dt;
        this.gvMsgList.DataBind();
    }
    private void bindOrderPro()
    {
        string sql = "select a.*,b.name as proname,b.pic,b.code proCode " +
            "from bs_ordersdtl a,bs_products b where a.Bs_ProductsCode=b.code and a.Bs_OrdersCode='"+this.PKID.Value+"'";
        DataTable dt = DBAccess.getRS(sql);
        this.gvProList.DataSource = dt;
        this.gvProList.DataBind();
    }
    private void initCtrl()
    {
        setControlRole();
        string type = Request.QueryString["type"].ToLower();
        setViewState("type", type);
        
        if (type == "add")
        {
            this.KK_Code.Value = "M"+DateTime.Now.ToString("yyMMddhhmmss");
            this.btnSvAdd.Visible = true;
            this.btnSvPrintOrder.Visible = true;
            this.btnSvPrintPro.Visible = false;
            this.btnSvEdit.Visible = false;
            this.divProAdd.Visible = true;
        }
        else if (type == "mod" || type == "del")
        {
            this.btnSvPrintOrder.Visible = true;
            this.btnSvAdd.Visible = false;
            this.btnSvEdit.Visible = true;
            this.divProAdd.Visible = false;
            ControlHelper.setControlsLocked(divDtls, true);
        }
        
        else 
        {
            this.btnSvAdd.Visible = false;
            this.btnSvPrintOrder.Visible = true;
            this.btnSvPrintPro.Visible = false;
            this.btnSvEdit.Visible = false;
            this.divProAdd.Visible = false;
        }
        DataTable status = DBAccess.getRS("select name,code from sy_code where code like '01%' order by sortno,code");
        ControlHelper.setCtrlDataSource(this.KK_status, status, new string[] {
            "name","code"
        });
        DataTable pay = DBAccess.getRS("select name,code from sy_pay order by sortno,code");
        ControlHelper.setCtrlDataSource(this.KK_Sy_PayCode, pay, new string[] {
            "name","code"
        });

        DataTable deliver = DBAccess.getRS("select name,code from sy_deliver order by sortno,code");
        ControlHelper.setCtrlDataSource(this.KK_Sy_DeliverCode, deliver, new string[] {
            "name","code"
        });

    }
   
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
            if (builder.AutoInsert())
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title,TblName, PKID.Value,
                    "添加", ControlHelper.getControlContent(divDtls, null));
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
            if (this.KK_status.SelectedValue == "0108")
                builder.AddData("isEnd", 1);
            else
                builder.AddData("isEnd", 0);
            builder.Where = string.Format(" and {0}='{1}'", PKField, PKID.Value);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);
            if (builder.AutoUpdate())
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, TblName, PKID.Value,
                    "修改", ControlHelper.getControlContent(divDtls, null));
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
    #endregion
    protected void btnReply_Click(object sender, EventArgs e)
    {
        SQLBuilder builder = new SQLBuilder("Bs_OrdersMsg");
        builder.AddData("notes", this.KK3_Notes.Value);
        builder.setAddUserInfo(BLL.User.AdminUser.UserID);
        builder.AddData("Bs_OrdersCode", this.PKID.Value);
        if (builder.AutoInsert())
        {
            BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, "Bs_Orders", PKID.Value,
                    "添加订单备注", "添加订单处理信息："+this.KK3_Notes.Value);
            bindOrderMsg();
        }
        
    }
    protected void btnAddPro_Click(object sender, EventArgs e)
    {
        SQLBuilder builder = new SQLBuilder("Bs_OrdersDtl");
        builder.DataFlag = "KK2_";
        builder.setAddUserInfo(BLL.User.AdminUser.UserID);
        builder.AutoSetInfo(divProAdd);
        builder.AddData("Bs_OrdersCode",this.PKID.Value);
        if (builder.AutoInsert())
        {
            BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, "Bs_Orders", PKID.Value,
                    "添加订单产品", ControlHelper.getControlContent(divProAdd, null));
            bindOrderPro();
        }
        
    }
    protected void gvMsgList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string pkid = gvMsgList.DataKeys[e.RowIndex].Value.ToString();
            string content = gvMsgList.Rows[e.RowIndex].Cells[2].Text;
            if (CSA.DAL.Util.deleteRecord("Bs_OrdersMsg", "id=" + pkid) > 0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title,"Bs_Orders", PKID.Value,
                    "删除订单备注", "删除主键：" + pkid + "<br />内容：" + content);
                bindOrderMsg();
            }
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
    protected void gvMsgList_PreRender(object sender, EventArgs e)
    {
        if (gvMsgList.Rows.Count > 0)
        {
            gvMsgList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void gvProList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string pkid = gvProList.DataKeys[e.RowIndex].Value.ToString();
            string code = gvProList.Rows[e.RowIndex].Cells[3].Text;
            string name = gvProList.Rows[e.RowIndex].Cells[4].Text;
            if (CSA.DAL.Util.deleteRecord("Bs_OrdersDtl", "id=" + pkid) > 0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, "Bs_Orders", PKID.Value,
                    "删除订单产品", "删除主键：" + pkid + "<br />产品编号：" + code+"<br />产品名称"+name);
                bindOrderPro();
            }
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
    protected void gvProList_PreRender(object sender, EventArgs e)
    {
        if (gvProList.Rows.Count > 0)
        {
            gvProList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void gvProList_RowDataBound(object sender, GridViewRowEventArgs e)
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
            int count = e.Row.Cells.Count;
            e.Row.Cells[0].Attributes.Add("onclick", "return confirm('您确定要删除此记录吗？');");
        }
        if (e.Row.RowIndex != -1)
        {
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[1].Text = id.ToString();
        }
    }
}
