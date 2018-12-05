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
using CSA.DAL;
using CSA.Control;
using System.IO;
public partial class sys_SyConfig : AdminPage
{
    private string TblName = "Sy_Config";
    protected bool isRoot = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        checkUserGoLogin();
        isRoot = IsRoot();
        if (!IsPostBack)
        {
            try
            {
                bindData();
            }
            catch (Exception ex)
            {
                JscriptMsg(ex.Message, "", "Error");
            }
        }
    }
    
    private void bindData()
    {
        try
        {
            ShowItemByType();            
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }

    private void ShowItemByType()
    {
        try
        {
            using (DataTable dt = DBAccess.getRS("select * from "+TblName))
            {
                
                if (dt.Rows.Count > 0)
                {
                    ControlHelper.bindControlByDataRow(divDtls, dt.Rows[0], null);
                    this.KK_UseSYType.SelectedValue = dt.Rows[0]["UseSYType"].ToString() == "True" ? "1" : "0";
                }

            }
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }


    protected void btnSvEdit_Click(object sender, EventArgs e)
    {
        try
        {
            SQLBuilder builder = new SQLBuilder(TblName);
            builder.Where = " and 1=1";
            builder.AutoSetInfo(divDtls);
            builder.AddData("moduser", BLL.User.AdminUser.UserID);
            builder.AddData("ModTime", DateTime.Now.ToString("s"));
            if (builder.AutoUpdate())
            {
                bindData();

                BLL.Sys.AdminLog.AddLog(Request.QueryString["mid"],Header.Title, TblName, PKID.Value,
                    "修改", ControlHelper.getControlContent(divDtls, null));

                JscriptMsg("修改操作成功!", "", "Success");
                
            }
            else
            {
				JscriptMsg("修改操作失败!", "", "Error");
            }
        }
        catch (Exception ex)
        {
            JscriptMsg("修改失败！" + ex.Message.Replace("\r\n",""), "", "Error");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = CSA.IO.UploadHelper.UploadFile(upWaterPic, false, Server.MapPath("~/upload/"), new string[] { "png" });
            this.KK_ImgSYUrl.Value = filename;
        }
        catch (Exception ex)
        {
            JscriptMsg(ex.Message, "", "Error");
        }
    }

}
