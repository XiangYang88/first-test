using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CSA.DAL;

namespace BLL.Ad
{
    /// <summary>
    /// Ad 的摘要说明
    /// </summary>
    public class Ad
    {
        private static string datatName = "bs_ad";
        private static string kindTable = "bs_adKind";
        public Ad()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 根据广告类型获取广告信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAdByAdKindCode(string kindCode)
        {

            return GetAdByAdKindCode(kindCode, 0);

        }
        public static DataTable GetAdByAdKindCode(string kindCode, int size)
        {
            string top = "";
            if (size > 0)
            {
                top = " top " + size;
            }
            string sql = string.Format("select {2}  * from {0} where bs_adkindCode ='{1}' and status=0  order by sortno desc,addTime desc", datatName, kindCode, top);
            return DBAccess.getRS(sql);

        }
        #region
        /// <summary>
        /// 根据ID获取广告信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataRow GetAdById(int id)
        {
            string sql = string.Format("select  * from {0} where id ={1}", datatName, id);
            DataTable dt = DBAccess.getRS(sql);
            if (null != dt && dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;
        }
        #endregion
    }
}
