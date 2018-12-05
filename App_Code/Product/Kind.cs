using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace BLL.Product
{
    /// <summary>
    /// ProductKind 的摘要说明
    /// </summary>
    public class Kind
    {
        private static string tblName = "bs_prokind";
        private static string kindTbl = "bs_proKind";
        public Kind()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取产品分类
        /// </summary>
        /// <param name="pcode">父类编号,null则表示全部分类,""则表示顶级分类</param>
        /// <param name="deep">子类深度,0表示全部</param>
        /// <returns></returns>
        public static DataTable getList(string pcode, int deep,int size)
        {
            string sql = "select {0} * from " + tblName + " where status=0 ";
            string topSize = "";
            if (size > 0)
            {
                topSize = "top " + size;
                
            }
            sql = string.Format(sql, topSize);
            if (pcode == null)
            {
                // sql += " and len(code)=2";
            }
            else if (pcode == "")
            {
                sql += " and len(code)=2";
            }
            else
            {
                sql += " and code like '" + pcode + "%'";
                if (deep > 0)
                {                    
                    sql += " and len(code)=" + (pcode.Length * deep).ToString();                    
                }
            }
            sql += " order by sortno,id";
            return CSA.DAL.DBAccess.getRS(sql);
        }
        public static DataTable getList(string pcode, int deep)
        {
            return getList(pcode, deep,0);
        }
        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="ch">中英文，TRUE为中文，FALSE为英文</param>
        /// <returns></returns>
        public static string getKindName(string code, string lange)
        {
          
            object obj=CSA.DAL.DBAccess.ExecuteScalar("select name" + lange + " from " + kindTbl + " where code='" + code + "'");
            if(obj!=null)
                return obj.ToString();
            else
            return null;
        }
        /// <summary>
        /// 根据条件获取分类列表
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="num"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetProKindListByCondition(string condition, int num, string orderBy)
        {
            string top = "";
            string order = " order by code";
            if (num != 0)
                top = "top " + num;
            if (orderBy != "")
                order = " order by " + orderBy;
            string sql = string.Format("select {1} Name,Code,ID,pic,notes from Bs_ProKind where {0} and status=0 {2}", condition, top, order);
            return CSA.DAL.DBAccess.getRS(sql);
        }

       
    }

}