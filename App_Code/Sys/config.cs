using System;
using System.Data;
using System.Configuration;
using System.Web.Caching;
using System.Text;

namespace BLL.Sys
{


    public class Config
    {
       
        public static string getConfigVal(string field)
        {
            using (DataTable dt = getConfig())
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][field].ToString();
                }
            }
            return "";
        }

        public static DataTable getConfig()
        {
            DataTable dt = CSA.DAL.DBAccess.getRS("select top 1 * from sy_config ");
            return dt;
        }
        /// <summary>
        /// 获取网站的标题,关键字,描述等
        /// </summary>
        /// <param name="preTitle">附加标题,加在前面</param>
        /// <param name="repKeywords">替换的关键字,空则用系统设置的关键字</param>
        /// <param name="repDesc">替换的描述,空则用系统设置的描述</param>
        /// <returns></returns>
        public static string getSiteConfig(string preTitle, string repKeywords, string repDesc)
        {
            DataTable dt = getConfig();
            string re = "";
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (!string.IsNullOrEmpty(preTitle))
                {
                    re += "<title>" + preTitle + "-" + dr["sitetitle"].ToString() + "</title>\r\n";
                }
                else
                {
                    re += "<title>" + dr["sitetitle"].ToString() + "</title>\r\n";
                }
                string kw = repKeywords;
                string desc = repDesc;
                if (string.IsNullOrEmpty(kw))
                {
                    kw = dr["keywords"].ToString();
                }
                if (string.IsNullOrEmpty(desc))
                {
                    desc = dr["descp"].ToString();
                }
                re += "<meta name=\"keywords\" content=\"" + kw + "\" />\r\n";
                re += "<meta name=\"description\" content=\"" + desc + "\" />\r\n";
                re += "<meta name=\"author\" content=\"网站建设：互诺科技 - http://www.hunuo.com\" />\r\n";
            }
            return re;
        }
        /// <summary>
        /// 文章页面描述信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string getSiteConfig(DataRow dr)
        {
            DataTable dt = getConfig();
            string re = "";
            if (dr != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow drDefault = dt.Rows[0];

                    if (!string.IsNullOrEmpty(dr["seoTitle"].ToString()))
                        re += "<title>" + dr["seoTitle"] + "|" + drDefault["sitetitle"] + "</title>\r\n";
                    else
                        re += "<title>" + dr["title"] + "|" + drDefault["sitetitle"].ToString() + "</title>\r\n";
                    if (!string.IsNullOrEmpty(dr["KeyWord"].ToString()))
                        re += "<meta name=\"keywords\" content=\"" + dr["KeyWord"] + "\" />\r\n";
                    else
                        re += "<meta name=\"keywords\" content=\"" + drDefault["KeyWords"] + "\" />\r\n";
                    if (!string.IsNullOrEmpty(dr["Description"].ToString()))
                        re += "<meta name=\"description\" content=\"" + dr["Description"] + "\" />\r\n";
                    else
                        re += "<meta name=\"description\" content=\"" + drDefault["descp"] + "\" />\r\n";
                }
                re += "<meta name=\"author\" content=\"网站建设：互诺科技 - http://www.hunuo.com\" />\r\n";
            }
            else
                return getSiteConfig();
            return re;

        }
        /// <summary>
        /// 文章页面描述信息-英文
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string getSiteConfigEn(DataRow dr)
        {
            DataTable dt = getConfig();
            string re = "";
            if (dr != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow drDefault = dt.Rows[0];

                    if (!string.IsNullOrEmpty(dr["seoTitle_en"].ToString()))
                        re += "<title>" + dr["seoTitle_en"] + "|" + drDefault["sitetitle_en"] + "</title>\r\n";
                    else
                        re += "<title>" + dr["title_en"] + "|" + drDefault["sitetitle_en"].ToString() + "</title>\r\n";
                    if (!string.IsNullOrEmpty(dr["KeyWord_en"].ToString()))
                        re += "<meta name=\"keywords\" content=\"" + dr["KeyWord_en"] + "\" />\r\n";
                    else
                        re += "<meta name=\"keywords\" content=\"" + drDefault["KeyWords_en"] + "\" />\r\n";
                    if (!string.IsNullOrEmpty(dr["Description_en"].ToString()))
                        re += "<meta name=\"description\" content=\"" + dr["Description_en"] + "\" />\r\n";
                    else
                        re += "<meta name=\"description\" content=\"" + drDefault["descp_en"] + "\" />\r\n";
                }
                re += "<meta name=\"author\" content=\"Website Design by HUNUO - http://www.hunuo.com\" />\r\n";
            }
            else
                return getSiteConfig();
            return re;

        }
        /// <summary>
        /// 产品页面描述信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="language">中文填写""，英文填_en</param>
        /// <returns></returns>
        public static string getSiteConfigPro(DataRow dr, string language)
        {
            DataTable dt = getConfig();
            string re = "";
            if (dr != null)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow drDefault = dt.Rows[0];

                    if (!string.IsNullOrEmpty(dr["seoTitle" + language].ToString()))
                        re += "<title>" + dr["seoTitle" + language] + "|" + drDefault["sitetitle" + language] + "</title>\r\n";
                    else
                        re += "<title>" + dr["name" + language] + "|" + drDefault["sitetitle" + language].ToString() + "</title>\r\n";
                    if (!string.IsNullOrEmpty(dr["KeyWord" + language].ToString()))
                        re += "<meta name=\"keywords\" content=\"" + dr["KeyWord" + language] + "\" />\r\n";
                    else
                        re += "<meta name=\"keywords\" content=\"" + drDefault["KeyWords" + language] + "\" />\r\n";
                    if (!string.IsNullOrEmpty(dr["Description" + language].ToString()))
                        re += "<meta name=\"description\" content=\"" + dr["Description" + language] + "\" />\r\n";
                    else
                        re += "<meta name=\"description\" content=\"" + drDefault["descp" + language] + "\" />\r\n";
                }
                if (language == "_en")
                    re += "<meta name=\"author\" content=\"Website Design by HUNUO - http://www.hunuo.com\" />\r\n";
                else
                    re += "<meta name=\"author\" content=\"网站建设：互诺科技 - http://www.hunuo.com\" />\r\n";
            }
            else
                return getSiteConfig();
            return re;

        }
        /// <summary>
        /// 获取网站的标题,关键字,描述等
        /// </summary>
        /// <returns></returns>
        public static string getSiteConfig()
        {
            return getSiteConfig(null, null, null);
        }


        /// <summary>
        /// 获取网站的标题,关键字,描述等 英文
        /// </summary>
        /// <param name="preTitle">附加标题,加在前面</param>
        /// <param name="repKeywords">替换的关键字,空则用系统设置的关键字</param>
        /// <param name="repDesc">替换的描述,空则用系统设置的描述</param>
        /// <returns></returns>
        public static string getSiteConfigEn(string preTitle, string repKeywords, string repDesc)
        {
            DataTable dt = getConfig();
            string re = "";
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (!string.IsNullOrEmpty(preTitle))
                {
                    re += "<title>" + preTitle + "-" + dr["sitetitle_en"].ToString() + "</title>\r\n";
                }
                else
                {
                    re += "<title>" + dr["sitetitle_en"].ToString() + "</title>\r\n";
                }
                string kw = repKeywords;
                string desc = repDesc;
                if (string.IsNullOrEmpty(kw))
                {
                    kw = dr["keywords_en"].ToString();
                }
                if (string.IsNullOrEmpty(desc))
                {
                    desc = dr["descp_en"].ToString();
                }
                re += "<meta name=\"keywords\" content=\"" + kw + "\" />\r\n";
                re += "<meta name=\"description\" content=\"" + desc + "\" />\r\n";
                re += "<meta name=\"author\" content=\"Website Design by HUNUO - http://www.hunuo.com\" />\r\n";
            }
            return re;
        }
        /// <summary>
        /// 获取网站的标题,关键字,描述等
        /// </summary>
        /// <returns></returns>
        public static string getSiteConfigEn()
        {
            return getSiteConfigEn(null, null, null);
        }

    }

}
