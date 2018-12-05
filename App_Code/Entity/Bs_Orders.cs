using System;
	/// <summary>
	/// 实体类Bs_Orders 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
[Serializable]
public class Bs_Orders
{
    public Bs_Orders()
    { }
    #region Model
    private int _id;
    private string _code;
    private int? _bs_userid;
    private int? _quantity;
    private decimal? _amount;
    private decimal? _deliverfee;
    private decimal? _productfee;
    private decimal? _additionalfee;
    private decimal? _discount;
    private decimal? _useraccount;
    private string _csgname;
    private string _csgcountry;
    private string _csgprovince;
    private string _csgcity;
    private string _csgcounty;
    private string _csgaddress;
    private string _csgpostcode;
    private string _csgmobile;
    private string _csgphone;
    private string _csgemail;
    private string _csgfax;
    private string _billname;
    private string _billcountry;
    private string _billprovince;
    private string _billcity;
    private string _billcounty;
    private string _billaddress;
    private string _billpostcode;
    private string _billphone;
    private string _billmobile;
    private string _billemail;
    private string _billfax;
    private string _sy_delivercode;
    private string _deliverno;
    private string _sy_paycode;
    private string _payno;
    private string _message;
    private string _notes;
    private string _status;
    private int? _isend;
    private int? _ispay;
    private string _addtime;
    private string _adduser;
    private string _modtime;
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
    [DataContextAttribute("Code")]
    public string Code
    {
        set { _code = value; }
        get { return _code; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Bs_UserID")]
    public int? Bs_UserID
    {
        set { _bs_userid = value; }
        get { return _bs_userid; }
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
    [DataContextAttribute("Amount")]
    public decimal? Amount
    {
        set { _amount = value; }
        get { return _amount; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("DeliverFee")]
    public decimal? DeliverFee
    {
        set { _deliverfee = value; }
        get { return _deliverfee; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("ProductFee")]
    public decimal? ProductFee
    {
        set { _productfee = value; }
        get { return _productfee; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("AdditionalFee")]
    public decimal? AdditionalFee
    {
        set { _additionalfee = value; }
        get { return _additionalfee; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Discount")]
    public decimal? Discount
    {
        set { _discount = value; }
        get { return _discount; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("UserAccount")]
    public decimal? UserAccount
    {
        set { _useraccount = value; }
        get { return _useraccount; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgName")]
    public string csgName
    {
        set { _csgname = value; }
        get { return _csgname; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgCountry")]
    public string csgCountry
    {
        set { _csgcountry = value; }
        get { return _csgcountry; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgProvince")]
    public string csgProvince
    {
        set { _csgprovince = value; }
        get { return _csgprovince; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgCity")]
    public string csgCity
    {
        set { _csgcity = value; }
        get { return _csgcity; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgCounty")]
    public string csgCounty
    {
        set { _csgcounty = value; }
        get { return _csgcounty; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgAddress")]
    public string csgAddress
    {
        set { _csgaddress = value; }
        get { return _csgaddress; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgPostCode")]
    public string csgPostCode
    {
        set { _csgpostcode = value; }
        get { return _csgpostcode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgMobile")]
    public string csgMobile
    {
        set { _csgmobile = value; }
        get { return _csgmobile; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgPhone")]
    public string csgPhone
    {
        set { _csgphone = value; }
        get { return _csgphone; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgEmail")]
    public string csgEmail
    {
        set { _csgemail = value; }
        get { return _csgemail; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("csgFax")]
    public string csgFax
    {
        set { _csgfax = value; }
        get { return _csgfax; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billName")]
    public string billName
    {
        set { _billname = value; }
        get { return _billname; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billCountry")]
    public string billCountry
    {
        set { _billcountry = value; }
        get { return _billcountry; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billProvince")]
    public string billProvince
    {
        set { _billprovince = value; }
        get { return _billprovince; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billCity")]
    public string billCity
    {
        set { _billcity = value; }
        get { return _billcity; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billCounty")]
    public string billCounty
    {
        set { _billcounty = value; }
        get { return _billcounty; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billAddress")]
    public string billAddress
    {
        set { _billaddress = value; }
        get { return _billaddress; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billPostCode")]
    public string billPostCode
    {
        set { _billpostcode = value; }
        get { return _billpostcode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billPhone")]
    public string billPhone
    {
        set { _billphone = value; }
        get { return _billphone; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billMobile")]
    public string billMobile
    {
        set { _billmobile = value; }
        get { return _billmobile; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billEmail")]
    public string billEmail
    {
        set { _billemail = value; }
        get { return _billemail; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("billFax")]
    public string billFax
    {
        set { _billfax = value; }
        get { return _billfax; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Sy_DeliverCode")]
    public string Sy_DeliverCode
    {
        set { _sy_delivercode = value; }
        get { return _sy_delivercode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("DeliverNo")]
    public string DeliverNo
    {
        set { _deliverno = value; }
        get { return _deliverno; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Sy_PayCode")]
    public string Sy_PayCode
    {
        set { _sy_paycode = value; }
        get { return _sy_paycode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("PayNo")]
    public string PayNo
    {
        set { _payno = value; }
        get { return _payno; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Message")]
    public string Message
    {
        set { _message = value; }
        get { return _message; }
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
    [DataContextAttribute("Status")]
    public string Status
    {
        set { _status = value; }
        get { return _status; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("IsEnd")]
    public int? IsEnd
    {
        set { _isend = value; }
        get { return _isend; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("IsPay")]
    public int? IsPay
    {
        set { _ispay = value; }
        get { return _ispay; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("AddTime")]
    public string AddTime
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
    public string ModTime
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

