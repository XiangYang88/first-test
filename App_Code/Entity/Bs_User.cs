using System;

	/// <summary>
	/// 实体类Bs_User 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
[Serializable]
public class Bs_User
{
    public Bs_User()
    { }
    #region Model
    private int _id;
    private string _code;
    private string _name;
    private string _bs_usergradecode;
    private string _levelname;
    private string _realname;
    private string _password;
    private string _sex;
    private DateTime? _birthday;
    private string _qq;
    private string _msn;
    private string _wanwan;
    private string _pic;
    private string _mobile;
    private string _phone;
    private string _fax;
    private string _email;
    private string _company;
    private string _address;
    private string _postcode;
    private string _county;
    private string _city;
    private string _province;
    private string _country;
    private string _csgname;
    private string _csgmobile;
    private string _csgphone;
    private string _csgaddress;
    private string _csgpostcode;
    private string _csgemail;
    private string _csgcounty;
    private string _csgcity;
    private string _csgprovince;
    private string _csgcountry;
    private string _question;
    private string _answer;
    private int? _lcount;
    private DateTime? _ltime;
    private string _lip;
    private int? _totalscore;
    private int? _usablescore;
    private decimal? _account;
    private DateTime? _addtime;
    private DateTime? _modtime;
    private string _moduser;
    private string _status;
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
    /// 
    [DataContextAttribute("Code")]
    public string Code
    {
        set { _code = value; }
        get { return _code; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// 
    [DataContextAttribute("Name")]
    public string Name
    {
        set { _name = value; }
        get { return _name; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Bs_UserGradeCode")]
    public string Bs_UserGradeCode
    {
        set { _bs_usergradecode = value; }
        get { return _bs_usergradecode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("levelName")]
    public string levelName
    {
        set { _levelname = value; }
        get { return _levelname; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("RealName")]
    public string RealName
    {
        set { _realname = value; }
        get { return _realname; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Password")]
    public string Password
    {
        set { _password = value; }
        get { return _password; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Sex")]
    public string Sex
    {
        set { _sex = value; }
        get { return _sex; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Birthday")]
    public DateTime? Birthday
    {
        set { _birthday = value; }
        get { return _birthday; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("QQ")]
    public string QQ
    {
        set { _qq = value; }
        get { return _qq; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("MSN")]
    public string MSN
    {
        set { _msn = value; }
        get { return _msn; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("WanWan")]
    public string WanWan
    {
        set { _wanwan = value; }
        get { return _wanwan; }
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
    [DataContextAttribute("Mobile")]
    public string Mobile
    {
        set { _mobile = value; }
        get { return _mobile; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Phone")]
    public string Phone
    {
        set { _phone = value; }
        get { return _phone; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Fax")]
    public string Fax
    {
        set { _fax = value; }
        get { return _fax; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("EMail")]
    public string EMail
    {
        set { _email = value; }
        get { return _email; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Company")]
    public string Company
    {
        set { _company = value; }
        get { return _company; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Address")]
    public string Address
    {
        set { _address = value; }
        get { return _address; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("PostCode")]
    public string PostCode
    {
        set { _postcode = value; }
        get { return _postcode; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("County")]
    public string County
    {
        set { _county = value; }
        get { return _county; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("City")]
    public string City
    {
        set { _city = value; }
        get { return _city; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Province")]
    public string Province
    {
        set { _province = value; }
        get { return _province; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Country")]
    public string Country
    {
        set { _country = value; }
        get { return _country; }
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
    [DataContextAttribute("csgEmail")]
    public string csgEmail
    {
        set { _csgemail = value; }
        get { return _csgemail; }
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
    [DataContextAttribute("csgCity")]
    public string csgCity
    {
        set { _csgcity = value; }
        get { return _csgcity; }
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
    [DataContextAttribute("csgCountry")]
    public string csgCountry
    {
        set { _csgcountry = value; }
        get { return _csgcountry; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Question")]
    public string Question
    {
        set { _question = value; }
        get { return _question; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Answer")]
    public string Answer
    {
        set { _answer = value; }
        get { return _answer; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("LCount")]
    public int? LCount
    {
        set { _lcount = value; }
        get { return _lcount; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("LTime")]
    public DateTime? LTime
    {
        set { _ltime = value; }
        get { return _ltime; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("LIP")]
    public string LIP
    {
        set { _lip = value; }
        get { return _lip; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("TotalScore")]
    public int? TotalScore
    {
        set { _totalscore = value; }
        get { return _totalscore; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("UsableScore")]
    public int? UsableScore
    {
        set { _usablescore = value; }
        get { return _usablescore; }
    }
    /// <summary>
    /// 
    /// </summary>
    [DataContextAttribute("Account")]
    public decimal? Account
    {
        set { _account = value; }
        get { return _account; }
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
    [DataContextAttribute("Status")]
    public string Status
    {
        set { _status = value; }
        get { return _status; }
    }
    #endregion Model

}
