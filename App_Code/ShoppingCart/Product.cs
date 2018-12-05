using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ShoppingCart
{
    /// <summary>
    /// ��Ʒ��
    /// </summary>
    public class Product
    {
        string _num = "";

        public Dictionary<string, string> Info = new Dictionary<string, string>();
        int _proId;


        /// <summary>
        /// ��ƷID
        /// </summary>
        public int ProId
        {
            get { return _proId; }
            set { _proId = value; }
        }
       

        /// <summary>
        /// ��Ʒ���
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
        string _sp = "|||"; //_other������Ϣ�ķָ���
        private string _color = "";
        private string _size = "";
        private string _width = "";
        /// <summary>
        /// ��Ʒ���
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
        /// ��ƷͼƬ
        /// </summary>        
        public string Pic
        {
            get { return _pic; }
            set { _pic = value; }
        }
        ///<summary>
        ///��Ʒ����
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
        /// ״̬
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// ��Ʒ����
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
        /// ��Ʒ����
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
        /// ��Ʒ����
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
        /// ��Ʒ�ܼ�
        /// </summary>
        public decimal Pay
        {
            get
            {
                return _price * _qty;
            }
        }

        /// <summary>
        /// ��ɫ
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
            /// ��ɫ
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
        /// �����л�Info����
        /// </summary>
        /// <param name="s">������ַ���</param>
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
        /// ���л�Info����
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
        /// ��Ӳ�Ʒ��Ϣ
        /// </summary>     
        /// <param name="num">��ƷID</param>
        /// <param name="name">����</param>
        /// <param name="num">��Ʒ���</param>
        /// <param name="qty">��Ʒ����</param>
        /// <param name="price">��Ʒ����</param>
        /// <param name="pic">��ƷͼƬ</param>
        /// <param name="status">��Ʒ״̬</param>
        /// <param name="color">��Ʒ��ɫ</param>
       /// <param name="quantity">��Ʒ����</param>
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
