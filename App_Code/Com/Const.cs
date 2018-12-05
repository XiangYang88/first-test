using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace Com
{
    /// <summary>
    /// Const 的摘要说明
    /// </summary>
    public class Const
    {

        /// <summary>
        /// 产品属性_分割符  ,
        /// </summary>
        public const char cutProPre = ',';

        /// <summary>
        /// 分隔符
        /// </summary>
        public const char ItemSp = ',';


        //订单状态
        /// <summary>
        /// 新订单
        /// </summary>
        public const string orderState_new = "0101";
        public const string orderState_fault = "0102";
        public const string orderState_submit_wait_pay = "0103";
        public const string orderState_payed_wait_send = "0104";
        public const string orderState_stock_up = "0105";
        public const string orderState_sended_wait_submit = "0106";
        public const string orderState_submit_receive = "0107";
        public const string orderState_order_done = "0108";



        /// <summary>
        /// 存放SESSION的名  "UserInfo",  "check_code", "Cart" ,"Order"
        /// </summary>
        public static string[] sessionNames
        {
            get { return new string[] { "UserInfo", "check_code", "Cart" , "Order" }; }
        }


    }
}