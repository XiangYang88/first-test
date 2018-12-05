using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Bs_OrderDao 的摘要说明
/// </summary>
public class Bs_OrderDao
{
    /// <summary>
    /// 新增订单
    /// </summary>
    /// <param name="order"></param>
    public void addOrder(Bs_Orders order)
    {
        CSA.DAL.SQLBuilder builder = new CSA.DAL.SQLBuilder();
        builder.TblName = "bs_orders";
        builder.AddData("Code", order.Code);
        builder.AddData("Bs_UserID", order.Bs_UserID);
        builder.AddData("Quantity", order.Quantity);
        builder.AddData("Amount", order.Amount);
        builder.AddData("DeliverFee", order.DeliverFee);
        builder.AddData("ProductFee", order.ProductFee);
        builder.AddData("AdditionalFee", order.AdditionalFee);
        builder.AddData("Discount", order.Discount);
        builder.AddData("UserAccount", order.UserAccount);
        builder.AddData("csgName", order.csgName);
        builder.AddData("csgCountry", order.csgCountry);
        builder.AddData("csgProvince", order.csgProvince);
        builder.AddData("csgCity", order.csgCity);
        builder.AddData("csgCounty", order.csgCounty);
        builder.AddData("csgAddress", order.csgAddress);
        builder.AddData("csgPostCode", order.csgPostCode);
        builder.AddData("csgMobile", order.csgMobile);
        builder.AddData("csgPhone", order.csgPhone);
        builder.AddData("csgEmail", order.csgEmail);
        builder.AddData("csgFax", order.csgFax);
        builder.AddData("billName", order.billName);
        builder.AddData("billCountry", order.billCountry);
        builder.AddData("billProvince", order.billProvince);
        builder.AddData("billCity", order.billCity);
        builder.AddData("billCounty", order.billCounty);
        builder.AddData("billAddress", order.billAddress);
        builder.AddData("billPostCode", order.billPostCode);
        builder.AddData("billPhone", order.billPhone);
        builder.AddData("billMobile", order.billMobile);
        builder.AddData("billEmail", order.billEmail);
        builder.AddData("billFax", order.billFax);
        builder.AddData("Sy_DeliverCode", order.Sy_DeliverCode);
        builder.AddData("DeliverNo", order.DeliverNo);
        builder.AddData("Sy_PayCode", order.Sy_PayCode);
        builder.AddData("PayNo", order.PayNo);
        builder.AddData("Message", order.Message);
        builder.AddData("Notes", order.Notes);
        builder.AddData("Status", order.Status);
        builder.AddData("AddTime", order.AddTime);
        builder.AutoInsert();

    }




    /// <summary>
    /// 保存订单商品详细
    /// </summary>
    /// <param name="orderDtl"></param>
    public void addOrderDtl(Bs_OrdersDtl orderDtl)
    {
        CSA.DAL.SQLBuilder builder = new CSA.DAL.SQLBuilder();
        builder.TblName = "bs_ordersdtl";
        builder.AddData("Bs_OrdersCode", orderDtl.Bs_OrdersCode);
        builder.AddData("Bs_ProductsCode", orderDtl.Bs_ProductsCode);
        builder.AddData("Quantity", orderDtl.Quantity);
        builder.AddData("Price", orderDtl.Price);
        //builder.AddData("Amount", orderDtl.Amount);
        builder.AddData("Dimension", orderDtl.Dimension);
        builder.AutoInsert();
    }


    /// <summary>
    /// 获取订单
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DataTable getOrderByCode(string code)
    {
        string strSql = string.Format("select * from bs_orders where code = '{0}' ", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 设为无效订单
    /// </summary>
    /// <param name="code"></param>
    public void editOrderToFault(string code)
    {
        string strSql = string.Format("select * from bs_orders where code = '{0}' ", code);
        DataRow drOrder = Com.Util.getDrByTable(CSA.DAL.DBAccess.getRS(strSql));
        string payCode = drOrder["Status"].ToString();
        if (payCode == Com.Const.orderState_new || payCode == Com.Const.orderState_submit_wait_pay)
        {
            strSql = string.Format("update bs_orders set Status = '{0}' where Code = '{1}'",Com.Const.orderState_fault,code);
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }
    }

    /// <summary>
    /// 设为无效礼品订单
    /// </summary>
    /// <param name="code"></param>
    public void editgiftOrderToFault(string code)
    {
        string strSql = string.Format("select * from Bs_GiftApply where code = '{0}' ", code);
        DataRow drOrder = Com.Util.getDrByTable(CSA.DAL.DBAccess.getRS(strSql));
        string payCode = drOrder["Status"].ToString();
        if (payCode == "0301" || payCode =="0302")
        {
            strSql = string.Format("update Bs_GiftApply set Status = '0102' where Code = '{0}'", code);
            CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
        }
    }


    /// <summary>
    /// 更新订单状态
    /// </summary>
    /// <param name="code"></param>
    /// <param name="orderStatus"></param>
    public void updateOrderStatus(string code, string orderStatus)
    {
        string strSql;
        strSql = string.Format("update bs_orders set Status = '{0}' where Code = '{1}'", orderStatus, code);
        CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
    }

    /// <summary>
    /// 更新礼品订单状态
    /// </summary>
    /// <param name="code"></param>
    /// <param name="orderStatus"></param>
    public void updategiftOrderStatus(string code, string orderStatus)
    {
        string strSql;
        strSql = string.Format("update Bs_GiftApply set Status = '{0}' where Code = '{1}'", orderStatus, code);
        CSA.DAL.DBAccess.ExecuteNonQuery(strSql);
    }


    /// <summary>
    /// 获取订单详细
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public DataTable getOrderDetailByCode(string code)
    {
        string strSql = string.Format("select a.*,b.name,b.pic,b.id proid,b.code,b.BS_ProKindCode kindCode from Bs_OrdersDtl a inner join Bs_Products b on a.Bs_ProductsCode=b.id where a.Bs_OrdersCode = '{0}' ", code);
        return CSA.DAL.DBAccess.getRS(strSql);

    }
    /// <summary>
    /// 获取礼品订单详细
    /// </summary>
    /// <param name="code">订单编号</param>
    /// <returns></returns>
    public DataTable getGiftDetailBycode(string code)
    {
        string strSql = string.Format("select a.*,b.name,b.color,b.pic,b.price from Bs_GiftApply a inner join Bs_Gift b on a.Bs_GiftCode=b.Code where a.code = '{0}' ", code);
        return CSA.DAL.DBAccess.getRS(strSql);

    }


    /// <summary>
    /// 获取订单
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="status">订单状态</param>
    /// <returns></returns>
    public DataTable getOrdersByUserId(int userId,string status)
    {
        string strWhere = "";
        if (status != "")
        {
            strWhere = string.Format(" and a.status = '{0}'", status);
        }
        string strSql = string.Format("select a.*,pro.code proCode from bs_orders a left join Bs_OrdersDtl b on(a.code=b.Bs_OrdersCode) inner join bs_products pro on(b.Bs_ProductsCode=pro.id) where Bs_UserID = {0} {1} order by addtime desc", userId, strWhere);
        return CSA.DAL.DBAccess.getRS(strSql);
    }
    /// <summary>
    /// 获取礼品订单
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="status">订单状态</param>
    /// <returns></returns>
    public DataTable getGiftOrderByUserId(int userId, string status)
    {
        string strWhere = "";
        if (status != "")
        {
            strWhere = string.Format(" and status='{0}'", status);
        }
        string strSql = string.Format("select * from Bs_GiftApply where Bs_UserID = {0} {1} order by addtime desc", userId, strWhere);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 根据表明获取总数
    /// </summary>
    /// <param name="tblName">表名</param>
    /// <param name="userid">用户id</param>
    /// <returns></returns>
    public  int getOrderTotal(string Tblname,int userid)
    {
        DataTable dt = CSA.DAL.DBAccess.getRS("select count(*) as countd from " + Tblname + " where Bs_UserID ="+userid+"");
        return Convert.ToInt32(dt.Rows[0]["countd"].ToString());
    }

    /// <summary>
    /// 获取订单
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="status">订单状态</param>
    /// <returns></returns>
    public DataTable getOrdersByUserIdInStatus(int userId, string status)
    {
        string strWhere = "";
        if (status != "")
        {
            strWhere = string.Format(" and trim(status) in ({0})", status);
        }
        string strSql = string.Format("select * from bs_orders where Bs_UserID = {0} {1} order by addtime desc", userId, strWhere);
        return CSA.DAL.DBAccess.getRS(strSql);
    }


    /// <summary>
    /// 获取订单状态
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public DataTable getOrderStatusByCode(string code)
    {
        string strSql = string.Format("select * from sy_code where code = '{0}'", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }




    /// <summary>
    /// 获取支付方式
    /// </summary>
    /// <returns></returns>
    public DataTable getPayList()
    {
        string strSql = "select * from sy_pay where status = 0 order by sortno";
        return CSA.DAL.DBAccess.getRS(strSql);
    }


    /// <summary>
    /// 获取运输方式
    /// </summary>
    /// <returns></returns>
    public DataTable getDeliverList()
    {
        string strSql = "select * from sy_deliver where status = 0 order by sortno";
        return CSA.DAL.DBAccess.getRS(strSql);
    }


    /// <summary>
    /// 获取支付方式
    /// </summary>
    /// <returns></returns>
    public DataTable getPayByCode(string code)
    {
        string strSql = string.Format("select * from sy_pay where code ='{0}' order by sortno", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }

    /// <summary>
    /// 获取运输方式
    /// </summary>
    /// <returns></returns>
    public DataTable getDeliverByCode(string code)
    {
        string strSql = string.Format("select * from sy_deliver where code ='{0}' order by sortno", code);
        return CSA.DAL.DBAccess.getRS(strSql);
    }
}
