using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// 图片相册
/// </summary>
[Serializable]
public partial class Bs_NewsAlbums
{
    public Bs_NewsAlbums()
    { }
    #region Model
    private int _id;
    private string _new_id;
    private string _big_img;
    private string _small_img;
    private string _remark = "";
    /// <summary>
    /// 自增ID
    /// </summary>
    public int id
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 主表pkid
    /// </summary>
    public string new_pkid
    {
        set { _new_id = value; }
        get { return _new_id; }
    }
    /// <summary>
    /// 大图
    /// </summary>
    public string big_img
    {
        set { _big_img = value; }
        get { return _big_img; }
    }
    /// <summary>
    /// 小图
    /// </summary>
    public string small_img
    {
        set { _small_img = value; }
        get { return _small_img; }
    }
    /// <summary>
    /// 描述
    /// </summary>
    public string remark
    {
        set { _remark = value; }
        get { return _remark; }
    }
    #endregion Model
}