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

public partial class admin_BsProItemMore : AdminPage
{
    private string TblName = "BS_Products";
    private string PKField = "pkid";
   // private string PKType = "string";
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
                        ControlHelper.bindControlByDataRow(divDtls, dt.Rows[0], null);

                    List<Bs_NewsAlbums> models = new Bs_NewsAlbumsDao().GetList(this.PKID.Value, "product");
                    LitAlbumList.Text = GetAlbumHtml(models, dt.Rows[0]["Photos"].ToString());
                    focus_photo.Value = dt.Rows[0]["Photos"].ToString();
                }
            }
        }
    }
    private void initCtrl()
    {
        setControlRole();
        string type = Request.QueryString["type"];
        setViewState("type", type);

        if (type.ToLower() == "add")
        {
            this.KK_Code.Value = DateTime.Now.ToString("yyMMddhhmmss");
            this.btnSvAdd.Visible = true;
            this.btnSvEdit.Visible = false;
            this.KK_sortNo.Value = Util.getNextSortNo(TblName, "SortNo", "").ToString();
        }
        else if (type.ToLower() == "mod" || type.ToLower() == "del")
        {
            this.btnSvAdd.Visible = false;
            this.btnSvEdit.Visible = true;
        }

        DataTable kind = DBAccess.getRS("select name,code from bs_prokind order by code");
        foreach (DataRow dr in kind.Rows)
        {
            dr["name"] = "".PadLeft(dr["code"].ToString().Length - 2, '－') + dr["name"].ToString();
        }

        ControlHelper.setCtrlDataSource(this.KK_Bs_ProKindCode, kind, new string[] {
            "name","code"
        });

        DataTable property = DBAccess.getRS("select name,code from Bs_ProProperty order by code");
        foreach (DataRow dr in property.Rows)
        {
            dr["name"] = "".PadLeft(dr["code"].ToString().Length - 2, '－') + dr["name"].ToString();
        }

        ControlHelper.setCtrlDataSource(this.KK_ProProperty, property, new string[] {
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
            builder.AddData("Photos", focus_photo.Value);
            builder.setAddUserInfo(BLL.User.AdminUser.UserID);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);

            List<string> lst = new List<string>();
            lst.Add(builder.InsertSql);

            #region 保存相册==========
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");
            if (albumArr != null && albumArr.Length > 0)
            {
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    if (imgArr.Length == 3)
                    {
                        string remark = "";
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            remark = remarkArr[i];
                        }
                        string sql = string.Format("insert into Bs_NewsAlbums(new_pkid,big_img,small_img,remark,type) values ('{0}','{1}','{2}','{3}','product')", this.PKID.Value, imgArr[1], imgArr[2], remark);
                        lst.Add(sql);
                    }
                }
            }
            #endregion
            if (CSA.DAL.DBAccess.ExecuteSqlTran(lst) > 0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "添加", ControlHelper.getControlContent(divDtls, null));
                JscriptMsg("添加操作成功", "", "Success");

                Response.Redirect(getLinkWidthBaseParas("BsProducts.aspx", "type", "del"));
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
            builder.AddData("Photos", focus_photo.Value);
            builder.Where = string.Format(" and {0}='{1}'", PKField, PKID.Value);
            builder.setModUserInfo(BLL.User.AdminUser.UserID);

            List<string> lst = new List<string>();
            lst.Add(builder.UpdateSql);

            #region 保存相册==========
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");
            if (albumArr != null && albumArr.Length > 0)
            {
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = int.Parse(imgArr[0]);
                    idList += img_id + ",";
                    if (imgArr.Length == 3)
                    {
                        string remark = "";
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            remark = remarkArr[i];
                        }
                        //图片id存在---修改  否则 添加
                        if (img_id > 0)
                        {
                            string sql = string.Format("update Bs_NewsAlbums set new_pkid='{0}',big_img='{1}',small_img='{2}',remark='{3}' where id={4}", this.PKID.Value, imgArr[1], imgArr[2], remark, img_id);
                            lst.Add(sql);
                        }
                        else
                        {
                            string sql = string.Format("insert into Bs_NewsAlbums(new_pkid,big_img,small_img,remark,type) values ('{0}','{1}','{2}','{3}','product')", this.PKID.Value, imgArr[1], imgArr[2], remark);
                            lst.Add(sql);
                        }
                    }
                }
                
            }
		//删除已删除的图片
                new Bs_NewsAlbumsDao().DeleteList(idList, PKID.Value);
            #endregion

            if (CSA.DAL.DBAccess.ExecuteSqlTran(lst) > 0)
            {
                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"], Header.Title, TblName, PKID.Value,
                    "修改", ControlHelper.getControlContent(divDtls, null));
                //JscriptMsg("修改操作成功!", "", "Success");
                Response.Write("<script language='javascript'>alert('修改操作成功');location.href='" + Request.Url.ToString() + "'</script>");
            }
            else
            {
                Response.Write("<script language='javascript'>alert('修改操作失败');location.href='" + Request.Url.ToString() + "'</script>");
                //JscriptMsg("修改操作失败！", "", "Error");
            }
        }
        catch (Exception ex)
        {
            JscriptMsg("修改失败！" + ex.Message.Replace("\r\n",""), "", "Error");
        }
    }
    #endregion

    #region 返回相册列表HMTL=========================
    private string GetAlbumHtml(List<Bs_NewsAlbums> models, string focus_photo)
    {
        StringBuilder strTxt = new StringBuilder();
        if (models != null)
        {
            foreach (Bs_NewsAlbums modelt in models)
            {
                strTxt.Append("<li>\n");
                strTxt.Append("<input type=\"hidden\" name=\"hide_photo_name\" value=\"" + modelt.id + "|" + modelt.big_img + "|" + modelt.small_img + "\" />\n");
                strTxt.Append("<input type=\"hidden\" name=\"hide_photo_remark\" value=\"" + modelt.remark + "\" />\n");
                strTxt.Append("<div onclick=\"focus_img(this);\" class=\"img_box");
                if (focus_photo == modelt.small_img)
                {
                    strTxt.Append(" current");
                }
                strTxt.Append("\">\n");
                strTxt.Append("<img bigsrc=\"" + modelt.big_img + "\" src=\"" + modelt.small_img + "\" />");
                strTxt.Append("<span class=\"remark\"><i>");
                if (!string.IsNullOrEmpty(modelt.remark))
                {
                    strTxt.Append(modelt.remark);
                }
                else
                {
                    strTxt.Append("暂无描述...");
                }
                strTxt.Append("</i></span></div>\n");
                strTxt.Append("<a onclick=\"show_remark(this);\" href=\"javascript:;\">描述</a><a onclick=\"del_img(this);\" href=\"javascript:;\">删除</a>\n");
                strTxt.Append("</li>\n");
            }
        }
        return strTxt.ToString();
    }
    #endregion
}
