<%@ WebHandler Language="C#" Class="upload_ajax" %>

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;
using System.Text.RegularExpressions;
using Common;
using LitJson;

/// <summary>
/// 文件上传处理页
/// </summary>
public class upload_ajax : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        //取得处事类型
        string action = HNRequest.GetQueryString("action");

        switch (action)
        {
            case "SingleFile": //单文件
                SingleFile(context);
                break;
            case "MultipleFile": //多文件
                MultipleFile(context);
                break;
            case "AttachFile": //附件
                AttachFile(context);
                break;
            case "EditorFile": //编辑器文件
                EditorFile(context);
                break;
            case "ManagerFile": //管理文件
                ManagerFile(context);
                break;
        }

    }

    #region 上传单文件处理===================================
    private void SingleFile(HttpContext context)
    {
        string _refilepath = HNRequest.GetQueryString("ReFilePath"); //取得返回的对象名称
        string _upfilepath = HNRequest.GetQueryString("UpFilePath"); //取得上传的对象名称
        string _delfile = HNRequest.GetString(_refilepath);
        HttpPostedFile _upfile = context.Request.Files[_upfilepath];
        bool _iswater = false; //默认不打水印
        bool _isthumbnail = false; //默认不生成缩略图
        bool _isimage = false; //默认不限制图片上传

        if (HNRequest.GetQueryString("IsWater") == "1")
            _iswater = true;
        if (HNRequest.GetQueryString("IsThumbnail") == "1")
            _isthumbnail = true;
        if (HNRequest.GetQueryString("IsImage") == "1")
            _isimage = true;

        if (_upfile == null)
        {
            context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
            return;
        }
        UpLoad upFiles = new UpLoad();
        string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, _isimage);
        //删除已存在的旧文件
        DeleteUpFile(_delfile);
        //返回成功信息
        context.Response.Write(msg);
        context.Response.End();
    }
    #endregion

    #region 上传多文件处理===================================
    private void MultipleFile(HttpContext context)
    {
        string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
        HttpPostedFile _upfile = context.Request.Files[_upfilepath];
        bool _iswater = false; //默认不打水印
        bool _isthumbnail = false; //默认不生成缩略图

        if (context.Request.QueryString["IsWater"] == "1")
            _iswater = true;
        if (context.Request.QueryString["IsThumbnail"] == "1")
            _isthumbnail = true;

        if (_upfile == null)
        {
            context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
            return;
        }
        UpLoad upFiles = new UpLoad();
        string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);
        //返回成功信息
        context.Response.Write(msg);
        context.Response.End();
    }
    #endregion

    #region 上传附件处理=====================================
    private void AttachFile(HttpContext context)
    {
        string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
        HttpPostedFile _upfile = context.Request.Files[_upfilepath];
        bool _iswater = false; //默认不打水印
        bool _isthumbnail = false; //默认不生成缩略图

        if (_upfile == null)
        {
            context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
            return;
        }
        UpLoad upFiles = new UpLoad();
        string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, false, true);
        //返回成功信息
        context.Response.Write(msg);
        context.Response.End();
    }
    #endregion

    #region 编辑器上传处理===================================
    private void EditorFile(HttpContext context)
    {
        bool _iswater = false; //默认不打水印
        if (context.Request.QueryString["IsWater"] == "1")
            _iswater = true;
        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
        {
            showError(context, "请选择要上传文件！");
            return;
        }
        UpLoad upFiles = new UpLoad();
        string remsg = upFiles.fileSaveAs(imgFile, false, _iswater);
        //string pattern = @"^{\s*msg:\s*(.*)\s*,\s*msgbox:\s*\""(.*)\""\s*}$"; //键名前和键值前后都允许出现空白字符
        //Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
        //Match m = r.Match(remsg); //搜索匹配项
        //string msg = m.Groups[1].Value; //msg的值，正则表达式中第1个圆括号捕获的值
        //string msgbox = m.Groups[2].Value; //msgbox的值，正则表达式中第2个圆括号捕获的值 
        JsonData jd = JsonMapper.ToObject(remsg);
        string msg = jd["msg"].ToString();
        string msgbox = jd["msgbox"].ToString();
        if (msg == "0")
        {
            showError(context, msgbox);
            return;
        }
        Hashtable hash = new Hashtable();
        hash["error"] = 0;
        hash["url"] = msgbox;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }
    //显示错误
    private void showError(HttpContext context, string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1;
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(hash));
        context.Response.End();
    }
    #endregion

    #region 浏览文件处理=====================================
    private void ManagerFile(HttpContext context)
    {
        //根目录路径，相对路径
        String rootPath = "/upload/";//webpath + attachpath + "/"; //站点目录+上传目录
        //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
        String rootUrl = "/upload/";//webpath + attachpath + "/";
        //图片扩展名
        String fileTypes = "gif,jpg,jpeg,png,bmp";

        String currentPath = "";
        String currentUrl = "";
        String currentDirPath = "";
        String moveupDirPath = "";

        String dirPath = GetMapPath(rootPath);
        String dirName = context.Request.QueryString["dir"];
        //if (!String.IsNullOrEmpty(dirName))
        //{
        //    if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
        //    {
        //        context.Response.Write("Invalid Directory name.");
        //        context.Response.End();
        //    }
        //    dirPath += dirName + "/";
        //    rootUrl += dirName + "/";
        //    if (!Directory.Exists(dirPath))
        //    {
        //        Directory.CreateDirectory(dirPath);
        //    }
        //}

        //根据path参数，设置各路径和URL
        String path = context.Request.QueryString["path"];
        path = String.IsNullOrEmpty(path) ? "" : path;
        if (path == "")
        {
            currentPath = dirPath;
            currentUrl = rootUrl;
            currentDirPath = "";
            moveupDirPath = "";
        }
        else
        {
            currentPath = dirPath + path;
            currentUrl = rootUrl + path;
            currentDirPath = path;
            moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
        }

        //排序形式，name or size or type
        String order = context.Request.QueryString["order"];
        order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

        //不允许使用..移动到上一级目录
        if (Regex.IsMatch(path, @"\.\."))
        {
            context.Response.Write("Access is not allowed.");
            context.Response.End();
        }
        //最后一个字符不是/
        if (path != "" && !path.EndsWith("/"))
        {
            context.Response.Write("Parameter is not valid.");
            context.Response.End();
        }
        //目录不存在或不是目录
        if (!Directory.Exists(currentPath))
        {
            context.Response.Write("Directory does not exist.");
            context.Response.End();
        }

        //遍历目录取得文件信息
        string[] dirList = Directory.GetDirectories(currentPath);
        string[] fileList = Directory.GetFiles(currentPath);

        switch (order)
        {
            case "size":
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new SizeSorter());
                break;
            case "type":
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new TypeSorter());
                break;
            case "name":
            default:
                Array.Sort(dirList, new NameSorter());
                Array.Sort(fileList, new NameSorter());
                break;
        }

        Hashtable result = new Hashtable();
        result["moveup_dir_path"] = moveupDirPath;
        result["current_dir_path"] = currentDirPath;
        result["current_url"] = currentUrl;
        result["total_count"] = dirList.Length + fileList.Length;
        List<Hashtable> dirFileList = new List<Hashtable>();
        result["file_list"] = dirFileList;
        for (int i = 0; i < dirList.Length; i++)
        {
            DirectoryInfo dir = new DirectoryInfo(dirList[i]);
            Hashtable hash = new Hashtable();
            hash["is_dir"] = true;
            hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
            hash["filesize"] = 0;
            hash["is_photo"] = false;
            hash["filetype"] = "";
            hash["filename"] = dir.Name;
            hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            dirFileList.Add(hash);
        }
        for (int i = 0; i < fileList.Length; i++)
        {
            FileInfo file = new FileInfo(fileList[i]);
            Hashtable hash = new Hashtable();
            hash["is_dir"] = false;
            hash["has_file"] = false;
            hash["filesize"] = file.Length;
            hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
            hash["filetype"] = file.Extension.Substring(1);
            hash["filename"] = file.Name;
            hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            dirFileList.Add(hash);
        }
        context.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
        context.Response.Write(JsonMapper.ToJson(result));
        context.Response.End();
    }

    #region Helper
    public class NameSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.FullName.CompareTo(yInfo.FullName);
        }
    }

    public class SizeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Length.CompareTo(yInfo.Length);
        }
    }

    public class TypeSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }
            FileInfo xInfo = new FileInfo(x.ToString());
            FileInfo yInfo = new FileInfo(y.ToString());

            return xInfo.Extension.CompareTo(yInfo.Extension);
        }
    }
    #endregion
    #endregion

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    #region 文件操作
    /// <summary>
    /// 删除单个文件
    /// </summary>
    /// <param name="_filepath">文件相对路径</param>
    public static bool DeleteFile(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return false;
        }
        string fullpath = GetMapPath(_filepath);
        if (File.Exists(fullpath))
        {
            File.Delete(fullpath);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 删除上传的文件(及缩略图)
    /// </summary>
    /// <param name="_filepath"></param>
    public static void DeleteUpFile(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return;
        }
        string fullpath = GetMapPath(_filepath); //原图
        if (File.Exists(fullpath))
        {
            File.Delete(fullpath);
        }
        if (_filepath.LastIndexOf("/") >= 0)
        {
            string thumbnailpath = _filepath.Substring(0, _filepath.LastIndexOf("/")) + "mall_" + _filepath.Substring(_filepath.LastIndexOf("/") + 1);
            string fullTPATH = GetMapPath(thumbnailpath); //宿略图
            if (File.Exists(fullTPATH))
            {
                File.Delete(fullTPATH);
            }
        }
    }

    /// <summary>
    /// 返回文件大小KB
    /// </summary>
    /// <param name="_filepath">文件相对路径</param>
    /// <returns>int</returns>
    public static int GetFileSize(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return 0;
        }
        string fullpath = GetMapPath(_filepath);
        if (File.Exists(fullpath))
        {
            FileInfo fileInfo = new FileInfo(fullpath);
            return ((int)fileInfo.Length) / 1024;
        }
        return 0;
    }

    /// <summary>
    /// 返回文件扩展名，不含“.”
    /// </summary>
    /// <param name="_filepath">文件全名称</param>
    /// <returns>string</returns>
    public static string GetFileExt(string _filepath)
    {
        if (string.IsNullOrEmpty(_filepath))
        {
            return "";
        }
        if (_filepath.LastIndexOf(".") > 0)
        {
            return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
        }
        return "";
    }

    /// <summary>
    /// 返回文件名，不含路径
    /// </summary>
    /// <param name="_filepath">文件相对路径</param>
    /// <returns>string</returns>
    public static string GetFileName(string _filepath)
    {
        return _filepath.Substring(_filepath.LastIndexOf(@"/") + 1);
    }

    /// <summary>
    /// 文件是否存在
    /// </summary>
    /// <param name="_filepath">文件相对路径</param>
    /// <returns>bool</returns>
    public static bool FileExists(string _filepath)
    {
        string fullpath = GetMapPath(_filepath);
        if (File.Exists(fullpath))
        {
            return true;
        }
        return false;
    }

    #endregion
    #region 获得当前绝对路径
    /// <summary>
    /// 获得当前绝对路径
    /// </summary>
    /// <param name="strPath">指定的路径</param>
    /// <returns>绝对路径</returns>
    public static string GetMapPath(string strPath)
    {
        if (strPath.ToLower().StartsWith("http://"))
        {
            return strPath;
        }
        if (HttpContext.Current != null)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }
        else //非web程序引用
        {
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
            }
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }
    }
    #endregion
}