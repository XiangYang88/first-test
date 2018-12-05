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
using System.Text;

public partial class admin_BsProItem : AdminPage
{
    private string TblName = "BS_Products";
    private string PKField = "pkid";
    private string PKType = "string";
    public string hascontent = "";
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
        string type = Request.QueryString["type"];
        if (type.ToLower() == "mod" || type.ToLower() == "del")
        {
            this.PKID.Value = Request.QueryString["pkid"];
            if (this.PKID.Value != "")
            {
                string sql = "select * from {0} where {1}='{2}'";
                sql = string.Format(sql, TblName, PKField, this.PKID.Value);
                using (DataTable dt = DBAccess.getRS(sql))
                {
                    if (dt.Rows.Count > 0)
                    {
                        ControlHelper.bindControlByDataRow(divDtls, dt.Rows[0], null);
                        if (dt.Rows[0]["Bs_ProKindCode"].ToString() == "02")
                        {
                            tips.InnerHtml = "193px*193px";
                        }
                        else
                        {
                            hascontent = "display:none;";
                        }
                    }
                }
            }
        }
    }
    private void initCtrl()
    {
        setControlRole();
        string type = Request.QueryString["type"];
        setViewState("type", type);
        string pcode = Request.QueryString["pcode"];
        setViewState("pcode", pcode);
        setViewState("casetype", Request.QueryString["casetype"]);
        if (type.ToLower() == "add")
        {
            this.btnSvAdd.Visible = true;
            this.btnSvEdit.Visible = false;
            this.KK_sortNo.Value = Util.getNextSortNo(TblName, "SortNo", pcode).ToString();
        }
        else if (type.ToLower() == "mod" || type.ToLower() == "del")
        {
            this.btnSvAdd.Visible = false;
            this.btnSvEdit.Visible = true;
        }

        DataTable kind = DBAccess.getRS("select name,code from bs_prokind where code like '" + getViewState("pcode") + "%' order by code");
        foreach (DataRow dr in kind.Rows)
        {
            dr["name"] = "".PadLeft(dr["code"].ToString().Length - 2, '－') + dr["name"].ToString();
        }

        ControlHelper.setCtrlDataSource(this.KK_Bs_ProKindCode, kind, new string[] {
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
            string casetype = getViewState("casetype");
            if (!string.IsNullOrEmpty(casetype))
                builder.AddData("type", casetype);
            builder.setAddUserInfo(BLL.User.AdminUser.UserID);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);

            if (builder.AutoInsert())
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "添加", ControlHelper.getControlContent(divDtls, null));
                JscriptMsg("添加操作成功", "", "Success");
                
                Response.Redirect(getLinkWidthBaseParas("BsProducts.aspx",new string[]{"type=del","casetype="+getViewState("casetype")}));
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
        string idList = "";
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
                //JscriptMsg("修改操作成功!", "", "Success");
                JscriptMsg("修改操作成功!", Request.Url.ToString(), "Success");
                //Response.Write("<script language='javascript'>alert('修改操作成功');location.href='" + Request.Url.ToString() + "'</script>");
            }
            else
            {
                JscriptMsg("修改操作失败!", "", "Error");
               // JscriptMsg("修改操作失败！", "", "Error");
            }
        }
        catch (Exception ex)
        {
            JscriptMsg("修改失败！" + ex.Message.Replace("\r\n",""), "", "Error");
        }
    }
    #endregion
}
