using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class usercontrl_uploadFile : System.Web.UI.UserControl
{
    /// <summary>
    /// 参数列表, 格式如下:path|width|height , 原图则width=height=0,默认是  upload|0|0  即上传原图到upload下,注意:目录相应于根目录
    /// 如果上传其他文件, width=height=0
    /// </summary>
    public string[] PathList
    {
        get
        {
            if (ViewState["PathList "] != null)
            {
                return (string[])ViewState["PathList "];
            }
            return new string[] { "upload|0|0" };
        }
        set
        {
            ViewState["PathList "] = value;
        }
    }
    /// <summary>
    /// 项目的分隔符,默认为 ,
    /// </summary>
    public string ItemSp
    {
        get
        {
            if (ViewState["ItemSp"] != null)
            {
                return ViewState["ItemSp"].ToString();
            }
            return ",";
        }
        set
        {
            ViewState["ItemSp"] = value;
        }
    }
    /// <summary>
    /// 当系统启用了加水印 并且IsWatermark为true才执行加水印
    /// </summary>
    public bool IsWatermark
    {
        get
        {
            if (ViewState["IsWatermark"] != null)
            {
                return (bool)ViewState["IsWatermark"];
            }
            return false;//全站开启
        }
        set { ViewState["IsWatermark"] = value; }
    }
    /// <summary>
    /// 设置是否多文件,true为多文件,false为单一文件
    /// </summary>
    public bool Multi
    {
        get
        {
            if (ViewState["Multi "] != null)
            {
                return (bool)ViewState["Multi "];
            }
            return true;
        }
        set
        {
            ViewState["Multi "] = value;

        }
    }
    private string[] ImgExt;
    /// <summary>
    /// 允许的文件列表,默认为  new string[]{"jpg","gif","bmp","jpeg","png"}
    /// </summary>
    public string[] AllExt
    {
        get
        {
            if (ViewState["AllExt"] != null)
            {
                return (string[])ViewState["AllExt"];
            }
            return new string[] { "jpg", "gif", "bmp", "jpeg", "png", "rar", "doc", "docx", "xls", "xlsx", "flv","avi","mp3","pdf","ico" };
        }
        set
        {
            ViewState["AllExt"] = value;
        }
    }
    public string FileList
    {
        get
        {
            return lblProPic.Text.Trim();
        }
        set
        {
            lblProPic.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ImgExt = new string[] { 
            "jpg",
            "gif",
            "bmp",
            "jpeg",
            "png"
        };
        FillFile();
    }
    protected void ShowMsg(string msg)
    {
        this.lblMsg.Text = msg;
    }
    protected void HideMsg()
    {
        
    }
    protected void uploadFile(FileUpload fu)
    {
        if (!fu.HasFile) { return; }
        string filename = fu.FileName;
        string filetype = Path.GetExtension(filename).ToLower().Substring(1);

        if (!CSA.Text.Util.inArray(AllExt, filetype))
        {
            ShowMsg("文件格式不正确");
            return;
        }
        
        //if (!CSA.IO.UploadHelper.IsAllowedExtension(fu))
        //{ 
        //    ShowMsg("不允许的文件类型");
        //    return;
        //}

        Random rd = new Random();
        filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
            + DateTime.Now.Day.ToString() + rd.Next(1000, 9999).ToString() + "." + filetype;

        string path = Request.PhysicalApplicationPath;
        string orgpic = path + "Upload\\temp\\" + filename;

        string datePre = DateTime.Now.ToString("yyMM");


        fu.SaveAs(orgpic);

        if (IsWatermark)
        {

            CSA.IO.ImageHelper.AddSign(orgpic);
        }
        foreach (string setInfo in PathList)
        {
            string[] _si = setInfo.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

            string fileDir = path + _si[0] + "\\" + datePre;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            if (_si[1] == "0" && _si[2] == "0")
            {
                File.Copy(orgpic, fileDir + "\\" + filename);
            }
            else if (_si[1] == "0")
            {
                CSA.IO.ImageHelper.ResizeImg(orgpic, fileDir + "\\" + filename, 0, int.Parse(_si[2]));  //宽度自动适应
            }
            else if (_si[2] == "0")
            {
                CSA.IO.ImageHelper.ResizeImg(orgpic, fileDir + "\\" + filename, int.Parse(_si[1]), 0);  //高度自动适应
            }
            else
            {
                CSA.IO.ImageHelper.ResizeImg(orgpic, fileDir + "\\" + filename, int.Parse(_si[1]), int.Parse(_si[2]));
            }
        }
        try
        {
            File.Delete(orgpic);
        }
        catch (Exception)
        {
        }
        if (this.lblProPic.Text != "" && Multi)
        {
            this.lblProPic.Text += ItemSp + datePre + "/" + filename;
        }
        else
        {
            this.lblProPic.Text = datePre + "/" + filename;
        }
    }
    protected void btnProPic_Click(object sender, EventArgs e)
    {
        HideMsg();
        uploadFile(fuProPic1);

        FillFile();
    }
    protected void FillFile()
    {
        string[] list = this.lblProPic.Text.Split(new string[] { ItemSp }, System.StringSplitOptions.RemoveEmptyEntries);
        this.dlProPic.DataSource = list;
        this.dlProPic.DataBind();
    }
    protected void deleteProPic(object sender, EventArgs e)
    {
        string pic = ((Button)sender).CommandArgument;
        string[] list = this.lblProPic.Text.Split(new string[] { ItemSp }, System.StringSplitOptions.RemoveEmptyEntries);
        this.lblProPic.Text = "";
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] != pic)
            {
                if (this.lblProPic.Text != "")
                {
                    this.lblProPic.Text += ItemSp + list[i];
                }
                else
                {
                    this.lblProPic.Text = list[i];
                }
            }
        }

        foreach (string str in PathList)
        {
            string filename = Request.PhysicalApplicationPath + str.Split(new char[] { '|' })[0] + "\\" + pic;
            if(File.Exists(filename))
                File.Delete(filename);
        }

        FillFile();
    }

    protected string getFile(string file)
    {
        if (PathList.Length > 0)
        {
            string pre = Request.ApplicationPath;
            if (pre == "/")
            {
                pre = "";
            }
            string filename = pre + "/" + PathList[PathList.Length - 1].Split(new char[] { '|' })[0] + "/" + file;

            if (CSA.Text.Util.inArray(ImgExt, Path.GetExtension(filename).ToLower().Substring(1)))
            {
                return "<img height=\"100\" src=\"" + filename + "\" />";
            }
            else
            {
                return "<a  href=\"" + filename + "\" />" + file + "</a>";
            }
        }
        return file;
    }
}
