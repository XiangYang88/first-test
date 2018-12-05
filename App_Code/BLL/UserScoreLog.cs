using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace BLL.BsUser
{
    /// <summary>
    ///UserScoreLog 的摘要说明
    /// </summary>
    public class UserScoreLog
    {
        public UserScoreLog()
        {

            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 流水积分的类型说明
        /// </summary>
       
        public static string REG="新用户注册";
        public static string GIFT = "兑换礼品";
        public static string BUY = "购物";
        public static string OTHER = "其它";
        public static string ShoppingTradeoff = "购物抵换";

        /// <summary>
        /// 增加一个流水积分记录
        /// </summary>
        /// <param name="userId">用户ＩＤ</param>
        /// <param name="code">流水积分对应的编号</param>
        /// <param name="type">流水积分类型</param>
        /// <param name="score">积分</param>
        /// <returns>成功返回true</returns>
        public static bool AddUserScoreLog(int userId, string code, string type, string score)
        {
            string sql = string.Format("insert into bs_UserScoreLog(bs_UserID,Type,Code,Score)values({0},'{1}','{2}',{3})", userId, type, code, score);
            return CSA.DAL.DBAccess.ExecuteNonQuery(sql)>0;
        }

        /// <summary>
        /// 按会员的ＩＤ查询会员的流水积分
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetUserScoreLogByUserId(int userId)
        {
            string sql = string.Format("select * from bs_UserScoreLog where bs_UserID = {0} order by AddTime desc",userId);
            return CSA.DAL.DBAccess.getRS(sql);
        }
        


    }
}
