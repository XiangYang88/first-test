using System;
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
    /// 单件生成业务处理
    /// </summary>
    public class Factory
    {
        private static Bs_UserBLL _userBLL = null;
        private static Bs_OrderBLL _orderBLL = null;
        private static Bs_ProBLL _proBLL = null;
        public static Bs_UserBLL getUserBllInstance()
        {
            if (_userBLL == null)
            {
                _userBLL = new Bs_UserBLL();
            }
            return _userBLL;
        }
        public static Bs_ProBLL getProBllInstance()
        {
            if (_proBLL == null)
            {
                _proBLL = new Bs_ProBLL();
            }
            return _proBLL;
        }
        public static Bs_OrderBLL getOrderBllInstance()
        {
            if (_orderBLL == null)
            {
                _orderBLL = new Bs_OrderBLL();
            }
            return _orderBLL;
        }
       
    }
}