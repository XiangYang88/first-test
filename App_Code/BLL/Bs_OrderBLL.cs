using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BLL.ShoppingCart;
using Com;
/// <summary>
/// Bs_OrderBLL 的摘要说明
/// </summary>
public class Bs_OrderBLL
{
    public readonly Bs_OrderDao dao = new Bs_OrderDao();


    /// <summary>
    /// 新增订单
    /// </summary>
    /// <param name="order"></param>
    /// <param name="cart"></param>
    /// <param name="proCode"></param>
    /// <param name="pay"></param>
    /// <param name="userId"></param>
    public void addOrder(Bs_Orders order, Cart cart, ref string proCode, ref decimal pay, int userId)
    {
        decimal deliverPay = 0;

        DataTable dtDeliver = dao.getDeliverByCode(order.Sy_DeliverCode);

        if (dtDeliver.Rows.Count > 0)
        {
            deliverPay = decimal.Parse(dtDeliver.Rows[0]["amount"].ToString());
            order.DeliverFee = deliverPay;
        }
        Random rd = new Random();
        string orderCode = DateTime.Now.ToString("yyMMddHHmmssff") + rd.Next(1000, 9999).ToString();
        order.Code = orderCode;
       // order.Discount = BLL.BsUser.User.GetDisCount();
        decimal Amount = 0;
        int qty = 0;
        foreach (Product cartPro in cart.Values.Values)
        {
            Bs_Products pro =  Factory.getProBllInstance().getProById(cartPro.ProId);
            Bs_OrdersDtl orderDtl = new Bs_OrdersDtl();
            orderDtl.Bs_OrdersCode = orderCode;
            orderDtl.Price = decimal.Parse(cartPro.Price.ToString());
            orderDtl.Quantity = cartPro.Qty;
            orderDtl.Bs_ProductsCode = pro.ID.ToString();
            orderDtl.Amount = (decimal)(cartPro.Price * cartPro.Qty);
            orderDtl.Color = cartPro.Color;
            orderDtl.Width = cartPro.Width;
            orderDtl.Dimension = cartPro.Size;
            dao.addOrderDtl(orderDtl);
            qty += cartPro.Qty;
            Amount += (decimal)orderDtl.Amount;
        }
        order.DeliverFee = (decimal)deliverPay;
        order.ProductFee = Amount;

        Amount += (decimal)deliverPay;
        pay = Amount;
        order.Bs_UserID = userId;
        order.Quantity = qty;
        order.Amount = pay;
        order.ModTime = DateTime.Now.ToString("s");
        order.AddTime = DateTime.Now.ToString("s");
        order.Status = Const.orderState_new;
        proCode = order.Code;
        dao.addOrder(order);
    }


    /// <summary>
    /// 设为无效订单
    /// </summary>
    /// <param name="code"></param>
    public void editOrderToFault(string code) {
        dao.editOrderToFault(code);
    }

    /// <summary>
    /// 设为无效礼品订单
    /// </summary>
    /// <param name="code"></param>
    public void editgiftOrderToFault(string code)
    {
        dao.editgiftOrderToFault(code);
    }

    /// <summary>
    /// 更新订单状态
    /// </summary>
    /// <param name="code"></param>
    /// <param name="orderStatus"></param>
    public void updateOrderStatus(string code,string orderStatus)
    {
        dao.updateOrderStatus(code, orderStatus);
    }

    /// <summary>
    /// 更新礼品订单状态
    /// </summary>
    /// <param name="code"></param>
    /// <param name="orderStatus"></param>
    public void updategiftOrderStatus(string code, string orderStatus)
    {
        dao.updategiftOrderStatus(code, orderStatus);
    }



    /// <summary>
    /// 获取订单
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public Bs_Orders getOrderByCode(string code)
    {
        Bs_Orders order = new Bs_Orders();
        Com.Util.ConvertToEntity(order, dao.getOrderByCode(code));
        return order;
    }


    /// <summary>
    /// 获取进行中的订单
    /// </summary>
    /// <returns></returns>
    public DataTable getOrderingStatusByUserId(int userId)
    {
        string status = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}'", "0101", "0103", "0104", "0105", "0106", "0107");
        return dao.getOrdersByUserIdInStatus(userId, status);
    }


    /// <summary>
    /// 获取结束的订单
    /// </summary>
    /// <returns></returns>
    public DataTable getOrderendStatusByUserId(int userId)
    {
        string status = string.Format("'{0}','{1}'", "0102", "0108");
        return dao.getOrdersByUserIdInStatus(userId, status);
    }


    /// <summary>
    /// 获取订单详细
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DataTable getOrderDetailByCode(string code)
    {
        return dao.getOrderDetailByCode(code);


    }

    /// <summary>
    /// 获取礼品订单详细
    /// </summary>
    /// <param name="code">订单编号</param>
    /// <returns></returns>
    public DataTable getGiftDetailBycode(string code)
    {
        return dao.getGiftDetailBycode(code);
    }

    



    /// <summary>
    /// 获取订单
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="status">订单状态</param>
    /// <returns></returns>
    public DataTable getOrdersByUserId(int userId, string status)
    {
        return dao.getOrdersByUserId(userId, status);
    }

    /// <summary>
    /// 获取订单
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public DataTable getOrdersByUserId(int userId)
    {
        return dao.getOrdersByUserId(userId, "");
    }

    /// <summary>
    /// 获取礼品订单
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="status">订单状态</param>
    /// <returns></returns>
    public DataTable getGiftOrderByUserId(int userId, string status)
    {
        return dao.getGiftOrderByUserId(userId,status);
    }
    /// <summary>
    /// 获取订单状态
    /// </summary>
    /// <returns></returns>
    public DataRow getOrderStatusByCode(string code)
    {
        return Com.Util.getDrByTable(dao.getOrderStatusByCode(code));
    }

    /// <summary>
    /// 根据表明获取总数
    /// </summary>
    /// <param name="tblName">表名</param>
    /// <param name="userid">用户id</param>
    /// <returns></returns>
    public int getOrderTotal(string tblName,int userid)
    {
        return dao.getOrderTotal(tblName, userid);
    }


    /// <summary>
    /// 获取支付方式
    /// </summary>
    /// <returns></returns>
    public DataTable getPayList()
    {
        return dao.getPayList();
    }

    /// <summary>
    /// 获取运输方式
    /// </summary>
    /// <returns></returns>
    public DataTable getDeliverList()
    {
        return dao.getDeliverList();
    }

    /// <summary>
    /// 获取支付方式
    /// </summary>
    /// <returns></returns>
    public DataTable getPayByCode(string code)
    {
        return dao.getPayByCode(code);
    }

    /// <summary>
    /// 获取运输方式
    /// </summary>
    /// <returns></returns>
    public DataTable getDeliverByCode(string code)
    {
        return dao.getDeliverByCode(code);
    }


}
