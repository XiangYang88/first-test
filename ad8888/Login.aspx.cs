using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Common;
public partial class ad8888_Login : System.Web.UI.Page
{
    protected const string RememberNameKey = "HNRememberName";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            txtUserName.Text = Utils.GetCookie(RememberNameKey);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string userPwd = txtUserPwd.Text.Trim();
        string code = txtCode.Text.Trim();

        if (userName.Equals("") || userPwd.Equals(""))
        {
            lblTip.Visible = true;
            lblTip.Text = "请输入用户名或密码";
            return;
        }
        if (code.Equals(""))
        {
            lblTip.Visible = true;
            lblTip.Text = "请输入验证码";
            return;
        }
        if (Session[HNKeys.SESSION_CODE] == null)
        {
            lblTip.Visible = true;
            lblTip.Text = "系统找不到验证码";
            return;
        }
        if (code.ToLower() != Session[HNKeys.SESSION_CODE].ToString().ToLower())
        {
            lblTip.Visible = true;
            lblTip.Text = "验证码输入不正确";
            return;
        }

        if (!BLL.User.AdminUser.verifyUser(userName, userPwd))
        {
            lblTip.Visible = true;
            lblTip.Text = "用户名或密码有误";
            return;
        }
       
        //写入Cookies
        if (cbRememberId.Checked)
        {
            Utils.WriteCookie(RememberNameKey, userName, 14400);
        }
        else
        {
            Utils.WriteCookie(RememberNameKey, userName, -14400);
        }
        Response.Redirect("main.aspx");
        return;
    }
}
