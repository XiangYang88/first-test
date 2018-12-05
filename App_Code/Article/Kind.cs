using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace BLL.Article
{
    /// <summary>
    /// ArticleKind 的摘要说明
    /// </summary>
    public class Kind
    {
        private static string detailTbl = "bs_news";
        private static string kindTbl = "bs_newskind";
        public Kind()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public static string getKindName(string code)
        {
            return getKindName(code, "");
        }

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="language">语言 英 _en 俄 _ru ...</param>
        /// <returns></returns>
        public static string getKindName(string code, string language
)
        {
            object obj = CSA.DAL.DBAccess.ExecuteScalar("select name"+ language+" from " + kindTbl + " where code='" + code + "'");
            if (obj != null)
                return obj.ToString();
            return null;
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="pcode">父类编号,null则表示全部分类,""则表示顶级分类</param>
        /// <param name="deep">子类深度,0表示全部</param>
        /// <param name="language">语言</param>
        /// <returns></returns>
        public static DataTable getKind(string pcode, int deep, string language)
        {
            return getKind(pcode, deep, language, "");
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="pcode">父类编号,null则表示全部分类,""则表示顶级分类</param>
        /// <param name="deep">子类深度,0表示全部</param>
        /// <returns></returns>
        public static DataTable getKind(string pcode, int deep)
        {
            return getKind(pcode, deep, "", "");
        }
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="pcode">父类编号,null则表示全部分类,""则表示顶级分类</param>
        /// <param name="deep">子类深度,0表示全部</param>
        /// <param name="language">语言</param>
        /// <param name="orderby">排序 不写order by</param>
        /// <returns></returns>
        public static DataTable getKind(string pcode, int deep, string language, string orderby)
        {
            string sql = "select *,(code+name" + language + ") showname from " + kindTbl + " where status=0";
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
                    if (pcode.Length == 4)
                    {
                        sql += " and len(code)=" + (pcode.Length * deep - 2).ToString();
                    }
                    else
                    {
                        sql += " and len(code)=" + (pcode.Length * deep).ToString();
                    }
                }
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                sql += string.Format(" order by {0}", orderby);
            }
            else
            {
                sql += " order by code";
            }
            return CSA.DAL.DBAccess.getRS(sql);
        }
       
       
    }
}