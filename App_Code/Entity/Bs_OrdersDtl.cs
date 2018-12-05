using System;
	/// <summary>
	/// 实体类Bs_OrdersDtl 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
[Serializable]
public class Bs_OrdersDtl
{
    public Bs_OrdersDtl()
    { }
    #region Model
    private int _id;
    private string _bs_orderscode;
    private string _bs_productscode;
    private int? _quantity;
    private decimal? _price;
    private decimal? _amount;
    private string _color;
    private string _width;
    private string _dimension;
    private int? _sendqty;
    private int? _backqty;
    private string _notes;
    private string _sendnotes;
    private string _backnotes;
    private DateTime? _addtime;
    private string _adduser;
    private DateTime? _modtime;
    private string _moduser;
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
    [DataContextAttribute("Bs_OrdersCode")]
    public string Bs_OrdersCode
    {
        set { _bs_orderscode = value; }
        get { return _bs_orderscode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Bs_ProductsCode")]
    public string Bs_ProductsCode
    {
        set { _bs_productscode = value; }
        get { return _bs_productscode; }
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
    [DataContextAttribute("Price")]
    public decimal? Price
    {
        set { _price = value; }
        get { return _price; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Amount")]
    public decimal? Amount
    {
        set { _amount = value; }
        get { return _amount; }
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
    [DataContextAttribute("Width")]
    public string Width
    {
        set { _width = value; }
        get { return _width; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Dimension")]
    public string Dimension
    {
        set { _dimension = value; }
        get { return _dimension; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("SendQty")]
    public int? SendQty
    {
        set { _sendqty = value; }
        get { return _sendqty; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("BackQty")]
    public int? BackQty
    {
        set { _backqty = value; }
        get { return _backqty; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Notes")]
    public string Notes
    {
        set { _notes = value; }
        get { return _notes; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("SendNotes")]
    public string SendNotes
    {
        set { _sendnotes = value; }
        get { return _sendnotes; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("BackNotes")]
    public string BackNotes
    {
        set { _backnotes = value; }
        get { return _backnotes; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("AddTime")]
    public DateTime? AddTime
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
    #endregion Model

}

