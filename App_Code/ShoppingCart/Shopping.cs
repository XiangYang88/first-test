using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace BLL.ShoppingCart
{
    /// <summary>
    /// Shopping 的摘要说明
    /// </summary>
    public class Shopping
    {
        public Shopping()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public static string KID
        {
            get
            {
                string sid = CSA.HC.CookiesHelper.get("Bs_SessionID");
                if (sid == "")
                {
                    sid = DateTime.Now.ToString("yyyyMMddhhmmss") + (new Random()).Next(1000, 9999).ToString();
                    CSA.HC.CookiesHelper.set("Bs_SessionID", sid, 24);
                }
                return sid;
            }
        }
        public const string KEY = "ShoppingCart";
        
        public static Cart getCart()
        {
            string sql = "select ss_value from Bs_Session where kid='{0}' and ss_key='{1}'";
            sql = string.Format(sql, KID, KEY);

            object c = CSA.DAL.DBAccess.ExecuteScalar(sql);
            if (c == null)
            {
                return new Cart();
            }
            else
            {
                Cart cart = new Cart();
                cart.Input(c.ToString());
                return cart;
            }
        }

        public static Product getProduct(int id,string color,int qty,string dimension)
        {
            Product p = new Product();
            return p;
        }
        
        public static void killCart()
        {

            string sql = "delete from Bs_Session where kid='{0}' and ss_key='{1}'";
            sql = string.Format(sql, KID, KEY);
            CSA.DAL.DBAccess.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 保存购物车
        /// </summary>
        /// <param name="cart"></param>
        public static void saveCart(Cart cart)
        {
            //string sql = "exec Update_Session '{0}','{1}','{2}'";
            //sql = string.Format(sql, KID, KEY, cart.Output());
            //CSA.DAL.DBAccess.ExecuteNonQuery(sql);
            killCart();
            string strSql = string.Format("insert into Bs_session (kid,ss_key,ss_value) values ('{0}','{1}','{2}')",KID,KEY,cart.Output());
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }

        ///// <summary>
        ///// 保存购物车
        ///// </summary>
        ///// <param name="cart"></param>
        //public static void saveCart(Cart cart)
        //{
        //    string sql = "exec Update_Session '{0}','{1}','{2}'";
        //    sql = string.Format(sql, KID, KEY, cart.Output());
        //    CSA.DAL.DBAccess.ExecuteNonQuery(sql);
        //}
    }
}