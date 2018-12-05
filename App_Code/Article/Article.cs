using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace BLL.Article
{
    /// <summary>
    /// Article 的摘要说明
    /// </summary>
    public class Article
    {
        private static string detailTbl = "bs_news";
        private static string kindTbl = "bs_newskind";

        public Article()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="code">文章类别编码,null或者""则返回全部</param>
        /// <param name="pagesize">条数size,0则返回全部</param>      
        /// <returns></returns>
        public static DataTable getArticle(string code, int size)
        {
            string top = size > 0 ? " top " + size : "";
            string where = "";
            if (code != null && code != "")
            {
                where = " and bs_newsKindCode like '" + CSA.Text.Util.getSqlStr(code) + "%'";
            }

            string sql = "select " + top + " * from " + detailTbl + " where status=0 " + where + " order by sortno desc,addTime desc";
            return CSA.DAL.DBAccess.getRS(sql);
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="code">文章类别编码,null或者""则返回全部</param>
        /// <param name="pagesize">条数size,0则返回全部</param>      
        /// <returns></returns>
        public static DataTable getArticle(string code, int size, string orderby, string px)
        {
            string top = size > 0 ? " top " + size : "";
            string where = "";
            if (!string.IsNullOrEmpty(orderby) && !string.IsNullOrEmpty(px))
            {
                orderby = " " + orderby + " " + px + ", ";
            }
            else if (!string.IsNullOrEmpty(orderby))
            {
                orderby = " " + orderby+ ", ";
            }
            else
            {
                orderby = "";
            }
            if (code != null && code != "")
            {
                where = " and bs_newsKindCode like '" + CSA.Text.Util.getSqlStr(code) + "%'";
            }

            string sql = "select " + top + " * from " + detailTbl + " where status=0 " + where + " order by " + orderby + " id desc";
            return CSA.DAL.DBAccess.getRS(sql);
        }
        public static DataTable getArticle(string code, int size, string orderbystr)
        {
            return getArticle(code, size, orderbystr,null);
        }
        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataRow getArticleDetail(int id)
        {
            return getArticleDetail(id, "");
        }
        public static DataRow getArticleDetail(int id, string language)
        {
            DataTable dtTemp= CSA.DAL.DBAccess.getRS("select top 1 * from " + detailTbl + language
+" where id=" + id);
            if (dtTemp.Rows.Count > 0 && dtTemp != null)
                return dtTemp.Rows[0];
            else
                return null;
        }
       
        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataRow getArticleDetail(string kindCode)
        {
            DataTable dt = CSA.DAL.DBAccess.getRS("select top 1 * from " + detailTbl + " where bs_Newskindcode='" + kindCode + "'");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
        }
        /// <summary>
        /// 加点击数
        /// </summary>
        /// <param name="id"></param>
        public static int addHit(int id)
        {
            return addHit(id, "");
        }
        public static int addHit(int id, string language)
        {
            return CSA.DAL.DBAccess.ExecuteNonQuery("update " + detailTbl + language+" set hits=isnull(hits,0)+1 where id=" + id);
        }
        /// <summary>
        /// 加点击数
        /// </summary>
        /// <param name="id"></param>
        public static int addHit(string guid)
        {
            return CSA.DAL.DBAccess.ExecuteNonQuery("update " + detailTbl + " set hits=isnull(hits,0)+1 where id=" + guid);
        }
        /// <summary>
        /// 获取文章内容
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        public static string getArticeContent(int id)
        {
            try
            {
                return CSA.DAL.DBAccess.ExecuteScalar("select content from " + detailTbl + " where id=" + id).ToString().Replace("../", "");
            }
            catch
            {
                return "";
            }
        }
        public static string getArticeContent(string code)
        {
            try
            {
                return CSA.DAL.DBAccess.ExecuteScalar("select content from " + detailTbl + " where bs_Newskindcode='" + code + "'").ToString();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取文章类别名称
        /// </summary>
        /// <param name="code">类别编码</param>
        /// <returns></returns>
        public static string getArticleKindName(string code)
        {
            return CSA.DAL.DBAccess.ExecuteScalar("select name from " + kindTbl + " where code='"
                + CSA.Text.Util.getSqlStr(code) + "'").ToString();
        }

        public static DataTable getSearchResult(string keyword)
        {
            string sql = "select * from " + detailTbl + " where (title like '%"
                + CSA.Text.Util.getSqlStr(keyword) + "%' or content like '%" + CSA.Text.Util.getSqlStr(keyword) + "%') and pcode<>'05' order by xuhao,id desc";
            return CSA.DAL.DBAccess.getRS(sql);
        }
        /// <summary>
        /// 获取上一篇文章
        /// </summary>
        /// <param name="id">当前文章id</param>
        /// <param name="kindCode">当前文章分类</param>
        /// <returns></returns>
        public static DataTable getPackArtice(int id, string kindCode,int sortno)
        {
            DataTable dt = CSA.DAL.DBAccess.getRS(string.Format("select top 1 id,Title,Title_en,bs_NewskindCode from bs_News where bs_NewsKindCode='{0}' and sortno = {1} and id>{2} order by sortno,id", kindCode, sortno, id));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return CSA.DAL.DBAccess.getRS(string.Format("select top 1 id,Title,Title_en,bs_NewskindCode from bs_News where bs_NewsKindCode='{0}' and sortno > {1} and id<>{2} order by sortno,id", kindCode, sortno, id));
        }
        /// <summary>
        /// 获取下一篇文章
        /// </summary>
        /// <param name="id">当前文章id</param>
        /// <param name="kindCode">当前文章分类</param>
        /// <returns></returns>
        public static DataTable getNextArtice(int id, string kindCode, int sortno)
        {
            DataTable dt = CSA.DAL.DBAccess.getRS(string.Format("select top 1 id,Title,Title_en,bs_NewskindCode from bs_News where bs_NewskindCode='{0}' and sortno = {1} and id<{2} order by sortno desc,id desc", kindCode, sortno, id));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return CSA.DAL.DBAccess.getRS(string.Format("select top 1 id,Title,Title_en,bs_NewskindCode from bs_News where bs_NewskindCode='{0}' and sortno < {1} and id<>{2} order by sortno desc,id desc", kindCode,sortno,id));
        }
        /// <summary>
        /// 获取分类Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBsNewsKindCode(int id)
        {
            string sql = "select bs_newsKindCode from bs_news where id={0}";
            sql = string.Format(sql, id);
            return CSA.DAL.DBAccess.getRS(sql).Rows[0][0].ToString();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="fields">字段</param>
        /// <param name="tables">表名</param>
        /// <param name="filters">条件</param>
        /// <param name="sortStr">排序</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页显示数</param>
        /// <returns></returns>
        public static DataTable ExecutePagination(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize)
        {
            string str;
            DataTable dataTable = new DataTable();
            if (currentPage == 1)
            {
                str = "select top " + pageSize.ToString() + " " + fields + " from " + tables + " where " + filters + " order by " + sortStr;
            }
            else
            {
                str = "select top " + pageSize.ToString() + " " + fields + " from " + tables
                                + " where p.[ID] not in ("
                                + "select top " + pageSize * (currentPage - 1) + " p.[ID] from " + tables + " where " + filters + " order by " + sortStr
                                + ") and " + filters + " order by " + sortStr;
            }
            dataTable = CSA.DAL.DBAccess.getRS(str);
            return dataTable;
        }
        public static DataTable ExecutePaginationSQL(string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        {
            total = 0;
            totalPage = 0;
            DataTable result = new DataTable();
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            string sqlstr = "select count(p.[ID]) from BS_news p where " + filters;
            try
            {
                total = int.Parse(CSA.DAL.DBAccess.ExecuteScalar(sqlstr).ToString());
                if (total % pageSize == 0)
                    totalPage = total / pageSize;
                else
                    totalPage = total / pageSize + 1;
                if (currentPage == 1)
                {
                    sqlstr = "select top " + pageSize.ToString() + " * from BS_news p"
                    + " where " + filters + " order by " + sortStr;
                }
                else
                {
                    sqlstr = "select top " + pageSize.ToString() + " * from BS_news p"
                            + " where p.[ID] not in ("
                            + "select top " + pageSize * (currentPage - 1) + " p.[ID] from BS_news p where " + filters + " order by " + sortStr
                            + ") and " + filters + " order by " + sortStr;
                }
                result = CSA.DAL.DBAccess.getRS(sqlstr);
            }
            catch { }
            finally { }
            return result;
        }
    }
}