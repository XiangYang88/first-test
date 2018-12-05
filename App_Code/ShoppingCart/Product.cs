using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ShoppingCart
{
    /// <summary>
    /// 产品类
    /// </summary>
    public class Product
    {
        string _num = "";

        public Dictionary<string, string> Info = new Dictionary<string, string>();
        int _proId;


        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProId
        {
            get { return _proId; }
            set { _proId = value; }
        }
       

        /// <summary>
        /// 产品编号
        /// </summary>
        public string Num
        {
            get { return _num; }
            set { _num = value; }
        }
        string _name = "", _pic = "";
        decimal _price = 0;
        int _qty = 0;
        int _status=0;
        string _quantity ="";
        string _sp = "|||"; //_other多信信息的分隔符
        private string _color = "";
        private string _size = "";
        private string _width = "";
        /// <summary>
        /// 产品宽度
        /// </summary>
        public string Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        /// <summary>
        /// 产品图片
        /// </summary>        
        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }
        ///<summary>
        ///产品重量
        ///</summary>
        public string quantity
        {
            get 
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int Qty
        {
            get
            {
                return _qty;
            }
            set
            {
                _qty = value;
            }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        /// <summary>
        /// 产品总价
        /// </summary>
        public decimal Pay
        {
            get
            {
                return _price * _qty;
            }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

            /// <summary>
            /// 颜色
            /// </summary>
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        /// <summary>
        /// 反序列化Info对象
        /// </summary>
        /// <param name="s">输入的字符串</param>
        public void DeserializeInfo(string s)
        {
            string[] list = s.Split(new string[] { _sp }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string _l in list)
            {
                string[] info = _l.Split(new char[] { '=' });
                if (Info.ContainsKey(info[0]))
                {
                    Info[info[0]] = info[1];
                }
                else
                {
                    Info.Add(info[0], info[1]);
                }
            }
        }
        /// <summary>
        /// 序列化Info对象
        /// </summary>
        /// <returns></returns>
        public string SerializeInfo()
        {
            string temp = "";
            foreach (string s in Info.Keys)
            {
                temp += _sp + s + "=" + Info[s];
            }
            return temp;
        }
        /// <summary>
        /// 添加产品信息
        /// </summary>     
        /// <param name="num">产品ID</param>
        /// <param name="name">名称</param>
        /// <param name="num">产品编号</param>
        /// <param name="qty">产品数量</param>
        /// <param name="price">产品单价</param>
        /// <param name="pic">产品图片</param>
        /// <param name="status">产品状态</param>
        /// <param name="color">产品颜色</param>
       /// <param name="quantity">产品数量</param>
        public Product(int proId, string name, string num, string pic, decimal price, int qty, int status, string color, string quantity)
        {
            _proId = proId;
            _name = name;
            _num = num;
            _price = price;
            _qty = qty;
            _pic = pic;
            _color = color;
            _quantity=quantity;
            _status = status;
        }
        //  p.Name + ColumnDelimiter + p.Num+ ColumnDelimiter + p.Pay+ ColumnDelimiter
        //               + p.Pic + ColumnDelimiter + p.Price+ ColumnDelimiter + p.Qty;
        public Product()
        {

        }
    }
}
