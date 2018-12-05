

namespace BLL.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Cart
    {
        Dictionary<string, Product> ProList = new Dictionary<string, Product>();

        private const string ItemDelimiter = "$$";//项分隔符
        private const string ColumnDelimiter = "@@";//字段分隔符

        public void Add(Product pro)
        {
            if (!ProList.ContainsKey(pro.Num))
            {
              
                ProList.Add(pro.Num, pro);
            }
        }
        public bool Exist(string num)
        {
            if (ProList.ContainsKey(num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Dictionary<string, Product> Values
        {
            get { return ProList; }
            set { ProList = value; }
        }
        public Product this[string num]
        {
            get
            {
                if (ProList.ContainsKey(num))
                {
                    return ProList[num];
                }
                else
                {
                    return new Product();
                }
            }
        }
        public void Remove(string num)
        {
            if (ProList.ContainsKey(num))
            {
                ProList.Remove(num);
            }
        }
        public void Clear()
        {
            ProList.Clear();
        }
        /// <summary>
        /// 导入购物车
        /// </summary>
        /// <param name="CookiesString">购物车的cookies</param>
        public void Input(string CookiesString)
        {
            string[] _list = CookiesString.Split(new string[] { ItemDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in _list)
            {
                string[] _info = s.Split(new string[] { ColumnDelimiter }, StringSplitOptions.None);
                if (_info.Length == 10)
                {
                    Product _p = new Product(int.Parse(_info[0]), _info[1], _info[2], _info[3], decimal.Parse(_info[4]), int.Parse(_info[5]), int.Parse(_info[6]),_info[7],_info[8]);
                    this.Add(_p);
                }
            }
        }
        /// <summary>
        /// 导出购物车
        /// </summary>
        /// <returns>购物车的cookies</returns>
        public string Output()
        {
            string _o = "";
            foreach (Product p in this.Values.Values)
            {
                _o += ItemDelimiter + p.ProId + ColumnDelimiter + p.Name + ColumnDelimiter + p.Num + ColumnDelimiter +
                      p.Pic
                      + ColumnDelimiter + p.Price + ColumnDelimiter + p.Qty + ColumnDelimiter + p.status
                      + ColumnDelimiter + p.Color + ColumnDelimiter + p.Size + ColumnDelimiter + p.Width;
            }
            return _o;
        }
    }
}
