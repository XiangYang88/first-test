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

public partial class ucProvince : System.Web.UI.UserControl
{
    protected bool isRoot = true;
    protected string province = "广东省";
    protected string city = "广州市";
    protected string county = "越秀区";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 初始化省/市/区
    /// </summary>
    /// <param name="_isRoot">是否位于根目录</param>
    /// <param name="_province">省</param>
    /// <param name="_city">市</param>
    /// <param name="_county">区</param>
    public void FillData(bool _isRoot, string _province, string _city, string _county)
    {
        isRoot = _isRoot;
        province = _province;
        city = _city;
        county = _county;
    }
    public string Province
    {
        get { return Request["hn_province"]; }
    }
    public string City
    {
        get { return Request["hn_city"]; }
    }
    public string County
    {
        get { return Request["hn_county"]; }
    }
}
