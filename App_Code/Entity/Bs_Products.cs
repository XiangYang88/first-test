using System;
    /// <summary>
    /// 实体类Bs_Products 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
[Serializable]
public class Bs_Products
{
    public Bs_Products()
    { }
    #region Model
    private string _pkid;
    private int _id;
    private string _code;
    private string _name;
    private string _bs_prokindcode;
    private string _bs_brandcode;
    private decimal? _price;
    private decimal? _pricein;
    private decimal? _pricemarket;
    private int? _quantity;
    private string _color;
    private string _intro;
    private string _weight;
    private string _pic;
    private string _content;
    private int? _sortno;
    private int? _status;
    private int? _isnew;
    private int? _ishotsales;
    private int? _issoldout;
    private int? _ispromote;
    private int? _iscommend;
    private int? _isindex;
    private int? _hits;
    private DateTime? _addtime;
    private string _adduser;
    private DateTime? _modtime;
    private string _moduser;
    private string _orderby;
    private string _size;
    private int? _fhit;
    private int? _sale;
    private string _colorpic;
    private string _Property;
    private string _keywords;
    private int? _score;
    private int? _pstatus;
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("pkid")]
    public string pkid
    {
        set { _pkid = value; }
        get { return _pkid; }
    }
  
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("ID")]
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Code")]
    public string Code
    {
        set { _code = value; }
        get { return _code; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Name")]
    public string Name
    {
        set { _name = value; }
        get { return _name; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("BS_ProKindCode")]
    public string BS_ProKindCode
    {
        set { _bs_prokindcode = value; }
        get { return _bs_prokindcode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Bs_BrandCode")]
    public string Bs_BrandCode
    {
        set { _bs_brandcode = value; }
        get { return _bs_brandcode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Price")]
    public decimal? Price
    {
        set { _price = value; }
        get { return _price; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("PriceIn")]
    public decimal? PriceIn
    {
        set { _pricein = value; }
        get { return _pricein; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("PriceMarket")]
    public decimal? PriceMarket
    {
        set { _pricemarket = value; }
        get { return _pricemarket; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Quantity")]
    public int? Quantity
    {
        set { _quantity = value; }
        get { return _quantity; }
    }
  
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Color")]
    public string Color
    {
        set { _color = value; }
        get { return _color; }
    }

    ///<summary>
    ///
    ///</summary>
    [DataContextAttribute("Weight")]
    public string Weight
    {
        set { _weight = value; }
        get { return _weight; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Intro")]
    public string Intro
    {
        set { _intro = value; }
        get { return _intro; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Pic")]
    public string Pic
    {
        set { _pic = value; }
        get { return _pic; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Content")]
    public string Content
    {
        set { _content = value; }
        get { return _content; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("SortNo")]
    public int? SortNo
    {
        set { _sortno = value; }
        get { return _sortno; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Status")]
    public int? Status
    {
        set { _status = value; }
        get { return _status; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("pStatus")]
    public int? pStatus
    {
        set { _pstatus = value; }
        get { return _pstatus; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("isNew")]
    public int? isNew
    {
        set { _isnew = value; }
        get { return _isnew; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("isHotSales")]
    public int? isHotSales
    {
        set { _ishotsales = value; }
        get { return _ishotsales; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("isSoldOut")]
    public int? isSoldOut
    {
        set { _issoldout = value; }
        get { return _issoldout; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("isPromote")]
    public int? isPromote
    {
        set { _ispromote = value; }
        get { return _ispromote; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("isCommend")]
    public int? isCommend
    {
        set { _iscommend = value; }
        get { return _iscommend; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("isIndex")]
    public int? isIndex
    {
        set { _isindex = value; }
        get { return _isindex; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Hits")]
    public int? Hits
    {
        set { _hits = value; }
        get { return _hits; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("score")]
    public int? score
    {
        set { _score = value; }
        get { return _score; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Addtime")]
    public DateTime? Addtime
    {
        set { _addtime = value; }
        get { return _addtime; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("AddUser")]
    public string AddUser
    {
        set { _adduser = value; }
        get { return _adduser; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("ModTime")]
    public DateTime? ModTime
    {
        set { _modtime = value; }
        get { return _modtime; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("ModUser")]
    public string ModUser
    {
        set { _moduser = value; }
        get { return _moduser; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Size")]
    public string Size
    {
        set { _size = value; }
        get { return _size; }
    }
  //  [DataContextAttribute("orderby")]
    public string orderby
    {
        set { _orderby = value; }
        get { return _orderby; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("sale")]
    public int? sale
    {
        set { _sale = value; }
        get { return _sale; }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("fhit")]
    public int? fhit
    {
        set { _fhit = value; }
        get { return _fhit; }
    }
       /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("colorpic")]
    public string colorpic
    {
        set { _colorpic = value; }
        get { return _colorpic; }
    }

    ///<summary>
    ///
    ///</summary>
    [DataContextAttribute("Property")]
    public string Property
    {
        set { _Property = value; }
        get { return _Property; }
    }
    public string keywords
    {
        set { _keywords = value; }
        get { return _keywords; }
    }

    #endregion Model

}

