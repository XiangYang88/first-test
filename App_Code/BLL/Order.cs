using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace BLL
{
    /// <summary>
    ///Order 的摘要说明
    /// </summary>
    public class Order
    {
        public Order()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 按用户的ID及类型来获得会员的订单
        /// </summary>
        /// <param name="userId">用户的ID</param>
        ///  /// <param name="opr">操作符号</param>
        /// <param name="type">订单状态号</param>
        /// <returns></returns>
        public static DataTable getOrderByUserId(int userId,int isEnd)
        {
            string sql = string.Format("select bs_Orders.*,sy_code.code as statusCode,sy_code.Name as orderStatus from bs_Orders " +
                " join sy_code on(bs_Orders.status = sy_code.code) "+
                " where bs_Orders.Bs_UserId={0} and bs_Orders.IsEnd={1} order by bs_Orders.addTime desc ", userId, isEnd);
            return CSA.DAL.DBAccess.getRS(sql);
        }

        /// <summary>
        /// 按订单的ＩＤ来获得订单的商品
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetailByOrderId(int orderId)
        {
            string sql = string.Format(@"select bs_Products.name,bs_Products.id,bs_Products.Pic,bs_OrdersDtl.* from bs_Orders
                            join bs_OrdersDtl on (bs_Orders.Code = bs_OrdersDtl.bs_OrdersCode)
                            join bs_Products on(bs_Products.Code = bs_OrdersDtl.bs_ProductsCode)
                            where bs_Orders.Id = {0}", orderId);
            return CSA.DAL.DBAccess.getRS(sql);
        }

        /// <summary>
        /// 更改订单的状态
        /// </summary>
        /// <param name="orderId">订单ＩＤ</param>
        /// <param name="status">订单状态码</param>
        /// <returns>成功返回true</returns>
        public static bool UpdateOrderStatus(int orderId, string status)
        {
            string sql = string.Format("update bs_Orders set status = '{0}' where id ={1}", status, orderId);
            return CSA.DAL.DBAccess.ExecuteNonQuery(sql) > 0;
        }
        /// <summary>
        /// 按订单的状体Code来获得订单的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetailByOrderStatu(string statuCode)
        {
            string sql = string.Format("select * from bs_Orders where status = '{0}'", statuCode);
            return CSA.DAL.DBAccess.getRS(sql);
        }
        /// <summary>
        /// 获取满足返点条件的用户ID和购物总额
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUseridAndAmount(string starttime,string endtime,int amount)
        {
            string sql = "select bs_userid,sum(Amount) as amount from bs_orders where addtime >='{0}' or addtime<='{1}' and Amount>={2} group by bs_userid";
            sql = string.Format(sql, starttime, endtime, amount);
            DataTable dt=CSA.DAL.DBAccess.getRS(sql);
            return dt;
        }
    }
}
